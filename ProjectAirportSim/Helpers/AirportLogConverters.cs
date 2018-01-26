using ProjectAirportSim.Models;
using ProjectAirportSim.ViewModels;
using System.Collections.Generic;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Windows;
using ProjectAirportSim.BL;

namespace ProjectAirportSim.Helpers
{
	public static class AirportLogConverters
	{
		public static Flight ConvertAirportLogToFlight(AirportLog log)
		{
			var _flight = new Flight
			{
				ID = log.ID,
				FlightName = log.FlightName,
				Location = log.Location,
				ArrivalDate = log.ArrivalDate,
				DepartureDate = log.DepartureDate,
				Arriving = (bool)log.Arriving
			};

			return _flight;
		}

		public static FlightViewModel ConvertAirportLogToFlightVM(AirportLog log)
		{
			var _flightVM = new FlightViewModel
			{
				ID = log.ID,
				FlightName = log.FlightName,
				PlaneLocation = log.Location,
				ArrivalDate = log.ArrivalDate,
				DepartureDate = log.DepartureDate,
				Arriving = (bool)log.Arriving
			};

			return _flightVM;
		}

		public static List<FlightViewModel> ConvertListOfAirportLogToFVModel(List<AirportLog> listOfLogs)
		{
			var newListOfFlightVM = new List<FlightViewModel>();
			if (listOfLogs.Count > 0)
			{
				listOfLogs.ForEach(item => newListOfFlightVM.Add(ConvertAirportLogToFlightVM(item)));
			}

			return newListOfFlightVM;
		}

		public static LocationViewModel ConvertFlightViewModelToLocationVM(FlightViewModel flight)
		{
			var _location = new LocationViewModel
			{
				LocationID = flight.PlaneLocation,
				LocationStatus = true
			};

			return _location;
		}
	}
}
