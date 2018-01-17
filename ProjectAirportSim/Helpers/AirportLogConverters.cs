using ProjectAirportSim.Models;
using ProjectAirportSim.ViewModels;
using System.Collections.Generic;

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

		public List<FlightViewModel> ConvertListOfAirportLogToFVModel(List<AirportLog> listOfLogs)
		{
			var newListOfFlightVM = new List<FlightViewModel>();
			if (listOfLogs.Count > 0)
			{
				listOfLogs.ForEach(item => newListOfFlightVM.Add(ConvertAirportLogToFlightViewModel(item)));
			}

			return newListOfFlightVM;
		}
	}
}
