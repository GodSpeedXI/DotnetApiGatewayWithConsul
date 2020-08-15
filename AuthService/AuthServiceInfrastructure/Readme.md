## To migrate DbContext
Add migration if there are some update from domain \
```
dotnet ef migrations add InitPersistedGrantDbContext -c PersistedGrantDbContext -o IdentityServer/Migrations/PersistedGrant &&
dotnet ef migrations add InitConfigurationDbContext -c ConfigurationDbContext -o IdentityServer/Migrations/Configuration
```


Update migration to DB\
`dotnet ef database update`