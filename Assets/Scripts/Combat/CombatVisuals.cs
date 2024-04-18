using UnityEngine;

namespace JSM.RPG.Combat
{
    public class CombatVisuals : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;

        private const string ATTACK_ANIMATION = "Attack 1";
        private const string HURT_ANIMATION = "Hurt";
        private const string DIE_ANIMATION = "Die";

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

        #endregion
    }
}