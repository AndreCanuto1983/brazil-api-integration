using Brazil.Api.Integration.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Di();
builder.Services.Extension();
builder.HttpClient();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapHealthChecks("/healthcheck");
app.CustomExceptionMiddleware();

//habilitar caso queira esconder o swagger após publicado em produção
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
