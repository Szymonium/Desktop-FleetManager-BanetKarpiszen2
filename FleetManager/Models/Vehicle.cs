using System;
using ReactiveUI;

namespace FleetManager.Models;

public class Vehicle : ReactiveObject
{
    private string _id = string.Empty;
    private string _name = string.Empty;
    private double _fuelLevel;
    private VehicleStatus _status;

    public string Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public double FuelLevel
    {
        get => _fuelLevel;
        set => this.RaiseAndSetIfChanged(ref _fuelLevel, Math.Clamp(value, 0, 100));
    }

    public VehicleStatus Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
}