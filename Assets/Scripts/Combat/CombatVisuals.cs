using System.Collections;
using TMPro;
using UnityEngine;

namespace JSM.RPG.Combat
{
    public class CombatVisuals : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;
        [SerializeField] private TextMeshPro _damageText = null;
        [SerializeField] private float _damageDisplayTime = 1.0f;

        private const string ATTACK_ANIMATION = "Attack 1";
        private const string HURT_ANIMATION = "Hurt";
        private const string DIE_ANIMATION = "Die";

        #region Unity Messages

        private void Start()
        {
            _damageText.gameObject.SetActive(false);
        }

        #endregion

        #region Public

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(ATTACK_ANIMATION);
        }

        public void PlayHurtAnimation()
        {
            _animator.SetTrigger(HURT_ANIMATION);
        }

        public void PlayDieAnimation()
        {
            _animator.SetTrigger(DIE_ANIMATION);
        }

        public void DisplayDamage(string damage)
        {
            _damageText.text = damage;
            StartCoroutine(DisplayDamageRoutine());
        }

        #endregion

        #region Routines

        private IEnumerator DisplayDamageRoutine()
        {
            _damageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_damageDisplayTime);
            _damageText.gameObject.SetActive(false);
        }

        #endregion
    }
}