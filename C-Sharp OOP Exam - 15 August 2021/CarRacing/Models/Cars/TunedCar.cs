using System;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double fuelAvailable = 65;
        private const double fuelConsumptionPerRace = 7.5;

        public TunedCar(string make, string model, string vIN, int horsePower)
            : base(make, model, vIN, horsePower, fuelAvailable, fuelConsumptionPerRace)
        {
        }

        public override void Drive()
        {
            base.Drive();

            this.HorsePower = (int)Math.Round(HorsePower * 0.97);
        }
    }
}