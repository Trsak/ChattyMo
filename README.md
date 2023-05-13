# Porovnání platforem pro návrh a~tvorbu GUI na platformě C#/.NET
## Spuštění aplikací
Nejprve bude nutné zprovoznit REST API, která musí běžet po celou dobu testování GUI aplikací. Jednotlivé GUI aplikace je možné míst spuštěné vícekrát a používat je zároveň.

### Backend REST API
1. Ke zprovoznění REST API budete potřebovat nainstalovanou služvu Docker, ideálně pak aplikaci Docker Desktop - https://www.docker.com/products/docker-desktop/.
2. Po nainstalování a spuštění aplikace Docker Desktop spusťe přiložený soubor `RunRestApi.bat`.
3. Jakmile dojde k sestavení kontejnerů, budou zároveň spuštěny.
4. To že už REST API správně běží je možné si ověřit otevřením Swagger dokumentace na adrese http://localhost:8080/swagger
5. Pokud se dokumentace správně otevře a načte, je vše připraveno a v pořádku
6. Ukončit spuštěné kontejnery je možné terminováním bat skriptu (CTRL+C) či přímo v aplikace Docker Desktop. Po celou dobu testování GUI aplikací musí být však REST API spuštěna.

### WinForms GUI
Jednoduše stačí otevřít adresář ReleasedApps\WinFormsGUI a spustit binární soubor `ChattyMoWinFormsGUI.exe`.   
Jelikož se nejedná o aplikaci vydanou bezpečným vydavatelem, může být potřebné spuštění aplikace potvrdit a povolit (Další informace -> Přesto spustit).

### WPF GUI
Jednoduše stačí otevřít adresář ReleasedApps\WPFGUI a spustit binární soubor `ChattyMoWPFGUI.exe`.  
Jelikož se nejedná o aplikaci vydanou bezpečným vydavatelem, může být potřebné spuštění aplikace potvrdit a povolit (Další informace -> Přesto spustit).

### UWPGUI
Spuštění aplikace implementované s knihovnou UWP je bohužel značně složitější, důvody jsou popsány v textu práce, konkrétně v kapitole číslo 4.3.3.  
Všechny soubory se nachází v adresáři ReleasedApps\UWPGUI.
1. Nejprve je nutné spustit instalační skript `Install.ps1` (pravým -> Run with PowerShell). Tento skript nainstaluje certifikát aplikace a následně i aplikaci samotnou. V průběhu instalace může být nutné potvrdit instalační kroky a postupné spouštění skriptů.  
2. V případě, že není povoleno spouštění skriptů PowerShell, je nutné jejich spouštění povolit příkazem `Set-ExecutionPolicy RemoteSigned`
3. Po úspěšném zpracování skriptu je aplikace nainstalována do systému a může být otevřena například pomocí nabídky windows pod názvem **ChattyMoUWPGUI**
4. Na závěr je nutné aplikaci udělit přístup k síti localhost kvůli připojení k REST API, docílíme toho spuštěním skriptu `AllowLocalhostForUWP.bat`
5. Aplikace by nyní měla být plně funkční a připravena k používání.

## Překlad a spuštění aplikací ze zdrojového kódu
Pro překlad a spuštění doporučuji využít vývojové prostředí Visual Studio 2022. Pro překlad je nutné mít nainstalovaný .NET 6.0 SDK - https://dotnet.microsoft.com/en-us/download/dotnet/6.0. Zároveň je nutné mít v rámci prostředí Visual Studia nainstalované komponenty pro vývoj aplikací WinForms, WPF a UWP.

Následně stačí otevřít celé řešení (soubor SourceCode/ChattyMo.sln) a přeložit či spustit jednotlivé projekty.
