using Microsoft.Graph;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnedriveUploadWinForms
{
  public partial class MainForm : Form
  {
    private IOneDriveClient oneDriveClient;

    // Application registered on apps.dev.microsoft.com:
    string msaClientId = "<application_id>"; // create "secrets.txt" file with content: "msaClientId=<real application id>" in root folder of solution
    string msaReturnUrl = "https://login.live.com/oauth20_desktop.srf";

    string[] scopes = { "onedrive.readwrite", "wl.signin" };

    // TODO fix at musikFolder ikke er en global
    // Onedrive online folder for musik
    Item musikFolder;

    // Flag to control whether copying files should be stopped
    private bool stop;

    public MainForm()
    {
      InitializeComponent();

      // load msaClientId from settings.txt
      string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
      string[] lines = System.IO.File.ReadAllLines(Path.Combine(exeDirectory, "secrets.txt"));
      if (lines.Length != 1)
        throw new ArgumentException("secrets.txt should only have one line");
      string[] content = lines[0].Split('=');
      if (content.Length != 2)
        throw new ArgumentException("secrets.txt single line should look like this: msaClientId=<real application id>");
      if (!content[0].ToLowerInvariant().Equals("msaClientId".ToLowerInvariant()))
        throw new ArgumentException("secrets.txt single line should look like this: msaClientId=<real application id>");
      msaClientId = content[1];
    }

    private async Task<bool> SignIn()
    {
      Task authTask;

      var msaAuthProvider = new MsaAuthenticationProvider(
        msaClientId,
        null,
        msaReturnUrl,
        scopes,
        null,
        new CredentialVault(msaClientId));
      this.oneDriveClient = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthProvider);
      authTask = msaAuthProvider.RestoreMostRecentFromCacheOrAuthenticateUserAsync();

      try
      {
        await authTask;
      }
      catch (ServiceException exception)
      {
        if (OAuthConstants.ErrorCodes.AuthenticationFailure == exception.Error.Code)
        {
          MessageBox.Show(
            "Authentication failed",
            "Authentication failed",
            MessageBoxButtons.OK);

          this.oneDriveClient = null;
        }
        else
        {
          PresentServiceException(exception);
        }
        return false;
      }
      return true;
    }

    private static void PresentServiceException(Exception exception)
    {
      string message = null;
      var oneDriveException = exception as ServiceException;
      if (oneDriveException == null)
      {
        message = exception.Message;
      }
      else
      {
        message = string.Format("{0}{1}", Environment.NewLine, oneDriveException.ToString());
      }

      MessageBox.Show(string.Format("OneDrive reported the following error: {0}", message));
    }

    private void UpdateSignedInUi(bool signedIn)
    {
      btnSignIn.Enabled = !signedIn;
      btnSignOut.Enabled = signedIn;
      btnCheckSourceFolder.Enabled = signedIn;
      btnFindMusikFolder.Enabled = signedIn;
      btnStartCopyingFiles.Enabled = signedIn;

      textSourceFolder.Enabled = signedIn;
    }

    private void btnSignOut_Click(object sender, EventArgs e)
    {
      if (this.oneDriveClient != null)
      {
        this.oneDriveClient = null;
      }
      UpdateSignedInUi(false);
    }

    private async void btnSignIn_Click(object sender, EventArgs e)
    {
      btnSignIn.Enabled = false;
      if (await SignIn())
      {
        lblStatus.Text = "Signed in";
        UpdateSignedInUi(true);
      }
    }

    private async void btnFindMusikFolder_Click(object sender, EventArgs e)
    {
      btnFindMusikFolder.Enabled = false;
      lblMusikFoundStatus.Text = "...";
      var rootFolder = await FindRootFolder("musik");
      if (rootFolder != null)
      {
        musikFolder = rootFolder;
        lblMusikFoundStatus.Text = "Musik folder was found.";
      }
      else
      {
        btnFindMusikFolder.Enabled = true;
      }
    }

    private async Task<Item> FindRootFolder(string nameOfRootFolder)
    {
      Item rootFolder;
      try
      {
        var expandValue = "children";
        rootFolder =
          await
            this.oneDriveClient.Drive.Root.ItemWithPath("/" + nameOfRootFolder)
              .Request()
              .Expand(expandValue)
              .GetAsync();

        if (rootFolder.Folder != null && rootFolder.Children != null && rootFolder.Children.CurrentPage != null)
        {
          return rootFolder;
        }
      }
      catch (Exception exception)
      {
        rootFolder = null;
        PresentServiceException(exception);
        Debug.Assert(false);
      }
      return rootFolder;
    }

    private void btnCheckSourceFolder_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(textSourceFolder.Text))
      {
        bool exists = Directory.Exists(textSourceFolder.Text);
        if (exists)
        {
          lblSourceFolder.Text = "Der er adgang til source folderen";
        }
      }
    }

    private void btnStartCopyingFiles_Click(object sender, EventArgs e)
    {
      btnStartCopyingFiles.Enabled = false;
      btnStopCopyingFiles.Enabled = true;
      lblNumberOfFilesCopied.Text = "0";
      lblMBCopied.Text = "0";
      lblCurrentFolder.Text = "";

      stop = false;

      CopyFiles(textSourceFolder.Text, musikFolder);
    }

    /// <summary>
    /// Copy files in a background thread.
    /// Look at stop flag to determine if it should be stopped before time.
    /// </summary>
    /// <param name="sourceFolder">Location of musik to upload to onedrive.</param>
    /// <param name="musikFolder">Reference to "Musik" folder on onedrive.</param>
    private void CopyFiles(string sourceFolder, Item musikFolder)
    {
      // run copying in a background thread
      Thread t = new Thread(() =>
      {
        bool error = false;
        bool finished = false;
        int numberOfFilesCopied = 0;
        long bytesCopied = 0;
        List<string> directories = Directory.EnumerateDirectories(sourceFolder).ToList();
        while (!stop)
        {
          try
          {
            foreach (var artistDirectory in directories)
            {
              if (stop)
              {
                break;
              }
              lblCurrentFolder.Invoke((MethodInvoker)delegate { lblCurrentFolder.Text = artistDirectory; });
              CopyDirectory(musikFolder, sourceFolder, new DirectoryInfo(artistDirectory).Name, ref numberOfFilesCopied,
                ref bytesCopied);
            }

            if (!stop)
            {
              // all folders has been processed, stop!
              finished = true;
              // needs to set stop flag, otherwise while-loop will continue
              stop = true;
            }
          }
          catch (Exception exp)
          {
            MessageBox.Show($"Noget gik gal!: {exp}");
            Debug.Assert(false);
            stop = true;
            error = true;
          }
          if (!error)
          {
            if (finished)
            {
              MessageBox.Show("Det gik godt! Helt færdig!");
            }
            else
            {
              MessageBox.Show("Det gik godt! Blev afbrudt!");
            }
          }
        }
        btnStartCopyingFiles.Invoke((MethodInvoker)delegate { btnStartCopyingFiles.Enabled = true; });
        btnStopCopyingFiles.Invoke((MethodInvoker)delegate { btnStopCopyingFiles.Enabled = false; });
      });
      t.IsBackground = true;
      t.Start();
    }

    /// <summary>
    /// Called recursively.
    /// Copy all directories and files in given folder to onedrive folder with same name
    /// </summary>
    /// <param name="oneDriveMusikFolder">Reference to onedrive object representing the folder we're currently working on</param>
    /// <param name="baseDirectory">Base folder of where musik exists locally</param>
    /// <param name="relativeDirectory">Folder below base folder holding the actual artist and album</param>
    private void CopyDirectory(Item oneDriveFolder, string baseFolder, string relativeDirectory,
      ref int numberOfFilesCopied, ref long bytesCopied)
    {
      string lastPartOfRelativeDirectory = Path.GetFileName(relativeDirectory);
      Item oneDriveFolderToStoreIn = null;

      // TODO remove check for Children != null - A new folder has a Children collection that is null
      if (oneDriveFolder.Children != null)
      {
        foreach (var child in oneDriveFolder.Children)
        {
          if (child.Name.Equals(lastPartOfRelativeDirectory))
          {
            oneDriveFolderToStoreIn = child;
            break;
          }
        }
      }
      if (oneDriveFolderToStoreIn == null)
      {
        try
        {
          var folderToCreate = new Item() { Folder = new Folder() };
          // by calling 'Expand with children' the Item.Children member is not null
          var expandValue = "children";
          oneDriveFolderToStoreIn = oneDriveClient
            .Drive
            .Items[oneDriveFolder.Id]
            .ItemWithPath(lastPartOfRelativeDirectory)
            .Request()
            .Expand(expandValue)
            .CreateAsync(folderToCreate).Result;
        }
        catch (Exception exp)
        {
          MessageBox.Show($"Opret folder gik galt: {exp}");
          Debug.Assert(false);
          throw;
        }
      }

      foreach (var albumDirectory in Directory.EnumerateDirectories(Path.Combine(baseFolder, relativeDirectory)))
      {
        CopyDirectory(oneDriveFolderToStoreIn, baseFolder,
          Path.Combine(relativeDirectory, Path.GetFileName(albumDirectory)), ref numberOfFilesCopied, ref bytesCopied);

        UpdateProgress(numberOfFilesCopied, bytesCopied);
      }
      foreach (var file in Directory.EnumerateFiles(Path.Combine(baseFolder, relativeDirectory)))
      {
        CopyFile(oneDriveFolderToStoreIn, file, ref numberOfFilesCopied, ref bytesCopied);
        UpdateProgress(numberOfFilesCopied, bytesCopied);
      }
    }

    private void CopyFile(Item oneDriveFolderToStoreIn, string file, ref int numberOfFilesCopied, ref long bytesCopied)
    {
      Item oneDriveFileItem = null;
      string fileName = Path.GetFileName(file);

      //TODO remove check for Children != null -  A new folder has a Children collection that is null
      if (oneDriveFolderToStoreIn.Children != null)
      {
        foreach (var child in oneDriveFolderToStoreIn.Children)
        {
          if (child.Name.Equals(fileName))
          {
            oneDriveFileItem = child;
            break;
          }
        }
      }
      if (oneDriveFileItem == null)
        try
        {
          using (var contentStream = new FileStream(file, FileMode.Open))
          {
            var uploadedItem = oneDriveClient
                                         .Drive
                                         .Items[oneDriveFolderToStoreIn.Id]
                                         .ItemWithPath(fileName)
                                         .Content
                                         .Request()
                                         .PutAsync<Item>(contentStream)
                                         .Result;
            numberOfFilesCopied += 1;
            bytesCopied += uploadedItem.Size.Value;
          }
        }
        catch (Exception exp)
        {
          MessageBox.Show($"Upload fil gik galt: {exp}");
          Debug.Assert(false);
          throw;
        }
    }

    /// <summary>
    /// Update ui with progress on number of files copied and mb copied
    /// </summary>
    private void UpdateProgress(int numberOfFilesCopied, long bytesCopied)
    {
      // Has to copy value into local field, because anonymous method cannot be called with parameter that is ref
      int nonRefNumberOfFilesCopied = numberOfFilesCopied;
      long nonRefBytesCopied = bytesCopied;
      lblNumberOfFilesCopied.Invoke(
        (MethodInvoker)delegate { lblNumberOfFilesCopied.Text = nonRefNumberOfFilesCopied.ToString(); });
      string byteString;
      if (nonRefBytesCopied < 102400)
        byteString = $"{(float)nonRefBytesCopied / 1024:0.0} kB";
      else if (nonRefBytesCopied < 102400000)
        byteString = $"{(float)nonRefBytesCopied / 1024 / 1024:0.0} MB";
      else
        byteString = $"{(float)nonRefBytesCopied / 1024 / 1024 / 1024:0.0} GB";
      lblMBCopied.Invoke((MethodInvoker)delegate { lblMBCopied.Text = byteString; });
    }

    private void btnStopCopyingFiles_Click(object sender, EventArgs e)
    {
      btnStartCopyingFiles.Enabled = false;
      btnStopCopyingFiles.Enabled = false;
      stop = true;
    }

  }
}