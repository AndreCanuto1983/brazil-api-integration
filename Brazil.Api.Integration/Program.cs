using Brazil.Api.Integration.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Redis();
builder.Services.Ioc();
builder.Services.JsonSettings();
builder.HttpClientFactory();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Cors();

var app = builder.Build();

app.MapHealthChecks("/healthcheck");
app.CustomExceptionMiddleware();

//app.UseCors(allowSpecificOrigins);
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()            
            );

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
