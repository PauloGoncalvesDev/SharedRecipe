using AutoMapper;
using FluentValidation.Results;
using SharedRecipe.Application.Services.Cryptography;
using SharedRecipe.Application.Services.Token;
using SharedRecipe.Application.Validators.User;
using SharedRecipe.Domain.Repositories;
using SharedRecipe.Domain.Repositories.User;
using SharedRecipe.Exceptions;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Application.BusinessRules.User.Register
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly IMapper _mapper;

        private readonly IWorkUnit _workUnit;

        private readonly PasswordEncryption _passwordEncryption;

        private readonly TokenController _tokenController;

        public RegisterUser(IUserWriteOnlyRepository userWriteOnlyRepository, IMapper mapper, IWorkUnit workUnit, PasswordEncryption passwordEncryption, TokenController tokenController, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
            _passwordEncryption = passwordEncryption;
            _tokenController = tokenController;
            _userReadOnlyRepository = userReadOnlyRepository;   
        }

        public async Task<UserResponseJson> Execute(UserRequestJson userRequestJson)
        {
            await ValidateUser(userRequestJson);

            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(userRequestJson);
            user.Password = _passwordEncryption.Encrypt(userRequestJson.Password);

            await _userWriteOnlyRepository.Insert(user);

            await _workUnit.Commit();

            string tokenJwt = _tokenController.GenerateTokenJwt(user.Email);

            return new UserResponseJson
            {
                Success = true,
                Message = APIMSG.USER_CREATED,
                Token = tokenJwt
            };
        }

        private async Task ValidateUser(UserRequestJson userRequestJson)
        {
            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            bool existingEmail = await _userReadOnlyRepository.GetExistingEmail(userRequestJson.Email);

            if (existingEmail)
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(userRequestJson.Email, APIMSG.EXISTING_EMAIL));

            if (!validationResult.IsValid)
            {
                List<string> errorMessage = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessage);
            }
        }
    }
}
