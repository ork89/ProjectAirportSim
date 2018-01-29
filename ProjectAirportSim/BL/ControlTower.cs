using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace ProjectAirportSim.BL
{
	public delegate void ControlTowerIncomingFlightNotify(List<Flight> flights, List<Location> locations);
	public class ControlTower
	{
		Timer timer;
		public event ControlTowerIncomingFlightNotify ControlTowerFlightNotifyEvent;

		public ControlTower()
		{
			StartContorlTowerManager();
		}


		public void CreateNewPlaneInDB(string flightName, DateTime arrivalTime)
		{
			using (var entities = new AirportEntities())
			{
				var plane = new AirportLog()
				{
					FlightName = flightName,
					Location = 1,
					ArrivalDate = arrivalTime,
					Arriving = true,
				};

				entities.AirportLogs.Add(plane);
				entities.SaveChanges();
			}
		}



		public void StartContorlTowerManager()
		{
			timer = new Timer { Interval = 1000 };
			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Start();
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			GetAllFlightsFromDB();
		}

		public void GetAllFlightsFromDB()
		{
			var _listOfFlights = new List<Flight>();
			var _locations = new List<Location>();

			using (var entities = new AirportEntities())
			{
				if (entities != null)
				{
					foreach (var item in entities.AirportLogs)
					{
						_listOfFlights.Add(Converters.ConvertAirportLogToFlight(item));
						_locations.Add(Converters.ConvertFlightLocationToLocation(item));
					}
				}
			}
			NotifyControlTower(_listOfFlights, _locations);
		}

		private void NotifyControlTower(List<Flight> listOfFlights, List<Location> locations)
		{
			if (ControlTowerFlightNotifyEvent != null)
			{
				ControlTowerFlightNotifyEvent.Invoke(new List<Flight>(listOfFlights), new List<Location>(locations));
			}
		}


		public void RemoveDepartingFlights()
		{
			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs.Any())
				{
					entities.AirportLogs.Where(pl => pl.Location == 9).Select(plane => plane).ToList().ForEach(d => d.DepartureDate = DateTime.UtcNow);
				}

				entities.SaveChanges();
			}
		}


		public void ControlTowerManager()
		{

			using (var entities = new AirportEntities())
			{

			}
		}
	}
}
