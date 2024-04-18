using JSM.RPG.Combat;
using JSM.RPG.Player;
using JSM.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Party
{
    public class PartyHandler : MonoBehaviour
    {
        public PartyHandler Instance { get; private set; } = null;

        [SerializeField] private PlayerStats _defaultPartyMember = null;
        [SerializeField] private PlayerStats[] _allMembers;
        [SerializeField] private List<PlayerStats> _currentParty = new List<PlayerStats>();

        #region Unity Messages

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            AddMemberToPartyByName(_defaultPartyMember.MemberName);
        }

        #endregion

        #region Private

        private void AddMemberToPartyByName(string memberName)
        {
            foreach (PlayerStats member in _allMembers)
            {
                if (string.Equals(member.MemberName, memberName, StringComparison.CurrentCultureIgnoreCase))
                {
                    _currentParty.Add(member);
                    break;
                }
            }
        }

        #endregion
    }
}