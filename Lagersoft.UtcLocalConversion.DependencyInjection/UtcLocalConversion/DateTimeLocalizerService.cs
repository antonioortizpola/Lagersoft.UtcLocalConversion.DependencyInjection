using System;
using Microsoft.Extensions.Configuration;

namespace Lagersoft.UtcLocalConversion.DependencyInjection.UtcLocalConversion
{
    /// <summary>
    /// This class in intended to be used as a service.
    /// More info in the readme.md file.
    /// </summary>
    public class DateTimeLocalizerService
    {
        private const string DefaultTimeZoneIdKey = "defaultTimeZoneId";
        
        private readonly DateTimeLocalizer _dateTimeLocalizer;

        public DateTimeLocalizerService(IConfiguration configuration)
        {
            _dateTimeLocalizer = new DateTimeLocalizer(configuration[DefaultTimeZoneIdKey]);
        }
        
        /// <summary>
        /// Converts the DateTime from UTC to the Local equivalent.
        /// </summary>
        /// <param name="dateToParse">The UTC DateTime object to parse</param>
        /// <returns>A DateTime in a Local equivalent</returns>
        public DateTime UtcToLocalDateTime(DateTime dateToParse)
        {
            return _dateTimeLocalizer.UtcToLocalDateTime(dateToParse);
        }

        /// <summary>
        /// Converts the DateTime from Local to the UTC equivalent.
        /// </summary>
        /// <param name="dateToParse">The Local DateTime object to parse</param>
        /// <returns>A DateTime in a UTC equivalent</returns>
        public DateTime LocalToUtcDateTime(DateTime dateToParse)
        {
            return _dateTimeLocalizer.LocalToUtcDateTime(dateToParse);
        }
    }
}