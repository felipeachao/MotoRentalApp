using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using MotoRental.Application.Validators;
using Microsoft.OpenApi.Models;
using MotoRental.Infrastructure;
using MotoRental.Infrastructure.Messaging;
using MediatR;
using RabbitMQ.Client;
using MotoRental.Application.Consumers;
using MotoRental.Infrastructure.Repositories;
using FluentValidation; 


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MotoRentalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddMediatR(typeof(Program)); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
{
    HostName = builder.Configuration["RabbitMQ:Host"] ?? "rabbitmq",
    UserName = builder.Configuration["RabbitMQ:Username"] ?? "guest",  
    Password = builder.Configuration["RabbitMQ:Password"] ?? "guest",  
    Port = int.Parse(builder.Configuration["RabbitMQ:Port"] ?? "5672")
});
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(80);  
});
builder.Services.AddSingleton<RabbitMqConnectionManager>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRentalCommandValidator>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MotoRental API",
        Description = "API para gerenciamento de locação de motos e entregadores",
        Contact = new OpenApiContact
        {
            Name = "MotoRental Team",
            Email = "contato@motturental.com",
        }
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.OperationFilter<FileUploadOperationFilter>();
});

var app = builder.Build();

var rabbitMqManager = app.Services.GetRequiredService<RabbitMqConnectionManager>();
var connection = rabbitMqManager.GetRabbitMqConnection(); 

var consumer = new MotoRegisterConsumer(app.Services.GetRequiredService<INotificationRepository>(), connection);
consumer.StartConsuming();

app.Lifetime.ApplicationStopping.Register(() =>
{
    consumer.Dispose();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MotoRental API v1");
    c.RoutePrefix = string.Empty;
});

app.MapGet("/health", () =>
{
    try
    {
        return Results.Ok("API is running");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro no endpoint /health: {ex.Message}");
        return Results.Problem("Ocorreu um erro no servidor");
    }
});
app.UseAuthorization();
app.MapControllers();
app.Run();
