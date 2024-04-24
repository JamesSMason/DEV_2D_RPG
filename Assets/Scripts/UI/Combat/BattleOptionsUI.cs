using JSM.RPG.Combat;
using UnityEngine;

namespace JSM.RPG.UI.Combat
{
    public class BattleOptionsUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject[] _enemySelectionButtons = null;
        [SerializeField] private GameObject _battleMenu = null;
        [SerializeField] private GameObject _enemySelectionMenu = null;

        #region Unity Messages

        private void OnEnable()
        {
            CombatSystem.OnEnemySelected += CombatSystem_OnEnemySelected;
        }

        private void OnDisable()
        {
            CombatSystem.OnEnemySelected -= CombatSystem_OnEnemySelected;
        }

        #endregion

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

        #region Private

        private void SetEnemySelectionButtons()
        {
            foreach (GameObject button in _enemySelectionButtons)
            {
                button.SetActive(false);
            }

            for (int i = 0; i < CombatSystem.Instance.EnemyCount; i++)
            {
                _enemySelectionButtons[i].GetComponent<TextSetterUI>().SetText(CombatSystem.Instance.EnemyName(i));
                _enemySelectionButtons[i].SetActive(true);
            }
        }

        #endregion

        #region Events

        private void CombatSystem_OnEnemySelected()
        {
            _enemySelectionMenu.SetActive(false);
            ShowBattleMenu();
        }

        #endregion
    }
}