using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead || enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            ModifyBeginnerPlayer(attackPlayer);
            ModifyBeginnerPlayer(enemyPlayer);

            attackPlayer = BoostPlayerHealth(attackPlayer);
            enemyPlayer = BoostPlayerHealth(enemyPlayer);

            while (true)
            {
                var attacherAttackPoints = attackPlayer.CardRepository
                    .Cards
                    .Select(c => c.DamagePoints)
                    .Sum();

                enemyPlayer.TakeDamage(attacherAttackPoints);

                if (enemyPlayer.IsDead)
                {
                    break;
                }

                var enemyPlayerAttackPoint = enemyPlayer.CardRepository
                    .Cards
                    .Select(c => c.DamagePoints)
                    .Sum();

                attackPlayer.TakeDamage(enemyPlayerAttackPoint);

                if (attackPlayer.IsDead)
                {
                    break;
                }
            }
        }

        private IPlayer BoostPlayerHealth(IPlayer player)
        {
            player.Health += player.CardRepository
                            .Cards.Select(c => c.HealthPoints)
                            .Sum();

            return player;
        }

        private static void ModifyBeginnerPlayer(IPlayer player)
        {
            if (player.GetType().Name == "Beginner")
            {
                player.Health += 40;
                foreach (var card in player.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }
        }
    }
}
