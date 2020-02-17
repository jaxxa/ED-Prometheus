$RimworldPath = "C:\Games\Steam\steamapps\common\RimWorld"

$ExePath = $RimworldPath + "\RimWorldWin64.exe"
$ModDestination = $RimworldPath + "\Mods\ED-Prometheus"

Remove-Item -Path $ModDestination -Recurse

Copy-Item -Path "C:\~Git\Jaxxa-Rimworld\~SubModules\ED-Prometheus\ED-Prometheus" -Destination $ModDestination -Recurse

start -FilePath $ExePath -ArgumentList "-savedatafolder=C:\~Git\RimworldSaves_1.1\ModTest"