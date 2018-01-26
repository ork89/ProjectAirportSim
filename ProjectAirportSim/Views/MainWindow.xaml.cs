using ProjectAirportSim.ViewModels;
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

		private void PlaneVisibility()
		{
			var locations = airportVM.ListOfLocations;
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

			foreach (var item in locations)
			{
				if (item.LocationStatus)
				{
					imageArr[item.LocationID].Visibility = Visibility.Visible;
				}
				else
				{
					imageArr[item.LocationID].Visibility = Visibility.Collapsed;
				}
			}
		}
	}
}
