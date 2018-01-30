using ProjectAirportSim.BL;
using System;
using System.Timers;

namespace ArrivalDepartureTimers
{
	public class FlightDepartureTimer
	{
		Timer timer;
		ControlTower _tower = new ControlTower();

		public void PlaneDepaturing()
		{
			timer = new Timer { Interval = 5000 };
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;
			timer.Start();
		}

		private void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			Console.WriteLine("Flight is Departured at " + e.SignalTime);
			_tower.RemoveDepartingFlights();
		}
	}
}
