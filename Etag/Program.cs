using Delta;
using ETag.Delta;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DescribeAllParametersInCamelCase(); // Parametreleri olduðu gibi göstermek için
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped(_ => new SqlConnection(connectionString));
builder.Services.AddDbContext<EF_Context>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseLoggerFactory(LoggerFactory.Create(builder =>
    {
        builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.None);
    }));
});

var app = builder.Build();

// Ensure the database is created (or migrated)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EF_Context>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.UseDelta();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(c =>
    {
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
        c.EnableTryItOutByDefault();
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
