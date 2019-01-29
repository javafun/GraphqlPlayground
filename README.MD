# Nuget packages required for this project

* GraphQL.Server.Transports.AspNetCore
* GraphQL.Server.Ui.Playground
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.SqlServer (Windows user)
* Microsoft.EntityFrameworkCore.Sqlite (Mac user)

# Startup.cs

## Dependency Injection

```csharp

public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    services.AddDbContext<GraphqlDbContext>(options =>
        options.UseSqlServer(_config.GetConnectionString("GraphqlDemo")));
            
    services.AddScoped<ProductRepository>();

    services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
    services.AddScoped<GraphqlDemoSchema>();

    services.AddGraphQL(o => { o.ExposeExceptions = true; })
            .AddGraphTypes(ServiceLifetime.Scoped);
}

```

## Middleware 

```csharp

public void Configure(IApplicationBuilder app, IHostingEnvironment env, GraphqlDbContext db)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseGraphQL<GraphqlDemoSchema>();
    app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            
	db.Seed();
}

```

# How to use the project

You must have the following installed

## Prerequisite

* ASP.NET Core SDK 2.2
* MS SQL LocalDb (For Windows user)
* SQL Lite (For Mac user)

## Windows command prompt / Mac terminal

1. Clone/Fork the solution to your local drive  
  
   >`git clone git@github.com:javafun/GraphqlPlayground.git`
2. Change directory 
   >`$ CD GraphqlPlayground`
3. Run database migration 
   >`dotnet ef database upgrade`
4. Build and run solution
   >`dotnet run`
4. Open Graphql playground in browser
   >`https://localhost:5001/ui/playground` 

## Database
>**NOTE:** Make sure you only uncomment one or the other service depends on the platform you use. 

### Windows
For Windows user, you can uncomment the following lines from `Startup.cs`

```csharp

services.AddDbContext<GraphqlDbContext>(options =>
                 options.UseSqlServer(_config.GetConnectionString("GraphqlDemo")));
            
```
### Mac SQL Lite
For Mac user, you can uncomment the following lines from `Startup.cs`

```csharp
services.AddDbContext<GraphqlDbContext>(options=>
                options.UseSqlite(_config.GetConnectionString("GraphqlDemoLite")));

```
# GraphQL playground

Once dependency injection and middleware are configured, the playground can be access `<host>/ui/playground`


