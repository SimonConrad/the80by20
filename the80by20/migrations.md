https://docs.microsoft.com/en-us/ef/core/cli/powershell
https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli

package manage console
default project core\core.infrastructure
nuget package Microsoft.EntityFrameworkCore.Design

fabryka CoreSqlServerDbContextDesignTimeFactory

Add-Migration Initial-Create -Context CoreSqlServerDbContext -o "DAL/Migrations"
Update-Database -context CoreSqlServerDbContext

Remove-Migration -Context CoreSqlServerDbContext