using Brazil.Api.Integration.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Di();
builder.Services.Extension();
builder.HttpClient();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

//set especific cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(
            "http://localhost:8080",
            "http://192.168.0.195:8080",
            "http://localhost:3000"));
});

var app = builder.Build();

app.MapHealthChecks("/healthcheck");
app.CustomExceptionMiddleware();

//Enable if you want to hide the swagger after publishing in production
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors("AllowSpecificOrigin");
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
