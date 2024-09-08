using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Dtos;
using SharedLibrary.Exceptions;
using System.Text.Json;

namespace SharedLibrary.Extensions;
public static class CustomExceptionHandler
{
    public static void UseCustomException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (errorFeature != null)
                {
                    var exception = errorFeature.Error;

                    ErrorDto errorDto = null;

                    if (exception is CustomExceptions)
                    {
                        errorDto = new ErrorDto(exception.Message, true);
                    }
                    else
                    {
                        errorDto = new ErrorDto(exception.Message, false);
                    }

                    var response = Response<NoDataDto>.Fail(errorDto, 500);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });






        });
    }
}
