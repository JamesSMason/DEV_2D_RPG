using JSM.RPG.Player;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Party
{
    public class PartyHandler : MonoBehaviour
    {
        public static PartyHandler Instance { get; private set; } = null;

        [SerializeField] private PlayerInfo _defaultPartyMember = null;
        [SerializeField] private PlayerInfo[] _allMembers;
        [SerializeField] private List<PartyMember> _currentParty = new List<PartyMember>();

        private Vector3 _playerPosition = Vector3.zero;

        public List<PartyMember> CurrentParty => _currentParty;
        public Vector3 PlayerPosition => _playerPosition;

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
                AddMemberToPartyByName(_defaultPartyMember.PlayerName);
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        #region Public

        public void SaveHealth(int partyMember, int health)
        {
            _currentParty[partyMember].ChangeHealth(health);
        }

        public void SetPosition(Vector3 position)
        {
            _playerPosition = position;
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