using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharedLibrary.Dtos;

// Tüm API'larda hata yakalanan kısım burası olacak
public class ErrorDto
{

    // Private set: Özelliğin sadece sınıfın iççinden ayarlanabilmesini sağlar.
    public List<string> Errors { get; private set; }

    public bool IsShow { get; private set; }


    public ErrorDto()
    {
        Errors = new List<string>();
    }

    public ErrorDto(string error, bool isShow)
    {
        Errors.Add(error);
        isShow = true;
    }

    public ErrorDto(List<string> errors, bool isShow)
    {
        Errors = Errors;
        IsShow = isShow;
    }

}
