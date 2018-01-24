using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using System;

namespace ProjectAirportSim.ViewModels
{
	public class FlightViewModel : NotifyPropertyChanged
	{
		public FlightViewModel()
		{
			_plane = new Flight { FlightName = "Dummy", Location = 1, ArrivalDate = DateTime.UtcNow, DepartureDate = null, Arriving = true, Visible = true };
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

		public int PlaneLocation
		{
			get { return Plane.Location; }
			set
			{
				if (Plane.Location != value)
					Plane.Location = value;
				RaisePropertyChanged("PlaneLocation");
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
				if (Plane.Arriving != value)
				{
					Plane.Arriving = value;
					RaisePropertyChanged("Arriving");
				}
			}
		}

		public bool Visible
		{
			get { return Plane.Visible; }
			set
			{
				if (Plane.Visible != value)
				{
					Plane.Visible = value;
					RaisePropertyChanged("Visible");
				}
			}
		}
	}
}