using JSM.RPG.Enemies;
using JSM.RPG.Party;
using JSM.RPG.Player;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace JSM.RPG.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        //Adding this session
        [Header("UI")]
        [SerializeField] private GameObject[] _enemySelectionButtons = null;
        [SerializeField] private GameObject _battleMenu = null;
        [SerializeField] private GameObject _enemySelectionMenu = null;
        //done
        [Header("Spawn Points")]
        [SerializeField] private List<Transform> _partySpawnPoints = new List<Transform>();
        [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();
        [Header("Combatants")]
        [SerializeField] List<CombatEntities> _allCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _partyCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _enemyCombatants = new List<CombatEntities>();

        //Adding this session
        private int _currentPlayer = 0;
        //done

        private void Start()
        {
            CreatePartyEntities();
            CreateEnemyEntities();
            ShowBattleMenu();
        }

        //Adding this session
        #region Public

        public void ShowBattleMenu()
        {
            _battleMenu.SetActive(true);
        }

        public void ShowEnemySelectionMenu()
        {
            _battleMenu.SetActive(false);
            SetEnemySelectionButtons();
            _enemySelectionMenu.SetActive(true);
        }

        #endregion
        //done

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

                CombatVisuals tempCombatVisuals = Instantiate(enemyParty[i].Visuals, _enemySpawnPoints[i].position, Quaternion.identity);
                newEntity.Visuals = tempCombatVisuals;

                _allCombatants.Add(newEntity);
                _enemyCombatants.Add(newEntity);
            }
        }

        //Adding this session
        private void SetEnemySelectionButtons()
        {
            foreach (GameObject button in _enemySelectionButtons)
            {
                button.SetActive(false);
            }

            for (int i = 0; i < _enemyCombatants.Count; i++)
            {
                _enemySelectionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _enemyCombatants[i].EntityName;
                _enemySelectionButtons[i].SetActive(true);
            }
        }
        //done

        #endregion
    }
}