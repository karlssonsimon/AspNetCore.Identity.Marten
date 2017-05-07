# AspNetCore.Identity.Marten
ASP.NET Core identity provider for Marten.

## Getting started
Add the `https://www.myget.org/F/aspnetcore-identity-marten/api/v3/index.json` myget repository to your nuget sources. This can be done by adding a `NuGet.config` to the root of your project:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="NuGet" value="https://api.nuget.org/v3/index.json" />
    <add key="aspnetcore-identity-marten" value="https://www.myget.org/F/aspnetcore-identity-marten/api/v3/index.json" />
  </packageSources>
</configuration>
```
    
The package can then be installed with:

    Install-Package AspNetCore.Identity.Marten
    
You can then use it by registering it in `ConfigureServices`:

```csharp    
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddScoped<IUserStore<User>, MartenUserStore<User, MartenRole>>();
    services.AddScoped<IRoleStore<MartenRole>, MartenRoleStore>();
    services.AddIdentity<User, MartenRole>();
    ...
    // Note that IDocumentSession needs to be registered in the container as well
    // and could be done something like this
    services.AddScoped(provider => provider.GetService<IDocumentStore>().LightweightSession());
}
```

Where `User` would be:

```csharp
public class User : MartenUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```
     
Currently the only supported type of `Id` for `MartenUser` and `MartenRole` is `Guid`.