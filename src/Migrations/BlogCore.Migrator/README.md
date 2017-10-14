```
cd <your path>\src\Migrations\BlogCore.Migrator
```

- **Access Control module**

```
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Migrations/PersistedGrantDb
```

```
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Migrations/ConfigurationDb
```

```
dotnet ef migrations add InitIdentityContext -c IdentityServerDbContext -o Migrations/IdentityDb
```

- **Blog module**

```
dotnet ef migrations add InitBlogContext -c BlogDbContext -o Migrations/BlogContextDb
```

- **Post module**

```
dotnet ef migrations add InitPostContext -c PostDbContext -o Migrations/PostContextDb
```
Finally, we need to run the command

```
dotnet run
```

That's it.