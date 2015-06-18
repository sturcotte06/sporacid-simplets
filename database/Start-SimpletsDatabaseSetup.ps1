
# (Re)create the SIMPLETS project database on the target host
Param (
		# The Server instance identifier
        [Parameter(Mandatory=$true)]
		[string]$ServerInstance,
)

[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo");
Add-PSSnapin SqlServerCmdletSnapin100
Add-PSSnapin SqlServerProviderSnapin100

# Dont screw the scripts order...
$sqlScripts = @(".\create-dbo-schema.sql", ".\create-security-schema.sql", ".\create-userspace-schema.sql", ".\create-clubs-schema.sql", ".\initial-dataload.sql")
$workingDir = Get-Location
$databaseName = "SIMPLETS"
	
Write-Host "Connecting to SQL Server instance..." -NoNewLine
$server = New-Object Microsoft.SqlServer.Management.Smo.Server($ServerInstance)

if($server -eq $null)
{
	Write-Host "Failed!" -ForeGroundColor Red
}
else
{
	Write-Host "Success!" -ForeGroundColor Green

	# 1) Old database destruction
	$database = $server.Databases[$databaseName]
	if ($database -ne $null)
	{		
		Write-Host "Deleting existing database $databaseName..." -NoNewLine
		$server.KillDatabase($databaseName)
		$server.Refresh()
		Write-Host " Success!" -ForeGroundColor Green
	} 
	else
	{
		Write-Host "Database '$databaseName' not found" -ForeGroundColor Yellow
	}
	Write-Host "(Re)creating database..." -NoNewLine

	# 2) Database creation
    $database = New-Object Microsoft.SqlServer.Management.Smo.Database($server, $databaseName)
	$database.Collation = "Latin1_General_CI_AS"
	$database.Create()
    
	Write-Host "Success!" -ForeGroundColor Green
	Write-Host "    Database: $databaseName" -ForeGroundColor Cyan
	Write-Host "    Created on: " $database.CreateDate -ForeGroundColor Cyan
    
	# 3) Create Schemas and perform initial dataload    
	foreach($script in $sqlScripts)
	{
		Write-Host "Executing script... $script" 
        
        $scriptAbs = [String]::Format("{0}\{1}", $workingDir, $script)
		Invoke-Sqlcmd -Inputfile $scriptAbs -ServerInstance $ServerInstance -Database $databaseName
	}
}