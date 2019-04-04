using System;
using FluentAssertions;
using Lagersoft.UtcLocalConversion.DependencyInjection.UtcLocalConversion;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Lagersoft.UtcLocalConversion.DependencyInjection.Test.UtcLocalConversion
{
    public class DateTimeLocalizerTest
    {
        private readonly DateTime _dateUtcSummerToTest = new DateTime(2018, 06, 27, 17, 44, 00);
        private readonly DateTime _expectedSummerDate = new DateTime(2018, 06, 27, 12, 44, 00);
        
        private readonly DateTime _dateUtcWinterToTest = new DateTime(2018, 11, 02, 17, 44, 00);
        private readonly DateTime _expectedWinterDate = new DateTime(2018, 11, 02, 11, 44, 00);
        
        private readonly IConfiguration _configuration;

        public DateTimeLocalizerTest()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x["TimeZoneId"])
                .Returns("America/Monterrey");
            _configuration = mockConfiguration.Object;
        }

        [Fact]
        public void Summer_Date_20180627_1744_UTC_must_be_20180627_1244_in_Mex()
        {
            var dateTimeLocalizer = new DateTimeLocalizer(_configuration["TimeZoneId"]);
            var convertedDate = dateTimeLocalizer.UtcToLocalDateTime(_dateUtcSummerToTest);
            convertedDate.Should().Be(_expectedSummerDate, "because summer in mexico has -5 UTC");
        }
        
        [Fact]
        public void Winter_Date_20181102_1744_UTC_must_be_20181102_1344_in_Mex()
        {
            var dateTimeLocalizer = new DateTimeLocalizer(_configuration["TimeZoneId"]);
            var convertedDate = dateTimeLocalizer.UtcToLocalDateTime(_dateUtcWinterToTest);
            convertedDate.Should().Be(_expectedWinterDate, "because winter in mexico has -6 UTC");
        } 
        
        [Fact]
        public void Summer_Date_20180627_1244_in_Mex_must_be_20180627_1744_UTC()
        {
            var dateTimeLocalizer = new DateTimeLocalizer(_configuration["TimeZoneId"]);
            var convertedDate = dateTimeLocalizer.LocalToUtcDateTime(_expectedSummerDate);
            convertedDate.Should().Be(_dateUtcSummerToTest, "because summer in mexico has -5 UTC");
        }
        
        [Fact]
        public void Winter_Date_20181102_1344_in_Mex_must_be_20181102_1744_UTC()
        {
            var dateTimeLocalizer = new DateTimeLocalizer(_configuration["TimeZoneId"]);
            var convertedDate = dateTimeLocalizer.LocalToUtcDateTime(_expectedWinterDate);
            convertedDate.Should().Be(_dateUtcWinterToTest, "because winter in mexico has -6 UTC");
        }
        
        [Fact]
        public void Utc_to_local_must_handle_date_time_kinds_as_utc()
        {
            var dateTimeLocalizer = new DateTimeLocalizer(_configuration["TimeZoneId"]);
            
            var utcDate = DateTime.SpecifyKind(_dateUtcSummerToTest, DateTimeKind.Utc);
            var convertedDate = dateTimeLocalizer.UtcToLocalDateTime(utcDate);
            
            var utcUnspecifiedDate = DateTime.SpecifyKind(_dateUtcSummerToTest, DateTimeKind.Unspecified);
            var convertedUnspecifiedDate = dateTimeLocalizer.UtcToLocalDateTime(utcUnspecifiedDate);
            
            convertedDate.Should().Be(convertedUnspecifiedDate, "because DateTime.Kind must be ignored");
        }
    }
}