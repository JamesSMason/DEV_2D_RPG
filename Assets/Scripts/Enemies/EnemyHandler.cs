using JSM.RPG.Party;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Enemies
{
    public class EnemyHandler : MonoBehaviour
    {
        [SerializeField] private EnemyInfo[] _allEnemies = null;
        [SerializeField] private List<Enemy> _currentEnemies = new List<Enemy>();

        #region Unity Messages

        private void Awake()
        {
            GenerateEnemyByName("Goblin");
        }

        #endregion
        #region Private

        private void GenerateEnemyByName(string enemyName)
        {
            foreach (EnemyInfo enemy in _allEnemies)
            {
                if (enemy.EnemyName == enemyName)
                {
                    Enemy newEnemy = new Enemy(enemy);
                    _currentEnemies.Add(newEnemy);
                    break;
                }
            }
        }

        #endregion
    }

    [Serializable]
    public class Enemy
    {
        public string EnemyName;
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Intelligence;
        public int Wisdom;
        public int Charisma;
        public int ArmorClass;
        public int MaxHP;
        public int CurrentHP;
        public int DamageDieType;
        public int NumberOfDamageDice;
        public int XPValue;
        public int ProficiencyBonus;
        public int NumberOfAttacks;
        public int Initiative;
        public GameObject Visuals;

        public Enemy(EnemyInfo stats)
        {
            EnemyName = stats.EnemyName;
            Strength = stats.Strength;
            Dexterity = stats.Dexterity;
            Constitution = stats.Constitution;
            Intelligence = stats.Intelligence;
            Wisdom = stats.Wisdom;
            Charisma = stats.Charisma;
            ArmorClass = stats.ArmorClass;
            MaxHP = stats.MaxHP;
            CurrentHP = stats.MaxHP;
            DamageDieType = stats.DamageDieType;
            NumberOfDamageDice = stats.NumberOfDamageDice;
            XPValue = stats.XPValue;
            ProficiencyBonus = stats.ProficiencyBonus;
            NumberOfAttacks = stats.NumberOfAttacks;
            Visuals = stats.Visuals;
        }
    }
}