using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Filiations.GetFiliation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Sale, GetSaleResult>()
            .ConstructUsing(dest => new GetSaleResult()
            {
                Number = dest.Number,
                Date = dest.Date,
                TotalSalesAmount = dest.TotalSalesAmount,
                Branch = dest.Branch,
                Status = dest.Status,
            })
            .ForMember(dest => dest.Customer,
            opt => opt.MapFrom(x => new GetCustomerResult()
            {
                Id = x.Customer.Id.ToString(),
                CPF_CNPJ = x.Customer.CPF_CNPJ,
                Nome = x.Customer.Nome,
            }))
            .ForMember(dest => dest.Products,
                opt => opt.MapFrom(x => x.Products.Select(y => new GetProductResult()
                {
                    Descriptions = y.Descriptions,
                    Discounts = y.Discounts,
                    Price = y.Price,
                    Quantity = y.Quantity,
                    TotalAmount = y.TotalAmount
                })
            ))
            .ForMember(dest => dest.Filiation,
            opt => opt.MapFrom(x => new GetFiliationResult()
            {
                Id = x.Filiation.Id,
                Codigo = x.Filiation.Codigo,
                Nome = x.Filiation.Nome,
            }
            ));
    }
}
