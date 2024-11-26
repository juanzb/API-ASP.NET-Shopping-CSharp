using Services;
using UnitOfWork.Interfaces;
using UnitOfWork.MysqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


// Inyeccion de Dependencias
builder.Services.AddScoped<IUnitOfWork, UnidOfWorkMySqlServer>();
builder.Services.AddScoped<InvoicesServices>();
builder.Services.AddScoped<InvoicesDetailsServices>();
builder.Services.AddScoped<ClientsServices>();
builder.Services.AddScoped<ProductsServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
