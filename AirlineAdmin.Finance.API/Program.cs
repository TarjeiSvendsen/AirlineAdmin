var builder = WebApplication.CreateBuilder(args);

// Add service defaults, Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddProblemDetails();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.MapControllers();

app.Run();
