using FlightService.Domain.Entities;
using FlightService.Domain.Enums;
using FlightService.Domain.ValueObjects;
using FlightService.Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlightService.Infrastructure.Extensions
{
    public static class DatabaseExtentions
    {
        public static async void InitializeDatabase(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            //await SeedAsync(dbContext);
        }

        private static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Airports.AnyAsync() ||
                await context.PlaneServices.AnyAsync() ||
                await context.Seats.AnyAsync() ||
                await context.Planes.AnyAsync() ||
                await context.Flights.AnyAsync())
            {
                return;
            }

            Airport airport1 = new(
                new Guid("c26bc687-af30-4392-9c0d-50ce861ae194"),
                "John F. Kennedy International Airport",
                "United States",
                "New York")
            { };

            Airport airport2 = new(
                new Guid("c839d7a4-7b1a-4adf-aabb-9643f21e8f4d"),
                "Los Angeles International Airport",
                "United States",
                "Los Angeles")
            { };

            Airport airport3 = new(
                new Guid("f8612dc8-0cb7-427f-9c71-9b959beca793"),
                "Heathrow Airport",
                "United Kingdom",
                "London")
            { };

            Airport airport4 = new(
                new Guid("3d84d194-3b7d-4dab-9275-df5f7f55ab67"),
                "Charles de Gaulle Airport",
                "France",
                "Paris")
            { };

            Airport airport5 = new(
                new Guid("9e308f59-3c65-412a-9977-14ab8961c1ea"),
                "Tokyo Haneda Airport",
                "Japan",
                "Tokyo")
            { };

            await context.Airports.AddRangeAsync([airport1, airport2, airport3, airport4, airport5]);

            PlaneService planeService1 = new(
                new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),
                "Wifi",
                "High-speed Wi-Fi")
            { };

            PlaneService planeService2 = new(
                new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),
                "USB",
                "USB power")
            { };

            PlaneService planeService3 = new(
                new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),
                "AC",
                "AC power")
            { };

            PlaneService planeService4 = new(
                new Guid("25aed40d-7a45-4485-82de-36877dc3c8d2"),
                "PersonalStreaming",
                "Personal device streaming")
            { };

            PlaneService planeService5 = new(
                new Guid("19207855-4db6-4954-acb4-cabe1d0308ab"),
                "LiveTV",
                "Live TV")
            { };

            PlaneService planeService6 = new(
                new Guid("25965efd-5a60-485f-8d51-5fc985d41962"),
                "AppleMusic",
                "Apple Music")
            { };

            PlaneService planeService7 = new(
                new Guid("53258871-4d12-42f9-ac49-b9d277e39061"),
                "SeatbackEntertainment",
                "Seatback entertainment")
            { };

            PlaneService planeService8 = new(
                new Guid("2f3dfcdf-2215-4e78-b8b4-ef95e15fe646"),
                "LieFlatSeats",
                "Lie-flat seats in Business and First only")
            { };

            await context.PlaneServices.AddRangeAsync([
                planeService1,
                planeService2,
                planeService3,
                planeService4,
                planeService5,
                planeService6,
                planeService7,
                planeService8]);

            Seat seat1 = new(new Guid("582db6d2-0735-4a31-b400-9ab12a01b4be"), SeatClass.Premium, "1A");
            Seat seat2 = new(new Guid("a7892205-a92b-48c5-9d7d-d69a647380af"), SeatClass.Premium, "2A");
            Seat seat3 = new(new Guid("6c83b899-9867-49fe-91b4-e6b1bcb9e026"), SeatClass.Premium, "3A");
            Seat seat4 = new(new Guid("11d8c1c3-3536-469f-a082-41f908b4c449"), SeatClass.PremiumEconomy, "11A");
            Seat seat5 = new(new Guid("56357f00-1a0e-4bee-9ff4-dc5b42a9d709"), SeatClass.PremiumEconomy, "12A");
            Seat seat6 = new(new Guid("388d6bb8-1b71-4500-a60a-cc597e902a35"), SeatClass.PremiumEconomy, "13A");
            Seat seat7 = new(new Guid("dd1473b6-4e18-4949-a391-bdee85756f5a"), SeatClass.Main, "21A");
            Seat seat8 = new(new Guid("e92a8706-9cd9-4e88-94b7-c83a7dd8115a"), SeatClass.Main, "22A");
            Seat seat9 = new(new Guid("55704630-1c0b-4b64-9c59-2fed3cfff14d"), SeatClass.Main, "23A");

            await context.Seats.AddRangeAsync([seat1, seat2, seat3, seat4, seat5, seat6, seat7, seat8, seat9]);

            Plane plane1 = new(
                new Guid("210b0ad7-5d20-4341-b696-97fe50ac9e22"),
                 "AB123",
                 "Boeing 737",
                 [planeService1, planeService2, planeService3, planeService4],
                 [seat1, seat4, seat7])
            { };

            Plane plane2 = new(
                new Guid("fe768d83-cfbc-417b-83c2-667ebf62d290"),
                "CD456",
                "Airbus A320",
                   [planeService1, planeService2, planeService3, planeService5, planeService6],
                   [seat2, seat5, seat8])
            { };

            Plane plane3 = new(
                new Guid("0a912ecc-e7ce-436b-a026-ad4ef1538e4f"),
                "EF789",
                "Boeing 777",
                [planeService1, planeService2, planeService3, planeService7, planeService8],
                [seat3, seat6, seat9])
            { };

            await context.Planes.AddRangeAsync([plane1, plane2, plane3]);

            Flight flight1 = Flight.Create(
                new Guid("aafd2a3c-acdb-43e5-a5f6-1956ca245bb6"),
                FlightNumber.Create("1234").Value,
                plane1,
                DateTime.UtcNow.AddHours(2),
                airport1,
                DateTime.UtcNow.AddHours(4),
                airport2
                ).Value;

            Flight flight2 = Flight.Create(
                new Guid("bfc9172e-7192-4f10-9417-f5efde560cb0"),
                FlightNumber.Create("5678").Value,
                plane2,
                DateTime.UtcNow.AddHours(3),
                airport1,
                DateTime.UtcNow.AddHours(8),
                airport3
                ).Value;

            var flight3 = Flight.Create(
                  new Guid("ff58c1a0-aad7-4eec-b001-dc1ec52d8abc"),
                  FlightNumber.Create("9876").Value,
                  plane3,
                  DateTime.UtcNow.AddHours(1),
                  airport4,
                  DateTime.UtcNow.AddHours(12),
                  airport5).Value;

            await context.Flights.AddRangeAsync([flight1, flight2, flight3]);

            await context.SaveChangesAsync();
        }
    }
}
