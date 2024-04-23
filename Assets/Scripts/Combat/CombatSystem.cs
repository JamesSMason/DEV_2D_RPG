using JSM.RPG.Enemies;
using JSM.RPG.Party;
using JSM.RPG.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        public static CombatSystem Instance { get; private set; } = null;

        public static Action OnPlayerSelectionChanged;

        [Header("Spawn Points")]
        [SerializeField] private List<Transform> _partySpawnPoints = new List<Transform>();
        [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();
        [Header("Combatants")]
        [SerializeField] List<CombatEntities> _allCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _partyCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _enemyCombatants = new List<CombatEntities>();

        private int _currentPlayer = 0;

        public int EnemyCount => _enemyCombatants.Count;
        public string EnemyName(int i) => _enemyCombatants[i].EntityName;
        public CombatEntities PlayerStats => _partyCombatants[_currentPlayer];

        #region Unity Messages

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            CreatePartyEntities();
            CreateEnemyEntities();
        }

        #endregion

        #region Private

        private void CreatePartyEntities()
        {
            List<PlayerStats> currentParty = PartyHandler.Instance.CurrentParty;
            for (int i = 0; i < currentParty.Count; i++)
            {
                CombatEntities newEntity = new CombatEntities();
                newEntity.SetEntityValues(currentParty[i]);

                CombatVisuals tempCombatVisuals = Instantiate(currentParty[i].Visuals, _partySpawnPoints[i].position, Quaternion.identity);
                newEntity.Visuals = tempCombatVisuals;

                _allCombatants.Add(newEntity);
                _partyCombatants.Add(newEntity);
            }
            OnPlayerSelectionChanged();
        }

        private void CreateEnemyEntities()
        {
            List<Enemy> enemyParty = EnemyHandler.Instance.CurrentEnemies;
            for (int i = 0; i < enemyParty.Count; i++)
            {
                CombatEntities newEntity = new CombatEntities();
                newEntity.SetEntityValues(enemyParty[i]);

                CombatVisuals tempCombatVisuals = Instantiate(enemyParty[i].Visuals, _enemySpawnPoints[i].position, Quaternion.identity);
                newEntity.Visuals = tempCombatVisuals;

                _allCombatants.Add(newEntity);
                _enemyCombatants.Add(newEntity);
            }
        }

        #endregion
    }
}