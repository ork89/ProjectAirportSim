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
		private int _counter;
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
			_counter++;
			GetAllFlightsFromDB();

			if (_counter == 5)
			{
				ControlTowerManager();
				_counter = 0;
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
			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs.Any())
				{
					entities.AirportLogs.Where(pl => pl.Location == 9)
										.Select(plane => plane).ToList()
										.ForEach(d => { d.DepartureDate = DateTime.UtcNow; d.Arriving = false; d.Location = 0; });
				}

				entities.SaveChanges();
			}
		}


		public void ControlTowerManager()
		{
			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs.Any())
				{
					var _planesLocations = Converters.ConvertAirportLogListToFlightList(entities.AirportLogs.OrderBy(l => l.Location).ToList());
					_planesLocations.SkipWhile(l => l.Location == 4 || l.Location == 6 || l.Location == 7);

					foreach (var item in _planesLocations)
					{
						var _nextLocation = item.Location + 1;
						if (!_planesLocations.Exists(l => l.Location == _nextLocation) && item.Location < 9 && item.DepartureDate == null)
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
