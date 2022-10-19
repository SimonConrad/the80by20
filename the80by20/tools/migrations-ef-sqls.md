https://docs.microsoft.com/en-us/ef/core/cli/powershell
https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli

## Migrations
package manager console
dotnet tool install --global dotnet-ef


--------------
default project the80by20.Solution.Infrastructure
nuget package Microsoft.EntityFrameworkCore.Design // maybe not needed

fabryka CoreSqlServerDbContextDesignTimeFactory

----------------------
startup project set as the80by20.Bootstrapper, it has package Microsoft.EntityFrameworkCore.Design installed 
in package-manager-console defulat-project: the80by20.Solution.Infrastructure
and with SolutionDbContextDesignTimeFactory - connection string from secrets

commands:
Add-Migration test -Context SolutionDbContext -o "EF/Migrations"
Update-Database -Context SolutionDbContext

Remove-Migration -Context SolutionDbContext


---- run for other dbctxts:
the80by20.Masterdata.Infrastructure
Add-Migration test -Context MasterDataDbContext -o "EF/Migrations"
Update-Database -context MasterDataDbContext

default project the80by20.Users.Infrastructure
Add-Migration test -Context UsersDbContext -o "EF/Migrations"
Update-Database -context UsersDbContext

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