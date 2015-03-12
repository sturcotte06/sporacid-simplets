Database deployment instructions:
------------------------------------

1) Install the SQL Server Cmdlet for PowerShell by running the 3 executables from the bin folder:
	- SQLSysClrTypes.msi
	- SharedManagementObjects.msi
	- PowerShellTools.msi

2) From the database folder, Open PowerShell as an Administrator and run:
	- ./Start-SimpletsDatabaseSetup.ps1 -ServerInstance "server/instance" -DatabaseName "DbName"
	
	** Optional -Force parameter can be used to redeploy the database over and over again.
	
	