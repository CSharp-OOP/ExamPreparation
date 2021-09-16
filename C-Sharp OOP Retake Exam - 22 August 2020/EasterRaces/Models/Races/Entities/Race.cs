using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private const int MinNameValue = 5;
        private const int MinLaps = 1;

        private string name;
        private int laps;

        private readonly IDictionary<string, Drivers.Contracts.IDriver> driversByName;

        public Race(string name, int laps)
        {
            Name = name;
            Laps = laps;
            driversByName = new Dictionary<string, Drivers.Contracts.IDriver>();
        }
        public string Name
        {
            get => name;
            private set
            {
                Validator.ThrowIfStringIsNullEmptyOrLessThan(value, MinNameValue, string
                    .Format(ExceptionMessages.InvalidName, value, MinNameValue));
            }
        }

        public int Laps
        {
            get => laps;
            private set
            {
                if (value < MinLaps)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, MinLaps));
                }
                laps = value;
            }
        }

        public IReadOnlyCollection<Drivers.Contracts.IDriver> Drivers => driversByName.Values.ToList();

        public void AddDriver(Drivers.Contracts.IDriver driver)
        {
            if (driver is null)
            {
                throw new ArgumentException(ExceptionMessages.DriverInvalid);
            }

            if (!driver.CanParticipate)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }

            if (driversByName.ContainsKey(driver.Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, Name));
            }

            driversByName.Add(driver.Name, driver);
        }
    }
}
