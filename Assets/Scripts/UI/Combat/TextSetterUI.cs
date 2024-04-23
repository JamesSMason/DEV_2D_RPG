using TMPro;
using UnityEngine;

namespace JSM.RPG.UI.Combat
{
    public class TextSetterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _buttonText = null;

        #region Public

        public void SetText(string buttonText)
        {
            _buttonText.text = buttonText;
        }

        #endregion
    }
}