https://docs.microsoft.com/en-us/ef/core/cli/powershell

https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli

## Migrations

### enable migrations 
in package manager console type

`dotnet tool install --global dotnet-ef`

### working with migrations

set startup project as `the80by20.Bootstrapper`, this project has package `Microsoft.EntityFrameworkCore.Design` installed and dbcontext registered into ioc container (with connection string); in package-manager-console set defulat-project to `the80by20.Solution.Infrastructure`

#### commands

in package manager console set defualt project to `the80by20.Solution.Infrastructure` as it has dbcontext to which we want to apply migrations

`Add-Migration initial -Context SolutionDbContext -o "EF/Migrations"`

`Update-Database -Context SolutionDbContext`

`Remove-Migration -Context SolutionDbContext` // remove last migration

#### commands for other dbctxts
in package manager console set defualt project to  `the80by20.Masterdata.Infrastructure`

`Add-Migration initial -Context MasterDataDbContext -o "EF/Migrations"`

`Update-Database -context MasterDataDbContext`


in package manager console set defualt project to  `the80by20.Users.Infrastructure`

`Add-Migration initial -Context UsersDbContext -o "EF/Migrations"`

`Update-Database -context UsersDbContext`

#### using dotnet ef cli

using powershell console go to `C:\the80by20\the80by20\src\Modules\Masterdata\the80by20.Masterdata.Infrastructure>` check if `dotnet ef` is working

add migration

`dotnet ef migrations add test-migration --context MasterDataDbContext --startup-project ..\..\..\Bootstrapper\the80by20.Bootstrapper\ -o ./EF/Migrations`

remove last migration

`dotnet ef migrations remove --context MasterDataDbContext --startup-project ..\..\..\Bootstrapper\the80by20.Bootstrapper\`


update database

`dotnet ef database update --context MasterDataDbContext --startup-project ..\..\..\Bootstrapper\the80by20.Bootstrapper\`

use help

`dotnet ef --help`
`dotnet ef migrations --help`
`dotnet ef database --help`
## SQL

### delete rows in transaction

```
BEGIN TRY
BEGIN TRANSACTION 
	DELETE  FROM [The80By20].[users].[Users]

	DELETE FROM [The80By20].[masterdata].[Categories]

	DELETE FROM [The80By20].[solutions].[ProblemsAggregates]
	DELETE FROM [The80By20].[solutions].[ProblemsCrudData]
	DELETE FROM [The80By20].[solutions].[SolutionsToProblemsAggregates]
	DELETE FROM [The80By20].[solutions].[SolutionsToProblemsReadModel]

COMMIT TRANSACTION
END TRY
BEGIN CATCH

	PRINT 'ERROR CATCHED'
	IF(@@TRANCOUNT > 0)
		ROLLBACK TRANSACTION;
		
	THROW;

END CATCH
GO
```

### delete test db
```
USE master;
ALTER database [The80By20-test] set offline with ROLLBACK IMMEDIATE;
DROP database [The80By20-test];
```