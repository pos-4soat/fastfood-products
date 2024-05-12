using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Enum;
using fastfood_products.Testes.UnitTests;
using MediatR;
using Moq;

namespace fastfood_products.Testes.Mocks;

public class MediatorMock<TRequest, TResponse>(TestFixture testFixture) : BaseCustomMock<IMediator>(testFixture) where TRequest : notnull
{
    public void SetupSendAsync(TResponse response, StatusResponse statusResponse)
        => Setup(x => x.Send(It.IsAny<TRequest>(), default))
            .ReturnsAsync(Result<TResponse>.Success(response, statusResponse));
}

