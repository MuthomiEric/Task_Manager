using Core.Interfaces;

namespace Cards.App_Services
{
    public class DefaultDateTimeFactory : IDateTimeFactory
    {
        public DateTime LocalToUtc(DateTime localTime)
        {
            return localTime.ToUniversalTime();
        }

        public DateTime? LocalToUtc(DateTime? localTime)
        {
            return localTime?.ToUniversalTime();
        }

        public DateTime Now()
        {
            return DateTime.UtcNow;
        }

        public DateTime UtcToLocal(DateTime utcTime)
        {
            return utcTime.ToLocalTime();
        }

        public DateTime? UtcToLocal(DateTime? utcTime)
        {
            return utcTime?.ToLocalTime();
        }
    }
}
