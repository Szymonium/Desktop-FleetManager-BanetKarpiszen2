using System;
using ReactiveUI;

namespace FleetManager.Models;

public class Vehicle : ReactiveObject
{
    private string _registration = string.Empty;
    private string _name = string.Empty;
    private double _fuelLevel;
    private VehicleStatus _status;

    public string RegistrationNumber
    {
        get => _registration;
        set => this.RaiseAndSetIfChanged(ref _registration, value);
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