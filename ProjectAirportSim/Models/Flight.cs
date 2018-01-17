using System;

namespace ProjectAirportSim.Models
{
	public class Flight
	{
		public int ID { get; set; }
		public string FlightName { get; set; }
		public int Location { get; set; }
		public DateTime? ArrivalDate { get; set; }
		public DateTime? DepartureDate { get; set; }
		public bool? Arriving { get; set; }
	}
}
