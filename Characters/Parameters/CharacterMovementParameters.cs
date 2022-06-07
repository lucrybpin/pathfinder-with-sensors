using UnityEngine;

namespace CryptoGame {

    [CreateAssetMenu( fileName = "CharacterMovementParameters", menuName = "Character/Parameters/Movement", order = 1 )]
    public class CharacterMovementParameters : ScriptableObject {

        [Header( "Nav Mesh Agent" )]
        [SerializeField] float walkSpeed = 3f;
        [SerializeField] float runSpeed = 7f;
        [SerializeField] float angularSpeed = 370f;
        [SerializeField] float acceleration = 25f;

        public float WalkSpeed { get => walkSpeed; }
        public float RunSpeed { get => runSpeed; }
        public float AngularSpeed { get => angularSpeed; }
        public float Acceleration { get => acceleration; }
    }

}