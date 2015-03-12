
# (Re)create the SIMPLETS project database on the target host
Param (
		# Force installation
        [Parameter(Mandatory=$false)]
		[switch]$Force,

		# The Server instance identifier
        [Parameter(Mandatory=$true)]
		[string]$ServerInstance,
		
		#The database to be created
		[Parameter(Mandatory=$true)]
		[string]$DatabaseName
)

# Usings.
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo");
# Add-PSSnapin SqlServerCmdletSnapin100
# Add-PSSnapin SqlServerProviderSnapin100

# Dont screw the scripts order...
# ".\create-database.sql", 
$sqlScripts = @(".\create-dbo-schema.sql", ".\create-security-schema.sql", ".\create-userspace-schema.sql", ".\create-clubs-schema.sql", ".\initial-dataload.sql")

# Working directory to append to scripts.
$workingDir = Get-Location

#foreach($script in $sqlScripts)
#{
#    Write-Host "Executing script... " $script
#    
#    $scriptAbs = [String]::Format("{0}\{1}", $workingDir, $script)
#    Invoke-Sqlcmd -Inputfile $scriptAbs -ServerInstance $ServerInstance -Database $DatabaseName
#}
	
Write-Host "Connecting to SQL Server instance..." -NoNewLine
$server = New-Object Microsoft.SqlServer.Management.Smo.Server($ServerInstance)

if($server -eq $null)
{
	Write-Host "Failed!" -ForeGroundColor Red
}
else
{
	Write-Host "Success!" -ForeGroundColor Green
	$database = $server.Databases[$DatabaseName] #New-Object Microsoft.SqlServer.Management.Smo.Database($server, $DatabaseName)
	
	# 1) Old database deletion
	#if ($Force) 
    if ($database)
	{
		Write-Host "Deleting existing database..."
        #$server.KillAllProcesses($DatabaseName)
        $server.KillDatabase($DatabaseName)
        $server.Refresh()
        # $database.UserAccess = "Single"
		# $database.Drop()
        
		Write-Host "(Re)creating database..." -NoNewLine
	} 
	else 
	{
		Write-Host "Trying to create database... (use the -Force parameter to get the option to delete the existing database and re-create it) " -NoNewLine
	}
	
	# 2) Database creation
    $database = New-Object Microsoft.SqlServer.Management.Smo.Database($server, $DatabaseName)
	$database.Collation = "Latin1_General_CI_AS"
	$database.Create()
    
	Write-Host "Success!" -ForeGroundColor Green
	Write-Host "    Database: $DatabaseName"
	Write-Host "    Created on: " $database.CreateDate
    
	# 3) Create Schemas and perform initial dataload    
	foreach($script in $sqlScripts)
	{
		Write-Host "Executing script... " $script
        
        $scriptAbs = [String]::Format("{0}\{1}", $workingDir, $script)
		Invoke-Sqlcmd -Inputfile $scriptAbs -ServerInstance $ServerInstance -Database $DatabaseName
	}
}