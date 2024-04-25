using UnityEngine;

namespace JSM.RPG.Enemies
{
    public class EncounterSystem : MonoBehaviour
    {
        [SerializeField] private Encounter[] encounterArray = null;

        private void Start()
        {
            EnemyHandler.Instance.GenerateEnemiesByEncounter(encounterArray);
        }
    }
}