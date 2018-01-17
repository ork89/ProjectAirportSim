using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using ProjectAirportSim.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectAirportSim.BL
{
	public class ControlTower
	{
		//var entities = new AirportEntities()
		AirportLogConverters _converter;
		public ControlTower()
		{
			_converter = new AirportLogConverters();
		}

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

		public Dictionary<int, bool> GetListOfLocations(ObservableCollection<FlightViewModel> flightsList)
		{
			var locationStatusDic = new Dictionary<int, bool>();

			using (var entities = new AirportEntities())
			{
				foreach (var flight in flightsList)
				{
					var location = flight.Location;
					var arriving = flight.Arriving;

					locationStatusDic.Add(location, arriving);
				}
			}

			return locationStatusDic;
		}
	}
}
