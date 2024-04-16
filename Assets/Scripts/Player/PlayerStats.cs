using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private string _playerName;
    [SerializeField] private int _strength;
    [SerializeField] private int _dexterity;
    [SerializeField] private int _constitution;
    [SerializeField] private int _intelligence;
    [SerializeField] private int _wisdom;
    [SerializeField] private int _charisma;
    [SerializeField] private int _armorClass;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _damageDieType;
    [SerializeField] private int _numberOfDamageDice;
    [SerializeField] private int _xPValue;
    [SerializeField] private int _proficiencyBonus;
    [SerializeField] private int _numberOfAttacks;
    [SerializeField] private GameObject _visuals;

    public string PlayerName => _playerName;
    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Constitution => _constitution;
    public int Intelligence => _intelligence;
    public int Wisdom => _wisdom;
    public int Charisma => _charisma;
    public int ArmorClass => _armorClass;
    public int MaxHP => _maxHP;
    public int DamageDieType => _damageDieType;
    public int NumberOfDamageDice => _numberOfDamageDice;
    public int XPValue => _xPValue;
    public int ProficiencyBonus => _proficiencyBonus;
    public int NumberOfAttacks => _numberOfAttacks;
    public GameObject Visuals => _visuals;
}