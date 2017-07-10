$webPath = "$PSScriptRoot\..\src\Web"

sl $webPath
& "npm" start

$HOST.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") | OUT-NULL
$HOST.UI.RawUI.Flushinputbuffer()