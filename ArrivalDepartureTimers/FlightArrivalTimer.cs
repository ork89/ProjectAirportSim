using ProjectAirportSim.ViewModels;
using System;
using System.Timers;

namespace ArrivalDepartureTimers
{
	public class FlightArrivalTimer
	{
		Timer timer;
		AirportViewModel airportVM = new AirportViewModel();
		private const string _alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		public void PlaneArriving()
		{
			timer = new Timer();
			timer.Interval = 5000;
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;

			timer.Start();
		}

		private void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			var planeName = GenerateNewFlightName();
			var arrivalTime = e.SignalTime;

			Console.WriteLine("Flight " + planeName + " Approaching Airport " + arrivalTime);
			airportVM.CreateNewPlaneInDB(planeName, arrivalTime);
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
