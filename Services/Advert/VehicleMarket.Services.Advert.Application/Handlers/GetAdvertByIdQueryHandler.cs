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
    public class GetAdvertByIdQueryHandler : IRequestHandler<GetAdvertByIdQuery, Response<GetAdvertByIdQueryResult>>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IMapper _mapper;
        public GetAdvertByIdQueryHandler(IAdvertRepository advertRepository, IMapper mapper)
        {
            _advertRepository = advertRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetAdvertByIdQueryResult>> Handle(GetAdvertByIdQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = new GetAdvertByIdQueryValidator().Validate(request);
            if (validationResult.IsValid == false)
                return Response<GetAdvertByIdQueryResult>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

            AdvertModel result =await _advertRepository.GetById(request.Id);
            if (result==null)
                return Response<GetAdvertByIdQueryResult>.Success();


            GetAdvertByIdQueryResult mapped = _mapper.Map<GetAdvertByIdQueryResult>(result);
            return Response<GetAdvertByIdQueryResult>.Success(mapped);
        }
    }
}
