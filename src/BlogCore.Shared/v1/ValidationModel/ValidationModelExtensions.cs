using FluentValidation.Results;
namespace BlogCore.Shared.v1.ValidationModel
{
    /// <summary>
    /// Ref https://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    /// </summary>
    public static class ValidationModelExtensions
    {
        public static ValidationResultModel ToValidationResultModel(this ValidationResult validationResult)
        {
            return new ValidationResultModel(validationResult);
        }
    }
}
