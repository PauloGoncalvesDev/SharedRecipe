using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedRecipe.Exceptions.ExceptionsBase
{
    public class ValidationException : SharedRecipeException
    {
        public List<string> ErrorsMessage { get; set; }

        public ValidationException(List<string> errorsMessage) : base(string.Empty)
        {
            ErrorsMessage = errorsMessage;
        }
    }
}
