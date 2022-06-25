Kode heri er kopieret fra (og tilrettet selvfølgelig) fra et winforms eksempel:
https://github.com/OneDrive/onedrive-sample-apibrowser-dotnet

TODO:
- hvis man henter koden fra github og bare prøver at kompilere den, så kommer der en fejl om at "copy" på en secrets.txt fil fejlede.
    Og det fejler fordi filen ikke findes. Det er sikkert en man selv skal lave, men hvordan?
    Der er en linje i .csproj filen, som laver kopieringen: <PostBuildEvent>copy $(SolutionDir)\secrets.txt $(TargetDir)</PostBuildEvent>

- Hvis man har kørt en kopiering færdig, og så bare trykker på "Start copying" igen, så får man en fejl med at man prøver at oprette en folder, 
    der allerede eksisterer. Det sker fordi musikFolder objektet ikke er blevet opdateret, så det indeholder kun de folder var inden man kørte første 
    gang.

- Ryd op i koden, så der ikke skal trykkes på alle knapper for at hente musik folder osv. mm.