<h2>Because startup project and dbcontext's project is not the same, we need to run the migration command in the root
    folder follow below pattern:</h2>

<b><i>dotnet ef migrations add InitialCreate --startup-project Web/BookStore.Web.csproj --project
        Core/BookStore.Core/BookStore.Core.csproj -c BookStoreDbContext -o Database/Migrations</i></b>

<h3>Explainations:</h3>
<ul>
    <li>
        - <b>dotnet ef migration add</b> : dotnet entityframework command to add migration files</li>
    <li>
        - <b> InitialCreate</b>: name of migration file which will be generated after the command is run (you need to
        change this everytime you add new migration)</li>
    <li>
        - <b> --startup-project Web/BookStore.Web.csproj</b>: point out start up project's path</li>
    <li>
        - <b> --project Core/BookStore.Core/BookStore.Core.csproj</b>: point out db context project's path</li>
    <li>
        - <b> -c BookStoreDbContext </b>: point out db context name (in case the project contain more than 1 db context)
    </li>
    <li>
        - <b> -o Database/Migrations </b>: point out the folder in the db context project that the generated files will
        be stored</li>
</ul>****
