using FlightService.Domain.Entities;
using FlightService.Domain.Enums;
using FlightService.Domain.ValueObjects;

namespace FlightService.Infrastructure.Extensions
{
    internal static class InitialData
    {
        public static IEnumerable<Airport> Airports =>
            [
            new(new Guid("c26bc687-af30-4392-9c0d-50ce861ae194"), "John F. Kennedy International Airport",  "United States", "New York"){ },
            new(new Guid("c839d7a4-7b1a-4adf-aabb-9643f21e8f4d"), "Los Angeles International Airport",  "United States", "Los Angeles"){ },
            new(new Guid("f8612dc8-0cb7-427f-9c71-9b959beca793"), "Heathrow Airport",  "United Kingdom", "London"){ },
            new(new Guid("3d84d194-3b7d-4dab-9275-df5f7f55ab67"), "Charles de Gaulle Airport",  "France", "Paris"){ },
            new(new Guid("9e308f59-3c65-412a-9977-14ab8961c1ea"), "Tokyo Haneda Airport",  "Japan", "Tokyo"){ },
            ];

        public static IEnumerable<Plane> Planes =>
            [
            new(
                new Guid("210b0ad7-5d20-4341-b696-97fe50ac9e22"),
                "AB123",
                "Boeing 737",
                [
                    new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"), "Wifi", "High-speed Wi-Fi" ){ },
                    new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"), "USB", "USB power" ){ },
                    new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"), "AC", "AC power" ){ },
                    new (new Guid("25aed40d-7a45-4485-82de-36877dc3c8d2"), "PersonalStreaming", "Personal device streaming" ){ },
                ],
                [
                    new(new Guid("582db6d2-0735-4a31-b400-9ab12a01b4be"), SeatClass.Premium, "1A" ),
                    new(new Guid("11d8c1c3-3536-469f-a082-41f908b4c449"), SeatClass.PremiumEconomy, "11A" ),
                    new(new Guid("dd1473b6-4e18-4949-a391-bdee85756f5a"), SeatClass.Main, "21A" ),
                ]
                ){ },

            new(
                new Guid("fe768d83-cfbc-417b-83c2-667ebf62d290"),
                "CD456",
                "Airbus A320",
                [
                    new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),"Wifi",  "High-speed Wi-Fi" ){ },
                    new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),"USB",  "USB power" ){ },
                    new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),"AC",  "AC power" ){ },
                    new (new Guid("19207855-4db6-4954-acb4-cabe1d0308ab"),"LiveTV",  "Live TV" ){ },
                    new (new Guid("25965efd-5a60-485f-8d51-5fc985d41962"),"AppleMusic",  "Apple Music" ){ },
                ],
                [
                    new(new Guid("a7892205-a92b-48c5-9d7d-d69a647380af"), SeatClass.Premium, "2A" ),
                    new(new Guid("56357f00-1a0e-4bee-9ff4-dc5b42a9d709"), SeatClass.PremiumEconomy, "12A" ),
                    new(new Guid("e92a8706-9cd9-4e88-94b7-c83a7dd8115a"), SeatClass.Main, "22A" ),
                ]
                ){ },

            new(
                new Guid("0a912ecc-e7ce-436b-a026-ad4ef1538e4f"),
                "EF789",
                "Boeing 777",
                [
                    new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),"Wifi",  "High-speed Wi-Fi" ){ },
                    new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),"USB",  "USB power" ){ },
                    new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),"AC",  "AC power" ){ },
                    new (new Guid("53258871-4d12-42f9-ac49-b9d277e39061"),"SeatbackEntertainment",  "Seatback entertainment" ){ },
                    new (new Guid("2f3dfcdf-2215-4e78-b8b4-ef95e15fe646"),"LieFlatSeats",  "Lie-flat seats in Business and First only" ){ },
                ],
                [
                    new(new Guid("6c83b899-9867-49fe-91b4-e6b1bcb9e026"), SeatClass.Premium, "3A" ),
                    new(new Guid("388d6bb8-1b71-4500-a60a-cc597e902a35"), SeatClass.PremiumEconomy, "13A" ),
                    new(new Guid("55704630-1c0b-4b64-9c59-2fed3cfff14d"), SeatClass.Main, "23A" ),
                ]
                ){ },

            new(
                new Guid("31801f3f-7549-4e94-bf22-3ea6b228302d"),
                "GH012",
                "Airbus A380",
                [
                    new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),"Wifi",  "High-speed Wi-Fi" ){ },
                    new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),"USB",  "USB power" ){ },
                    new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),"AC",  "AC power" ){ },
                    new (new Guid("25aed40d-7a45-4485-82de-36877dc3c8d2"),"PersonalStreaming",  "Personal device streaming" ){ },
                    new (new Guid("19207855-4db6-4954-acb4-cabe1d0308ab"),"LiveTV",  "Live TV" ){ },
                ],
                [
                    new(new Guid("ec588a9b-4c23-4f99-a4a9-28e6e61c724d"), SeatClass.Premium, "4A" ),
                    new(new Guid("55e2087f-c013-4a9b-aa4e-7318f556829e"), SeatClass.PremiumEconomy, "14A" ),
                    new(new Guid("fcc9fdd9-d817-4089-a535-6b1836463ce5"), SeatClass.Main, "24A" ),
                ]
                ){ },

            new(
                new Guid("348ed6db-9f6a-4be1-9a64-65ca57c82bd7"),
                "IJ345",
                "Boeing 747",
                [
                    new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),"Wifi",  "High-speed Wi-Fi" ){ },
                    new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),"USB",  "USB power" ){ },
                    new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),"AC",  "AC power" ){ },
                    new (new Guid("25aed40d-7a45-4485-82de-36877dc3c8d2"),"PersonalStreaming",  "Personal device streaming" ){ },
                    new (new Guid("19207855-4db6-4954-acb4-cabe1d0308ab"),"LiveTV",  "Live TV" ){ },
                    new (new Guid("25965efd-5a60-485f-8d51-5fc985d41962"),"AppleMusic",  "Apple Music" ){ },
                ],
                [
                    new(new Guid("ec588a9b-4c23-4f99-a4a9-28e6e61c724d"), SeatClass.Premium, "5A" ),
                    new(new Guid("55e2087f-c013-4a9b-aa4e-7318f556829e"), SeatClass.PremiumEconomy, "15A" ),
                    new(new Guid("fcc9fdd9-d817-4089-a535-6b1836463ce5"), SeatClass.Main, "25A" ),
                ]
                ){ },
            ];

        public static IEnumerable<PlaneService> PlaneServices =>
            [
            new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),"Wifi",  "High-speed Wi-Fi" ){ },
            new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),"USB",  "USB power" ){ },
            new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),"AC",  "AC power" ){ },
            new (new Guid("25aed40d-7a45-4485-82de-36877dc3c8d2"),"PersonalStreaming",  "Personal device streaming" ){ },
            new (new Guid("19207855-4db6-4954-acb4-cabe1d0308ab"),"LiveTV",  "Live TV" ){ },
            new (new Guid("25965efd-5a60-485f-8d51-5fc985d41962"),"AppleMusic",  "Apple Music" ){ },
            new (new Guid("53258871-4d12-42f9-ac49-b9d277e39061"),"SeatbackEntertainment",  "Seatback entertainment" ){ },
            new (new Guid("2f3dfcdf-2215-4e78-b8b4-ef95e15fe646"),"LieFlatSeats",  "Lie-flat seats in Business and First only" ){ },
            ];

        public static IEnumerable<Seat> Seats =>
           [
            new(new Guid("582db6d2-0735-4a31-b400-9ab12a01b4be"), SeatClass.Premium, "1A" ),
            new(new Guid("a7892205-a92b-48c5-9d7d-d69a647380af"), SeatClass.Premium, "2A" ),
            new(new Guid("6c83b899-9867-49fe-91b4-e6b1bcb9e026"), SeatClass.Premium, "3A" ),
            new(new Guid("ec588a9b-4c23-4f99-a4a9-28e6e61c724d"), SeatClass.Premium, "4A" ),
            new(new Guid("ec588a9b-4c23-4f99-a4a9-28e6e61c724d"), SeatClass.Premium, "5A" ),

            new(new Guid("11d8c1c3-3536-469f-a082-41f908b4c449"), SeatClass.PremiumEconomy, "11A" ),
            new(new Guid("56357f00-1a0e-4bee-9ff4-dc5b42a9d709"), SeatClass.PremiumEconomy, "12A" ),
            new(new Guid("388d6bb8-1b71-4500-a60a-cc597e902a35"), SeatClass.PremiumEconomy, "13A" ),
            new(new Guid("55e2087f-c013-4a9b-aa4e-7318f556829e"), SeatClass.PremiumEconomy, "14A" ),
            new(new Guid("55e2087f-c013-4a9b-aa4e-7318f556829e"), SeatClass.PremiumEconomy, "15A" ),

            new(new Guid("dd1473b6-4e18-4949-a391-bdee85756f5a"), SeatClass.Main, "21A" ),
            new(new Guid("e92a8706-9cd9-4e88-94b7-c83a7dd8115a"), SeatClass.Main, "22A" ),
            new(new Guid("55704630-1c0b-4b64-9c59-2fed3cfff14d"), SeatClass.Main, "23A" ),
            new(new Guid("fcc9fdd9-d817-4089-a535-6b1836463ce5"), SeatClass.Main, "24A" ),
            new(new Guid("fcc9fdd9-d817-4089-a535-6b1836463ce5"), SeatClass.Main, "25A" ),
           ];
               
        public static IEnumerable<Flight> Flights =>
            [
            Flight.Create(
                new Guid("aafd2a3c-acdb-43e5-a5f6-1956ca245bb6"),
                FlightNumber.Create("1234").Value,
                new Plane(
                    new Guid("210b0ad7-5d20-4341-b696-97fe50ac9e22"),
                    "AB123",
                    "Boeing 737",
                    [
                        new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"), "Wifi", "High-speed Wi-Fi" ){ },
                        new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"), "USB", "USB power" ){ },
                        new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"), "AC", "AC power" ){ },
                        new (new Guid("25aed40d-7a45-4485-82de-36877dc3c8d2"), "PersonalStreaming", "Personal device streaming" ){ },
                    ],
                    [
                        new(new Guid("582db6d2-0735-4a31-b400-9ab12a01b4be"), SeatClass.Premium, "1A" ),
                        new(new Guid("11d8c1c3-3536-469f-a082-41f908b4c449"), SeatClass.PremiumEconomy, "11A" ),
                        new(new Guid("dd1473b6-4e18-4949-a391-bdee85756f5a"), SeatClass.Main, "21A" ),
                    ]
                    ){ },
                DateTime.UtcNow.AddHours(2),
                new Airport(new Guid("c26bc687-af30-4392-9c0d-50ce861ae194"), "John F. Kennedy International Airport",  "United States", "New York"){ },
                DateTime.UtcNow.AddHours(4),
                new Airport(new Guid("c839d7a4-7b1a-4adf-aabb-9643f21e8f4d"), "Los Angeles International Airport",  "United States", "Los Angeles"){ }).Value,

            Flight.Create(
                new Guid("bfc9172e-7192-4f10-9417-f5efde560cb0"),
                FlightNumber.Create("5678").Value,
                new Plane(
                    new Guid("fe768d83-cfbc-417b-83c2-667ebf62d290"),
                    "CD456",
                    "Airbus A320",
                    [
                        new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),"Wifi",  "High-speed Wi-Fi" ){ },
                        new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),"USB",  "USB power" ){ },
                        new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),"AC",  "AC power" ){ },
                        new (new Guid("19207855-4db6-4954-acb4-cabe1d0308ab"),"LiveTV",  "Live TV" ){ },
                        new (new Guid("25965efd-5a60-485f-8d51-5fc985d41962"),"AppleMusic",  "Apple Music" ){ },
                    ],
                    [
                        new(new Guid("a7892205-a92b-48c5-9d7d-d69a647380af"), SeatClass.Premium, "2A" ),
                        new(new Guid("56357f00-1a0e-4bee-9ff4-dc5b42a9d709"), SeatClass.PremiumEconomy, "12A" ),
                        new(new Guid("e92a8706-9cd9-4e88-94b7-c83a7dd8115a"), SeatClass.Main, "22A" ),
                    ]
                    ){ },
                DateTime.UtcNow.AddHours(3),
                new Airport(new Guid("c26bc687-af30-4392-9c0d-50ce861ae194"), "John F. Kennedy International Airport",  "United States", "New York"){ },
                DateTime.UtcNow.AddHours(8),
                new Airport(new Guid("f8612dc8-0cb7-427f-9c71-9b959beca793"), "Heathrow Airport",  "United Kingdom", "London"){ }).Value,

            Flight.Create(
                new Guid("ff58c1a0-aad7-4eec-b001-dc1ec52d8abc"),
                FlightNumber.Create("9876").Value,
                new Plane(
                    new Guid("0a912ecc-e7ce-436b-a026-ad4ef1538e4f"),
                    "EF789",
                    "Boeing 777",
                    [
                        new (new Guid("9c8d507e-2bfb-49b3-8b52-59d46e712044"),"Wifi",  "High-speed Wi-Fi" ){ },
                        new (new Guid("5184d713-b5ac-4ca9-9531-e0b7d0cd54dd"),"USB",  "USB power" ){ },
                        new (new Guid("5c1b05a9-df67-45d7-b661-8bae4f645043"),"AC",  "AC power" ){ },
                        new (new Guid("53258871-4d12-42f9-ac49-b9d277e39061"),"SeatbackEntertainment",  "Seatback entertainment" ){ },
                        new (new Guid("2f3dfcdf-2215-4e78-b8b4-ef95e15fe646"),"LieFlatSeats",  "Lie-flat seats in Business and First only" ){ },
                    ],
                    [
                        new(new Guid("6c83b899-9867-49fe-91b4-e6b1bcb9e026"), SeatClass.Premium, "3A" ),
                        new(new Guid("388d6bb8-1b71-4500-a60a-cc597e902a35"), SeatClass.PremiumEconomy, "13A" ),
                        new(new Guid("55704630-1c0b-4b64-9c59-2fed3cfff14d"), SeatClass.Main, "23A" ),
                    ]
                    ){ },
                DateTime.UtcNow.AddHours(1),
                new Airport(new Guid("c26bc687-af30-4392-9c0d-50ce861ae194"), "John F. Kennedy International Airport",  "United States", "New York"){ },
                DateTime.UtcNow.AddHours(12),
                new Airport(new Guid("9e308f59-3c65-412a-9977-14ab8961c1ea"), "Tokyo Haneda Airport",  "Japan", "Tokyo"){ }).Value            
            ];     
    }
}
