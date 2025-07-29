using PanPan.WebApp.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebAppConfig(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseWebAppConfig(builder.Environment);

app.Run();
