namespace SharedRecipe.Reporting.Responses
{
    public class ErrorBaseResponseJson
    {
        public List<string> Message { get; set; }

        public bool Success { get; set; }

        public ErrorBaseResponseJson(string message, bool success)
        {
            Message = new List<string> { message };
            Success = success;            
        }

        public ErrorBaseResponseJson(List<string> message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}
