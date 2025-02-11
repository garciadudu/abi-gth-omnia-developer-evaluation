using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Filiations.CreateFiliation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, CreateSaleDTO>()
            .ForAllMembers(x => x.Ignore());

        CreateMap<CreateSaleDTO, Sale>()
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(x => string.IsNullOrEmpty(x.Branch) ? "" : x.Branch))
            .ForMember(dest => dest.Customer,
                opt => opt.MapFrom(x => new Customer()
                {
                    Id = x.Customer.Id,
                }))
            .ForMember(dest => dest.Products,
                opt => opt.MapFrom(x => x.Products.Select(y => new Product()
                {
                    Descriptions = y.Descriptions,
                    Discounts = y.Discounts,
                    Price = y.Price,
                    Quantity = y.Quantity,
                    TotalAmount = y.TotalAmount
                })
            ));

        CreateMap<CreateProductCommand, CreateProductDTO>()
            .ConstructUsing(dest => new CreateProductDTO()
            {
                Id = new Guid(dest.Id),
                Descriptions = dest.Descriptions,
                Quantity = dest.Quantity,
                Price = dest.Price,
                Discounts = dest.Discounts,
                TotalAmount = dest.TotalAmount,
            });

        CreateMap<CreateProductDTO, Product>()
            .ConstructUsing(dest => new Product()
            {
                Descriptions = dest.Descriptions,
                Quantity = dest.Quantity,
                Price = dest.Price,
                Discounts = dest.Discounts,
                TotalAmount = dest.TotalAmount,
            });


        CreateMap<CreateSaleCommand, Sale>()
            .ConstructUsing(dest => new Sale()
            {
                Number = dest.Number,
                Date = dest.Date,
                TotalSalesAmount = dest.TotalSalesAmount,
                Branch = dest.Branch,
                Status = dest.Status,
            })
            .ForMember(dest => dest.Customer,
            opt => opt.MapFrom(x => new Customer()
            {
                Id = new Guid(x.Customer.Id),
                CPF_CNPJ = x.Customer.CPF_CNPJ,
                Nome = x.Customer.Nome,
            }))
            .ForMember(dest => dest.Products,
                opt => opt.MapFrom(x => x.Products.Select(y => new Product()
                {
                    Descriptions = y.Descriptions,
                    Discounts = y.Discounts,
                    Price = y.Price,
                    Quantity = y.Quantity,
                    TotalAmount = y.TotalAmount
                })
            ))
            .ForMember(dest => dest.Filiation,
            opt => opt.MapFrom(x => new Filiation()
            {
                Id = new Guid(x.Filiation.Id),
                Codigo = x.Filiation.Codigo,
                Nome = x.Filiation.Nome,
            }
            ));

        CreateMap<Sale, CreateSaleResult>()
            .ConstructUsing(dest => new CreateSaleResult()
            {
                Number = dest.Number,
                Date = dest.Date,
                TotalSalesAmount = dest.TotalSalesAmount,
                Branch = dest.Branch,
                Status = dest.Status,
            })
            .ForMember(dest => dest.Customer,
            opt => opt.MapFrom(x => new CreateCustomerResult()
            {
                Id = x.Customer.Id,
                CPF_CNPJ = x.Customer.CPF_CNPJ,
                Nome = x.Customer.Nome,
            }))
            .ForMember(dest => dest.Products,
                opt => opt.MapFrom(x => x.Products.Select(y => new Product()
                {
                    Descriptions = y.Descriptions,
                    Discounts = y.Discounts,
                    Price = y.Price,
                    Quantity = y.Quantity,
                    TotalAmount = y.TotalAmount
                })
            ))
            .ForMember(dest => dest.Filiation,
            opt => opt.MapFrom(x => new Filiation()
            {
                Id = x.Filiation.Id,
                Codigo = x.Filiation.Codigo,
                Nome = x.Filiation.Nome,
            }
            ));
    }
}
