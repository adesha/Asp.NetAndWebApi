using FluentValidation;

namespace AspWebApi.net.Validators
{
    public class UpdateWalkDRequestValidator:AbstractValidator<Models.DTO.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
