using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products
{
    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;
        public Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance
        {
            get
            {
                if (!this.Components.Any())
                {
                    return base.OverallPerformance;
                }
                var componentsAveragePerformance = this.Components.Any() ? this.Components.Average(c => c.OverallPerformance) : 0;

                return base.OverallPerformance + componentsAveragePerformance;
            }
        }

        public override decimal Price =>
            base.Price + (Components.Any() ? Components.Sum(x => x.Price) : 0) + (Peripherals.Any() ? Peripherals.Sum(x => x.Price) : 0);

        public IReadOnlyCollection<IComponent> Components => this.components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals.AsReadOnly();

        public void AddComponent(IComponent component)
        {
            string componentType = component
                   .GetType()
                   .Name;

            if (this.Components.Any(c => c.GetType().Name == componentType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, componentType, this.GetType().Name, this.Id));
            }

            this.components.Add(component);
        }
        public IComponent RemoveComponent(string componentType)
        {
            var wantedComponent = Components.FirstOrDefault(x => x.GetType().Name == componentType);

            if (wantedComponent == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, Id));
            }
            components.Remove(wantedComponent);
            return wantedComponent;
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            string wantedPeripheral = Peripherals.GetType().Name;

            if (Peripherals.Any(p => p.GetType().Name == wantedPeripheral))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, wantedPeripheral, this.GetType().Name, this.Id));
            }
        }


        public IPeripheral RemovePeripheral(string peripheralType)
        {

            if (this.Peripherals.All(c => c.GetType().Name != peripheralType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }

            IPeripheral peripheral = this.Peripherals
                .First(c => c.GetType().Name == peripheralType);

            this.peripherals.Remove(peripheral);
            return peripheral;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(base.ToString());
            stringBuilder.AppendLine(" " + String.Format(SuccessMessages.ComputerComponentsToString, this.Components.Count));
            foreach (IComponent component in this.Components)
            {
                stringBuilder.AppendLine("  " + component.ToString());
            }

            stringBuilder.AppendLine(" " + String.Format(SuccessMessages.ComputerPeripheralsToString, this.Peripherals.Count,
                this.Peripherals.Any() ? this.Peripherals.Average(p => p.OverallPerformance) : 0));
            foreach (var peripheral in this.Peripherals)
            {
                stringBuilder.AppendLine("  " + peripheral.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
