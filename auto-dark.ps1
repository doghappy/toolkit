$baseCmd = "Set-ItemProperty -Path HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize -Name AppsUseLightTheme -Value "

$trigger = New-ScheduledTaskTrigger -At 7PM -Daily
$cmd = "$($baseCmd) 0"
$action = New-ScheduledTaskAction -Execute 'powershell.exe' -Argument "-windowstyle hidden -c `"$cmd`""
Register-ScheduledTask -TaskName "Auto-Dark" -Action $action -Trigger $trigger -Force

$trigger = New-ScheduledTaskTrigger -At 7AM -Daily
$cmd = "$($baseCmd) 1"
$action = New-ScheduledTaskAction -Execute 'powershell.exe' -Argument "-windowstyle hidden -c `"$cmd`""
Register-ScheduledTask -TaskName "Auto-Light" -Action $action -Trigger $trigger -Force