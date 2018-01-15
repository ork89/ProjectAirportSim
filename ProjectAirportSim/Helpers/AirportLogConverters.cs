using ProjectAirportSim.Models;
using ProjectAirportSim.ViewModels;

namespace ProjectAirportSim.Helpers
{
	public class AirportLogConverters
	{
		public AirportLogConverters() { }

		public Flight ConvertAirportLogToFlight(AirportLog log)
		{
			var _flight = new Flight
			{
				ID = log.ID,
				FlightName = log.FlightName,
				Location = log.Location,
				ArrivalDate = log.ArrivalDate,
				DepartureDate = log.DepartureDate,
				Arriving = log.Arriving
			};

			return _flight;
		}

		public FlightViewModel ConvertAirportLogToFlightViewModel(AirportLog log)
		{
			var _flightVM = new FlightViewModel
			{
				ID = log.ID,
				FlightName = log.FlightName,
				Location = log.Location,
				ArrivalDate = log.ArrivalDate,
				DepartureDate = log.DepartureDate,
				Arriving = log.Arriving
			
			};

			return _flightVM;
		}
	}
}
