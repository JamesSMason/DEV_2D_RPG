using JSM.RPG.Combat;
using UnityEngine;

namespace JSM.RPG.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private string _memberName;
        [SerializeField] private int _maxHP;
        [SerializeField] private int _currentHP;
        [SerializeField] private int _armorClass;
        [SerializeField] private int _hitBonus;
        [SerializeField] private int _damageDieType;
        [SerializeField] private int _numberOfDamageDice;
        [SerializeField] private int _damageBonus;
        [SerializeField] private int _numberOfAttacks;
        [SerializeField] private int _initiativeBonus;
        [SerializeField] private CombatVisuals _combatVisuals = null;


        public string MemberName => _memberName;
        public int MaxHP => _maxHP;
        public int CurrentHP => _currentHP;
        public int ArmorClass => _armorClass;
        public int HitBonus => _hitBonus;
        public int DamageDieType => _damageDieType;
        public int NumberOfDamageDice => _numberOfDamageDice;
        public int DamageBonus => _damageBonus;
        public int NumberOfAttacks => _numberOfAttacks;
        public int InitiativeBonus => _initiativeBonus;
        public CombatVisuals Visuals => _combatVisuals;

        private void Awake()
        {
            _currentHP = _maxHP;
        }

        public void ChangeHealth(int health)
        {
            _currentHP = health;
        }
    }
}