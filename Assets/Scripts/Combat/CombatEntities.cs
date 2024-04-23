using JSM.RPG.Enemies;
using JSM.RPG.Player;
using JSM.Utilities;
using System;

namespace JSM.RPG.Combat
{
    [Serializable]
    public class CombatEntities
    {
        public string EntityName;
        public int Initiative;
        public int ArmorClass;
        public int MaxHP;
        public int CurrentHP;
        public int HitBonus;
        public int NumberOfAttacks;
        public int NumberOfDamageDice;
        public int DamageDieType;
        public int DamageBonus;
        public bool IsPlayer;
        public CombatVisuals Visuals;

        public void SetEntityValues(PlayerStats stats)
        {
            EntityName = stats.MemberName;
            Initiative = DiceRoller.RollDice(1, 20) + stats.InitiativeBonus;
            ArmorClass = stats.ArmorClass;
            MaxHP = stats.MaxHP;
            CurrentHP = stats.CurrentHP;
            HitBonus = stats.HitBonus;
            NumberOfAttacks = stats.NumberOfAttacks;
            NumberOfDamageDice = stats.NumberOfDamageDice;
            DamageDieType = stats.DamageDieType;
            DamageBonus = stats.DamageBonus;
            IsPlayer = true;
        }

        public void SetEntityValues(Enemy stats)
        {
            EntityName = stats.EnemyName;
            Initiative = DiceRoller.RollDice(1, 20) + stats.InitiativeBonus;
            ArmorClass = stats.ArmorClass;
            MaxHP = stats.MaxHP;
            CurrentHP = stats.MaxHP;
            HitBonus = stats.HitBonus;
            NumberOfAttacks = stats.NumberOfAttacks;
            NumberOfDamageDice = stats.NumberOfDamageDice;
            DamageDieType = stats.DamageDieType;
            DamageBonus = stats.DamageBonus;
            IsPlayer = false;
        }
    }
}