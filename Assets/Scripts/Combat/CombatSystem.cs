using JSM.RPG.Enemies;
using JSM.RPG.Party;
using JSM.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
        public static Action OnShowMenu;
        public static Action OnHideMenus;
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
        private const string WORLD_SCENE = "WorldScene";

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
            DetermineBattleOrder();
            OnPlayerSelectionChanged?.Invoke(_partyCombatants[_currentPlayer]);
            OnShowMenu?.Invoke();
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
                OnShowMenu?.Invoke();
            }
        }

        public void SelectRunAction()
        {
            _state = CombatState.Selection;
            CombatEntities currentPlayerEntity = _partyCombatants[_currentPlayer];
            currentPlayerEntity.Initiative = 0;
            DetermineBattleOrder();
            currentPlayerEntity.CombatAction = CombatEntities.Action.Run;
            //OnHideMenus?.Invoke();
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
                    if (potentialDamage <= 0) { potentialDamage = 1; }
                    damage += potentialDamage;
                }
                currentTarget.ApplyDamage(damage);
                currentTarget.Visuals.DisplayDamage(damage.ToString());

                OnPlayerSelectionChanged?.Invoke(currentTarget);
                OnEnemySelected?.Invoke();
            }
            else
            {
                currentTarget.Visuals.DisplayDamage("MISS");
            }

            SaveHealth();
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

        private void CreatePartyEntities()
        {
            List<PartyMember> currentParty = PartyHandler.Instance.GetAliveParty();
            for (int i = 0; i < currentParty.Count; i++)
            {
                CombatEntities newEntity = new CombatEntities();
                newEntity.SetEntityValues(currentParty[i]);

                CombatVisuals tempCombatVisuals = Instantiate(currentParty[i].CombatVisuals, _partySpawnPoints[i].position, Quaternion.identity);
                newEntity.Visuals = tempCombatVisuals;

                _allCombatants.Add(newEntity);
                _partyCombatants.Add(newEntity);
            }
        }

        private void DetermineBattleOrder()
        {
            _allCombatants.Sort((bi1, bi2) => -bi1.Initiative.CompareTo(bi2.Initiative));
        }

        private int GetRandomTarget(bool isTargetAPlayer)
        {
            List<int> targetIndexList = new List<int>();
            for (int i = 0; i < _allCombatants.Count; i++)
            {
                CombatEntities entity = _allCombatants[i];
                if (entity.IsPlayer == isTargetAPlayer && entity.CurrentHP > 0)
                {
                    targetIndexList.Add(i);
                }
            }
            int randomIndex = Random.Range(0, targetIndexList.Count);
            return targetIndexList[randomIndex];
        }

        private void RemoveDeadCombatabts()
        {
            for (int i = 0; i < _allCombatants.Count; i++)
            {
                if (_allCombatants[i].CurrentHP <= 0)
                {
                    _allCombatants.RemoveAt(i);
                }
            }
        }

        private void SaveHealth()
        {
            for (int i = 0; i < _partyCombatants.Count; ++i)
            {
                PartyHandler.Instance.SaveHealth(i, _partyCombatants[i].CurrentHP);
            }
        }

        #endregion

        #region Routines

        private IEnumerator CombatRoutine()
        {
            OnHideMenus?.Invoke();
            _state = CombatState.Battle;

            for (int i = 0; i < _allCombatants.Count; i++)
            {
                if (_state == CombatState.Battle && _allCombatants[i].CurrentHP > 0)
                {
                    switch (_allCombatants[i].CombatAction)
                    {
                        case CombatEntities.Action.Attack:
                            yield return StartCoroutine(AttackRoutine(i));
                            break;
                        case CombatEntities.Action.Run:
                            yield return StartCoroutine(RunRoutine(_allCombatants[i]));
                            break;
                        default:
                            Debug.Log("No action found");
                            break;
                    }
                }
            }

            RemoveDeadCombatabts();

            if (_state == CombatState.Battle)
            {
                _currentPlayer = 0;
                OnPlayerSelectionChanged?.Invoke(_partyCombatants[_currentPlayer]);
                OnShowMenu?.Invoke();
            }

            yield return null;
        }

        private IEnumerator AttackRoutine(int index)
        {
            if (_allCombatants[index].IsPlayer)
            {
                CombatEntities currentAttacker = _allCombatants[index];
                if (_allCombatants[currentAttacker.Target].CurrentHP <= 0)
                {
                    currentAttacker.SetTarget(GetRandomTarget(false));
                }
                CombatEntities currentTarget = _allCombatants[currentAttacker.Target];

                AttackAction(_allCombatants[index], currentTarget);

                yield return new WaitForSeconds(TURN_DURATION);

                if (currentTarget.CurrentHP <= 0)
                {
                    yield return new WaitForSeconds(TURN_DURATION);

                    _enemyCombatants.Remove(currentTarget);

                    if (_enemyCombatants.Count <= 0)
                    {
                        _state = CombatState.Won;
                        yield return new WaitForSeconds(TURN_DURATION);
                        SceneManager.LoadScene(WORLD_SCENE);
                    }
                }
            }

            if (index < _allCombatants.Count && !_allCombatants[index].IsPlayer)
            {
                CombatEntities currentAttacker = _allCombatants[index];
                currentAttacker.SetTarget(GetRandomTarget(true));
                CombatEntities currentTarget = _allCombatants[currentAttacker.Target];
                AttackAction(currentAttacker, currentTarget);

                yield return new WaitForSeconds(TURN_DURATION);

                if (currentTarget.CurrentHP <= 0)
                {
                    yield return new WaitForSeconds(TURN_DURATION);

                    _partyCombatants.Remove(currentTarget);

                    if (_partyCombatants.Count <= 0)
                    {
                        _state = CombatState.Lost;
                        Debug.Log("YOU ARE DEAD!");
                    }
                }
            }

            yield return null;
        }

        private IEnumerator RunRoutine(CombatEntities combatant)
        {
            int runnersSpeed = 0;
            int chasersSpeed = 0;

            foreach (CombatEntities combatantEntity in _allCombatants)
            {
                if (combatantEntity.IsPlayer == combatant.IsPlayer)
                {
                    if (combatantEntity.Speed > runnersSpeed)
                    {
                        combatantEntity.Speed = runnersSpeed;
                    }
                }
                else
                {
                    if (combatantEntity.Speed > chasersSpeed)
                    {
                        combatantEntity.Speed = chasersSpeed;
                    }
                }
            }
            runnersSpeed += DiceRoller.RollDice(1, 20);
            chasersSpeed += DiceRoller.RollDice(1, 20);

            if (_state == CombatState.Battle)
            {
                if (runnersSpeed > chasersSpeed)
                {
                    combatant.Visuals.DisplayDamage("ESCAPED");
                    _state = CombatState.Run;
                    _allCombatants.Clear();
                    yield return new WaitForSeconds(TURN_DURATION);
                    SceneManager.LoadScene(WORLD_SCENE);
                    yield break;
                }
                else
                {
                    combatant.Visuals.DisplayDamage("CAUGHT");
                    yield return new WaitForSeconds(TURN_DURATION);
                }
            }
        }

        #endregion
    }
}