using Cards.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilogLogger();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseSwaggerDocumentation();

await app.RunDbMigrations(app.Environment);

app.UseApplicationServices(app.Environment);

app.MapControllers();

app.Run();
