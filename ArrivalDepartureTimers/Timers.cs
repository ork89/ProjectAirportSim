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
			//Run the console application alongside the WPF window
			MainWindow _window = new MainWindow();
			Application _app = new Application();

			FlightArrivalTimer arrivalTimer = new FlightArrivalTimer();
			FlightDepartureTimer departureTimer = new FlightDepartureTimer();
			ControlTowerTimer controlTower = new ControlTowerTimer();

			arrivalTimer.PlaneArriving();
			departureTimer.PlaneDepaturing();
			controlTower.StartContorlTowerManager();
			
			_app.Run(_window);
		}
	}
}
