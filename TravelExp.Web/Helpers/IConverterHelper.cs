using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Web.Data.Entities;
using TravelExp.Web.Models;

namespace TravelExp.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<City> ToCityAsync(CityViewModel model, bool isNew);

        CityViewModel ToCityViewModel(City city);
    }
}
