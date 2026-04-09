using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IVehicleService _vehicleService;
    private string _systemStatus = "Ładowanie...";
    private bool _isEmergencyMode;

    public MainWindowViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();

    public string SystemStatus
    {
        get => _systemStatus;
        set => this.RaiseAndSetIfChanged(ref _systemStatus, value);
    }

    public bool IsEmergencyMode
    {
        get => _isEmergencyMode;
        set => this.RaiseAndSetIfChanged(ref _isEmergencyMode, value);
    }

    public async Task InitializeAsync()
    {
        try
        {
            var vehicles = await _vehicleService.LoadVehiclesAsync();
            Vehicles.Clear();

            foreach (var vehicle in vehicles)
                Vehicles.Add(new VehicleItemViewModel(vehicle, SaveAsync));

            IsEmergencyMode = false;
            SystemStatus = "System działa poprawnie.";
        }
        catch
        {
            IsEmergencyMode = true;
            SystemStatus = "Tryb awaryjny - uruchomiono dane zastępcze.";
        }
    }

    public async Task SaveAsync()
    {
        try
        {
            var rawVehicles = Vehicles.Select(v => v.GetVehicle()).ToList();
            await _vehicleService.SaveVehiclesAsync(rawVehicles);
            SystemStatus = IsEmergencyMode ? "Tryb awaryjny aktywny." : "Zmiany zapisane.";
        }
        catch
        {
            IsEmergencyMode = true;
            SystemStatus = "Błąd zapisu - aktywowano tryb awaryjny.";
        }
    }
}