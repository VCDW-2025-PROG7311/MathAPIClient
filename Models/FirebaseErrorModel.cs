namespace MathAPIClient.Models
{
    public class FirebaseErrorModel
    {
        public FirebaseError error { get; set; }
    }
   
    public class FirebaseError
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Error> errors { get; set; }
    }

   public class Error
    {
    public string? ErrorMessage { get; set; }

    public Error(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }
    }
}