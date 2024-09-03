using AuthServer.Core.Configuration;
using AuthServer.Core.Entity;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using AuthServer.Data;
using AuthServer.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// CustomTokenOptions, appsettings'deki TokenOptionslar� doldurup bir nesne �rne�i d�nd�r�r. ( OptionsPattern )
// DI container'a bir nesne ge�tik.

builder.Services.Configure<CustomTokenOptions>(
    builder.Configuration.GetSection("TokenOptions"));

builder.Services.Configure<List<Client>>(
    builder.Configuration.GetSection("Clients"));


// DI Register, Autofac gibi bir  DI Container ( IoC Container ) k�t�phanesi de kullan�labilir

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Generic oldu�u i�in type of kulland�k
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        sqlOptions=>
        {
            sqlOptions.MigrationsAssembly("AuthServer.Data");
        });
}
);



// Singleton -> Singleton tan�ml� servisler uygulama aya�a kalk�nca bir defa olu�ur
// ve uygulama �mr� boyunca ayn� refereans �zerinden hizmetini s�rd�r�r.
// �rnekler: Loglama, uygulama �zelli�i a��p kapama

// Scoped -> Bu ya�am d�ng�s� her HTTP istek ba�� bir adet nesne �retmek i�in kullan�l�r.
// HTTP iste�i sona erdi�inde ise Garbage Collector taraf�ndan toplanacakt�r. 
// �rnekleri: Yetkilendirme, Oturum y�netme

// Transient ( ge�ici, her seferinden yeni bir instance olu�turulur )



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


