namespace ProjectAirportSim.Models
{
	public class Location
	{
		public Locations LocationID { get; set; }
		public bool IsOccupiedOrEmpty { get; set; }
	}

	public enum Locations
	{
		NotYetExists,
		InAir,
		ApprochingAirport,
		Landing,
		Runway,
		ApproachParking,
		ParkingSpot1,
		ParkingSpot2,
		ApproachRunway,
		Departure
	}
}
