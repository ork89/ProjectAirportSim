using ProjectAirportSim.BL;
using ProjectAirportSim.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjectAirportSim.ViewModels
{
	public class AirportViewModel : NotifyPropertyChanged
	{

		ControlTower _tower;
		AirportLogConverters _converter;
		private ObservableCollection<FlightViewModel> _flights;

		public AirportViewModel()
		{
			_tower = new ControlTower();
			_converter = new AirportLogConverters();
			_flights = new ObservableCollection<FlightViewModel>();

			GetListOfFlights();
			GetLocations();
		}

		public ObservableCollection<FlightViewModel> ListOfPlanes
		{
			get { return _flights; }
			set { _flights = value; }
		}

		private void GetListOfFlights()
		{
			_flights = _tower.GetAllFlightsFromDB();
		}

		public bool[] GetLocations()
		{
			var locationStatus = _tower.GetListOfLocationsAndStatus();
			var locationArr = locationStatus.Values.ToArray();

			return locationArr;
		}

		private bool CanExecuteGetFlightsUpdate() => true;
		//private bool CanExecuteGetLocationsUpdate() => true;

		public ICommand UpdateListOfFlights { get { return new RelayCommand(GetListOfFlights, CanExecuteGetFlightsUpdate); } }
		//public ICommand UpdateLocations { get { return new RelayCommand(GetLocations, CanExecuteGetLocationsUpdate); } }
	}
}
