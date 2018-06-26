#root path
$Path = ".\"
# migartion string pieces
$SSMA_Comand = "SSMAforAccessConsole.EXE "
$VariablePath = $Path + "MigrationVar\"
$VariableFile1 = "-v `"" + $VariablePath + "DB1Vars.xml`" "
$VariableFile2 = "-v `"" + $VariablePath + "DB2Vars.xml`" "
$VariableFile3 = "-v `"" + $VariablePath + "DB3Vars.xml`" "
$ServerMigration = "-s `"" + $Path + "ServerMigration.xml`" "
$SchemaMigration = "-s `"" + $Path + "SchemaMigration.xml`" "
$ServerConnectionFile = "-c `"" + $Path + "ServerConnection.xml`" "
# SQL Variables
$TransferSQL = $Path + "TempTransfer.sql"
$DeleteColSQL = $Path + "DeleteCol.sql"
#server variables
$ServerInstance = "DESKTOP-4KNRLI5\SQLEXPRESS"
$TempDatabaseName = "Temp_InventorAccessPortal_SQL"
$DatabaseName = "InventorAccessPortal_SQL"
$ServerUsername = "0itinventorportal@ad.siu.edu"
$ServerPassword = "0neJumpyMu`$t@ng13"
# build the migration strings
$SchemaMigration  = $SSMA_Comand + $SchemaMigration + $VariableFile1 + $ServerConnectionFile # for schema migration
$Server1Migration = $SSMA_Comand + $ServerMigration + $VariableFile1 + $ServerConnectionFile
$Server2Migration = $SSMA_Comand + $ServerMigration + $VariableFile2 + $ServerConnectionFile
$Server3Migration = $SSMA_Comand + $ServerMigration + $VariableFile3 + $ServerConnectionFile

# runs the inital schema migration
cmd.exe /c $SchemaMigration

# server names
$AccessDbsData = @()
$AccessDbsData += ,@('"SIUC Intellectual Property Database"', $Server1Migration)
$AccessDbsData += ,@('"SIUE Intellectual Property Database"', $Server2Migration)
$AccessDbsData += ,@('"SOM Intellectual Property Database"', $Server3Migration)

# source database vars to pass to sql
$FromDBVar = "FROM_DATABASE=" + $TempDatabaseName
$ToDBVar = "TO_DATABASE=" + $DatabaseName

# identity columns to be removed to avoid clashing tables
$IdentityCols = @("Reminder ID", "ID", "Transaction ID")

foreach($Data in $AccessDbsData)
{
    # turns the access DB into the Temp Database
    cmd.exe /c $Data[1]
    # Origin DB name
    $OriginVar = "ORIGIN_DATABASE=" + $Data[0]
    # sets the table names in order so that everything loads
    $OrderedTableNames = @("Codes", # low to no dependencies
                    "Colleges", 
                    "Departments", 
                    "Status",
                    "Gender",
                    "Organizations",
                    "Records Status",
                    "Starting Fiscal Year",
                    "Ending Fiscal Year",
                    "Combo Families", # medium depednencies
                    "Combo Family Listings",
                    "Reminders",
                    "Families",
                    "Investigators",
                    "Project Numbers",
                    "All Investigators", # high dependencies
                    "File Numbers",
                    "Family Listings",
                    "Transactions")

    foreach ($TableName in $OrderedTableNames) 
    {
        # sets the vars passed into the sql commands
        $TableVar = "TABLE_NAME=" + $TableName
        $vars = $OriginVar, $FromDBVar, $ToDBVar, $TableVar
       
        # removes the identity columns from the temp db
        foreach ($IdCol in $IdentityCols) 
        {
            $IdColVar = "COL_NAME=" + $IdCol
            $ColVars = $vars + $IdColVar
            Invoke-Sqlcmd -ServerInstance $ServerInstance -InputFile $DeleteColSQL -Variable $ColVars
        }
        # runs the transfer from the temp db to the real db
        Invoke-Sqlcmd -ServerInstance $ServerInstance -InputFile $TransferSQL -Variable $vars
    }
}