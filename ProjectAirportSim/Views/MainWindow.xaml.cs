using ProjectAirportSim.ViewModels;
using System.Windows;

namespace ProjectAirportSim.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		AirportViewModel airportVM;

		public MainWindow()
		{
			InitializeComponent();
			airportVM = new AirportViewModel();
			this.DataContext = airportVM;
		}
	}
}
