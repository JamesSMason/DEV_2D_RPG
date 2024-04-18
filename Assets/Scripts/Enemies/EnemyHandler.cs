using JSM.RPG.Combat;
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
        public CombatVisuals Visuals;

        public Enemy(EnemyInfo enemyInfo)
        {
            EnemyName = enemyInfo.EnemyName;
            InitiativeBonus = enemyInfo.InitiativeBonus;
            ArmorClass = enemyInfo.ArmorClass;
            MaxHP = enemyInfo.MaxHP;
            CurrentHP = enemyInfo.MaxHP;
            HitBonus = enemyInfo.HitBonus;
            NumberOfAttacks = enemyInfo.NumberOfAttacks;
            NumberOfDamageDice = enemyInfo.NumberOfDamageDice;
            DamageDieType = enemyInfo.DamageDieType;
            DamageBonus = enemyInfo.DamageBonus;
            XPValue = enemyInfo.XPValue;
            Visuals = enemyInfo.Visuals;
        }
    }
}