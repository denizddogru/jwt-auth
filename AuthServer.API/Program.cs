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
using Microsoft.AspNetCore.Authentication.JwtBearer;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // CustomTokenOptions, appsettings'deki TokenOptionslar� doldurup bir nesne �rne�i d�nd�r�r. ( OptionsPattern )
        // DI container'a bir nesne ge�tik.

        builder.Services.Configure<CustomTokenOptions>(
            builder.Configuration.GetSection("TokenOptions"));


        builder.Services.Configure<List<Client>>(
            builder.Configuration.GetSection("Clients"));

        // ---- PART 1, Program.cs Config
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
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("AuthServer.Data");
                });
        }
        );

        // -----  PART 2, Progrma.cs Configuration, Udemy ----

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();

            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                // 1: Benim g�ndermi� oldu�um issue mu onu kontrol ediyorum
                // 2: appsettingsdeki TokenOptions Audience ger�ekten do�ru adresten mi geliyor ( audience )
                // 3: Ard�ndan issuer ve lifetime'�n� kontrol ediyoruz.

                // ClockSkew: Farkl� timezonelar , farkl� serverlara kurulmu� API'lar aras�nda token �mr�ne default 5 dk ekler. 
                // Biz  ayn� serverda ayn� anda �al��ss�n diye default de�eri kald�r�p 0'ya �ektik.(test ama�l�)

                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience[0],
                IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero


            };

        });



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
    }
}