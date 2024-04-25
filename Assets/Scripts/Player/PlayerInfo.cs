using JSM.RPG.Combat;
using UnityEngine;

namespace JSM.RPG.Player
{
    [CreateAssetMenu(fileName = "RPG/New Player Info", menuName = "RPG/Create Player Character")]
    public class PlayerInfo : ScriptableObject
    {
        public string PlayerName;
        public int InitiativeBonus;
        public int ArmorClass;
        public int MaxHP;
        public int HitBonus;
        public int NumberOfAttacks;
        public int NumberOfDamageDice;
        public int DamageDieType;
        public int DamageBonus;
        public GameObject OverworldVisuals;
        public CombatVisuals CombatVisuals;
    }
}