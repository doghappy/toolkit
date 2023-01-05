Param(
    [switch]$Dark
)

$value = 1
if ($Dark) {
    $value = 0
}

Set-ItemProperty `
-Path HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize `
-Name AppsUseLightTheme `
-Value $value