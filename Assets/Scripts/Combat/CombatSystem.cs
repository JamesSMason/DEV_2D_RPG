using JSM.RPG.Enemies;
using JSM.RPG.Party;
using JSM.RPG.Player;
using JSM.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        public static CombatSystem Instance { get; private set; } = null;

        public static Action<CombatEntities> OnPlayerSelectionChanged;
        public static Action OnEnemySelected;

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
            OnPlayerSelectionChanged?.Invoke(_partyCombatants[_currentPlayer]);
            OnEnemySelected?.Invoke();
        }

        #endregion

        #region Public

        public void SelectEnemy(int currentEnemy)
        {
            CombatEntities currentPlayerEntity = _partyCombatants[_currentPlayer];
            currentPlayerEntity.SetTarget(_allCombatants.IndexOf(_enemyCombatants[currentEnemy]));
            currentPlayerEntity.CombatAction = CombatEntities.Action.Attack;
            _currentPlayer++;

            if (_currentPlayer >= _partyCombatants.Count)
            {
                AttackAction(currentPlayerEntity, _allCombatants[currentPlayerEntity.Target]);
            }
            else
            {
                //display battle options for next player character
                Debug.Log("next choice");

                OnEnemySelected?.Invoke();
            }
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
        }

        private void CreateEnemyEntities()
        {
            List<Enemy> enemyParty = EnemyHandler.Instance.CurrentEnemies;
            for (int i = 0; i < enemyParty.Count; i++)
            {
                CombatEntities newEntity = new CombatEntities();
                newEntity.SetEntityValues(enemyParty[i]);
                newEntity.EntityName += $" {i}";

                CombatVisuals tempCombatVisuals = Instantiate(enemyParty[i].Visuals, _enemySpawnPoints[i].position, Quaternion.identity);
                newEntity.Visuals = tempCombatVisuals;

                _allCombatants.Add(newEntity);
                _enemyCombatants.Add(newEntity);
            }
        }

        private void AttackAction(CombatEntities currentAttacker, CombatEntities currentTarget)
        {
            currentAttacker.Visuals.PlayAttackAnimation();

            bool successfulHit = DiceRoller.RollDice(1, 20) + currentAttacker.HitBonus >= currentTarget.ArmorClass;

            if (successfulHit)
            {
                int damage = 0;
                for (int i = 0; i < currentAttacker.NumberOfAttacks; i++)
                {
                    int potentialDamage = DiceRoller.RollDice(currentAttacker.NumberOfDamageDice, currentAttacker.DamageDieType) + currentAttacker.DamageBonus;
                    if (potentialDamage <= 0) {  potentialDamage = 1; }
                    damage += potentialDamage;
                }
                currentTarget.CurrentHP -= damage;
                currentTarget.Visuals.PlayHurtAnimation();
                currentTarget.Visuals.DisplayDamage(damage.ToString());
            }
            else
            {
                currentTarget.Visuals.DisplayDamage("MISS");
            }

            if (currentTarget.CurrentHP <= 0)
            {
                Debug.Log($"{currentTarget.EntityName} is dead.");
            }
        }

        #endregion

        #region Routines

        private IEnumerator AttackRoutine(CombatEntities currentAttacker, CombatEntities currentTarget)
        {
            yield return null;
        }

        #endregion
    }
}