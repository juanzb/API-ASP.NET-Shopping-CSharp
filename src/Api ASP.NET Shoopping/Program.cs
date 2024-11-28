using Services;
using System.Text;
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


// pathBase
app.UsePathBase("/api");


// Configure the HTTP request pipeline.


//Middlewere Test
app.Use(async (context, next) =>
{
    Console.WriteLine(context.Request.Host);
    Console.WriteLine(context.Request.Method);
    Console.WriteLine(context.Request.Path);
    Console.WriteLine(context.Request.PathBase);
    Console.WriteLine(context.Request.Protocol);
    Console.WriteLine(context.Request.Query);
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(context.Request.Cookies));
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(context.Request.Headers));
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(context.Request.ContentType));
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(context.Request.ContentLength));

    // Habilitar relectura del cuerpo
    context.Request.EnableBuffering();
    // Leer el cuerpo como texto
    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
    {
        var body = await reader.ReadToEndAsync();
        Console.WriteLine("Body: " + body);

        // Reiniciar el flujo del cuerpo para que otros middlewares puedan leerlo
        context.Request.Body.Position = 0;
    }

    if (context.Request.PathBase != "/api") 
        context.Response.StatusCode = 400;
    else 
        await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
