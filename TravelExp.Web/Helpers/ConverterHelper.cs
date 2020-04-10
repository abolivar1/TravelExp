using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Web.Data;
using TravelExp.Web.Data.Entities;
using TravelExp.Web.Models;

namespace TravelExp.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;

        public ConverterHelper(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<City> ToCityAsync(CityViewModel model, bool isNew)
        {
            var city = new City
            {
                Name = model.Name,
                Country = await _dataContext.Countries.FindAsync(model.CountryId),
                Id = isNew ? 0 : model.Id
            };

            return city;
        }

        public CityViewModel ToCityViewModel(City city)
        {
            return new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country,
                CountryId = city.Country.Id
            };
        }
        
    }
}
