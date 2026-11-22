var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.AddServiceDefaults();
builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();