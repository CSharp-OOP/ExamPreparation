using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private List<IAstronaut> models;

        public AstronautRepository()
        {
            models = new List<IAstronaut>();
            Models = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models { get; private set; }

        public void Add(IAstronaut model)
        {
            models.Add(model);
            Models = models;
        }

        public bool Remove(IAstronaut model)
        {
            bool removed = models.Remove(model);
            Models = models;

            return removed;
        }

        public IAstronaut FindByName(string name)
        {
            return Models.FirstOrDefault(x => x.Name == name);
        }

    }
}
