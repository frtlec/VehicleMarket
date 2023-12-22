using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Queries;

namespace VehicleMarket.Services.Advert.Application.Helpers.ModelValidators.FluentValidation
{
    public class GetAdvertByIdQueryValidator : AbstractValidator<GetAdvertByIdQuery>
    {
        public GetAdvertByIdQueryValidator()
        {
            RuleFor(f => f.Id).NotEqual(0).WithMessage("Take field cannot be 0");
        }
    }
}
