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

			ControlPlaneImageVisibility();
		}

		private void ControlPlaneImageVisibility()
		{
			var locations = airportVM.GetLocations();

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
				Departure
			};

			for (int index = 0; index < imageArr.Length; index++)
			{
				if (locations[index])
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
