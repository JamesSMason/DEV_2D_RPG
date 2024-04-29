using JSM.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Enemies
{
    public class EnemyHandler : Singleton<EnemyHandler>
    {
        //[SerializeField] private EnemyInfo _defaultEnemy = null;
        [SerializeField] private EnemyInfo[] _allEnemies = null;
        [SerializeField] private List<Enemy> _currentEnemies = new List<Enemy>();

        public List<Enemy> CurrentEnemies => _currentEnemies;

        #region Unity Messages

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Public

        public void GenerateEnemiesByEncounter(Encounter[] encounterArray)
        {
            _currentEnemies.Clear();
            int randomEnemyIndex = Random.Range(0, encounterArray.Length);
            int numEnemies = Random.Range(encounterArray[randomEnemyIndex].MinEnemies, encounterArray[randomEnemyIndex].MaxEnemies + 1);

            for (int i = 0; i < numEnemies; i++)
            {
                string name = encounterArray[randomEnemyIndex].EnemyInfo.EnemyName;
                GenerateEnemyByName(name);
            }
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