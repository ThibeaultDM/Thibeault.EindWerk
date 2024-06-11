using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Thibeault.EindWerk.Api.Models;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.DataLayer.DataSeeding;
using Thibeault.EindWerk.DataLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

#region Dependency Injection

// DataLayer
string connectionString = builder.Configuration.GetConnectionString("Thibeault");

builder.Services.AddDbContext<IDataContext, DataContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Thibeault.EindWerk.DataLayer")).EnableSensitiveDataLogging());
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Services

// Api
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

#endregion Dependency Injection

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Thibeault Eindwerk API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<SeedDatabaseHelper>();
    await SeedDatabaseHelper.SeedInitialUserAsync(scope.ServiceProvider);
}


app.Run();