namespace AviationFleetService.Api.Contracts.Seats.Enums
{
    public static class EnumConverter
    {
        public static Domain.Enums.SeatClass ToDomainSeatClass(SeatClass apiSeatClass)
        {
            return (Domain.Enums.SeatClass)Enum.Parse(
                typeof(Domain.Enums.SeatClass),
                apiSeatClass.ToString());
        }

        public static SeatClass ToDtoSeatClass(Domain.Enums.SeatClass seatClass)
        {
            return (SeatClass)Enum.Parse(
                typeof(SeatClass),
                seatClass.ToString());
        }

        public static Domain.Enums.SeatType ToDomainSeatType(SeatType apiSeatType)
        {
            return (Domain.Enums.SeatType)Enum.Parse(
                typeof(Domain.Enums.SeatType),
                apiSeatType.ToString());
        }

        public static SeatType ToDtoSeatType(Domain.Enums.SeatType seatType)
        {
            return (SeatType)Enum.Parse(
                typeof(SeatType),
                seatType.ToString());
        }
    }
}
