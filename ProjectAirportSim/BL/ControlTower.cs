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
		private int _getFlightsCounter;
		private int _removalCounter;
		private static Random rand;
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
			rand = new Random();
			_getFlightsCounter++;
			GetAllFlightsFromDB();

			if (_getFlightsCounter == 5)
			{
				int _emergencyLocation = rand.Next(1, 10);
				bool _emergency = rand.NextDouble() > 0.5;

				ControlTowerManager(_emergency, _emergencyLocation);
				_getFlightsCounter = 0;
			}
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
			_removalCounter++;

			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs.Any())
				{
					entities.AirportLogs.Where(pl => pl.Location == 9)
										.Select(plane => plane).ToList()
										.ForEach(d => { d.DepartureDate = DateTime.UtcNow; d.Arriving = false; d.Location = 0; });
				}

				// Remove flights that have already departed, every 10 seconds
				if (_removalCounter == 10)
				{
					var departedFlights = entities.AirportLogs.Where(loc => loc.Location == 0).ToList();
					entities.AirportLogs.RemoveRange(departedFlights);
					_removalCounter = 0;
				}

				entities.SaveChanges();
			}
		}


		public void ControlTowerManager(bool isEmergency, int emergencyloction)
		{
			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs.Any())
				{
					var _planesLocations = Converters.ConvertAirportLogListToFlightList(entities.AirportLogs.OrderBy(l => l.Location).ToList());

					// If locations 4/6/7 are occupied don't add to the list the planes one location before them.
					_planesLocations.SkipWhile(l => l.Location == 3 && _planesLocations.Exists(loc => loc.Location == 4) ||
													l.Location == 5 && _planesLocations.Exists(loc => loc.Location == 6) ||
													l.Location == 6 && _planesLocations.Exists(loc => loc.Location == 7));

					// Skip the location if ther's an emergency on that location
					if (isEmergency)
						_planesLocations.Skip(emergencyloction);

					foreach (var item in _planesLocations)
					{
						var _nextLocation = item.Location + 1;

						// Advance the plane one location if the next location is free and smaller then 9 (since 9 is the last location)
						if (!_planesLocations.Exists(l => l.Location == _nextLocation)
															&& item.Location < 9
															&& item.DepartureDate == null)
						{
							entities.AirportLogs.Where(p => p.ID == item.ID).FirstOrDefault().Location++;
						}
					}

					entities.SaveChanges();
				}
			}
		}
	}
}
