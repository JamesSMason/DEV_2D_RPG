using UnityEngine;

namespace JSM.RPG.Enemies
{
    [CreateAssetMenu(fileName = "RPG/New Enemy Info", menuName = "RPG/Create Enemy")]
    public class EnemyInfo : ScriptableObject
    {
        public string EnemyName;
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Intelligence;
        public int Wisdom;
        public int Charisma;
        public int ArmorClass;
        public int MaxHP;
        public int DamageDieType;
        public int NumberOfDamageDice;
        public int XPValue;
        public int ProficiencyBonus;
        public int NumberOfAttacks;
        public GameObject Visuals;
    }
}