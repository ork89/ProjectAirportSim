using ProjectAirportSim.Models;
using System;
using System.Linq;

namespace ProjectAirportSim.BL
{
	public class ControlTower
	{
		public void CreateNewPlaneInDB(string flightName, DateTime arrivalTime)
		{
			using (var entities = new AirportEntities())
			{
				var currentLocation = Locations.InAir;

				var plane = new AirportLog()
				{
					FlightName = flightName,
					Location = (int)currentLocation,
					ArrivalDate = arrivalTime,
					Arriving = true
				};

				entities.AirportLogs.Add(plane);
				entities.SaveChanges();
			}
		}

		public bool CheckIfplanesArePresentInAirport()
		{
			using (var entities = new AirportEntities())
			{
				var cehckForPlanes = entities.AirportLogs.Any(p => p.DepartureDate == null);
				return cehckForPlanes;
			}
		}

		public void RemovePlaneFromAirport(string planeName)
		{
			using (var entities = new AirportEntities())
			{
				var planeToRemove = entities.AirportLogs.Where(flight => flight.FlightName == planeName).FirstOrDefault();

				if (planeToRemove.Location == 6 || planeToRemove.Location == 7 || planeToRemove.Location == 9)
					entities.AirportLogs.Remove(planeToRemove);

				entities.SaveChanges();
			}
		}
	}
}
