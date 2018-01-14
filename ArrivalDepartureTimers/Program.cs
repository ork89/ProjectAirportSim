using System;

namespace ArrivalDepartureTimers
{
	public class Program
	{
		static void Main(string[] args)
		{
			FlightArrivalTimer arrivalTimer = new FlightArrivalTimer();
			FlightDepartureTimer departureTimer = new FlightDepartureTimer();

			arrivalTimer.PlaneArriving();
			//departureTimer.PlaneDepaturing();

			Console.ReadKey();
		}
	}
}
