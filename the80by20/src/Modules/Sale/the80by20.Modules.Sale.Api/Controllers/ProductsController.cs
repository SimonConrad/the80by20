using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the80by20.Modules.Sale.App.Events;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Modules.Sale.Api.Controllers;

internal class ProductsController : BaseController
{
    private readonly IMessageBroker _messageBroker;

    public ProductsController(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    [HttpPost("CreateProductMocked/{solutionId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult> CreateProductMocked(Guid solutionId)
    {
        // todo
        // move to commandhandler

        await _messageBroker.PublishAsync(new ProductCreated(Guid.NewGuid(), Guid.NewGuid()));


        return Ok();
    }
}