﻿using ProjectAirportSim.Helpers;
using ProjectAirportSim.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjectAirportSim.ViewModels
{
	public class AirportViewModel
	{
		AirportLogConverters _converter;
		private ObservableCollection<FlightViewModel> _planes = new ObservableCollection<FlightViewModel>();

		public AirportViewModel()
		{
			_converter = new AirportLogConverters();
			GetListOfPlanes();
		}

		public ObservableCollection<FlightViewModel> ListOfPlanes
		{
			get
			{
				return _planes;
			}
			set
			{
				_planes = value;
			}
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

		public bool CheckIfplanesPresentInAirport()
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

			if (CheckIfplanesPresentInAirport())
			{
				using (var entities = new AirportEntities())
				{
					foreach (var item in entities.AirportLogs)
					{
						_planes.Add(_converter.ConvertAirportLogToFlightViewModel(item));
					}
				}
			}

			var test = _planes.Where(x => x.ID == 1).FirstOrDefault();
			test.Arriving = false;
		}

		bool CanUpdateListofPlanes()
		{
			return true;
		}

		public ICommand UpdateListOfPlanes { get { return new RelayCommand(GetListOfPlanes, CanUpdateListofPlanes); } }
	}
}
