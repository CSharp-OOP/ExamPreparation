using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const double BiologistOxygen = 70;
        public Biologist(string name) 
            : base(name, BiologistOxygen)
        {
        }

        public override void Breath()
        {
            base.Breath();
            Oxygen -= 5;
        }
    }
}
