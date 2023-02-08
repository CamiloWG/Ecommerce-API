using FluentValidation;
using Group.Ecommerce.Application.DTO;


namespace Group.Ecommerce.Application.Validator
{
    public class UsersDtoValidator : AbstractValidator<UsersDto>
    {
        public UsersDtoValidator() 
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull().NotEmpty();

        }
    }
}