using UnityEngine;

namespace JSM.RPG.Party
{
    [CreateAssetMenu(fileName = "RPG/New Member Info", menuName = "RPG/Create New Member")]
    public class PartyMemberInfo : ScriptableObject
    {
        public string MemberName;
        public int Level;
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