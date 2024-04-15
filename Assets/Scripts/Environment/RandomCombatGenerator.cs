using JSM.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JSM.RPG.Environment
{
    public class RandomCombatGenerator : MonoBehaviour
    {
        private const string COMBAT_SCENE = "CombatScene";

        [Tooltip("Time (in seconds) between random encounter checks.")]
        [SerializeField] private float _encounterTimer = 20.0f;
        [Tooltip("1 = very little chance, 20 = definite")]
        [SerializeField][Range(1, 20)] private int _chanceOfEncounter = 2;

        private float _timer = 0.0f;

        #region Unity Messages

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _encounterTimer)
            {
                _timer = 0.0f;
                if (GenerateEncounter())
                {
                    SceneManager.LoadScene(COMBAT_SCENE);
                }
            }
        }

        #endregion

        #region Private
        
        private bool GenerateEncounter()
        {
            int rollCheck = DiceRoller.RollDice(1, 20);
            return rollCheck <= _chanceOfEncounter;
        }

        #endregion
    }
}