# Přidání certifikátu aplikace
certutil -addstore "Root" "%~dp0\ReleasedApps\UWPGUI\ChattyMoUWPGUI_1.0.11.0_x86_x64_arm_arm64.cer"

# Povolení přístupu do sítě localhost
checknetisolation loopbackexempt -a -n=15497848-7eb4-46f7-9920-e695ba086f0c_pts9qda9qs9ea

# Instalace aplikace
Powershell.exe -executionpolicy unrestricted -File "%~dp0\ReleasedApps\UWPGUI\Install.ps1"
