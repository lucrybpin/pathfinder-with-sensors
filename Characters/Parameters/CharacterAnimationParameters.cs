using UnityEngine;

namespace CryptoGame {

    [CreateAssetMenu( fileName = "CharacterAnimationParameters", menuName = "Character/Parameters/Animation", order = 1 )]
    public class CharacterAnimationParameters : ScriptableObject {

        [Header( "Animator" )]
        [SerializeField] RuntimeAnimatorController standardController;

        [Header("Skinned Mesh Renderer")]
        [SerializeField] Mesh mesh;
        [SerializeField] Bounds bounds;

        public RuntimeAnimatorController StandardController { get => standardController; }
        public Mesh Mesh { get => mesh; }
        public Bounds Bounds { get => bounds; }
    }

}