using CodeBookAPL.Models;
using CodeBookAPL.Repository.IRepository;
using CodeBookAPL.Repository;
using Microsoft.EntityFrameworkCore;
using CodeBookAPL;

//var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // ✅ Bind to the Render-provided PORT
    ApplicationName = typeof(Program).Assembly.FullName,
    WebRootPath = "wwwroot",
    ContentRootPath = Directory.GetCurrentDirectory()
});


var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(Int32.Parse(port));
});

//Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<CodeBookContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));

builder.Services.AddScoped<IProducts, ProductsRepository>();
builder.Services.AddScoped<IFeaturedProducts, FeaturedProductsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddAutoMapper(typeof(MappingConfig));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // React app URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
