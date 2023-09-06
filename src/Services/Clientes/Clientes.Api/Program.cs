using Clientes.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseApiConfig();

app.Run();
