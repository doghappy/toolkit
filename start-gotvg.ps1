Start-Process "${env:ProgramFiles(x86)}/gotvg/gotvg.exe"
$lnk = "$HOME/Desktop/游聚游戏平台.lnk"
while ($true) {
    if (Test-Path $lnk) {
        Write-Host "gotvg.lnk exists in Desktop"
        Remove-Item $lnk
        Write-Host "gotvg.lnk has been removed from Desktop" -f Green
        break
    }
    else {
        Write-Host "gotvg.lnk is not in Desktop, will be retring..."
        Start-Sleep -Seconds 1
    }
}