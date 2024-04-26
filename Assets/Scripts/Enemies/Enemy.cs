using JSM.RPG.Combat;
using System;

namespace JSM.RPG.Enemies
{
    [Serializable]
    public class Enemy
    {
        public string EnemyName;
        public int InitiativeBonus;
        public int ArmorClass;
        public int MaxHP;
        public int CurrentHP;
        public int HitBonus;
        public int NumberOfAttacks;
        public int NumberOfDamageDice;
        public int DamageDieType;
        public int DamageBonus;
        public int XPValue;
        public int Speed;
        public CombatVisuals Visuals;

        public Enemy(EnemyInfo enemy)
        {
            EnemyName = enemy.EnemyName;
            InitiativeBonus = enemy.InitiativeBonus;
            ArmorClass = enemy.ArmorClass;
            MaxHP = enemy.MaxHP;
            CurrentHP = enemy.MaxHP;
            HitBonus = enemy.HitBonus;
            NumberOfAttacks = enemy.NumberOfAttacks;
            NumberOfDamageDice = enemy.NumberOfDamageDice;
            DamageDieType = enemy.DamageDieType;
            DamageBonus = enemy.DamageBonus;
            XPValue = enemy.XPValue;
            Speed = enemy.Speed;
            Visuals = enemy.Visuals;
        }
    }
}