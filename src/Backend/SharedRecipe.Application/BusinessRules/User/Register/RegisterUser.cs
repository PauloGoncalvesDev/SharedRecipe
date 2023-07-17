using AutoMapper;
using FluentValidation.Results;
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

        public RegisterUser(IUserWriteOnlyRepository userWriteOnlyRepository, IMapper mapper, IWorkUnit workUnit)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
        }

        public async Task Execute(UserRequestJson userRequestJson)
        {
            ValidateUser(userRequestJson);

            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(userRequestJson);
            user.Password = "2313";

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
