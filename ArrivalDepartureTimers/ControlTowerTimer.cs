using ProjectAirportSim.BL;
using System.Timers;


namespace ArrivalDepartureTimers
{
	public class ControlTowerTimer
	{
		Timer timer;
		ControlTower _tower = new ControlTower();

		public void StartContorlTowerManager()
		{
			timer = new Timer { Interval = 5000 };
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;
			timer.Start();
		}

		private void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			_tower.ControlTowerManager();
		}
	}
}
