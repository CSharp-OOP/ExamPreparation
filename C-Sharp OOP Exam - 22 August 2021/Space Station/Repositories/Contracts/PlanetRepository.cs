using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories.Contracts
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> models;

        public PlanetRepository()
        {
            models = new List<IPlanet>();
            Models = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models { get; private set; }

        public void Add(IPlanet model)
        {
            models.Add(model);
            Models = models;
        }

        public bool Remove(IPlanet model)
        {
            bool removed = models.Remove(model);
            Models = models;

            return removed;
        }

        public IPlanet FindByName(string name)
        {
            return Models.FirstOrDefault(x => x.Name == name);
        }
    }
}
