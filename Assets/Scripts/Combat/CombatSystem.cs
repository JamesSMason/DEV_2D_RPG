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
        [Serializable]
        private enum CombatState
        {
            Start,
            Selection,
            Battle,
            Won,
            Lost,
            Run,
        }

        public static CombatSystem Instance { get; private set; } = null;

        public static Action<CombatEntities> OnPlayerSelectionChanged;
        public static Action OnEnemySelected;

        [Header("Combat State")]
        [SerializeField] private CombatState _state = CombatState.Start;
        [Header("Spawn Points")]
        [SerializeField] private List<Transform> _partySpawnPoints = new List<Transform>();
        [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();
        [Header("Combatants")]
        [SerializeField] List<CombatEntities> _allCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _partyCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _enemyCombatants = new List<CombatEntities>();

        private int _currentPlayer = 0;

        private const float TURN_DURATION = 2.0f;

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
                StartCoroutine(CombatRoutine());
            }
            else
            {
                OnEnemySelected?.Invoke();
                OnPlayerSelectionChanged?.Invoke(_partyCombatants[_currentPlayer]);
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
                currentTarget.ApplyDamage(damage);
                currentTarget.Visuals.DisplayDamage(damage.ToString());
            }
            else
            {
                currentTarget.Visuals.DisplayDamage("MISS");
            }
        }

        #endregion

        #region Routines

        private IEnumerator CombatRoutine()
        {
            OnEnemySelected?.Invoke();
            _state = CombatState.Battle;

            foreach (CombatEntities combatEntity in _allCombatants)
            {
                switch (combatEntity.CombatAction)
                {
                    case CombatEntities.Action.Attack:
                        yield return StartCoroutine(AttackRoutine(combatEntity));
                        break;
                    case CombatEntities.Action.Run:
                        Debug.Log($"{combatEntity.EntityName} is running");
                        break;
                    default:
                        Debug.Log("No action found");
                        break;
                }
            }

            if (_state == CombatState.Battle)
            {
                _currentPlayer = 0;
                OnPlayerSelectionChanged?.Invoke(_partyCombatants[_currentPlayer]);
            }

            yield return null;
        }

        private IEnumerator AttackRoutine(CombatEntities currentAttacker)
        {
            if (currentAttacker.IsPlayer)
            {
                CombatEntities currentTarget = _allCombatants[currentAttacker.Target];
                AttackAction(currentAttacker, currentTarget);

                yield return new WaitForSeconds(TURN_DURATION);

                if (currentTarget.CurrentHP <= 0)
                {
                    Debug.Log($"You have killed {currentTarget.EntityName}!");

                    yield return new WaitForSeconds(TURN_DURATION);

                    _allCombatants.Remove(currentTarget);
                    _enemyCombatants.Remove(currentTarget);

                    if (_enemyCombatants.Count <= 0)
                    {
                        _state = CombatState.Won;
                        Debug.Log("You have won!");
                    }
                }
            }

            yield return null;
        }

        #endregion
    }
}