using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public class JsonVehicleService : IVehicleService
{
    private readonly string _filePath;

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

            var data = JsonSerializer.Deserialize<List<Vehicle>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data?.Count > 0 ? data : GetFallbackVehicles();
        }
        catch
        {
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

            var json = JsonSerializer.Serialize(vehicles, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(_filePath, json);
        }
        catch(Exception err)
        {
            Console.WriteLine($"Błąd podczas zapisywania pliku:\n{err.Message}");
            throw;
        }
    }

    private static IList<Vehicle> GetFallbackVehicles()
    {
        return new List<Vehicle>
        {
            new() { Id = "VH-001", Name = "Rover Alpha", FuelLevel = 75, Status = VehicleStatus.Available },
            new() { Id = "VH-002", Name = "Rover Beta", FuelLevel = 40, Status = VehicleStatus.Service },
            new() { Id = "VH-003", Name = "Rover Gamma", FuelLevel = 10, Status = VehicleStatus.Available }
        };
    }
}