using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// CustomTokenOptions, appsettings'deki TokenOptionslarý doldurup bir nesne örneði döndürür. ( OptionsPattern )
// DI container'a bir nesne geçtik.

builder.Services.Configure<CustomTokenOptions>(
    builder.Configuration.GetSection("TokenOptions"));


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


