using System;
using UnityEngine;
namespace CryptoGame {

    [System.Serializable]
    public class CharacterAnimator : CharacterComponent {

        [SerializeField][ReadOnly] Animator animator;
        [SerializeField][ReadOnly] SkinnedMeshRenderer skinnedMeshRenderer;

        public CharacterAnimator (CharacterCommander commander, Animator animator, SkinnedMeshRenderer skinnedMeshRenderer) : base( commander )
        {
            this.animator = animator;
            this.skinnedMeshRenderer = skinnedMeshRenderer;
            SetAnimatorController( this.Commander.CharacterParameters.Animation.StandardController );
            SetMesh( this.Commander.CharacterParameters.Animation.Mesh );
            SetBounds( this.Commander.CharacterParameters.Animation.Bounds );
            
        }

        private void SetAnimatorController(RuntimeAnimatorController animatorController)
        {
            this.animator.runtimeAnimatorController = animatorController;
        }

        private void SetMesh (Mesh mesh)
        {
            this.skinnedMeshRenderer.sharedMesh = mesh;
        }

        private void SetBounds (Bounds bounds)
        {
            this.skinnedMeshRenderer.localBounds = bounds;
        }

        public void SetSpeed(float speed)
        {
            this.animator.SetFloat( "speed", speed );
        }

        public override void Update () {
        }
    }

}