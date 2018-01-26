using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;

namespace ProjectAirportSim.ViewModels
{
	public class LocationViewModel : NotifyPropertyChanged
	{
		public LocationViewModel()
		{
			_location = new Location { LocationID = 1, IsOccupied = true };
		}

		Location _location;

		public Location AirportLocation
		{
			get { return _location; }
			set { _location = value; }
		}

		public int LocationID
		{
			get { return AirportLocation.LocationID; }
			set
			{
				if (AirportLocation.LocationID != value)
				{
					AirportLocation.LocationID = value;
					RaisePropertyChanged("LocationID");
				}
			}
		}

		public bool LocationStatus
		{
			get { return AirportLocation.IsOccupied; }
			set
			{
				if (AirportLocation.IsOccupied != value)
				{
					AirportLocation.IsOccupied = value;
					RaisePropertyChanged("LocationStatus");
				}
			}
		}
	}
}
