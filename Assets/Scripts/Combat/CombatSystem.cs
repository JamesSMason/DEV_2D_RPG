using JSM.RPG.Enemies;
using JSM.RPG.Party;
using JSM.RPG.Player;
using JSM.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSM.RPG.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        [Header("Spawn Points")]
        [SerializeField] private List<Transform> _partySpawnPoints = new List<Transform>();
        [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();
        [Header("Combatants")]
        [SerializeField] List<CombatEntities> _allCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _partyCombatants = new List<CombatEntities>();
        [SerializeField] List<CombatEntities> _enemyCombatants = new List<CombatEntities>();

        private void Start()
        {
            CreatePartyEntities();
            CreateEnemyEntities();
        }

        #region Private

        private void CreatePartyEntities()
        {
            List<PlayerStats> currentParty = PartyHandler.Instance.CurrentParty;
            for (int i = 0; i < currentParty.Count; i++)
            {
                CombatEntities newEntity = new CombatEntities();
                newEntity.SetEntityValues(currentParty[i]);

                CombatVisuals tempCombatVisuals = Instantiate(currentParty[i].Visuals, _partySpawnPoints[i].position, Quaternion.identity);
                newEntity.Visuals = tempCombatVisuals;

                _allCombatants.Add(newEntity);
                _partyCombatants.Add(newEntity);
            }
        }

        private void CreateEnemyEntities()
        {
            List<Enemy> enemyParty = EnemyHandler.Instance.CurrentEnemies;
            for (int i = 0; i < enemyParty.Count; i++)
            {
                CombatEntities newEntity = new CombatEntities();
                newEntity.SetEntityValues(enemyParty[i]);

                CombatVisuals tempCombatVisuals = Instantiate(enemyParty[i].Visuals, _enemySpawnPoints[i].position, Quaternion.identity);
                newEntity.Visuals = tempCombatVisuals;

                _allCombatants.Add(newEntity);
                _enemyCombatants.Add(newEntity);
            }
        }

        #endregion
    }

    [Serializable]
    public class CombatEntities
    {
        public string EntityName;
        public int Initiative;
        public int ArmorClass;
        public int MaxHP;
        public int CurrentHP;
        public int HitBonus;
        public int NumberOfAttacks;
        public int NumberOfDamageDice;
        public int DamageDieType;
        public int DamageBonus;
        public bool IsPlayer;
        public CombatVisuals Visuals;

        public void SetEntityValues(PlayerStats stats)
        {
            EntityName = stats.MemberName;
            Initiative = DiceRoller.RollDice(1, 20) + stats.InitiativeBonus;
            ArmorClass = stats.ArmorClass;
            MaxHP = stats.MaxHP;
            CurrentHP = stats.CurrentHP;
            HitBonus = stats.HitBonus;
            NumberOfAttacks = stats.NumberOfAttacks;
            NumberOfDamageDice = stats.NumberOfDamageDice;
            DamageDieType = stats.DamageDieType;
            DamageBonus = stats.DamageBonus;
            IsPlayer = true;
        }

        public void SetEntityValues(Enemy stats)
        {
            EntityName = stats.EnemyName;
            Initiative = DiceRoller.RollDice(1, 20) + stats.InitiativeBonus;
            ArmorClass = stats.ArmorClass;
            MaxHP = stats.MaxHP;
            CurrentHP = stats.MaxHP;
            HitBonus = stats.HitBonus;
            NumberOfAttacks = stats.NumberOfAttacks;
            NumberOfDamageDice = stats.NumberOfDamageDice;
            DamageDieType = stats.DamageDieType;
            DamageBonus = stats.DamageBonus;
            IsPlayer = false;
        }
    }
}