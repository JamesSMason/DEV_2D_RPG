using JSM.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JSM.RPG.Scenes
{
    public class UIFade : Singleton<UIFade>
    {
        [SerializeField] private Image _fadeScreen = null;
        [SerializeField] private float _fadeImageInSpeed = 0.3f;
        [SerializeField] private float _fadeImageOutSpeed = 1.0f;

        private IEnumerator _fadeRoutine = null;

        #region Unity Messages

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Public

        public void FadeInImage()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }
            _fadeRoutine = FadeRoutine(1.0f, _fadeImageInSpeed);
            StartCoroutine(_fadeRoutine);
        }

        public void FadeOutImage()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }
            _fadeRoutine = FadeRoutine(0.0f, _fadeImageOutSpeed);
            StartCoroutine(_fadeRoutine);
        }

        #endregion

        #region Routines

        private IEnumerator FadeRoutine(float targetAlpha, float fadeTime)
        {
            while (!Mathf.Approximately(_fadeScreen.color.a, targetAlpha))
            {
                float newAlpha = Mathf.MoveTowards(_fadeScreen.color.a, targetAlpha, fadeTime * Time.deltaTime);
                _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, newAlpha);
                yield return null;
            }
        }

        #endregion
    }
}