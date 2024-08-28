using System.Text.Json.Serialization;

namespace SharedLibrary.Dtos;
public class Response<T> where T:class
{
    //Response<T> :her bir Entity'ye istek yapıldığında başarılı ya da başarısız yanıtnıı tek biryerden dönmek


    // T'nin bir referans türü olduğunu belirtir ( sınıf, arayüz givi yani kısaca T'nin class olduğınmı belirtir)
    

    // Data, T türünde bir veri içerir. API'nin başarılı bir yanıt verdiğinde döndürecği veriyi tutar.
    // özelliği private set ile tanımlanmıştır yani sadece özellik sınıf içinde değiştirilebilir.
    public T Data { get; private set; }
    public int StatusCode { get; private set; }

    [JsonIgnore]
    public bool IsSuccessfull { get; private set; }
    public ErrorDto Error { get;  private set; }

    public static Response<T> Success(T data, int statusCode)
    {
        return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessfull = true };
    }

    public static Response<T> Success(int statusCode)
    {
        return new Response<T> { Data = default, StatusCode = statusCode, IsSuccessfull = true };
    }

    public static Response<T> Fail(ErrorDto errorDto, int statusCode)
    {
        return new Response<T> { Error = errorDto, StatusCode = statusCode  , IsSuccessfull = false };
    }

    // fail metodu overload'u yazdık
    public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
    {
        var errorDto = new ErrorDto(errorMessage, isShow);
        return new Response<T> { Error = errorDto ,StatusCode = statusCode , IsSuccessfull = false};
    }

 
}
