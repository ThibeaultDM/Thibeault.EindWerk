using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Thibeault.Example.Api.Helper;
using Thibeault.Example.DataLayer;
using Thibeault.Example.DataLayer.DataSeeding;
using Thibeault.Example.DataLayer.Interfaces;
using Thibeault.Example.Services;

var builder = WebApplication.CreateBuilder(args);

#region Dependency Injection

// DataLayer
string connectionString;
if (Environment.MachineName == "DESKTOP-S7BR7BO")
{
    connectionString = builder.Configuration.GetConnectionString("Thibeault")
        ?? throw new Exception("Missing connection string, Thibeault");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("ProfDieHetChecked")
        ?? throw new Exception("Missing connection string, ProfDieHetChecked");
}

builder.Services.AddDbContext<IDataContext, DataContext>(options => options.UseSqlServer(connectionString,
                                                               b => b.MigrationsAssembly("Thibeault.Example.DataLayer"))
                                                                     .EnableSensitiveDataLogging());
// TODO remove EnableSensitiveDataLogging
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockActionRepository, StockActionRepository>();
builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();

//register identity core
builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

// Services
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<CustomerService>();

// Api
builder.Services.AddAutoMapper(typeof(AutoMapperHelper));
builder.Services.AddScoped<JwtHelper>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7215", "https://google.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.HttpContext.Request.Cookies["access_token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                    return Task.CompletedTask;
                }
                return Task.CompletedTask;
            }
        };
    });

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

app.UseRouting();

app.UseAuthentication();  //the middleware order must like here
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<SeedUserHelper>();
    await SeedUserHelper.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();