using FluentValidation;

namespace AspWebApi.net.Validators
{
    public class AddWalkDRequestValidator:AbstractValidator<Models.DTO.AddWalkDifficultyRequest>
    {
        public AddWalkDRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
