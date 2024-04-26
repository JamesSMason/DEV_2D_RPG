using JSM.Utilities;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JSM.RPG.Environment
{
    public class RandomCombatGenerator : MonoBehaviour
    {
        public RandomCombatGenerator Instance { get; private set; } = null;

        public static Action OnCombatStart;

        [Tooltip("Time (in seconds) between random encounter checks.")]
        [SerializeField] private float _encounterTimer = 20.0f;
        [Tooltip("1 = very little chance, 20 = definite")]
        [SerializeField][Range(1, 20)] private int _chanceOfEncounter = 2;

        private float _timer = 0.0f;

        private const string COMBAT_SCENE = "CombatScene";

        #region Unity Messages

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _encounterTimer)
            {
                _timer = 0.0f;
                if (GenerateEncounter())
                {
                    OnCombatStart?.Invoke();
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