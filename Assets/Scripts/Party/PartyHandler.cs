using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Party
{
    public class PartyHandler : MonoBehaviour
    {
        [SerializeField] private PartyMemberInfo _defaultPartyMember = null;
        [SerializeField] private PartyMemberInfo[] _allMembers = null;
        [SerializeField] private List<PartyMember> _currentParty = new List<PartyMember>();

        #region Unity Messages

        private void Awake()
        {
            AddMemberToPartyByName(_defaultPartyMember.MemberName);
        }

        #endregion

        #region Private

        private void AddMemberToPartyByName(string memberName)
        {
            foreach (PartyMemberInfo member in _allMembers)
            {
                if (member.name == memberName)
                {
                    PartyMember newMember = new PartyMember(member);
                    _currentParty.Add(newMember);
                    break;
                }
            }
        }

        #endregion
    }

    [Serializable]
    public class PartyMember
    {
        public string Name;
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Intelligence;
        public int Wisdom;
        public int Charisma;
        public int ArmorClass;
        public int MaxHP;
        public int CurrentHP;
        public int DamageDieType;
        public int NumberOfDamageDice;
        public int XPValue;
        public int ProficiencyBonus;
        public int NumberOfAttacks;
        public int Initiative;
        public GameObject Visuals;

        public PartyMember(PartyMemberInfo stats)
        {
            Name = stats.MemberName;
            Strength = stats.Strength;
            Dexterity = stats.Dexterity;
            Constitution = stats.Constitution;
            Intelligence = stats.Intelligence;
            Wisdom = stats.Wisdom;
            Charisma = stats.Charisma;
            ArmorClass = stats.ArmorClass;
            MaxHP = stats.MaxHP;
            CurrentHP = stats.MaxHP;
            DamageDieType = stats.DamageDieType;
            NumberOfDamageDice = stats.NumberOfDamageDice;
            XPValue = stats.XPValue;
            ProficiencyBonus = stats.ProficiencyBonus;
            NumberOfAttacks = stats.NumberOfAttacks;
            Visuals = stats.Visuals;
        }
    }
}