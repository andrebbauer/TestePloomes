using Microsoft.Extensions.Options;
using TestePloomes.Models;
using TestePloomes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
// injecao de dependencia
builder.Services.Configure<ClientesDatabaseSettings>(builder.Configuration.GetSection(nameof(ClientesDatabaseSettings)));
builder.Services.AddSingleton<IClientesDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ClientesDatabaseSettings>>().Value);
builder.Services.AddSingleton<ClientesService>();
builder.Services.AddSingleton<ComunicacaoService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
