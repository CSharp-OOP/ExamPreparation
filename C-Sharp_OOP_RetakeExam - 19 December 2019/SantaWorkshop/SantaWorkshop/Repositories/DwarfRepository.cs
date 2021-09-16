using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Repositories
{
    public class DwarfRepository : IRepository<IDwarf>
    {
        private readonly List<IDwarf> models;

        public DwarfRepository()
        {
            models = new List<IDwarf>();
        }

        public IReadOnlyCollection<IDwarf> Models
            => (IReadOnlyCollection<IDwarf>)models;

        public void Add(IDwarf model)
       => models.Add(model);

        public bool Remove(IDwarf model) =>
            models.Remove(model);

        public IDwarf FindByName(string name) =>
            models.FirstOrDefault(x=>x.Name==name);
    }
}
