using System;

namespace JSM.RPG.Enemies
{
    [Serializable]
    public class Encounter
    {
        public EnemyInfo EnemyInfo;
        public int MinEnemies;
        public int MaxEnemies;
    }
}