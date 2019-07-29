using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.ValidationModel;
using FluentValidation;
using Google.Protobuf;
using System.Threading.Tasks;

namespace BlogCore.Shared.v1.Usecase
{
    /// <summary>
    /// Ref at https://andrewlock.net/adding-decorated-classes-to-the-asp.net-core-di-container-using-scrutor
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidUseCaseDecorator<TRequest, TResponse> : IUseCase<TRequest, TResponse>
        where TRequest : class, IMessage
        where TResponse : class, IMessage
    {
        private readonly IValidator<TRequest> _validator;
        private readonly IUseCase<TRequest, TResponse> _inner;

        public ValidUseCaseDecorator(IValidator<TRequest> validator, IUseCase<TRequest, TResponse> inner)
        {
            _validator = validator.NotNull();
            _inner = inner.NotNull();
        }

        public async Task<TResponse> ExecuteAsync(TRequest request)
        {
            await _validator.HandleValidation(request);
            return await _inner.ExecuteAsync(request);
        }
    }
}
