namespace ProjectAirportSim.Models
{
	public class Location
	{
		public int LocationID { get; set; }
		public bool IsOccupied { get; set; }
	}

	public enum Locations
	{
		InAir1,
		InAir2,
		InAir3,
		Runway,
		ApproachParking,
		ParkingSpot1,
		ParkingSpot2,
		ApproachRunway,
		TakeOff
	}
}
