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

// CustomTokenOptions, appsettings'deki TokenOptionslarý doldurup bir nesne örneði döndürür. ( OptionsPattern )
// DI container'a bir nesne geçtik.

builder.Services.Configure<CustomTokenOptions>(
    builder.Configuration.GetSection("TokenOptions"));

builder.Services.Configure<List<Client>>(
    builder.Configuration.GetSection("Clients"));


// DI Register, Autofac gibi bir  DI Container ( IoC Container ) kütüphanesi de kullanýlabilir

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Generic olduðu için type of kullandýk
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



// Singleton -> Singleton tanýmlý servisler uygulama ayaða kalkýnca bir defa oluþur
// ve uygulama ömrü boyunca ayný refereans üzerinden hizmetini sürdürür.
// Örnekler: Loglama, uygulama özelliði açýp kapama

// Scoped -> Bu yaþam döngüsü her HTTP istek baþý bir adet nesne üretmek için kullanýlýr.
// HTTP isteði sona erdiðinde ise Garbage Collector tarafýndan toplanacaktýr. 
// Örnekleri: Yetkilendirme, Oturum yönetme

// Transient ( geçici, her seferinden yeni bir instance oluþturulur )



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


