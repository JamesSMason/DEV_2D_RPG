using JSM.RPG.Combat;
using JSM.RPG.Player;
using System;
using UnityEngine;

namespace JSM.RPG.Party
{
    [Serializable]
    public class PartyMember
    {
        public string PlayerName;
        public int InitiativeBonus;
        public int ArmorClass;
        public int MaxHP;
        public int CurrentHP;
        public int HitBonus;
        public int NumberOfAttacks;
        public int NumberOfDamageDice;
        public int DamageDieType;
        public int DamageBonus;
        public GameObject OverworldVisuals;
        public CombatVisuals CombatVisuals;

        public PartyMember(PlayerInfo playerInfo)
        {
            PlayerName = playerInfo.PlayerName;
            InitiativeBonus = playerInfo.InitiativeBonus;
            ArmorClass = playerInfo.ArmorClass;
            MaxHP = playerInfo.MaxHP;
            CurrentHP = playerInfo.MaxHP;
            HitBonus = playerInfo.HitBonus;
            NumberOfAttacks = playerInfo.NumberOfAttacks;
            NumberOfDamageDice = playerInfo.NumberOfDamageDice;
            DamageDieType = playerInfo.DamageDieType;
            DamageBonus = playerInfo.DamageBonus;
            OverworldVisuals = playerInfo.OverworldVisuals;
            CombatVisuals = playerInfo.CombatVisuals;
        }

        public void ChangeHealth(int health)
        {
            CurrentHP = health;
        }
    }
}