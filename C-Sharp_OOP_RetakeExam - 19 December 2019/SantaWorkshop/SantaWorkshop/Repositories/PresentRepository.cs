using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SantaWorkshop.Repositories
{
    public class PresentRepository : IRepository<IPresent>
    {
        private readonly List<IPresent> models;

        public PresentRepository()
        {
            models = new List<IPresent>();
        }

        public IReadOnlyCollection<IPresent> Models
            => models;

        public void Add(IPresent model)
            => models.Add(model);

        public bool Remove(IPresent model)
        => models.Remove(model);

        public IPresent FindByName(string name)
        => models.FirstOrDefault(x => x.Name == name);
    }
}
