using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adding database service
builder.Services.AddDbContext<TodoWebApi.Data.TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDbContext")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    {
        Version = "v1",
        Title = "Todo API",
        Description = "Todo API built on ASP.NET Core 6.0",
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
