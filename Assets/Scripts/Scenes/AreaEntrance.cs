using JSM.RPG.Controls;
using UnityEngine;

namespace JSM.RPG.Scenes
{
    public class AreaEntrance : MonoBehaviour
    {
        [SerializeField] private string _portalName;

        #region Unity Messages

        private void Start()
        {
            if (_portalName == SceneHandler.Instance.TargetPortalName)
            {
                PlayerController.Instance.SetPosition(transform.position);
            }
        }

        #endregion
    }
}