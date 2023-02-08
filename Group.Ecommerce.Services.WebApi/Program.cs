using Group.Ecommerce.Application.Interface;
using Group.Ecommerce.Application.Main;
using Group.Ecommerce.Application.Validator;
using Group.Ecommerce.Domain.Core;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Infraestructure.Data;
using Group.Ecommerce.Infraestructure.Interface;
using Group.Ecommerce.Infraestructure.Repository;
using Group.Ecommerce.Services.WebApi.Helpers;
using Group.Ecommerce.Services.WebApi.Modules;
using Group.Ecommerce.Transversal.Common;
using Group.Ecommerce.Transversal.Logging;
using Group.Ecommerce.Transversal.Mapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

string myPolicy = "policyApiEcommerce";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

//builder.Services.AddSingleton<IConfiguration>();
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();

builder.Services.AddScoped<ICustomersApplication, CustomersApplication>();
builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IUsersApplication, UsersApplication>();
builder.Services.AddScoped<IUsersDomain, UsersDomain>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

builder.Services.AddTransient<UsersDtoValidator>();


builder.Services.AddCors(options => options.AddPolicy(myPolicy, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddHealthCheck(configuration);

var appSettingsSection = configuration.GetSection("Config");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();

var key = Encoding.ASCII.GetBytes(appSettings.Secret);
var Issuer = appSettings.Issuer;
var Audience = appSettings.Audience;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userId = int.Parse(context.Principal.Identity.Name);
            return Task.CompletedTask;
        },

        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = Audience,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddSwaggerGen(swa =>
{
    swa.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API Testing by CamiloWG",
        Description = "A simple API made for testing using ASP.NET Core",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "CamiloWG",
            Email = "sergio.madero2423@gmail.com"
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "License Testing"
        }

    });


    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swa.IncludeXmlComments(xmlPath);

    var SecurityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    swa.AddSecurityDefinition(SecurityScheme.Reference.Id, SecurityScheme);

    swa.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { SecurityScheme, new List<string>() { } }
    });


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Testing API V1");
});

app.UseCors(myPolicy);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();
