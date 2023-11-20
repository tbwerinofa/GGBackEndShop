using Microsoft.EntityFrameworkCore;
using Minio;
using ProductWebAPI.DataRepository;
using ProductWebAPI.Services.DataRepository.Service;
using ProductWebAPI.Services.DocumentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* database context dependency injection*/
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

var connectionString = $"Server={dbHost};DataBase={dbName};Uid=SA;Pwd={dbPassword};MultipleActiveResultSets=true;Integrated Security=False;Encrypt=False;TrustServerCertificate=True";

builder.Services.AddDbContext<ProductDBContext>(options =>
                options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var endpoint = builder.Configuration.GetSection("MinIOSettings:EndPoint").Value;
var accessKey = builder.Configuration.GetSection("MinIOSettings:AccessKey").Value;
var secretKey = builder.Configuration.GetSection("MinIOSettings:SecretKey").Value;

// Add Minio using the default endpoint
builder.Services.AddMinio(accessKey, secretKey);

// Add Minio using the custom endpoint and configure additional settings for default MinioClient initialization
builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(endpoint)
    .WithCredentials(accessKey, secretKey));


builder.Services.AddScoped<IDocumentManager, DocumentManager>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

app.UseCors(options =>
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
