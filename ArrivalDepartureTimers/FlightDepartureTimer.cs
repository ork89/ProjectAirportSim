using ProjectAirportSim.ViewModels;
using System;
using System.Timers;

namespace ArrivalDepartureTimers
{
	public class FlightDepartureTimer
	{
		Timer timer;
		AirportViewModel airportVM = new AirportViewModel();
		public bool FlightDepartured { get; set; }
		private string _FlightDeparturing;

		public void PlaneDepaturing(string flightName)
		{
			_FlightDeparturing = flightName;

			timer = new Timer();
			timer.Interval = 10000;
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;
			timer.Start();
		}

		private void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			Console.WriteLine("Flight " + _FlightDeparturing + " is Departured at " + e.SignalTime);

			
		}
	}
}
