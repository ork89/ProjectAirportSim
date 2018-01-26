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
		AirportViewModel _airportVM;

		public ControlTower() { }

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
						listOfFlights.Add(AirportLogConverters.ConvertAirportLogToFlightVM(item));
					}
				}
			}

			return listOfFlights;
		}


		public ObservableCollection<LocationViewModel> GetListOfLocationsAndStatus()
		{
			var listOfLocations = new ObservableCollection<LocationViewModel>();
			var listOfFlightsInDb = GetAllFlightsFromDB();

			if (listOfFlightsInDb.Any())
			{
				foreach (var flight in listOfFlightsInDb)
				{
					listOfLocations.Add(AirportLogConverters.ConvertFlightViewModelToLocationVM(flight));
				}
			}

			return listOfLocations;
		}


		public void RemoveDepartingFlights()
		{
			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs.Any())
				{
					var departingFlightsFromDB = entities.AirportLogs.Where(plane => plane.Location == 6 || plane.Location == 7 || plane.Location == 9).ToList();

					if (departingFlightsFromDB.Any())
					{
						departingFlightsFromDB.ForEach(plane => entities.AirportLogs.Remove(plane));
					}
				}
			}
		}



		
	}
}
