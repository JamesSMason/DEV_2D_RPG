using JSM.RPG.Combat;
using UnityEngine;

namespace JSM.RPG.Enemies
{
    [CreateAssetMenu(fileName = "RPG/New Enemy Info", menuName = "RPG/Create Enemy")]
    public class EnemyInfo : ScriptableObject
    {
        public string EnemyName;
        public int InitiativeBonus;
        public int ArmorClass;
        public int MaxHP;
        public int HitBonus;
        public int NumberOfAttacks;
        public int NumberOfDamageDice;
        public int DamageDieType;
        public int DamageBonus;
        public int XPValue;
        public int Speed;
        public CombatVisuals Visuals;
    }
}