using ProjectAirportSim.BL;
using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjectAirportSim.ViewModels
{
	public class AirportViewModel : NotifyPropertyChanged
	{

		ControlTower _tower;
		private ObservableCollection<Flight> _flights;
		private ObservableCollection<Location> _locations;

		public AirportViewModel()
		{
			_flights = new ObservableCollection<Flight>();
			_locations = new ObservableCollection<Location>();
			_tower = new ControlTower();
			_tower.ControlTowerFlightNotifyEvent += NotifyListOfFlightsUpdated;

		}

		private void NotifyListOfFlightsUpdated(List<Flight> flightList, List<Location> locationList)
		{
			ListOfPlanes = new ObservableCollection<Flight>();
			ListOfLocations = new ObservableCollection<Location>();
			foreach (var item in flightList)
			{
				ListOfPlanes.Add(item);
			}

			foreach (var item in locationList)
			{
				if (item.LocationID == 1 && item.IsOccupied) { InAir = true; }
				ListOfLocations.Add(item);
			}
		}

		public ObservableCollection<Flight> ListOfPlanes
		{
			get { return _flights; }
			set
			{
				_flights = value;
				RaisePropertyChanged("ListOfPlanes");
			}
		}

		public ObservableCollection<Location> ListOfLocations
		{
			get { return _locations; }
			set
			{
				_locations = value;
				RaisePropertyChanged("ListOfLocations");
			}
		}

		private bool _inAir;

		public bool InAir
		{
			get { return _inAir; }
			set
			{
				_inAir = value;

				RaisePropertyChanged();
			}
		}


		//private void GetListOfLocations()
		//{
		//	_locations = _tower.GetListOfLocationsAndStatus();
		//	RaisePropertyChanged();
		//}

		//private void ExecuteGetListOfFlights()
		//{
		//	_flights = _tower.GetAllFlightsFromDB();
		//	RaisePropertyChanged();
		//}

		//private bool CanExecuteGetFlightsUpdate() => true;

		//public ICommand UpdateListOfFlights { get { return new RelayCommand(ExecuteGetListOfFlights, CanExecuteGetFlightsUpdate); } }
	}
}
