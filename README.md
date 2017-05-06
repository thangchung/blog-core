## BlogCore project
### Database migrations 
```
  dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Migrations/PersistedGrantDb
```

```
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Migrations/ConfigurationDb
```

```
dotnet ef migrations add InitDatabase -c BlogCoreDbContext -o Migrations/BlogCoreDb
```

```
dotnet run
```
### Bootstrap the application
> Run the **IdentityServer** first

> Run the **BlogCore.Web**

> Username: **root** / Password: **r00t1@3**

### Clean Architecture
![GitHub Logo](https://8thlight.com/blog/assets/posts/2012-08-13-the-clean-architecture/CleanArchitecture-8b00a9d7e2543fa9ca76b81b05066629.jpg)

### Usecase Diagram
![Usecases](https://github.com/thangchung/blog-core/blob/master/docs/Usecases.png)



