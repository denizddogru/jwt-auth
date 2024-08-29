using AuthServer.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Data;

// Identity üyelik tabloları
//  Kullanıcı ile ilgili tüm db setler IdentityDbContext'ten gelicek
public class AppDbContext : IdentityDbContext<AppUser, IdentityRole,string>
{

    // DbContext sınıfının constructor'ına gelen DbContextOptions<AppDbContext> parametresi,
    // AppDbContext sınıfının konfigürasyon ayarlarını içerir. Veritabanı sağlayıcısı, 
    // bağlantı dizesi gibi ayarlar bu seçenekler içinde tanımlanır ve base(options) ifadesiyle 
    // bu ayarlar DbContext sınıfına iletilir.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }




    // OnModelCreating metodu, EF Core tarafından modelin oluşturulması sırasında çağrılır.
    // Bu metod, Entity Framework'ün veritabanı tablolarını nasıl oluşturacağını, 
    // ilişkileri nasıl kuracağını ve ek yapılandırmaların nasıl uygulanacağını belirler.
    // Örneğin, burada tabloların isimlendirilmesi veya belirli sütunlara ek özellikler eklenmesi sağlanabilir.

    protected override void OnModelCreating(ModelBuilder builder)
    {

        // Assembly içinde IEntity olan tüm configürasyonları ekleyecek

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(builder);
    }
}
