using System;
using NodaTime;

namespace Lagersoft.UtcLocalConversion.DependencyInjection.UtcLocalConversion
{
    /// <summary>
    /// Basic class to convert between timezones using Noda Time.
    /// </summary>
    public class DateTimeLocalizer
    {
        private readonly DateTimeZone _dateTimeZone;

        /// <summary>
        /// Examples of this value are "America/Monterrey" or "America/El_Salvador".
        /// </summary>
        /// <param name="timeZoneId">The TimezoneId to be used in the conversions</param>
        public DateTimeLocalizer(string timeZoneId)
        {
            _dateTimeZone = DateTimeZoneProviders.Tzdb[timeZoneId];
        }

        /// <summary>
        /// Converts the DateTime from UTC to the Local equivalent.
        /// </summary>
        /// <param name="dateToParse">The UTC DateTime object to parse</param>
        /// <returns>A DateTime in a Local equivalent</returns>
        public DateTime UtcToLocalDateTime(DateTime dateToParse)
        {
            if (dateToParse.Kind != DateTimeKind.Utc)
            {
                dateToParse = DateTime.SpecifyKind(dateToParse, DateTimeKind.Utc);
            }
            var instant = Instant
                .FromDateTimeUtc(dateToParse);
            return instant                
                .InZone(_dateTimeZone)
                .ToDateTimeUnspecified();
        }

        /// <summary>
        /// Converts the DateTime from Local to the UTC equivalent.
        /// </summary>
        /// <param name="dateToParse">The Local DateTime object to parse</param>
        /// <returns>A DateTime in a UTC equivalent</returns>
        public DateTime LocalToUtcDateTime(DateTime dateToParse)
        {
            return LocalDateTime.FromDateTime(dateToParse)
                .InZoneLeniently(_dateTimeZone)
                .ToInstant()
                .ToDateTimeUtc();
        }
    }
}