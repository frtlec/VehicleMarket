using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Queries;
using VehicleMarket.Services.Advert.Domain.SeedWork.Dtos;

namespace VehicleMarket.Services.Advert.Application.Helpers.ModelValidators.FluentValidation
{
    public class GetAllAdvertsByFilterQueryValidator : AbstractValidator<GetAllAdvertsByFilterQuery>
    {
        public readonly string[] AllowedSortFields_GetAllByFilter = { "price","year","km" };
        public GetAllAdvertsByFilterQueryValidator()
        {
            RuleFor(f => f.Take).NotEqual(0).WithMessage("Take field cannot be 0").Must(f=>f<1000).WithMessage("Maximum allowed value 1000");
            RuleForEach(f=>f.Sort).Must(CheckSortField).WithMessage($"Invalid Sort field, allowed {string.Join(',',AllowedSortFields_GetAllByFilter)}");
        }
        private bool CheckSortField(SortModel field)
        {
            return AllowedSortFields_GetAllByFilter.Any(f => f == field.ColumnName.ToLower(new System.Globalization.CultureInfo("en-EN")));
        }
    }
}
