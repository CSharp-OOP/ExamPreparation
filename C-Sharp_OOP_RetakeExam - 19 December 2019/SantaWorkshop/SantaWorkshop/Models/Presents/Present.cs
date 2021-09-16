using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Utilities.Messages;
using System;

namespace SantaWorkshop.Models.Presents
{
    public class Present : IPresent
    {
        private string name;
        private int energyRequired;

        public Present(string name, int energyRequired)
        {
            Name = name;
            EnergyRequired = energyRequired;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPresentName);
                }
                name = value;
            }
        }
        public int EnergyRequired
        {
            get => energyRequired;
            private set
            {
                energyRequired = value > 0 ? value : 0;
            }
        }

        public void GetCrafted()
        {
            this.EnergyRequired -= 10;
        }

        public bool IsDone() => EnergyRequired == 0;
    }
}
