using JSM.RPG.Combat;
using TMPro;
using UnityEngine;

namespace JSM.RPG.UI.Combat
{
    public class DetailsUI : MonoBehaviour
    {
        [Header("UI Text Boxes")]
        [SerializeField] private TextMeshProUGUI _nameText = null;
        [SerializeField] private TextMeshProUGUI _initiativeText = null;
        [SerializeField] private TextMeshProUGUI _aCText = null;
        [SerializeField] private TextMeshProUGUI _hPText = null;
        [SerializeField] private TextMeshProUGUI _hitBonusText = null;
        [SerializeField] private TextMeshProUGUI _numberOfAttacksText = null;
        [SerializeField] private TextMeshProUGUI _baseDamageText = null;
        [SerializeField] private TextMeshProUGUI _damageBonusText = null;

        #region Unity Messages

        private void OnEnable()
        {
            CombatSystem.OnPlayerSelectionChanged += CombatSystem_OnPlayerSelectionChanged;
        }

        private void OnDisable()
        {
            CombatSystem.OnPlayerSelectionChanged -= CombatSystem_OnPlayerSelectionChanged;
        }

        #endregion

        #region Private

        public void SetText(CombatEntities playerStats)
        {
            _nameText.text = $"{playerStats.EntityName}";
            _initiativeText.text = $"{playerStats.Initiative}";
            _aCText.text = $"{playerStats.ArmorClass}";
            _hPText.text = $"{playerStats.CurrentHP} / {playerStats.MaxHP}";
            _hitBonusText.text = $"{playerStats.HitBonus}";
            _numberOfAttacksText.text = $"{playerStats.NumberOfAttacks}";
            _baseDamageText.text = $"{playerStats.NumberOfDamageDice}d{playerStats.DamageDieType}";
            _damageBonusText.text = $"{playerStats.DamageBonus}";
        }

        #endregion

        #region Events

        private void CombatSystem_OnPlayerSelectionChanged(CombatEntities playerStats)
        {
            SetText(playerStats);
        }

        #endregion
    }
}