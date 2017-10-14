### How to run the migration

```
cd <your path>\src\Migrations\BlogCore.Migrator
```

If you want to create the migration schema for these database contexts, you need to follow these steps

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

After that, we only need to type the command

```
dotnet run
```

That's it.
