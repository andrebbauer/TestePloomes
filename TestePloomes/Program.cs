using Microsoft.Extensions.Options;
using TestePloomes.Models;
using TestePloomes.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

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

// secret
SecretClientOptions options = new SecretClientOptions()
{
    Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
};
var client = new SecretClient(new Uri("https://TestePloomes-vault.vault.azure.net/"), new DefaultAzureCredential(), options);
KeyVaultSecret secret = client.GetSecret("ConnectionString");
string secretValue = secret.Value;
// end secret

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
