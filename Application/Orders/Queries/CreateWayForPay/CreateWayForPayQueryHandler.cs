using System.Security.Cryptography;
using System.Text;
using Croptor.Application.Orders.Queries.CreateCallbackResponse;
using Croptor.Application.Orders.Queries.CreateRequest;
using MediatR;

namespace Croptor.Application.Orders.Queries.CreateWayForPay;

public class CreateWayForPayQueryHandler : 
    IRequestHandler<CreateRequestQuery, WayForPayRequest>,
    IRequestHandler<CreateCallbackResponseQuery, WayForPayCallbackResponse>
{
    public async Task<WayForPayRequest> Handle(CreateRequestQuery request, CancellationToken cancellationToken)
    {
        var wfpr = new WayForPayRequest(request.Order, request.Account);
        
        wfpr.MerchantSignature = HashParams([
            wfpr.MerchantAccount,
            wfpr.MerchantDomainName,
            wfpr.OrderReference,
            wfpr.OrderDate.ToString(),
            wfpr.Amount.ToString(),
            wfpr.Currency,
            wfpr.ProductName,
            wfpr.ProductCount.ToString(),
            wfpr.ProductPrice.ToString()
        ], request.SecretKey);
        
        return wfpr;
    }

    public async Task<WayForPayCallbackResponse> Handle(CreateCallbackResponseQuery request,
        CancellationToken cancellationToken)
    {
        var wfpcr = new WayForPayCallbackResponse(request.Order);
        
        wfpcr.Signature = HashParams([
            wfpcr.OrderReference.ToString(),
            wfpcr.Status,
            wfpcr.Time.ToString()
        ], request.SecretKey);
        
        return wfpcr;
    }

    private string HashParams(List<string> data, string keyString)
    {
        var source = Encoding.UTF8.GetBytes(String.Join(";", data));
        var key = Encoding.UTF8.GetBytes(keyString);
        return BitConverter.ToString(HMACMD5.HashData(key, source)).Replace("-", "").ToLower();
    }
    
    // var request = new WayForPayRequest
    // {
    //     MerchantAccount = "www_croptor_com",
    //     MerchantDomainName = "www.market.ua",
    //     OrderReference = "DH7830238",
    //     OrderDate = 1415379863,
    //     Amount = 1000 * dto.Amount,
    //     Currency = "UAH",
    //     ProductName = "ProPlan",
    //     ProductCount = dto.Amount,
    //     ProductPrice = 1000,
    //     ClientAccountId = ""
    // };


    // var source = Encoding.UTF8.GetBytes(String.Join(";",
    //     [
    //         "www_croptor_com",
    //         "www.market.ua",
    //         "DH7830236",
    //         "1415379863",
    //         "1547",
    //         "UAH",
    //         "ProPlan",
    //         "1",
    //         "1000"
    //     ]
    // ));
}