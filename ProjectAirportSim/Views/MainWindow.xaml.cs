using ProjectAirportSim.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjectAirportSim.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		AirportViewModel airportVM = new AirportViewModel();

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = airportVM;
			
			PlaneVisibility();
		}

		public void PlaneVisibility()
		{
			Image[] imageArr =
			{
				InAir,
				InAir,
				InAir,
				Runway,
				DrivewayLanding,
				Parking6,
				Parking7,
				DrivewayTakeOff,
				TakeOff
			};

			for (int index = 0; index < imageArr.Length; index++)
			{
				bool locationIndex = airportVM.ListOfLocations.Where(l => l.LocationID == index+1).Select( s => s.LocationStatus).FirstOrDefault();
				if (locationIndex)
				{
					imageArr[index].Visibility = Visibility.Visible;
				}
				else
				{
					imageArr[index].Visibility = Visibility.Collapsed;
				}
			}
		}
	}
}
