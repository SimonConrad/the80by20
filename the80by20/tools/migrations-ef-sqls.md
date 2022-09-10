https://docs.microsoft.com/en-us/ef/core/cli/powershell
https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli

## Migrations
package manager console
dotnet tool install --global dotnet-ef

default project core\core.infrastructure
nuget package Microsoft.EntityFrameworkCore.Design

fabryka CoreSqlServerDbContextDesignTimeFactory

Add-Migration Initial-Create -Context CoreDbContext -o "DAL/Migrations"
Update-Database -context CoreDbContext

Add-Migration newcolumn -Context CoreDbContext

Remove-Migration -Context CoreDbContext

## SQL

### delete rows in transaction
BEGIN TRY
BEGIN TRANSACTION 
	DELETE  FROM [The80By20].[dbo].[Users]

	DELETE FROM [The80By20].[dbo].[Categories]

	DELETE FROM [The80By20].[dbo].[ProblemsAggregates]
	DELETE FROM [The80By20].[dbo].[ProblemsCrudData]
	DELETE FROM [The80By20].[dbo].[SolutionsToProblemsAggregates]
	DELETE FROM [The80By20].[dbo].[SolutionsToProblemsReadModel]

COMMIT TRANSACTION
END TRY
BEGIN CATCH

	PRINT 'ERROR CATCHED'
	IF(@@TRANCOUNT > 0)
		ROLLBACK TRANSACTION;
		
	THROW;

END CATCH
GO

### delete test db
USE master;
ALTER database [The80By20-test] set offline with ROLLBACK IMMEDIATE;
DROP database [The80By20-test];