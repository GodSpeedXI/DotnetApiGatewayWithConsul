## To migrate DbContext
Add migration if there are some update from domain \
`dotnet ef migrations add init --output-dir "Persistence/Migrations"`


Update migration to DB\
`dotnet ef database update`