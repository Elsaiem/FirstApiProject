namespace Store.G04.Api.Errors
{
    public class ValidationApiErrorResponse:ApiErrorResponse
    {
      

        public IEnumerable<string> Errors { get; set; }=new List<string>(); 
        public ValidationApiErrorResponse() : base(400)
        {
           
        }


    }
}
