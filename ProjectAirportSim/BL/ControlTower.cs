using ProjectAirportSim.Helpers;
using ProjectAirportSim.ViewModels;
using ProjectAirportSim.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectAirportSim.BL
{
	public class ControlTower
	{
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
					entities.AirportLogs.Where(pl => pl.Location == 9).Select(plane => plane).ToList().ForEach(d => d.DepartureDate = DateTime.UtcNow);
				}

				entities.SaveChanges();
			}
		}


		public void ControlTowerManager()
		{
			AirportViewModel airportVM = new AirportViewModel();
			MainWindow window = new MainWindow();

			using (var entities = new AirportEntities())
			{
				if (entities.AirportLogs != null && airportVM.ListOfLocations != null && airportVM.ListOfPlanes != null)
				{
					window.PlaneVisibility();
					foreach (var item in airportVM.ListOfLocations)
					{
						var next = item.LocationID + 1;

						if (airportVM.ListOfLocations.Where(l => l.LocationID == next).Select(lo => lo.LocationStatus).FirstOrDefault())
						{
							item.LocationStatus = false;
							var location = airportVM.ListOfLocations.Where(l => l.LocationID == next).Select(lo => lo.LocationStatus = true).ToList();
							var flights = airportVM.ListOfPlanes.Where(p => p.PlaneLocation == next).Select(pl => pl.PlaneLocation++);
						}
					}

					foreach (var log in entities.AirportLogs)
					{
						log.Location = airportVM.ListOfPlanes.Where(i => i.ID == log.ID).Select(l => l.PlaneLocation).FirstOrDefault();
					}

					entities.SaveChanges();
				}
			}
		}
	}
}
