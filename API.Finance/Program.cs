using API.Data;
using API.Interfaces;
using API.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
//using Org.BouncyCastle.Asn1.X509;
using System.Reflection;
//using MySql.Data.EntityFrameworkCore.Extensions;
//using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1",
    new OpenApiInfo
    {
        Version = "v1.3c",
        Title = "Stock Tool Demo",
        Description = "This is a demo of .Net Core 8.0 WebApi using React and TypeScript UI front-end",
        //*
        TermsOfService = new Uri("/TermsOfService", UriKind.Relative),
        Contact = new OpenApiContact
        {
            Name = "Contact Us",
            Url = new Uri("/Contact", UriKind.Relative)
        },
        License = new OpenApiLicense
        {
            Name = "EULA",
            Url = new Uri("/EULA", UriKind.Relative)
        }
        //*/
    });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    options.IncludeXmlComments(xmlPath);
});
//*/

builder.Services.AddDbContextPool<ApplicationDBContext>(options =>
{
    //options.UseSqlite(builder.Configuration.GetConnectionString("LocalDB"));
    //*
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")
        , options => options.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: System.TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
        );
    //*/
});

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI( 
        ui => ui.InjectStylesheet("./Assets/Style/swagger.css")
    );
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();