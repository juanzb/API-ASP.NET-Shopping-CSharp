using Services;
using UnitOfWork.Interfaces;
using UnitOfWork.MysqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString =
    builder.Configuration.GetConnectionString("CadenaMySql")
        ?? throw new InvalidOperationException("Conexión string 'CadenaMySql' no se encuentra");

// Inyeccion de Dependencias
builder.Services.AddScoped<IUnitOfWork>(p => new UnidOfWorkMySqlServer(connectionString));
builder.Services.AddScoped<InvoicesServices>();
builder.Services.AddScoped<InvoicesDetailsServices>();
builder.Services.AddScoped<ClientsServices>();
builder.Services.AddScoped<ProductsServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
