using JSM.RPG.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Enemies
{
    public class EnemyHandler : MonoBehaviour
    {
        public static EnemyHandler Instance { get; private set; } = null;

        [SerializeField] private EnemyInfo _defaultEnemy = null;
        [SerializeField] private EnemyInfo[] _allEnemies = null;
        [SerializeField] private List<Enemy> _currentEnemies = new List<Enemy>();

        public List<Enemy> CurrentEnemies => _currentEnemies;

        #region Unity Messages

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            GenerateEnemyByName(_defaultEnemy.EnemyName);
            GenerateEnemyByName(_defaultEnemy.EnemyName);
            GenerateEnemyByName(_defaultEnemy.EnemyName);
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
            Visuals = enemy.Visuals;
        }
    }
}