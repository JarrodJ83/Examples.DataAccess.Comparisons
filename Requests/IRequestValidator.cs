namespace Requests
{
    public interface IRequestValidator<in TModel>
    {
        RequestValidationResult Validate(TModel model);
    }
}
