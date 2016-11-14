using Microsoft.Graph;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnedriveUploadWinForms
{
  public partial class MainForm : Form
  {
    private IOneDriveClient oneDriveClient;

    // Application id registered on apps.dev.microsoft.com:
    string msaClientId = "000000004C1B625C";
    string msaReturnUrl = "https://login.live.com/oauth20_desktop.srf";
    string[] scopes = { "onedrive.readwrite", "wl.signin" };
    //new[] { "onedrive.readonly", "wl.signin" }

    public MainForm()
    {
      InitializeComponent();
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

    private async void DoSomethingWithFolders()
    {
      try
      {
        await LoadFolderFromPath();

        //UpdateConnectedStateUx(true);
      }
      catch (ServiceException exception)
      {
        PresentServiceException(exception);
        this.oneDriveClient = null;
      }
    }

    // TODO gammel metode - slettes
    private async void _Signin()
    {
      // Application id registered on apps.dev.microsoft.com:
      string clientId = "000000004C1B625C";

      var msaAuthenticationProvider = new MsaAuthenticationProvider(
              clientId,
              "https://login.live.com/oauth20_desktop.srf",
              new[] { "onedrive.readonly", "wl.signin" });

      await msaAuthenticationProvider.AuthenticateUserAsync();
      oneDriveClient = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthenticationProvider);
    }

    private async Task LoadFolderFromPath(string path = null)
    {
      try
      {
        Item folder;

        // Kode kopieret
        //var expandValue = this.clientType == ClientType.Consumer
        //    ? "thumbnails,children(expand=thumbnails)"
        //    : "thumbnails,children";
        var expandValue = "thumbnails,children(expand=thumbnails)";

        if (path == null)
        {
          folder = await this.oneDriveClient.Drive.Root.Request().Expand(expandValue).GetAsync();
        }
        else
        {
          folder =
              await
                  this.oneDriveClient.Drive.Root.ItemWithPath("/" + path)
                      .Request()
                      .Expand(expandValue)
                      .GetAsync();
        }

        ProcessFolder(folder);
      }
      catch (Exception exception)
      {
        PresentServiceException(exception);
      }

    }

    private void ProcessFolder(Item folder)
    {
      if (folder != null)
      {
        //this.CurrentFolder = folder;

        //LoadProperties(folder);

        if (folder.Folder != null && folder.Children != null && folder.Children.CurrentPage != null)
        {
          LoadChildren(folder.Children.CurrentPage);
        }
      }
    }

    private void LoadChildren(IList<Item> items)
    {
      foreach (var item in items)
      {
        //AddItemToFolderContents(obj);
        string id = item.Id;
        string name = item.Name;
        long? size = item.Size;
        string description = item.Description;

        if (name == "musik")
        {

        }
      }
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
      if (await SignIn())
      {
        lblStatus.Text = "Signed in";
        UpdateSignedInUi(true);
      }
    }

    private async void btnFindMusikFolder_Click(object sender, EventArgs e)
    {
      if (await FindRootFolder("musik"))
      {
        lblMusikFoundStatus.Text = "Found Musik folder.";
      }
    }

    Item musikFolder;

    private async Task<bool> FindRootFolder(string nameOfRootFolder)
    {
      try
      {
        var expandValue = "thumbnails,children(expand=thumbnails)";

        musikFolder =
            await
                this.oneDriveClient.Drive.Root.ItemWithPath("/" + nameOfRootFolder)
                    .Request()
                    .Expand(expandValue)
                    .GetAsync();
      }
      catch (Exception exception)
      {
        musikFolder = null;
        PresentServiceException(exception);
        return false;
      }
      return true;
    }

    private void btnCheckSourceFolder_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(textSourceFolder.Text))
      {
        bool exists = System.IO.Directory.Exists(textSourceFolder.Text);
        if (exists)
        {
          lblSourceFolder.Text = "Der er adgang til source folderen";
        }
      }
    }


    lav kode, der løber igennem alle source foldere - spring over $RecycleBin, .Zyxel (skjult folder), .mvfs (skjult folder)
  }
}