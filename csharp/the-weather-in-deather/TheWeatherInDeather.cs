using System;
using System.Collections.Generic;

public class WeatherStation
{
    private Reading reading;
    private List<DateTime> recordDates = new List<DateTime>();
    private List<decimal> temperatures = new List<decimal>();

    public void AcceptReading(Reading reading)
    {
        this.reading = reading;
        recordDates.Add(DateTime.Now);
        temperatures.Add(reading.Temperature);
    }

    public void ClearAll()
    {
        reading = new Reading();
        recordDates.Clear();
        temperatures.Clear();
    }

    public decimal LatestTemperature
    {
        get
        {
            return reading.Temperature;
        }
    }

    public decimal LatestPressure
    {
        get
        {
            return reading.Pressure;
        }
    }

    public decimal LatestRainfall
    {
        get
        {
            return reading.Rainfall;
        }
    }

    public bool HasHistory
    {
        get
        {
            return recordDates.Count > 1 ? true : false;
        }
    }

    public Outlook ShortTermOutlook
    {
        get
        {
            return reading.Equals(new Reading())
                ? throw new ArgumentException()
                : reading.Temperature switch
                    {
                        < 30m when reading.Pressure < 10m => Outlook.Cool,
                        > 50 => Outlook.Good,
                        _ => Outlook.Warm
                    };
        }
    }

    public Outlook LongTermOutlook
    {
        get
        {
            if (reading.WindDirection == WindDirection.Southerly
                || reading.WindDirection == WindDirection.Easterly
                && reading.Temperature > 20)
            {
                return Outlook.Good;
            }
            if (reading.WindDirection == WindDirection.Northerly)
            {
                return Outlook.Cool;
            }
            if (reading.WindDirection == WindDirection.Easterly
                && reading.Temperature <= 20)
            {
                return Outlook.Warm;
            }
            if (reading.WindDirection == WindDirection.Westerly)
            {
                return Outlook.Rainy;
            }
            throw new ArgumentException();
        }
    }

    public State RunSelfTest()
    {
        if (reading.Equals(new Reading()))
        {
            return State.Bad;
        }
        else
        {
            return State.Good;
        }
    }
}

/*** Please do not modify this struct ***/
public struct Reading
{
    public decimal Temperature { get; }
    public decimal Pressure { get; }
    public decimal Rainfall { get; }
    public WindDirection WindDirection { get; }

    public Reading(decimal temperature, decimal pressure,
        decimal rainfall, WindDirection windDirection)
    {
        Temperature = temperature;
        Pressure = pressure;
        Rainfall = rainfall;
        WindDirection = windDirection;
    }
}

/*** Please do not modify this enum ***/
public enum State
{
    Good,
    Bad
}

/*** Please do not modify this enum ***/
public enum Outlook
{
    Cool,
    Rainy,
    Warm,
    Good
}

/*** Please do not modify this enum ***/
public enum WindDirection
{
    Unknown, // default
    Northerly,
    Easterly,
    Southerly,
    Westerly
}
