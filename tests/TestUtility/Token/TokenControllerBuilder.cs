using SharedRecipe.Application.Services.Token;

namespace TestUtility.Token
{
    public class TokenControllerBuilder
    {
        public static TokenController CreateInstance()
        {
            return new TokenController(1000, "b2RjeTk0al5MRzNsdWJhITRqc3EwdjVURg==");
        }
    }
}
