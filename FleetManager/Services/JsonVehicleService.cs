using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public class JsonVehicleService : IVehicleService
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    public JsonVehicleService(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<IList<Vehicle>> LoadVehiclesAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
                return GetFallbackVehicles();

            var json = await File.ReadAllTextAsync(_filePath);

            if (string.IsNullOrWhiteSpace(json))
                return GetFallbackVehicles();

            var data = JsonSerializer.Deserialize<List<Vehicle>>(json, _jsonOptions);

            return data ?? GetFallbackVehicles();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return GetFallbackVehicles();
        }
    }

    public async Task SaveVehiclesAsync(IEnumerable<Vehicle> vehicles)
    {
        try
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(vehicles, _jsonOptions);

            await File.WriteAllTextAsync(_filePath, json);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static IList<Vehicle> GetFallbackVehicles()
    {
        return new List<Vehicle>
        {
            new() { RegistrationNumber = "VH-001", Name = "Rover Alpha", FuelLevel = 75, Status = VehicleStatus.Available },
            new() { RegistrationNumber = "VH-002", Name = "Rover Beta", FuelLevel = 40, Status = VehicleStatus.Service },
            new() { RegistrationNumber = "VH-003", Name = "Rover Gamma", FuelLevel = 10, Status = VehicleStatus.Available }
        };
    }
}