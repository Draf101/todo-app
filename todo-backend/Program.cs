var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader() // Allow all headers
              .AllowAnyMethod(); // Allow all HTTP methods
    });
});
builder.Services.AddSingleton<LiteDbService>();
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseCors();
app.MapControllers();
app.UseHttpsRedirection();
app.MapGet("/", () => "TODO Backend API!");

app.Run("http://localhost:7277");