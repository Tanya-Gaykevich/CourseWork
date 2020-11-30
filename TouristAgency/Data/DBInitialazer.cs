using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Data
{
    public class DBInitializer
    {
        int _refferenceTableSize;
        int _operationalTableSize;
        public DBInitializer(int refferenceTableSize = 100, int operationalTableSize = 10000)
        {
            _refferenceTableSize = refferenceTableSize;
            _operationalTableSize = operationalTableSize;
        }
        public async Task Initialize(TouristAgencyContext dbContext)
        {
            Random rand = new Random();

            if (!dbContext.Positions.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    dbContext.Positions.Add(new Models.Position
                    {
                        Name = GetRandomString(50)
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.Employees.Any())
            {
                var positions = await dbContext.Positions.ToListAsync();

                for (int i = 0; i < _operationalTableSize; i++)
                {
                    var position = positions.ElementAt(rand.Next(dbContext.Positions.Count() - 1));

                    dbContext.Employees.Add(new Models.Employee
                    {
                        LastName = GetRandomString(50),
                        FirstName = GetRandomString(50),
                        MiddleName = GetRandomString(50),
                        BirthDate = GetRandomDate(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1)),
                        PositionId = position.Id,
                        Position = position
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.Clients.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    dbContext.Clients.Add(new Models.Client
                    {
                        LastName = GetRandomString(50),
                        FirstName = GetRandomString(50),
                        MiddleName = GetRandomString(50),
                        BirthDate = GetRandomDate(new DateTime(1950, 1, 1), DateTime.Now),
                        Gender = rand.Next(0, 2) == 1 ? true : false,
                        Address = GetRandomString(50),
                        Phone = rand.Next(1000000000, int.MaxValue),
                        PassportData = GetRandomString(50),
                        Discount = rand.Next(0, 50)

                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.TypesOfRest.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    dbContext.TypesOfRest.Add(new Models.TypeOfRest
                    {
                        Name = GetRandomString(50),
                        Description = GetRandomString(150),
                        Limitation = GetRandomString(100)
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.Services.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    dbContext.Services.Add(new Models.Service
                    {
                        Name = GetRandomString(50),
                        Description = GetRandomString(150),
                        Cost = rand.Next(500, 3000)
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.Hotels.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    dbContext.Hotels.Add(new Models.Hotel
                    {
                        Name = GetRandomString(50),
                        Country = GetRandomString(50),
                        Town = GetRandomString(50),
                        Address = GetRandomString(50),
                        Phone = rand.Next(1000000000, int.MaxValue),
                        Stars = rand.Next(1, 6),
                        ContactPerson = GetRandomString(50),
                        HotelPhotoLink = GetRandomString(100)
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.HotelRoomPhotoLinks.Any())
            {
                var hotels = await dbContext.Hotels.ToListAsync();
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    var hotel = hotels.ElementAt(rand.Next(dbContext.Hotels.Count() - 1));
                    dbContext.HotelRoomPhotoLinks.Add(new Models.HotelRoomPhotoLink
                    {
                        Hotel = hotel,
                        HotelId = hotel.Id,
                        RoomNumber = GetRandomString(10),
                        RoomPhotoLink = GetRandomString(100)
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.Vouchers.Any())
            {
                var hotels = await dbContext.Hotels.ToListAsync();
                var typesOfRest = await dbContext.TypesOfRest.ToListAsync();
                var employees = await dbContext.Employees.ToListAsync();

                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    var hotel = hotels.ElementAt(rand.Next(dbContext.Hotels.Count() - 1));
                    var typeOfRest = typesOfRest.ElementAt(rand.Next(dbContext.TypesOfRest.Count() - 1));
                    var employee = employees.ElementAt(rand.Next(dbContext.Employees.Count() - 1));
                    var startDate = GetRandomDate(new DateTime(2010, 1, 1), DateTime.Now);

                    dbContext.Vouchers.Add(new Models.Voucher
                    {
                        Name = GetRandomString(50),
                        StartDate = startDate,
                        EndDate = startDate.AddDays(rand.Next(3, 21)),

                        Hotel = hotel,
                        HotelId = hotel.Id,
                        TypeOfRest = typeOfRest,
                        TypeOfRestId = typeOfRest.Id,
                        Employee = employee,
                        EmployeeId = employee.Id
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.ClientsVouchers.Any())
            {
                var vouchers = await dbContext.Vouchers.ToListAsync();
                var clients = await dbContext.Clients.ToListAsync();

                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    var voucher = vouchers.ElementAt(rand.Next(dbContext.Vouchers.Count() - 1));
                    var client = clients.ElementAt(rand.Next(dbContext.Clients.Count() - 1));

                    dbContext.ClientsVouchers.Add(new Models.ClientVoucher
                    {
                        ReservationMark = rand.Next(0, 2) == 1,
                        PaymentMark = rand.Next(0, 2) == 1,

                        Voucher = voucher,
                        VoucherId = voucher.Id,
                        Client = client,
                        ClientId = client.Id
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.ServicesVouchers.Any())
            {
                var vouchers = await dbContext.Vouchers.ToListAsync();
                var services = await dbContext.Services.ToListAsync();

                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    var voucher = vouchers.ElementAt(rand.Next(dbContext.Vouchers.Count() - 1));
                    var service = services.ElementAt(rand.Next(dbContext.Services.Count() - 1));

                    dbContext.ServicesVouchers.Add(new Models.ServiceVoucher
                    {
                        Voucher = voucher,
                        VoucherId = voucher.Id,
                        Service = service,
                        ServiceId = service.Id
                    });
                }
            }
            await dbContext.SaveChangesAsync();
        }
        public string GetRandomString(int maxLength)
        {
            Random rand = new Random();
            int length = rand.Next(maxLength / 3, maxLength);
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var str = new char[length];
            for (int i = 0; i < length; i++)
            {
                if ((i + 1) % 10 == 0)
                {
                    str[i] = ' ';
                    continue;
                }
                str[i] = chars[rand.Next(chars.Length)];
            }
            return new string(str);
        }
        public DateTime GetRandomDate(DateTime minDate, DateTime maxDate)
        {
            Random rand = new Random();
            int range = (maxDate - minDate).Days;
            return minDate.AddDays(rand.Next(range));
        }
    }
}
