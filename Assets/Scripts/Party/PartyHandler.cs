using JSM.RPG.Player;
using JSM.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Party
{
    public class PartyHandler : Singleton<PartyHandler>
    {
        [SerializeField] private PlayerInfo _defaultPartyMember = null;
        [SerializeField] private PlayerInfo[] _allMembers;
        [SerializeField] private List<PartyMember> _currentParty = new List<PartyMember>();

        private Vector3 _playerPosition = Vector3.zero;

        public List<PartyMember> CurrentParty => _currentParty;
        public Vector3 PlayerPosition => _playerPosition;

        #region Unity Messages

        protected override void Awake()
        {
            base.Awake();
            AddMemberToPartyByName(_defaultPartyMember.PlayerName);
            AddMemberToPartyByName(_defaultPartyMember.PlayerName);
            AddMemberToPartyByName(_defaultPartyMember.PlayerName);
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Public

        public void SaveHealth(int partyMember, int health)
        {
            List<PartyMember> members = GetAliveParty();
            members[partyMember].ChangeHealth(health);
        }

        public void SetPosition(Vector3 position)
        {
            _playerPosition = position;
        }

        public List<PartyMember> GetAliveParty()
        {
            List<PartyMember> aliveParty = new List<PartyMember>();
            foreach (PartyMember partyMember in _currentParty)
            {
                if (partyMember.CurrentHP > 0)
                {
                    aliveParty.Add(partyMember);
                }
            }
            return aliveParty;
        }

        #endregion

        #region Private

        private void AddMemberToPartyByName(string memberName)
        {
            foreach (PlayerInfo member in _allMembers)
            {
                if (member.PlayerName == memberName)
                {
                    PartyMember newMember = new PartyMember(member);
                    _currentParty.Add(newMember);
                    break;
                }
            }
        }

        #endregion
    }
}