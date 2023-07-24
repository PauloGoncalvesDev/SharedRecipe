using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SharedRecipe.Application.Services.Token;
using SharedRecipe.Domain.Entities;
using SharedRecipe.Domain.Repositories.User;
using SharedRecipe.Exceptions;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Api.Filters
{
    public class UserAuthorization : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly TokenController _tokenController;

        public UserAuthorization(IUserReadOnlyRepository userReadOnlyRepository, TokenController tokenController)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _tokenController = tokenController;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                string token = GetTokenFromRequest(context);

                string email = _tokenController.GetEmailFromTokenJwt(token);

                User user = await _userReadOnlyRepository.GetUserByEmail(email);

                if (user == null)
                    throw new Exception();
            }
            catch (SecurityTokenExpiredException)
            {
                ReturnsExceptionTokenExpired(context);
            }
            catch
            {
                ReturnsExceptionUserWithoutAuthorization(context);
            }
        }

        private string GetTokenFromRequest(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
                throw new Exception();

            return token["Bearer".Length..].Trim();
        }

        private void ReturnsExceptionUserWithoutAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorBaseResponseJson(APIMSG.USER_UNAUTHORIZED, false));
        }

        private void ReturnsExceptionTokenExpired(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorBaseResponseJson(APIMSG.TOKEN_EXPIRED, false));
        }
    }
}
