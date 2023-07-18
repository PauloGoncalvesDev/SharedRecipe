using AutoMapper;
using FluentValidation.Results;
using SharedRecipe.Application.Services.Cryptography;
using SharedRecipe.Application.Validators.User;
using SharedRecipe.Domain.Repositories;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Requests;

namespace SharedRecipe.Application.BusinessRules.User.Register
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        private readonly IMapper _mapper;

        private readonly IWorkUnit _workUnit;

        private readonly PasswordEncryption _passwordEncryption;

        public RegisterUser(IUserWriteOnlyRepository userWriteOnlyRepository, IMapper mapper, IWorkUnit workUnit, PasswordEncryption passwordEncryption)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
            _passwordEncryption = passwordEncryption;
        }

        public async Task Execute(UserRequestJson userRequestJson)
        {
            ValidateUser(userRequestJson);

            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(userRequestJson);
            user.Password = _passwordEncryption.Encrypt(userRequestJson.Password);

            await _userWriteOnlyRepository.Insert(user);

            await _workUnit.Commit();
        }

        private void ValidateUser(UserRequestJson userRequestJson)
        {
            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            if (!validationResult.IsValid)
            {
                List<string> errorMessage = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessage);
            }
        }
    }
}
