using JSM.RPG.Scenes;
using JSM.Utilities;
using System;
using UnityEngine;

namespace JSM.RPG.Environment
{
    public class RandomCombatGenerator : Singleton<RandomCombatGenerator>
    {
        public static Action OnCombatStart;

        [Tooltip("Time (in seconds) between random encounter checks.")]
        [SerializeField] private float _encounterTimer = 20.0f;
        [Tooltip("1 = very little chance, 20 = definite")]
        [SerializeField][Range(1, 20)] private int _chanceOfEncounter = 2;
        [SerializeField] private SceneList _sceneToLoad = SceneList.GrasslandCombat;

        private float _timer = 0.0f;

        #region Unity Messages

        protected override void Awake()
        {
            base.Awake();
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
                    SceneHandler.Instance.LoadScene(_sceneToLoad.ToString());
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