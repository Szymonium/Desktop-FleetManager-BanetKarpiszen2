using System;
using System.Reactive;
using System.Threading.Tasks;
using FleetManager.Models;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class VehicleItemViewModel : ViewModelBase
{
    private readonly Vehicle _vehicle;
    private readonly Func<Task> _saveAction;
    private string _message = string.Empty;

    public VehicleItemViewModel(Vehicle vehicle, Func<Task> saveAction)
    {
        _vehicle = vehicle;
        _saveAction = saveAction;

        RefuelCommand = ReactiveCommand.CreateFromTask(RefuelAsync);
        SendInRouteCommand = ReactiveCommand.CreateFromTask(SendInRouteAsync);
        SetAvailableCommand = ReactiveCommand.CreateFromTask(SetAvailableAsync);
        SetServiceCommand = ReactiveCommand.CreateFromTask(SetServiceAsync);
    }

    public string RegistrationNumber => _vehicle.RegistrationNumber;
    public string Name => _vehicle.Name;

    public double FuelLevel
    {
        get => _vehicle.FuelLevel;
        set
        {
            _vehicle.FuelLevel = value;
            this.RaisePropertyChanged();
        }
    }

    public VehicleStatus Status
    {
        get => _vehicle.Status;
        set
        {
            _vehicle.Status = value;
            this.RaisePropertyChanged();
        }
    }

    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public ReactiveCommand<Unit, Unit> RefuelCommand { get; }
    public ReactiveCommand<Unit, Unit> SendInRouteCommand { get; }
    public ReactiveCommand<Unit, Unit> SetAvailableCommand { get; }
    public ReactiveCommand<Unit, Unit> SetServiceCommand { get; }

    private async Task RefuelAsync()
    {
        if (Status == VehicleStatus.InRoute)
        {
            Message = "Nie można tankować pojazdu będącego w trasie.";
            return;
        }

        FuelLevel += 20;
        Message = "Tankowanie pojazdu";
        await _saveAction();
    }

    private async Task SendInRouteAsync()
    {
        if (FuelLevel < 15)
        {
            Message = "Nie można wysłać pojazdu w trasę - paliwo poniżej 15%.";
        }
        
        if (Status == VehicleStatus.Service || Status == VehicleStatus.InRoute)
        {
            Message = "Nie można wysłać pojazdu w trasę - pojazd jest w serwisie/trasie.";
            return;
        }

        Status = VehicleStatus.InRoute;
        FuelLevel -= new Random().Next(20, 65);
        Message = "Pojazd wysłany w trasę.";
        await _saveAction();
    }

    private async Task SetAvailableAsync()
    {
        Status = VehicleStatus.Available;
        Message = "Pojazd dostępny.";
        await _saveAction();
    }

    private async Task SetServiceAsync()
    {
        Status = VehicleStatus.Service;
        Message = "Pojazd wysłany do serwisu.";
        await _saveAction();
    }
    
    public Vehicle GetVehicle() => _vehicle;
}