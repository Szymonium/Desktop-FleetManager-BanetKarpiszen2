using System.Collections.Generic;
using System.Threading.Tasks;
using FleetManager.Models;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private JsonVehicleService _vehicleService;
    private IList<Vehicle> _allVehicles;
    
    public MainWindowViewModel()
    {
        // utworzenie instancji JsonVehicleService z użyciem pliku vehicles.json
        _vehicleService = new JsonVehicleService("vehicles.json");
    }
    

    // pola z użyciem RaiseAndSetIfChanged()
    public Vehicle? Vehicle1
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle2
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle3
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle4
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle5
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle6
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle7
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle8
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Vehicle? Vehicle9
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }


    // wywołanie metody serwisu LoadVehiclesAsync() i użycie await do wyniku
    public async Task LoadVehiclesAsync()
    {
        _allVehicles = await _vehicleService.LoadVehiclesAsync();
        
        // TODO – compile error
        
        Vehicle1 = _allVehicles[0];
        Vehicle2 = _allVehicles[1];
        Vehicle3 = _allVehicles[2];
        Vehicle4 = _allVehicles[3];
        Vehicle5 = _allVehicles[4];
        Vehicle6 = _allVehicles[5];
        Vehicle7 = _allVehicles[6];
        Vehicle8 = _allVehicles[7];
        Vehicle9 = _allVehicles[8];
    }
}