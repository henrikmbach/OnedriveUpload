Kode heri er kopieret fra (og tilrettet selvfølgelig) fra et winforms eksempel:
https://github.com/OneDrive/onedrive-sample-apibrowser-dotnet

TODO:

- Hvis man har kørt en kopiering færdig, og så bare trykker på "Start copying" igen, så får man en fejl med at man prøver at oprette en folder, 
    der allerede eksisterer. Det sker fordi musikFolder objektet ikke er blevet opdateret, så det indeholder kun de folder var inden man kørte første 
    gang.

- Ryd op i koden, så der ikke skal trykkes på alle knapper for at hente musik folder osv. mm.