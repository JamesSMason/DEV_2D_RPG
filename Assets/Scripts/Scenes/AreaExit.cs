using JSM.RPG.Controls;
using UnityEngine;

namespace JSM.RPG.Scenes
{
    public class AreaExit : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint = null;
        [SerializeField] private SceneList _sceneToLoad = SceneList.Nightstone;
        [SerializeField] private string _targetPortalName;

        #region Unity Messages

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                SceneHandler.Instance.SetTransitionName(_targetPortalName);
                SceneHandler.Instance.LoadScene(_sceneToLoad.ToString());
            }
        }

        #endregion
    }
}