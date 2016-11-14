using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using System;

namespace OnedriveUpload
{
  class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      Do();
    }

    static async void Do()
    {
      // Application id registered on apps.dev.microsoft.com:
      string clientId = "000000004C1B625C";

      var msaAuthenticationProvider = new MsaAuthenticationProvider(
              clientId,
              "https://login.live.com/oauth20_desktop.srf",
              new[] { "onedrive.readonly", "wl.signin" });

      await msaAuthenticationProvider.AuthenticateUserAsync();
      var oneDriveClient = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthenticationProvider);

      // Get users drive:
      var drive = await oneDriveClient
                          .Drive
                          .Request()
                          .GetAsync();

      var rootItem = await oneDriveClient
                         .Drive
                         .Root
                         .Request()
                         .GetAsync();
    }

  }
}
