using ProjectAirportSim.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace ProjectAirportSim.Helpers
{
	//Adapters
	public static class Converters
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

		public static Location ConvertFlightLocationToLocation(AirportLog log)
		{
			var _location = new Location
			{
				LocationID = log.Location,
				IsOccupied = true
			};

			return _location;
		}

		public static List<Flight> ConvertAirportLogListToFlightList(List<AirportLog> logList)
		{
			var _flightList = new List<Flight>();

			if (logList != null && logList.Count > 0)
				logList.ForEach(log => _flightList.Add(ConvertAirportLogToFlight(log)));

			return _flightList;
		}
	}

	//Converters
	public class BoolToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? "Arriving" : "Departing";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
