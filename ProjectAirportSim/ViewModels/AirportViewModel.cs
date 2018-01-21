using ProjectAirportSim.BL;
using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectAirportSim.ViewModels
{
	public class AirportViewModel : NotifyPropertyChanged
	{

		ControlTower _tower;
		AirportLogConverters _converter;
		private ObservableCollection<FlightViewModel> _flights;
		private bool _isVisible;

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

		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				if (_isVisible != value)
				{
					_isVisible = value;
					RaisePropertyChanged("IsVisible");
				}
			}
		}

		private void GetListOfFlights()
		{
			_flights = _tower.GetAllFlightsFromDB();
		}

		private void GetLocations()
		{
			var locationStatus = _tower.GetListOfLocationsAndStatus();
			
			foreach (var flight in _flights)
			{
				if (locationStatus.ContainsKey(flight.PlaneLocation))
				{
					flight.Visible = true;
					_isVisible = true;
					RaisePropertyChanged("IsVisible");
				}	
			}
		}

		private bool CanExecuteGetFlightsUpdate() => true;
		private bool CanExecuteGetLocationsUpdate() => true;

		public ICommand UpdateListOfFlights { get { return new RelayCommand(GetListOfFlights, CanExecuteGetFlightsUpdate); } }
		public ICommand UpdateLocations { get { return new RelayCommand(GetLocations, CanExecuteGetLocationsUpdate); } }
	}
}
