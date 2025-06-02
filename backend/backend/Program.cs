using backend.Core;
using DataModel;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AppDataConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new AppDataConnection(config.GetConnectionString("Connection"));

});

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<OrderTrackingService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<UlEatsDb>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connStr = config.GetConnectionString("Connection");
    if (string.IsNullOrEmpty(connStr))
        throw new InvalidOperationException("La cadena de conexión 'Connection' no está definida en la configuración.");
    var baseOptions = new DataOptions().UsePostgreSQL(connStr);
    var options = new DataOptions<UlEatsDb>(baseOptions);
    return new UlEatsDb(options);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
