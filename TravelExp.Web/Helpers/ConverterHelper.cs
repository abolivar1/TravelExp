using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Common.Models;
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

        /*public TournamentViewModel ToTournamentViewModel(TournamentEntity tournamentEntity)
        {
            return new TournamentViewModel
            {
                EndDate = tournamentEntity.EndDate,
                Groups = tournamentEntity.Groups,
                Id = tournamentEntity.Id,
                IsActive = tournamentEntity.IsActive,
                LogoPath = tournamentEntity.LogoPath,
                Name = tournamentEntity.Name,
                StartDate = tournamentEntity.StartDate
            };
        }*/

        public TripResponse ToTripResponse(Trip tripEntities)
        {
            return new TripResponse
            {
                Id = tripEntities.Id,
                StartDate = tripEntities.StartDate,
                EndDate = tripEntities.EndDate,
                TotalAmount = tripEntities.TotalAmount,
                Description = tripEntities.Description,
                Employee = new EmployeeResponse
                {
                    Id = tripEntities.Employee.Id,
                    Email = tripEntities.Employee.Email,
                    PhoneNumber = tripEntities.Employee.PhoneNumber,
                    Document = tripEntities.Employee.Document,
                    FirstName = tripEntities.Employee.FirstName,
                    LastName = tripEntities.Employee.LastName,
                    Address = tripEntities.Employee.Address,
                    PicturePath = tripEntities.Employee.PicturePath,
                    UserType = tripEntities.Employee.UserType
                },
                City = new CityResponse
                {
                    Id = tripEntities.City.Id,
                    Name = tripEntities.City.Name,
                    Country = new CountryResponse
                    {
                        Id = tripEntities.City.Country.Id,
                        Name = tripEntities.City.Country.Name
                    }
                },
                TripDetails = tripEntities.TripDetails.Select(g => new TripDetailResponse
                {
                    Id = g.Id,
                    Date = g.Date,
                    Amount = g.Amount,
                    Description = g.Description,
                    PicturePath = g.PicturePath,
                    ExpenseType = new ExpenseTypeResponse
                    {
                        Id = g.ExpenseType.Id,
                        Name = g.ExpenseType.Name
                    }
                }).ToList()
            };
        }

        public List<TripResponse> ToTripResponse(List<Trip> tripEntities)
        {
            List<TripResponse> list = new List<TripResponse>();
            foreach (Trip tripEntity in tripEntities)
            {
                list.Add(ToTripResponse(tripEntity));
            }

            return list;
        }

        public EmployeeResponse ToUserResponse(Employee user)
        {
            if (user == null)
            {
                return null;
            }

            return new EmployeeResponse
            {
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType,
                Trips = ToTripResponse(user.Trips.ToList())
            };
        }

       /* private TeamResponse ToTeamResponse(TeamEntity team)
        {
            if (team == null)
            {
                return null;
            }

            return new TeamResponse
            {
                Id = team.Id,
                LogoPath = team.LogoPath,
                Name = team.Name
            };
        }*/

    }
}
