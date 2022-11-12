using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using ProjectTasksApi.Data;
using ProjectTasksApi.Interfaces;
using ProjectTasksApi.Services;
using ProjectTasksApi.Models.Mapper;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

InitializeDb(app);

app.Run();


void ConfigureServices(WebApplicationBuilder builder)
{
    // Add endpoints versioning support
    builder.Services.AddMvcCore();
    builder.Services.AddApiVersioning(options =>
    {
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

    builder.Services.AddDbContext<ProjectTasksContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["AzureSqlConnection"]);
    });

    // Add swagger support
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add injectable services
    builder.Services.AddScoped<IHealthcheckService, HealthCheckService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<ITaskService, TaskService>();

    // Add mapping configurations
    builder.Services.AddAutoMapper(typeof(EntityToDtoMappingProfile));

    builder.Services.AddControllers()
        .AddNewtonsoftJson();
}

void InitializeDb(IHost app) {
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ProjectTasksContext>();

        dbContext.Database.EnsureCreated();
    }
}
