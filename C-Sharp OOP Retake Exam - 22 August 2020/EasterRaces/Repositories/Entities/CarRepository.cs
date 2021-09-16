using System;
using System.Linq;
using EasterRaces.Utilities;
using System.Collections.Generic;
using EasterRaces.Utilities.Messages;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly IDictionary<string, ICar> carByModel;

        public CarRepository()
        {
            carByModel = new Dictionary<string, ICar>();
        }
        public void Add(ICar model)
        {
            if (carByModel.ContainsKey(model.Model))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model.Model));
            }

            carByModel.Add(model.Model, model);
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return carByModel.Values.ToList();
        }

        public ICar GetByName(string name)
        {
            return carByModel.GetByKeyOrDefault(name);
        }

        public bool Remove(ICar model)
        {
            return carByModel.Remove(model.Model);
        }
    }
}
