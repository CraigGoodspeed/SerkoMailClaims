# I would love to take credit for this but it was downloaded from 
# https://github.com/philoushka/blog/blob/master/install-a-windows-service-using-powershell.md
# and slightly modified to install on build.

$serviceName = "MailboxMonitor"
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
