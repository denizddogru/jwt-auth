using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SharedLibrary.Services;

// Private key şifreler, public key doğrular


// A

// B


// B bir public key ve private key oluşturur
// daha sonra public key'i A'yı gönderir.
// A, word dosyasıyla gelen key'i imzalar.

// Özetle, B A'ya ulaşırken A'nın public key'ini kullanarak şifrelenmiş mesajlar gönderebilir veya A'nın dijital imasını doğrulayabilir
// bu şekilde A'nın ( yani bizim API ) private anahtarı hiçbir zaman B'ye açıklanmaz.


public static class SignService
{
    public static SecurityKey GetSymmetricSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}
