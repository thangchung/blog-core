Start-Process Powershell.exe -Argumentlist "-NoExit -file start_access_control_host.ps1" -Verb runAs
Start-Process Powershell.exe -Argumentlist "-NoExit -file start_api_gateway_host.ps1" -Verb runAs
Start-Process Powershell.exe -Argumentlist "-NoExit -file start_web.ps1" -Verb runAs