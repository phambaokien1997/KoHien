Because startup project and dbcontext's project is not the same, we need to run the migration command in the root folder follow below pattern:

dotnet ef migrations add InitialCreate --startup-project Web/BookStore.Web.csproj --project Core/BookStore.Core/BookStore.Core.csproj -c BookStoreDbContext -o Database/Migrations
 remember to rename the migration's name
