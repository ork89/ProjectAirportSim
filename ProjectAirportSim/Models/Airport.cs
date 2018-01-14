using System.Collections.Generic;

namespace ProjectAirportSim.Models
{
	public class Airport
	{

		private List<Flight> _listOfFlights;
		private List<Location> _listOfLocations;
		private bool _emergency;

		public List<Flight> ListOfFlights
		{
			get { return _listOfFlights; }
			set { _listOfFlights = value; }
		}

		public List<Location> Locations
		{
			get { return _listOfLocations; }
			set { _listOfLocations = value; }
		}


		public bool Emergency
		{
			get { return _emergency; }
			set { _emergency = value; }
		}
	}
}
