using System;
using System.Timers;

namespace ProjectAirportSim.Models
{
	public class Airport
	{
		Timer _timer;
		private bool _emergency;
		private int _emergencyLocation;
		private static readonly Random rnd = new Random();

		public bool Emergency
		{
			get { return _emergency; }
			set { _emergency = value; }
		}

		public int EmergencyLocation
		{
			get { return _emergencyLocation; }
			set { _emergencyLocation = value; }
		}


		private void CreateRandomEmergency()
		{
			var randInterval = rnd.Next(8000, 20000);
			_timer = new Timer { Interval = randInterval };
			_timer.Elapsed += Emergency_Timer;
			_timer.AutoReset = true;
			_timer.Start();
		}

		private void Emergency_Timer(object sender, ElapsedEventArgs e)
		{
			_emergency = true;
			_emergencyLocation = rnd.Next(1, 10);

			var randInterval = rnd.Next(8000, 20000);
			_timer.Interval = randInterval;
		}
	}
}
