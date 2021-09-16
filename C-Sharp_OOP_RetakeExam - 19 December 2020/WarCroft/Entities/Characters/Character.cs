using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private readonly double baseHealth;
        private readonly double baseArmor;
        protected readonly double abilityPoints;

        private string name;
        private double health;
        private double armor;

        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            baseHealth = health;
            Health = health;
            baseArmor = armor;
            Armor = armor;
            this.abilityPoints = abilityPoints;
            Bag = bag;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }
            }
        }

        public double Health
        {
            get => health;
            set
            {
                if (value <0)
                {
                    health = 0;
                }
                else if (value>baseHealth)
                {
                    health = baseHealth;
                }
                else
                {
                    health = value;
                }
            }
        }
        public double BaseHealth { get; }
        public double Armor
        {
            get => armor;
            private set
            {
                if (value < 0)
                {
                    armor = 0;
                }
                else
                {
                    armor = value;
                }
            }
        }

        public double BaseArmor { get; }
        public double AbilityPoints { get; }

        public IBag Bag { get; }
        public bool IsAlive { get; set; } = true;

        public virtual void TakeDamage(double hitPoints) {
            EnsureAlive();
            double healthReduce = hitPoints - Armor;
            Armor -= hitPoints;

            if (healthReduce>0)
            {
                Health -= healthReduce;
            }

            if (Health==0)
            {
                IsAlive = false;
            }
        }

        public virtual void UseItem(Item item) {
            EnsureAlive();
            item.AffectCharacter(this);
        }
        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
    }
}