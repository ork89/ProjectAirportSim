using System;

namespace ArrivalDepartureTimers
{
	public class Timers
	{
		static void Main(string[] args)
		{
			FlightArrivalTimer arrivalTimer = new FlightArrivalTimer();
			FlightDepartureTimer departureTimer = new FlightDepartureTimer();

			arrivalTimer.PlaneArriving();

			Console.ReadKey();
		}
	}
}
