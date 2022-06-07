using NaughtyAttributes;
using UnityEngine;

namespace CryptoGame {

    [CreateAssetMenu( fileName = "CharacterParameter", menuName = "Character/Parameters/Parameters (Base)", order = 1 )]
    public class CharacterParameters : ScriptableObject {

        [SerializeField] [Expandable] [Required] CharacterMovementParameters movement;
        [SerializeField] [Expandable] [Required] CharacterAnimationParameters animation;

        public CharacterMovementParameters Movement { get => movement; }
        public CharacterAnimationParameters Animation { get => animation; }

        private void OnValidate ()
        {
            if (movement == null)
                Debug.LogError( "Movement Parameters not set!", this );

            if (animation == null)
                Debug.LogError( "Animation Parameters not set!", this );
        }
    }

}