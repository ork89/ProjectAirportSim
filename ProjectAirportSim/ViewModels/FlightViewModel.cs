using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using System;

namespace ProjectAirportSim.ViewModels
{
	public class FlightViewModel : NotifyPropertyChanged
	{
		public FlightViewModel()
		{
			_plane = new Flight { FlightName = "TestFlight", Location = 1, ArrivalDate = DateTime.UtcNow, DepartureDate = null, Arriving = true };
		}

		Flight _plane;


		public Flight Plane
		{
			get
			{
				return _plane;
			}
			set
			{
				_plane = value;
			}
		}

		public int ID
		{
			get { return Plane.ID; }
			set
			{
				if (Plane.ID != value)
				{
					Plane.ID = value;
					RaisePropertyChanged("ID");
				}
			}
		}

		public string FlightName
		{
			get { return Plane.FlightName; }
			set
			{
				if (Plane.FlightName != value)
				{
					Plane.FlightName = value;
					RaisePropertyChanged("FlightName");
				}
			}
		}

		public int Location
		{
			get { return Plane.Location; }
			set
			{
				if (Plane.Location != value)
					Plane.Location = value;
					RaisePropertyChanged("Location");
			}
		}

		public DateTime? ArrivalDate
		{
			get { return Plane.ArrivalDate; }
			set
			{
				if (Plane.ArrivalDate != value)
				{
					Plane.ArrivalDate = value;
					RaisePropertyChanged("ArrivalDate");
				}
			}
		}

		public DateTime? DepartureDate
		{
			get { return Plane.DepartureDate; }
			set
			{
				if (Plane.DepartureDate != value)
				{
					Plane.DepartureDate = value;
					RaisePropertyChanged("DepartureDate");
				}
			}
		}

		public bool Arriving
		{
			get { return Plane.Arriving; }
			set
			{
				if(Plane.Arriving != value)
				{
					Plane.Arriving = value;
					RaisePropertyChanged("Arriving");
				}
			}
		}

	}
}
