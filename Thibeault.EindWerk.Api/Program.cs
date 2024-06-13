using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Thibeault.EindWerk.Api.Helper;
using Thibeault.EindWerk.Api.Models;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.DataLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);


#region Dependency Injection

// DataLayer
string connectionString = builder.Configuration.GetConnectionString("Thibeault");

builder.Services.AddDbContext<IDataContext, DataContext>(options => options.UseSqlServer(connectionString,
                                                               b => b.MigrationsAssembly("Thibeault.EindWerk.DataLayer"))
                                                                     .EnableSensitiveDataLogging());
//register identity core 
builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Services

// Api
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
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

app.Run();