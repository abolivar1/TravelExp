using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelExp.Web.Data.Entities;
using TravelExp.Common.Models;

namespace TravelExp.Web.Data
{
    public class DataContext : IdentityDbContext<Employee>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<TripDetail> TripDetails { get; set; }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<TravelExp.Common.Models.CityResponse> CityResponse { get; set; }
    }
}

