using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using ThinhBlogAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connect to azure to inject connection string
string kvURL = builder.Configuration["KeyVaultConfig:KeyVaultURL"];
string tenantID = builder.Configuration["KeyVaultConfig:TenantID"];
string clientID = builder.Configuration["KeyVaultConfig:ClientID"];
string clientSecret = builder.Configuration["KeyVaultConfig:ClientSecret"];
var credential = new ClientSecretCredential(tenantID, clientID, clientSecret);
var client = new SecretClient(new Uri(kvURL), credential);
// inject Keyvault secrets to Configuration
builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

// connect to sql server
builder.Services.AddDbContext<DataContext>(options =>
options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration["ConnectionStrings:KilatusSQL"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
