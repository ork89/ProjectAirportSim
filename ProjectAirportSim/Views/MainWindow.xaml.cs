using ProjectAirportSim.ViewModels;
using System.Windows;

namespace ProjectAirportSim.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var airportViewModel = new ViewModels.AirportViewModel();
			this.DataContext = airportViewModel;
		}
	}
}
