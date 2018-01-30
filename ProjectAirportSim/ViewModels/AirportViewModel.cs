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
			_tower = new ControlTower();
			_flights = new ObservableCollection<Flight>();
			_locations = new ObservableCollection<Location>();
			_tower.ControlTowerFlightNotifyEvent += NotifyListOfFlightsUpdated;

		}

		private void NotifyListOfFlightsUpdated(List<Flight> flightList, List<Location> locationList)
		{
			ListOfPlanes = new ObservableCollection<Flight>(flightList);
			ListOfLocations = new ObservableCollection<Location>(locationList);
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
	}
}
