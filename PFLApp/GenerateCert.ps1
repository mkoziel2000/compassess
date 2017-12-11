If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{   
    #"No Administrative rights, it will display a popup window asking user for Admin rights"

    $arguments = "& '" + $myinvocation.mycommand.definition + "' > GeneratePfx.log"
    Start-Process "$psHome\powershell.exe" -Verb runAs -ArgumentList $arguments

    break
}

$cert = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname localhost,localhost,127.0.0.1
$pwd = ConvertTo-SecureString -String ‘pfl’ -Force -AsPlainText
$path = 'cert:\localMachine\my\' + $cert.thumbprint
$currentPath=Split-Path ((Get-Variable MyInvocation -Scope 0).Value).MyCommand.Path
Export-PfxCertificate -cert $path -FilePath "$currentPath\pfl.pfx" -Password $pwd