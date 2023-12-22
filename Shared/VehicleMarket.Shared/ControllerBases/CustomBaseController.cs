using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Shared.Dtos;

namespace VehicleMarket.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            if (response.IsSuccessful == false)
            {
                return new ObjectResult(response.Errors)
                {
                    StatusCode = (int)response.StatusCode
                };
            }
            return new ObjectResult(response.Data)
            {
                StatusCode = (int)response.StatusCode
            };

        }
    }
}
