using Avalonia.Controls;
using Avalonia.Interactivity;
using FleetManager.ViewModels;

namespace FleetManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override async void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        
        // załadowanie danych z pliku JSON za pomocą metody LoadVehiclesAsync() gdy MainWindow zostanie załadowane
        if (DataContext is MainWindowViewModel viewModel)
        {
            await viewModel.LoadVehiclesAsync();
        }
    }
}