Start-Process Powershell.exe -Argumentlist "-NoExit -file start_webapp_host.ps1" -Verb runAs
Start-Process Powershell.exe -Argumentlist "-NoExit -file start_web.ps1" -Verb runAs