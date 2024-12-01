using Delta;
using ETag.Delta;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
