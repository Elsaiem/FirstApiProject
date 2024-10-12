using Store.G04.Api.Errors;
using System.Text.Json;

namespace Store.G04.Api.Middleware
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _Next;
       private readonly ILogger<ExceptionMiddleWare> _Logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            _Next = next;
            _Logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next.Invoke(context);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                context.Response.ContentType = "apllication/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response =_env.IsDevelopment()?
                    
                     new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace.ToString())
                    :new ApiExceptionResponse(StatusCodes.Status500InternalServerError) ;


                var json= JsonSerializer.Serialize(response);

              await  context.Response.WriteAsync(json);


            }






        }





       
    }
}
