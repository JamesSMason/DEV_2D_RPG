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
}