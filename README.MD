# Nuget packages required for this project

* GraphQL.Server.Transports.AspNetCore
* GraphQL.Server.Ui.Playground
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.SqlServer

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
# GraphQL playground

Once dependency injection and middleware are configured, the playground can be access `<host>/ui/playground`


