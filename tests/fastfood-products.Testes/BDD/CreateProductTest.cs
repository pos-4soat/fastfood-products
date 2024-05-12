using fastfood_products.API.Controllers;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.CreateProduct;
using fastfood_products.Domain.Enum;
using fastfood_products.Testes.UnitTests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechTalk.SpecFlow;

namespace fastfood_products.Testes.BDD;

[TestFixture]
public class CreateProductTest : TestFixture
{
    private Mock<IMediator> _mediatorMock;
    private CreateProductRequest _request;
    private IActionResult _result;

    [Test, Description("")]
    public async Task RegisterANewProduct()
    {
        GivenIHaveAValidCreateProductRequest();
        GivenTheRepositoryReturnsASuccessfulResult();
        await WhenIRequestAProductCreation();
        ThenTheResultShouldBeACreatedResult();
    }

    [Given(@"I have a valid create product request")]
    public void GivenIHaveAValidCreateProductRequest()
    {
        _request = new CreateProductRequest(
                "X-Calabresa",
                CategoryType.Burguer,
                10.99m,
                "Hamburguer de calabresa artesanal",
                "imgSrc"
            );
    }

    [Given(@"the repository returns a successful result")]
    public void GivenTheRepositoryReturnsASuccessfulResult()
    {
        _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateProductRequest>(), default))
            .ReturnsAsync(Result<CreateProductResponse>.Success(new CreateProductResponse()
            {
                Name = "X-Calabresa",
                Description = "Hamburguer de calabresa artesanal",
                Price = 10.99m,
                ProductImageUrl = "imgSrc",
                Type = CategoryType.Burguer
            }, StatusResponse.CREATED));
    }

    [When(@"I request a product creation")]
    public async Task WhenIRequestAProductCreation()
    {
        ProductController controller = new ProductController(_mediatorMock.Object);

        _result = await controller.CreateProductAsync(_request, default);
    }

    [Then(@"the result should be a CreatedResult")]
    public void ThenTheResultShouldBeACreatedResult()
    {
        ObjectResult? objectResult = _result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

        Response<object>? response = objectResult.Value as Response<object>;
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Status, Is.EqualTo(nameof(StatusResponse.CREATED)));

        CreateProductResponse? body = response.Body as CreateProductResponse;
        Assert.That(body.Name, Is.EqualTo("X-Calabresa"));
        Assert.That(body.Description, Is.EqualTo("Hamburguer de calabresa artesanal"));
        Assert.That(body.Type, Is.EqualTo(CategoryType.Burguer));
        Assert.That(body.Price, Is.EqualTo(10.99m));
    }
}
