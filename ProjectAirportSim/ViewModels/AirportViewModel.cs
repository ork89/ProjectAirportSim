using ProjectAirportSim.Helpers;
using ProjectAirportSim.BL;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjectAirportSim.ViewModels
{
	public class AirportViewModel
	{
		private bool? _isVisible;
		ControlTower _tower;
		AirportLogConverters _converter;
		private ObservableCollection<FlightViewModel> _planes;

		public AirportViewModel()
		{
			_tower = new ControlTower();
			_converter = new AirportLogConverters();
			_planes = new ObservableCollection<FlightViewModel>();
			GetListOfPlanes();
		}

		public ObservableCollection<FlightViewModel> ListOfPlanes
		{
			get { return _planes; }
			set { _planes = value; }
		}

		private void GetListOfPlanes()
		{
			_planes.Clear();

			if (_tower.CheckIfplanesArePresentInAirport())
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

		public bool? Visible => _isVisible;

		private bool CanUpdateListofPlanes()
		{
			return true;
		}

		public ICommand UpdateListOfPlanes { get { return new RelayCommand(GetListOfPlanes, CanUpdateListofPlanes); } }
	}
}
