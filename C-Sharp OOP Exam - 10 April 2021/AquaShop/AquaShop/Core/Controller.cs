using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private List<IAquarium> aquariums;
        private DecorationRepository decorations;

        public Controller()
        {
            aquariums = new List<IAquarium>();
            decorations = new DecorationRepository();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium;
            switch (aquariumType)
            {
                case nameof(FreshwaterAquarium):
                    aquarium = new FreshwaterAquarium(aquariumName);
                    break;
                case nameof(SaltwaterAquarium):
                    aquarium = new SaltwaterAquarium(aquariumName);
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded,aquarium.GetType().Name);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;
            switch (decorationType)
            {
                case nameof(Ornament):
                    decoration = new Ornament();
                    break;
                case nameof(Plant):
                    decoration = new Plant();
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            decorations.Add(decoration);
            return string.Format(OutputMessages.SuccessfullyAdded, decoration.GetType().Name);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration desiredDecoration = this.decorations.FindByType(decorationType);

            if (desiredDecoration==null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration,decorationType));
            }
            decorations.Remove(desiredDecoration);
            IAquarium desiredAquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            desiredAquarium.AddDecoration(desiredDecoration);

            return string.Format(OutputMessages.EntityAddedToAquarium,decorationType,aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish;
            IAquarium desiredAquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            var aquariumType = desiredAquarium.GetType().Name;

            switch (fishType)
            {
                case nameof(FreshwaterFish):
                    fish = new FreshwaterFish(fishName,fishSpecies,price);
                    if (aquariumType != nameof(FreshwaterAquarium))
                    {
                        return OutputMessages.UnsuitableWater;
                    }
                    break;
                case nameof(SaltwaterFish):
                    fish = new SaltwaterFish(fishName, fishSpecies, price);
                    if (aquariumType != nameof(SaltwaterAquarium))
                    {
                        return OutputMessages.UnsuitableWater;
                    }
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }
            desiredAquarium.AddFish(fish);
            return string.Format(OutputMessages.EntityAddedToAquarium,fishType,aquariumName);
            
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium desiredAquarium = aquariums.First(x => x.Name == aquariumName);

            desiredAquarium.Feed();

            return string.Format(OutputMessages.FishFed,desiredAquarium.Fish.Count);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium desiredAquarium = aquariums.First(x => x.Name == aquariumName);
            decimal value = desiredAquarium.Fish.Sum(x => x.Price) + desiredAquarium.Decorations.Sum(x => x.Price);

            return string.Format(OutputMessages.AquariumValue,aquariumName, value);
        }
        
        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
