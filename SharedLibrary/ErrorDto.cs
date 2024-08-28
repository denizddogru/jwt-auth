using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary;

// Tüm API'larda hata yakalanan kısım burası olacak
public class ErrorDto
{
    
    // Private set: Özelliğin sadece sınıfın iççinden ayarlanabilmesini sağlar.
    public  List<String> Errors { get; private set; }

    public bool IsShow { get; private set; }


    public ErrorDto()
    {
        Errors  = new List<String>();
    }

    public ErrorDto(string error, bool isShow)
    {
        Errors.Add(error);
        isShow = true;
    }

    public ErrorDto(List<String> errors, bool isShow)
    {
        Errors = Errors;
        IsShow = isShow;
    }



}
