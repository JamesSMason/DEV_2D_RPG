using JSM.RPG.Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JSM.RPG.UI.Combat
{
    public class DetailsUI : MonoBehaviour
    {
        [Header("FOR TESTING")]
        [SerializeField] private EnemyInfo _enemyInfo = null;

        [Header("UI Text Boxes")]
        [SerializeField] private TextMeshProUGUI _nameText = null;
        [SerializeField] private TextMeshProUGUI _sizeText = null;
        [SerializeField] private TextMeshProUGUI _raceText = null;
        [SerializeField] private TextMeshProUGUI _alignmentText = null;
        [SerializeField] private TextMeshProUGUI _speedText = null;
        [SerializeField] private TextMeshProUGUI _strengthText = null;
        [SerializeField] private TextMeshProUGUI _dexterityText = null;
        [SerializeField] private TextMeshProUGUI _constitutionText = null;
        [SerializeField] private TextMeshProUGUI _aCText = null;
        [SerializeField] private TextMeshProUGUI _hPText = null;
        [SerializeField] private TextMeshProUGUI _intelligenceText = null;
        [SerializeField] private TextMeshProUGUI _wisdomText = null;
        [SerializeField] private TextMeshProUGUI _charismaText = null;
        [SerializeField] private TextMeshProUGUI _meleeText = null;
        [SerializeField] private TextMeshProUGUI _rangedText = null;

        private void Start()
        {
            SetText(_enemyInfo);
        }

        public void SetText(EnemyInfo enemyInfo)
        {
            _nameText.text = enemyInfo.EnemyName;
            _sizeText.text = "Small";
            _raceText.text = "Goblinoid";
            _alignmentText.text = "Neutral Evil";
            _speedText.text = "30 ft.";
            _strengthText.text = enemyInfo.Strength.ToString();
            _dexterityText.text = enemyInfo.Dexterity.ToString();
            _constitutionText.text = enemyInfo.Constitution.ToString();
            _aCText.text = enemyInfo.ArmorClass.ToString();
            _hPText.text = enemyInfo.MaxHP.ToString();
            _intelligenceText.text = enemyInfo.Intelligence.ToString();
            _wisdomText.text = enemyInfo.Wisdom.ToString();
            _charismaText.text = enemyInfo.Charisma.ToString();
            _meleeText.text = "Scimitar";
            _rangedText.text = "Shortbow";
        }
    }
}