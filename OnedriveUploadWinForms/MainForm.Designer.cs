namespace OnedriveUploadWinForms
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnSignIn = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.btnSignOut = new System.Windows.Forms.Button();
      this.btnFindMusikFolder = new System.Windows.Forms.Button();
      this.lblMusikFoundStatus = new System.Windows.Forms.Label();
      this.btnCheckSourceFolder = new System.Windows.Forms.Button();
      this.lblSourceFolder = new System.Windows.Forms.Label();
      this.textSourceFolder = new System.Windows.Forms.TextBox();
      this.btnStartCopyingFiles = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.lblNumberOfFilesCopied = new System.Windows.Forms.Label();
      this.lblMBCopied = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.lblCurrentFolder = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.btnStopCopyingFiles = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnSignIn
      // 
      this.btnSignIn.Location = new System.Drawing.Point(13, 13);
      this.btnSignIn.Name = "btnSignIn";
      this.btnSignIn.Size = new System.Drawing.Size(120, 23);
      this.btnSignIn.TabIndex = 0;
      this.btnSignIn.Text = "Sign in";
      this.btnSignIn.UseVisualStyleBackColor = true;
      this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
      // 
      // lblStatus
      // 
      this.lblStatus.Location = new System.Drawing.Point(139, 18);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(236, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Signed out";
      // 
      // btnSignOut
      // 
      this.btnSignOut.Enabled = false;
      this.btnSignOut.Location = new System.Drawing.Point(12, 42);
      this.btnSignOut.Name = "btnSignOut";
      this.btnSignOut.Size = new System.Drawing.Size(121, 23);
      this.btnSignOut.TabIndex = 2;
      this.btnSignOut.Text = "Sign out";
      this.btnSignOut.UseVisualStyleBackColor = true;
      this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
      // 
      // btnFindMusikFolder
      // 
      this.btnFindMusikFolder.Enabled = false;
      this.btnFindMusikFolder.Location = new System.Drawing.Point(13, 81);
      this.btnFindMusikFolder.Name = "btnFindMusikFolder";
      this.btnFindMusikFolder.Size = new System.Drawing.Size(120, 23);
      this.btnFindMusikFolder.TabIndex = 3;
      this.btnFindMusikFolder.Text = "Find musik folder";
      this.btnFindMusikFolder.UseVisualStyleBackColor = true;
      this.btnFindMusikFolder.Click += new System.EventHandler(this.btnFindMusikFolder_Click);
      // 
      // lblMusikFoundStatus
      // 
      this.lblMusikFoundStatus.Location = new System.Drawing.Point(139, 86);
      this.lblMusikFoundStatus.Name = "lblMusikFoundStatus";
      this.lblMusikFoundStatus.Size = new System.Drawing.Size(227, 23);
      this.lblMusikFoundStatus.TabIndex = 4;
      this.lblMusikFoundStatus.Text = "n/a";
      // 
      // btnCheckSourceFolder
      // 
      this.btnCheckSourceFolder.Enabled = false;
      this.btnCheckSourceFolder.Location = new System.Drawing.Point(13, 137);
      this.btnCheckSourceFolder.Name = "btnCheckSourceFolder";
      this.btnCheckSourceFolder.Size = new System.Drawing.Size(154, 23);
      this.btnCheckSourceFolder.TabIndex = 5;
      this.btnCheckSourceFolder.Text = "Verify access to source folder";
      this.btnCheckSourceFolder.UseVisualStyleBackColor = true;
      this.btnCheckSourceFolder.Click += new System.EventHandler(this.btnCheckSourceFolder_Click);
      // 
      // lblSourceFolder
      // 
      this.lblSourceFolder.Location = new System.Drawing.Point(173, 142);
      this.lblSourceFolder.Name = "lblSourceFolder";
      this.lblSourceFolder.Size = new System.Drawing.Size(194, 23);
      this.lblSourceFolder.TabIndex = 6;
      this.lblSourceFolder.Text = "n/a";
      // 
      // textSourceFolder
      // 
      this.textSourceFolder.Location = new System.Drawing.Point(13, 111);
      this.textSourceFolder.Name = "textSourceFolder";
      this.textSourceFolder.Size = new System.Drawing.Size(353, 20);
      this.textSourceFolder.TabIndex = 7;
      this.textSourceFolder.Text = "C:\\Temp\\OnedriveUpload";
      // 
      // btnStartCopyingFiles
      // 
      this.btnStartCopyingFiles.Enabled = false;
      this.btnStartCopyingFiles.Location = new System.Drawing.Point(12, 275);
      this.btnStartCopyingFiles.Name = "btnStartCopyingFiles";
      this.btnStartCopyingFiles.Size = new System.Drawing.Size(121, 23);
      this.btnStartCopyingFiles.TabIndex = 8;
      this.btnStartCopyingFiles.Text = "Start copying files";
      this.btnStartCopyingFiles.UseVisualStyleBackColor = true;
      this.btnStartCopyingFiles.Click += new System.EventHandler(this.btnStartCopyingFiles_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 206);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(121, 23);
      this.label1.TabIndex = 9;
      this.label1.Text = "Number of files copied:";
      // 
      // lblNumberOfFilesCopied
      // 
      this.lblNumberOfFilesCopied.Location = new System.Drawing.Point(142, 206);
      this.lblNumberOfFilesCopied.Name = "lblNumberOfFilesCopied";
      this.lblNumberOfFilesCopied.Size = new System.Drawing.Size(154, 23);
      this.lblNumberOfFilesCopied.TabIndex = 10;
      this.lblNumberOfFilesCopied.Text = "0";
      // 
      // lblMBCopied
      // 
      this.lblMBCopied.Location = new System.Drawing.Point(142, 229);
      this.lblMBCopied.Name = "lblMBCopied";
      this.lblMBCopied.Size = new System.Drawing.Size(154, 23);
      this.lblMBCopied.TabIndex = 12;
      this.lblMBCopied.Text = "0";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(12, 229);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(121, 23);
      this.label3.TabIndex = 11;
      this.label3.Text = "MB copied:";
      // 
      // lblCurrentFolder
      // 
      this.lblCurrentFolder.Location = new System.Drawing.Point(142, 252);
      this.lblCurrentFolder.Name = "lblCurrentFolder";
      this.lblCurrentFolder.Size = new System.Drawing.Size(429, 23);
      this.lblCurrentFolder.TabIndex = 13;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(12, 252);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(121, 23);
      this.label4.TabIndex = 14;
      this.label4.Text = "Folder:";
      // 
      // btnStopCopyingFiles
      // 
      this.btnStopCopyingFiles.Enabled = false;
      this.btnStopCopyingFiles.Location = new System.Drawing.Point(145, 275);
      this.btnStopCopyingFiles.Name = "btnStopCopyingFiles";
      this.btnStopCopyingFiles.Size = new System.Drawing.Size(121, 23);
      this.btnStopCopyingFiles.TabIndex = 15;
      this.btnStopCopyingFiles.Text = "Stop copying files";
      this.btnStopCopyingFiles.UseVisualStyleBackColor = true;
      this.btnStopCopyingFiles.Click += new System.EventHandler(this.btnStopCopyingFiles_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(581, 310);
      this.Controls.Add(this.btnStopCopyingFiles);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.lblCurrentFolder);
      this.Controls.Add(this.lblMBCopied);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lblNumberOfFilesCopied);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnStartCopyingFiles);
      this.Controls.Add(this.textSourceFolder);
      this.Controls.Add(this.lblSourceFolder);
      this.Controls.Add(this.btnCheckSourceFolder);
      this.Controls.Add(this.lblMusikFoundStatus);
      this.Controls.Add(this.btnFindMusikFolder);
      this.Controls.Add(this.btnSignOut);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.btnSignIn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "MainForm";
      this.Text = "Onedrive Upload by Henrik Bach";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSignIn;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnSignOut;
    private System.Windows.Forms.Button btnFindMusikFolder;
    private System.Windows.Forms.Label lblMusikFoundStatus;
    private System.Windows.Forms.Button btnCheckSourceFolder;
    private System.Windows.Forms.Label lblSourceFolder;
    private System.Windows.Forms.TextBox textSourceFolder;
    private System.Windows.Forms.Button btnStartCopyingFiles;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblNumberOfFilesCopied;
    private System.Windows.Forms.Label lblMBCopied;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lblCurrentFolder;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnStopCopyingFiles;
  }
}

