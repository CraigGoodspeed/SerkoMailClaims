# requires set-executionpolicy remotesigned
# I would love to take credit for this but it was downloaded from 
#https://github.com/philoushka/blog/blob/master/install-a-windows-service-using-powershell.md

param([string]$exePath)
$serviceName = "MailboxMonitor"
"Installing the service."
New-Service -BinaryPathName $exePath -Name $serviceName -DisplayName $serviceName -StartupType Automatic 
"Installed the service."
"Starting the service."
Start-Service $serviceName
"Completed."