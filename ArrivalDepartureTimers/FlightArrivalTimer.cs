using ProjectAirportSim.BL;
using System;
using System.Timers;

namespace ArrivalDepartureTimers
{
	public class FlightArrivalTimer
	{
		Timer _timer;
		ControlTower _tower = new ControlTower();
		private const string _alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		public void PlaneArriving()
		{
			_timer = new Timer { Interval = 10000 };
			_timer.Elapsed += OnTimedEvent;
			_timer.AutoReset = true;

			_timer.Start();
		}

		private void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			var planeName = GenerateNewFlightName();
			var arrivalTime = e.SignalTime;
			
			_tower.CreateNewPlaneInDB(planeName, arrivalTime);
		}

		private string GenerateNewFlightName()
		{
			Random rnd = new Random();
			char[] chars = new char[5];

			for (int i = 0; i < 5; i++)
			{
				chars[i] = _alphaNumeric[rnd.Next(_alphaNumeric.Length)];
			}

			return new string(chars);
		}
	}
}
