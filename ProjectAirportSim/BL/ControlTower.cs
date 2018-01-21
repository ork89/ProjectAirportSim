using ProjectAirportSim.Helpers;
using ProjectAirportSim.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectAirportSim.BL
{
	public class ControlTower
	{
		AirportLogConverters _converter;

		public ControlTower()
		{
			_converter = new AirportLogConverters();
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

		public ObservableCollection<FlightViewModel> GetAllFlightsFromDB()
		{
			var listOfFlights = new ObservableCollection<FlightViewModel>();

			using (var entites = new AirportEntities())
			{
				if (entites != null)
				{
					foreach (var item in entites.AirportLogs)
					{
						listOfFlights.Add(_converter.ConvertAirportLogToFlightVM(item));
					}
				}
			}

			return listOfFlights;
		}

		public bool CheckIfFlightsArePresentInAirport()
		{
			using (var entities = new AirportEntities())
			{
				if (entities == null) return false;

				var cehckForPlanes = entities.AirportLogs.Any(p => p.DepartureDate == null);
				return cehckForPlanes;
			}
		}

		public void RemoveFlightFromAirport(string planeName)
		{
			using (var entities = new AirportEntities())
			{
				var planeToRemove = entities.AirportLogs.Where(flight => flight.FlightName == planeName).FirstOrDefault();

				if (planeToRemove.Location == 6 || planeToRemove.Location == 7 || planeToRemove.Location == 9)
					entities.AirportLogs.Remove(planeToRemove);

				entities.SaveChanges();
			}
		}

		public bool IsAnyFlightAboutToDepart()
		{
			using (var entities = new AirportEntities())
			{
				var planesInAirport = entities.AirportLogs.Where(loc => loc.Location == 9).ToList();
				return planesInAirport.Any();
			}
		}

		public List<FlightViewModel> GetListOfDepartingFlights()
		{
			using (var entities = new AirportEntities())
			{
				var departingFlights = entities.AirportLogs.Where(plane => plane.Location == 6 || plane.Location == 7 || plane.Location == 9).ToList();

				if (departingFlights.Any())
					return _converter.ConvertListOfAirportLogToFVModel(departingFlights);

				//If there are no planes about to depart return an empty list
				return new List<FlightViewModel>();
			}
		}

		public Dictionary<int, bool> GetListOfLocationsAndStatus()
		{
			var _locations = new List<int>();
			Dictionary<int, bool> locationDic = new Dictionary<int, bool>();

			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs.Any())
				{
					_locations = entities.AirportLogs.Select(loc => loc.Location).OrderBy(o => o).Distinct().ToList();

					for (int index = 1; index < 10; index++)
					{
						var status = false;

						if (_locations.Contains(index))
						{
							status = true;
						}

						locationDic.Add(index, status);
					}
				}
			}

			return locationDic;
		}
	}
}
