using System;
using FluentAssertions;
using Lagersoft.UtcLocalConversion.DependencyInjection.UtcLocalConversion;
using Microsoft.Extensions.Configuration;
using Moq;
using NodaTime.Testing;
using Xunit;

namespace Lagersoft.UtcLocalConversion.DependencyInjection.Test.UtcLocalConversion
{
    public class DateTimeProviderTest
    {
        private readonly IConfiguration _configuration;

        public DateTimeProviderTest()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x["TimeZoneId"])
                .Returns("America/Monterrey");
            _configuration = mockConfiguration.Object;
        }

        [Fact]
        public void Summer_local_today_with_20180710_163030_in_utc_must_be_20180710_0500_for_mex()
        {
            var clock = FakeClock.FromUtc(2018, 07, 10, 16, 30, 30);
            var dateTimeProvider = new DateTimeProvider(_configuration["TimeZoneId"], clock);
            var todayMexInUtc = dateTimeProvider.TodayLocalInUtc();
            var expectedToday = new DateTime(2018, 07, 10, 05, 00, 00);
            
            todayMexInUtc.Should().Be(expectedToday, "because mex is -5 in UTC for summer");
        }
        
        [Fact]
        public void Winter_local_today_with_20181102_163030_in_utc_must_be_20181102_0600_for_mex()
        {
            var clock = FakeClock.FromUtc(2018, 11, 02, 16, 30, 30);
            var dateTimeProvider = new DateTimeProvider(_configuration["TimeZoneId"], clock);
            var todayMexInUtc = dateTimeProvider.TodayLocalInUtc();
            var expectedToday = new DateTime(2018, 11, 02, 06, 00, 00);
            
            todayMexInUtc.Should().Be(expectedToday, "because mex is -6 in UTC for winter");
        }
        
        [Fact]
        public void With_20181102_163030_in_utc_must_be_same_in_utc_now()
        {
            var clock = FakeClock.FromUtc(2018, 11, 02, 16, 30, 30);
            var dateTimeProvider = new DateTimeProvider(_configuration["TimeZoneId"], clock);
            var utcNow = dateTimeProvider.UtcNow();
            var expectedNow = new DateTime(2018, 11, 02, 16, 30, 30);
            
            utcNow.Should().Be(expectedNow, "because it must be the same time");
        }
        
        [Fact]
        public void Winter_20181102_163030_in_utc_must_be_20181102_103030_in_mex_local_now()
        {
            var clock = FakeClock.FromUtc(2018, 11, 02, 16, 30, 30);
            var dateTimeProvider = new DateTimeProvider(_configuration["TimeZoneId"], clock);
            var localNow = dateTimeProvider.LocalNow();
            var expectedLocalNow = new DateTime(2018, 11, 02, 10, 30, 30);
            
            localNow.Should().Be(expectedLocalNow, "because mex is -6 in that date");
        }
        
        [Fact]
        public void Summer_20180710_163030_in_utc_must_be_20180710_113030_in_mex_local_now()
        {
            var clock = FakeClock.FromUtc(2018, 07, 10, 16, 30, 30);
            var dateTimeProvider = new DateTimeProvider(_configuration["TimeZoneId"], clock);
            var localNow = dateTimeProvider.LocalNow();
            var expectedLocalNow = new DateTime(2018, 07, 10, 11, 30, 30);
            
            localNow.Should().Be(expectedLocalNow, "because mex is -5 in that date");
        }
    }
}