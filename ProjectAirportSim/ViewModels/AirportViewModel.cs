using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjectAirportSim.ViewModels
{
	public class AirportViewModel
	{
		private bool? _isVisible;
		AirportLogConverters _converter;
		private ObservableCollection<FlightViewModel> _planes;

		public AirportViewModel()
		{
			_planes = new ObservableCollection<FlightViewModel>();
			_converter = new AirportLogConverters();
			GetListOfPlanes();
		}

		public ObservableCollection<FlightViewModel> ListOfPlanes
		{
			get { return _planes; }
			set { _planes = value; }
		}

		public void CreateNewPlaneInDB(string flightName, DateTime arrivalTime)
		{
			using (var entities = new AirportEntities())
			{
				var firstLocation = Locations.InAir;

				var plane = new AirportLog()
				{
					FlightName = flightName,
					Location = (int)firstLocation,
					ArrivalDate = arrivalTime,
					DepartureDate = null
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

		private void GetListOfPlanes()
		{
			_planes.Clear();

			if (CheckIfplanesArePresentInAirport())
			{
				using (var entities = new AirportEntities())
				{
					if (entities != null)
					{
						foreach (var item in entities.AirportLogs)
						{
							_planes.Add(_converter.ConvertAirportLogToFlightViewModel(item));
							_isVisible = item.Arriving;
						}
					}
				}
			}
		}

		private bool CanUpdateListofPlanes()
		{
			return true;
		}

		public bool? Visible => _planes.Where(v => v.Arriving.Value == false).Any();


		public ICommand UpdateListOfPlanes { get { return new RelayCommand(GetListOfPlanes, CanUpdateListofPlanes); } }
	}
}
