using Brazil.Api.Integration.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Redis();
builder.Services.Di();
builder.Services.Extension();
builder.HttpClient();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:8080",
                "http://192.168.0.195:8080",
                "http://localhost:3000"
                );
        });
});

var app = builder.Build();

app.MapHealthChecks("/healthcheck");
app.CustomExceptionMiddleware();

app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
