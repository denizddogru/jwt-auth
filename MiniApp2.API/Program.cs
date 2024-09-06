using Microsoft.OpenApi.Models;
using SharedLibrary.Configurations;
using SharedLibrary.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Get TokenOption section from configuration
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOption"));
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOptions>();

// Add custom token authentication
builder.Services.AddCustomTokenAuth(tokenOptions);

// Add controllers
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiniApp2.API", Version = "v1" });
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniApp2.API v1"));
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthentication();

// Map the controllers to routes
app.MapControllers();

// Run the application
app.Run();
