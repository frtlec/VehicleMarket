using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Queries;

namespace VehicleMarket.Services.Advert.Application.Helpers.ModelValidators.FluentValidation
{
    public class GetAllAdvertsByFilterQueryValidator : AbstractValidator<GetAllAdvertsByFilterQuery>
    {
        public GetAllAdvertsByFilterQueryValidator()
        {
            RuleFor(f => f.Take).NotEqual(0).WithMessage("Not equal 0").Must(f=>f<1000).WithMessage("Max 1000");
        }
    }
}
