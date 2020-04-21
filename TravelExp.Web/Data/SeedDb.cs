using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Web.Data.Entities;
using TravelExp.Web.Helpers;

namespace TravelExp.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); 
            await CheckRoles();
            await CheckUserAsync("123456789","Alex Jhojan", "Bolivar Vargas",
                "alex.jhojanx@gmail.com", "Admin", "Cra 48 # 44-67", $"~/images/Users/Alex.png");
            await CheckUserAsync("123456789", "Miguel", "Saavedra",
                "alexbolivar240835@correo.itm.edu.co", "Employee", "Cra 48 # 44-67", $"~/images/Users/Miguel.png");
            await CheckCountriesAsync();
            await CheckExpenseTypesAsync();
            await CheckTripsAsync();
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Employee");

        }
      
        private async Task CheckUserAsync(string document,
            string firstName,string lastName, string email, string role, string address, string path)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new Employee
                {
                    Document = document,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    Address = address,
                    PicturePath = path,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }
            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);
        }
        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "Medellin"
                        },
                        new City
                        {
                            Name = "Bogota"
                        },
                        new City
                        {
                            Name = "Cartagena"
                        }
                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "Buenos Aires"
                        }
                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "New York"
                        },
                        new City
                        {
                            Name = "Las Vegas"
                        },
                        new City
                        {
                            Name = "Miami"
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckExpenseTypesAsync()
        {
            if (!_context.ExpenseTypes.Any())
            {
                AddExpenseType("Alimentacion");
                AddExpenseType("Transporte");
                AddExpenseType("Hospedaje");
                await _context.SaveChangesAsync();
            }
        }

        private void AddExpenseType(string name)
        {
            _context.ExpenseTypes.Add(new ExpenseType { Name = name});
        }

        private async Task CheckTripsAsync()
        {
            if (!_context.Trips.Any())
            {
                var startDate = DateTime.Today.AddMonths(2).ToUniversalTime();
                var endDate = DateTime.Today.AddMonths(2).AddDays(4).ToUniversalTime();
                _context.Trips.Add(new Trip
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Employee = _context.Users.FirstOrDefault(u => u.FirstName.Equals("Miguel")),
                    City = _context.Cities.FirstOrDefault(c => c.Name.Equals("Bogota")),
                    TotalAmount = 60000,
                    Description = "Viaje de negocios",
                TripDetails = new List<TripDetail>
                    {
                        new TripDetail
                        {
                             Date = DateTime.Today.AddMonths(2).ToUniversalTime(),
                             Amount = 50000,
                             Description = "Comida en el retuarante del hotel",
                             PicturePath = $"~/images/TripDetails/Factura1.png",
                             ExpenseType = _context.ExpenseTypes.FirstOrDefault(et => et.Name.Equals("Alimentacion"))
                        },

                        new TripDetail
                        {
                             Date = DateTime.Today.AddMonths(2).AddDays(1).ToUniversalTime(),
                             Amount = 10000,
                             Description = "Transportes por la cidudad",
                             PicturePath = $"~/images/TripDetails/Factura4.png",
                             ExpenseType = _context.ExpenseTypes.FirstOrDefault(et => et.Name.Equals("Transporte"))
                        },
                    }
                });

                startDate = DateTime.Today.AddMonths(1).ToUniversalTime();
                endDate = DateTime.Today.AddMonths(1).AddDays(5).ToUniversalTime();

                _context.Trips.Add(new Trip
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Employee = _context.Users.FirstOrDefault(u => u.FirstName.Equals("Miguel")),
                    City = _context.Cities.FirstOrDefault(c => c.Name.Equals("Miami")),
                    TotalAmount = 530000,
                    Description = "Viaje de negocios",
                    TripDetails = new List<TripDetail>
                    {
                        new TripDetail
                        {
                             Date = DateTime.Today.AddMonths(1).ToUniversalTime(),
                             Amount = 500000,
                             Description = "Transporte en avion",
                             PicturePath = $"~/images/TripDetails/Factura3.png",
                             ExpenseType = _context.ExpenseTypes.FirstOrDefault(et => et.Name.Equals("Transporte"))
                        },

                        new TripDetail
                        {
                             Date = DateTime.Today.AddMonths(1).AddDays(1).ToUniversalTime(),
                             Amount = 20000,
                             Description = "Comida",
                             PicturePath = $"~/images/TripDetails/Factura2.png",
                             ExpenseType = _context.ExpenseTypes.FirstOrDefault(et => et.Name.Equals("Alimentacion"))
                        },
                        new TripDetail
                        {
                             Date = DateTime.Today.AddMonths(1).AddDays(1).ToUniversalTime(),
                             Amount = 10000,
                             Description = "Tranporte publico",
                             PicturePath = $"~/images/TripDetails/Factura5.png",
                             ExpenseType = _context.ExpenseTypes.FirstOrDefault(et => et.Name.Equals("Transporte"))
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

    }

}
