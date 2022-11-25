using MinimalAPIMongoDB.Models;
using MinimalAPIMongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services to the container.
RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureApp(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.Configure<DeveloperDatabaseSettings>(builder.Configuration.GetSection("DeveloperDatabaseSettings"));
    services.AddSingleton<DeveloperService>();

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Aggregatio API",
            Description = "Storing and retrieving data about developers",
            Version = "v1"
        });
    });
}

void ConfigureApp(WebApplication app)
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapGet("/api/developer", async (DeveloperService service)
        => await service.GetAll());

    app.MapGet("/api/developer/{id}", async (DeveloperService service, string id)
        => await service.Get(id));

    app.MapPost("api/developer", async (DeveloperService service, Developer developer) =>
    {
        await service.Create(developer);
        return Results.Created($"/developer/{developer._id}", developer);
    });

    app.MapPut("/api/developer/{id}", async(DeveloperService service, string id, Developer updateDeveloper) =>
    {
        var developer = await service.Get(id);
        
        if (developer is null)
            return Results.NotFound();

        updateDeveloper._id = developer._id;

        await service.Update(id, updateDeveloper);

        return Results.NoContent();
    });
    
    app.MapDelete("/api/developers/{id}", async (DeveloperService service, string id) =>
    {
        var developer = await service.Get(id);
        
        if (developer is null)
            return Results.NotFound();
        
        await service.Delete(id);
        
        return Results.NotFound();
        
    });
}