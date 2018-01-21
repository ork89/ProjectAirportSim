using ProjectAirportSim.ViewModels;
using System.Windows;

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
		}
	}
}
