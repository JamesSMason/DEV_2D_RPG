using JSM.RPG.Player;
using TMPro;
using UnityEngine;

namespace JSM.RPG.UI.Combat
{
    public class DetailsUI : MonoBehaviour
    {
        [Header("FOR TESTING")]
        [SerializeField] private PlayerStats _playerStats = null;

        [Header("UI Text Boxes")]
        [SerializeField] private TextMeshProUGUI _nameText = null;
        [SerializeField] private TextMeshProUGUI _initiativeText = null;
        [SerializeField] private TextMeshProUGUI _aCText = null;
        [SerializeField] private TextMeshProUGUI _hPText = null;
        [SerializeField] private TextMeshProUGUI _hitBonusText = null;
        [SerializeField] private TextMeshProUGUI _numberOfAttacksText = null;
        [SerializeField] private TextMeshProUGUI _baseDamageText = null;
        [SerializeField] private TextMeshProUGUI _damageBonusText = null;

        private void Start()
        {
            SetText(_playerStats);
        }

        public void SetText(PlayerStats playerStats)
        {
            _nameText.text = $"{playerStats.MemberName}";
            _initiativeText.text = $"{playerStats.InitiativeBonus}";
            _aCText.text = $"{playerStats.ArmorClass}";
            _hPText.text = $"{playerStats.CurrentHP} / {playerStats.MaxHP}";
            _hitBonusText.text = $"{playerStats.HitBonus}";
            _numberOfAttacksText.text = $"{playerStats.NumberOfAttacks}";
            _baseDamageText.text = $"{playerStats.NumberOfDamageDice}d{playerStats.DamageDieType}";
            _damageBonusText.text = $"{playerStats.DamageBonus}";
        }
    }
}