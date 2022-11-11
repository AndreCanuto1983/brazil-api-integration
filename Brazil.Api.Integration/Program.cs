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

var app = builder.Build();

app.MapHealthChecks("/healthcheck");
app.CustomExceptionMiddleware();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
