using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AutoMapper;

namespace AuthServer.Service;

// Mapper çağırılana kadar memory'de çağırılmayacak. ( ihtiyaç duyulana kadar çağırılmayacak ) 
// Lazy'de geç yüklenmesi gereken nesneleri kunllanabiliriz, ihtiyaç halinde.
public static  class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DtoMapper>();
        });

        return config.CreateMapper();   

    });

    public static IMapper Mapper => lazy.Value;


    // Bu örnekte, ObjectMapper.Mapper çağrıldığında IMapper nesnesi oluşturulur (eğer daha önce oluşturulmadıysa)
    // ve Map metodu kullanılarak ProductDto nesnesi Product nesnesine dönüştürülür.

    // var productDto = new ProductDto { Id = 1, Name = "Example", Price = 10.0m };
    // var product = ObjectMapper.Mapper.Map<Product>(productDto);

}
