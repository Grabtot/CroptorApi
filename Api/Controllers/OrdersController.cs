using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using Croptor.Api.ViewModels.Order;
using Croptor.Application.Orders.Commands;
using Croptor.Application.Orders.Commands.ApproveOrder;
using Croptor.Application.Orders.Queries;
using Croptor.Application.Orders.Queries.CreateCallbackResponse;
using Croptor.Application.Orders.Queries.CreateRequest;
using Croptor.Application.Orders.Queries.CreateWayForPay;
using Croptor.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Croptor.Api.Controllers;

[Route("orders")]
[ApiController]
public class OrdersController(IMediator mediator, IConfiguration configuration) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<WayForPayRequest>> Create(CreateOrderCommand dto)
    {
        var keyString = configuration["WayForPay:Key"];
        if (keyString == null) return BadRequest("SecretKey is null");
        var account = configuration["WayForPay:MerchantLogin"];
        if (account == null) return BadRequest("MerchantLogin is null");

        Order order = await mediator.Send(dto);

        var request = await mediator.Send(new CreateRequestQuery(order, account, keyString));

        return Ok(request);
    }

    [HttpPost("callback")]
    public async Task<ActionResult<WayForPayCallbackResponse>> Callback(WayForPayCallback callback)
    {
        if (callback.TransactionStatus == "Approved")
        {
            var keyString = configuration["WayForPay:Key"];
            if (keyString == null) return BadRequest("SecretKey is null");
            var account = configuration["WayForPay:MerchantLogin"];
            if (account == null) return BadRequest("MerchantLogin is null");

            Order order = await mediator.Send(new GetOrderQuery(callback.OrderReference));

            var request = await mediator.Send(new CreateRequestQuery(order, account, keyString));

            if (request.MerchantSignature == callback.MerchantSignature)
            {
                var response = await mediator.Send(new CreateCallbackResponseQuery(order,keyString));
                await mediator.Send(new ApproveOrderCommand(order));
                return Ok(response);
            }
            return BadRequest("Signatures aren't the same");
        }
        return BadRequest("TransactionStatus must equal \"Approved\"");
    }
}