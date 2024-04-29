using JSM.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JSM.RPG.Scenes
{
    public class SceneHandler : Singleton<SceneHandler>
    {
        [SerializeField] private float _loadDelay = 1.0f;

        private string _previousScene;
        private string _targetPortalName;

        public string TargetPortalName => _targetPortalName;

        #region Unity Messages

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Public

        public void LoadScene()
        {
            SetTransitionName("NULL");
            LoadScene(_previousScene);
        }

        public void LoadScene(string sceneToLoad)
        {
            _previousScene = SceneManager.GetActiveScene().name;
            StartCoroutine(LoadSceneRoutine(sceneToLoad));
        }

        public void SetTransitionName(string targetPortalName)
        {
            _targetPortalName = targetPortalName;
        }

        #endregion

        #region Routines

        private IEnumerator LoadSceneRoutine(string sceneToLoad)
        {
            UIFade.Instance.FadeInImage();
            yield return new WaitForSeconds(_loadDelay);
            SceneManager.LoadScene(sceneToLoad);
            UIFade.Instance.FadeOutImage();
        }

        #endregion
    }
}