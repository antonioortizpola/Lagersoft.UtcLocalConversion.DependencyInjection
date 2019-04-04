using System;
using NodaTime;
using NodaTime.Extensions;

namespace Lagersoft.UtcLocalConversion.DependencyInjection.UtcLocalConversion
{
    /// <summary>
    /// This class in intended to be used as a service.
    /// More info in the readme.md file.
    /// </summary>
    public class DateTimeProvider
    {
        private readonly DateTimeZone _dateTimeZone;
        private readonly IClock _clock;

        public DateTimeProvider(
            string timeZoneId,
            IClock clock)
        {
            _dateTimeZone = DateTimeZoneProviders.Tzdb[timeZoneId];
            _clock = clock;
        }

        public DateTime TodayLocalInUtc()
        {
            return _clock
                .InZone(_dateTimeZone)
                .GetCurrentZonedDateTime()
                .Date
                .AtStartOfDayInZone(_dateTimeZone)
                .ToDateTimeUtc();
        }

        public DateTime UtcNow()
        {
            return _clock
                .GetCurrentInstant()
                .ToDateTimeUtc();
        }
        
        public DateTime LocalNow()
        {
            return _clock
                .InZone(_dateTimeZone)
                .GetCurrentLocalDateTime()
                .ToDateTimeUnspecified();
        }
    }
}