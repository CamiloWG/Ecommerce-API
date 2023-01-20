using AutoMapper;
using Group.Ecommerce.Transversal.Mapper;
using Group.Ecommerce.Transversal.Common;
using Group.Ecommerce.Infraestructure.Data;
using Group.Ecommerce.Infraestructure.Interface;
using Group.Ecommerce.Infraestructure.Repository;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Domain.Core;
using Group.Ecommerce.Application.Interface;
using Group.Ecommerce.Application.Main;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

//builder.Services.AddSingleton<IConfiguration>();
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();

builder.Services.AddScoped<ICustomersApplication, CustomersApplication>();
builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

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
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Testing API V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
