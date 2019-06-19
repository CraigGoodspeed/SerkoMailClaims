# requires set-executionpolicy remotesigned
# I would love to take credit for this but it was downloaded from 
#https://github.com/philoushka/blog/blob/master/install-a-windows-service-using-powershell.md
param([string]$exePath)
$serviceName = "MailboxMonitor"

$username = "admin"
$password = convertto-securestring -String "admin01" -AsPlainText -Force  
$cred = new-object -typename System.Management.Automation.PSCredential -argumentlist $username, $password

$existingService = Get-WmiObject -Class Win32_Service -Filter "Name='$serviceName'"

if ($existingService) 
{
  "'$serviceName' exists already. Stopping."
  Stop-Service $serviceName
  "Waiting 3 seconds to allow existing service to stop."
  Start-Sleep -s 3
    
  $existingService.Delete()
  "Waiting 5 seconds to allow service to be uninstalled."
  Start-Sleep -s 5  
}

"Installing the service."
New-Service -BinaryPathName $exePath -Name $serviceName -DisplayName $serviceName -StartupType Automatic 
"Installed the service."
"Starting the service."
Start-Service $serviceName
"Completed."