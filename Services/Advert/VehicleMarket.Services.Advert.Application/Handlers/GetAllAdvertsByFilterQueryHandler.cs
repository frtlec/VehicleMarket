using AutoMapper;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Dtos;
using VehicleMarket.Services.Advert.Application.Helpers.ModelValidators.FluentValidation;
using VehicleMarket.Services.Advert.Application.Queries;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;
using VehicleMarket.Shared.Dtos;

namespace VehicleMarket.Services.Advert.Application.Handlers
{
    public class GetAllAdvertsByFilterQueryHandler : IRequestHandler<GetAllAdvertsByFilterQuery, Response<GetAllAdvertsByFilterQueryResult>>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IMapper _mapper;
        public GetAllAdvertsByFilterQueryHandler(IAdvertRepository advertRepository, IMapper mapper)
        {
            _advertRepository = advertRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetAllAdvertsByFilterQueryResult>> Handle(GetAllAdvertsByFilterQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = new GetAllAdvertsByFilterQueryValidator().Validate(request);
            if (validationResult.IsValid==false)
                return Response<GetAllAdvertsByFilterQueryResult>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList(), HttpStatusCode.BadRequest);


            var adverts = await _advertRepository.GetAllByFilter(new Domain.SeedWork.Dtos.AdvertGetAllByFilterInput
            {
                CategoryId = request.CategoryId,
                Fuel = request.Fuel,
                Gear = request.Gear,
                Skip = request.Skip,
                Take = request.Take,
                BeginPrice = request.BeginPrice,
                EndPrice = request.EndPrice,
                Sort=request.Sort
            });
            if (adverts.Items==null || adverts.Items.Count<1)
            {
                return Response<GetAllAdvertsByFilterQueryResult>.Success();
            }
            GetAllAdvertsByFilterQueryResult advertDto = new GetAllAdvertsByFilterQueryResult()
            {
                Page = (request.Skip + 1) / request.Take + 1,
                Adverts = _mapper.Map<List<GetAllAdvertsByFilterQueryResult.Item>>(adverts.Items),
                Total = adverts.Total,
            };
            return Response<GetAllAdvertsByFilterQueryResult>.Success(advertDto);

        }
    }
}
