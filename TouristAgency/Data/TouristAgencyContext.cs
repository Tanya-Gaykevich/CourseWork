using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TouristAgency.Data
{
    public partial class TouristAgencyContext : DbContext
    {
        public TouristAgencyContext()
        {
        }

        public TouristAgencyContext(DbContextOptions<TouristAgencyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.ClientVoucher> ClientsVouchers { get; set; }
        public DbSet<Models.Voucher> Vouchers { get; set; }
        public DbSet<Models.ServiceVoucher> ServicesVouchers { get; set; }
        public DbSet<Models.Service> Services { get; set; }
        public DbSet<Models.TypeOfRest> TypesOfRest { get; set; }
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Position> Positions { get; set; }
        public DbSet<Models.Hotel> Hotels { get; set; }
        public DbSet<Models.HotelRoomPhotoLink> HotelRoomPhotoLinks { get; set; }
    }
}