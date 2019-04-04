# UTC to Local conversion for Net Core 2.2

This is a simple tool to integrate a date conversion library based on [Noda Time](https://nodatime.org/) with net core
dependency injection.

The service will search for a configuration parameter named `defaultTimeZoneId` to know which time zone id will be using
for the conversions.

## Install

Just run

    Install-Package Lagersoft.UtcLocalConversion.DependencyInjection -Version 1.0.0

To use, simply call

    DateTimeModule.AddUtcLocalConversion()
    
Example _Startup.cs_
 
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
 	    services
 		    .AddMvc()
 		    .AddUtcLocalConversion();
    }
   
Example _appsettings.json_
 
    {
    	"DefaultTimeZoneId": "America/Guatemala"
    }

For now, it has three main usages.

### DateTimeLocalizer

Used to make conversions between some local and UTC.

    dateTimeLocalizer = new DateTimeLocalizer("America/Guatemala");

### DateTimeLocalizerService

Is build on top of `DateTimeLocalizer`, but is made to take advantage of the Dependency Injection.

This class should be injected, with the configuration like in the Examples, and allows to convert between a particular
TimeZoneId (defined in the configuration) and UTC.
 
 		public SomeServiceConstructor(DateTimeLocalizerService dateTimeLocalizerService)
 		{
 			_dateTimeLocalizerService = dateTimeLocalizerService;
 		}
 
 		public void SomeFunction()
 		{
 			var someLocalDateTime = _dateTimeLocalizerService.UtcToLocalDateTime(DateTime.UtcNow);
 		}

### DateTimeProviderService

As `DateTimeLocalizerService`, it needs the Dependency Injection configuration.

The class allows to get some common date functions.
 
 		public SomeServiceConstructor(DateTimeProviderService dateTimeProviderService)
 		{
 			_dateTimeProviderService = dateTimeProviderService;
 		}
 
 		public void SomeFunction()
 		{
 			var localStartOfDayInUtcForDbReport = _dateTimeProviderService.TodayLocalInUtc();
 		}
 		
