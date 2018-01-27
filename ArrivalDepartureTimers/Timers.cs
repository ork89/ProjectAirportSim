using ProjectAirportSim.Views;
using System;
using System.Windows;

namespace ArrivalDepartureTimers
{
	public class Timers
	{
		[STAThread]
		static void Main(string[] args)
		{
			MainWindow _window = new MainWindow();
			Application _app = new Application();

			FlightArrivalTimer arrivalTimer = new FlightArrivalTimer();
			FlightDepartureTimer departureTimer = new FlightDepartureTimer();
			arrivalTimer.PlaneArriving();
			departureTimer.PlaneDepaturing();

			_app.Run(_window);
		}
	}
}
