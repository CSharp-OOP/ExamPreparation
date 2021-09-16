using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private readonly IList<Character> characterParty;
        private readonly Stack<Item> itemPool;
        public WarController()
        {
            characterParty = new List<Character>();
            itemPool = new Stack<Item>();
        }

        public string JoinParty(string[] args)
        {
            switch (args[0])
            {
                case "Warrior":
                    characterParty.Add(new Warrior(args[1]));
                    break;
                case "Priest":
                    characterParty.Add(new Priest(args[1]));
                    break;
                default:
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, args[0]));
            }
            return string.Format(SuccessMessages.JoinParty, args[1]);
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            switch (itemName)
            {
                case "FirePotion":
                    itemPool.Push(new FirePotion());
                    break;
                case "HealthPotion":
                    itemPool.Push(new HealthPotion());
                    break;
                default:
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }
            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            Character character = characterParty.FirstOrDefault(x=>x.Name == characterName);

            if (character==null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty,characterName));
            }
            if (!itemPool.Any())
            {
                throw new ArgumentException(ExceptionMessages.ItemPoolEmpty);
            }
            var takenItem = itemPool.Pop();
            character.Bag.AddItem(takenItem);
            return string.Format(SuccessMessages.PickUpItem,characterName, takenItem);
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];
            Character character = characterParty.FirstOrDefault(x => x.Name == characterName);
            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }
            Item item = character.Bag.GetItem(itemName);
            character.UseItem(item);

            return string.Format(SuccessMessages.UsedItem, character.Name, item.GetType().Name);
        }

        public string GetStats()
        {
            var sortedCharacters = characterParty
                .OrderByDescending(x => x.IsAlive)
                .ThenByDescending(x => x.Health);

            StringBuilder sb = new StringBuilder();

            foreach (var item in sortedCharacters)
            {
                sb.AppendLine(string.Format(SuccessMessages.CharacterStats,item.Name,item.Health,item.BaseHealth,item.Armor,item.BaseArmor,item.IsAlive ? "Alive" : "Dead"));
            }

            return sb.ToString();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receivername = args[1];
            Character attacker = characterParty.FirstOrDefault(x => x.Name == attackerName);
            Character receiver = characterParty.FirstOrDefault(x => x.Name == receivername);

            if (attacker ==null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty,attackerName));
            }
            if (receiver==null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receivername));
            }

            Warrior warrior = attacker as Warrior;

            if (warrior == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));
            }

            warrior.Attack(receiver);
            bool isDead = receiver.Health == 0;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(SuccessMessages.AttackCharacter, attackerName, receivername,
                attacker.AbilityPoints, receivername, receiver.Health, receiver.BaseHealth,
                receiver.Armor, receiver.BaseHealth));

            if (isDead)
            {
                sb.AppendLine(string.Format(SuccessMessages.AttackKillsCharacter,receivername));
            }

            return sb.ToString();
        }

        public string Heal(string[] args)
        {
            string healerNamae = args[0];
            string receivername = args[1];
            Character healer = characterParty.FirstOrDefault(x => x.Name == healerNamae);
            Character receiver = characterParty.FirstOrDefault(x => x.Name == receivername);

            if (healer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerNamae));
            }
            if (receiver == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receivername));
            }

            Priest priest = healer as Priest;

            if (priest == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerNamae));
            }

            return string.Format(SuccessMessages.HealCharacter,healerNamae,receivername,
                healer.AbilityPoints,receivername,receiver.Health);
        }
    }
}
