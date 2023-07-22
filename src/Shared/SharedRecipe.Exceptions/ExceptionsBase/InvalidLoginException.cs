namespace SharedRecipe.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : SharedRecipeException
    {
        public InvalidLoginException() : base(APIMSG.LOGIN_ERROR) { }
    }
}
