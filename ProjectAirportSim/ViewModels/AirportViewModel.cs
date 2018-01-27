using ProjectAirportSim.BL;
using ProjectAirportSim.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjectAirportSim.ViewModels
{
	public class AirportViewModel : NotifyPropertyChanged
	{

		ControlTower _tower = new ControlTower();
		private ObservableCollection<FlightViewModel> _flights;
		private ObservableCollection<LocationViewModel> _locations;

		public AirportViewModel()
		{
			_flights = new ObservableCollection<FlightViewModel>();
			_locations = new ObservableCollection<LocationViewModel>();
			
			ExecuteGetListOfFlights();
			GetListOfLocations();
		}

		public ObservableCollection<FlightViewModel> ListOfPlanes
		{
			get { return _flights; }
			set
			{
				_flights = value;
				RaisePropertyChanged("ListOfPlanes");
			}
		}

		public ObservableCollection<LocationViewModel> ListOfLocations
		{
			get { return _locations; }
			set
			{
				_locations = value;
				RaisePropertyChanged("ListOfLocations");
			}
		}

		private void GetListOfLocations()
		{
			_locations = _tower.GetListOfLocationsAndStatus();
			RaisePropertyChanged("ListOfLocations");
		}

		private void ExecuteGetListOfFlights()
		{
			_flights = _tower.GetAllFlightsFromDB();
			RaisePropertyChanged("ListOfPlanes");
		}

		private bool CanExecuteGetFlightsUpdate() => true;

		public ICommand UpdateListOfFlights { get { return new RelayCommand(ExecuteGetListOfFlights, CanExecuteGetFlightsUpdate); } }
	}
}
