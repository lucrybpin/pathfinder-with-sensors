using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CryptoGame {

    [RequireComponent( typeof( NavMeshAgent ) )]
    public class CharacterCommander : MonoBehaviour {

        [Header( "Character Components" )]
        [SerializeField] [Expandable] [Required] CharacterParameters characterParameters;
        [SerializeField] CharacterMovement characterMovement;
        [SerializeField] CharacterInputHandler inputHandler;
        [SerializeField] CharacterAnimator characterAnimator;

        public CharacterParameters CharacterParameters { get => characterParameters; }
        public CharacterMovement CharacterMovement { get => characterMovement; }
        public CharacterInputHandler InputHandler { get => inputHandler; }
        public CharacterAnimator CharacterAnimator { get => characterAnimator; }

        private void Start ()
        {
            inputHandler = new CharacterInputHandler( this );
            characterAnimator = new CharacterAnimator( this, GetComponentInChildren<Animator>(), GetComponentInChildren<SkinnedMeshRenderer>() );
            characterMovement = new CharacterMovement( this, GetComponent<NavMeshAgent>() );
        }

        private void Update ()
        {
            characterMovement.Update();
            inputHandler.Update();
            characterAnimator.Update();
        }

        [Button( "Enable Input" )]
        public void EnableInput ()
        {
            this.inputHandler.Enable();
        }

        [Button( "Disable Input" )]
        public void DisableInput ()
        {
            this.inputHandler.Disable();
        }

        [Button( "Find Random Destination" )]
        public void FindRandomDestination ()
        {
            List<Node> nodes = new List<Node>();
            nodes.AddRange( GameObject.FindObjectsOfType<Node>() );
            int randomIndex = Random.Range( 0, nodes.Count );
            characterMovement.SetDestination( nodes [ randomIndex ] );
        }


        [Button( "Follow Path" )]
        public void FollowPath (PathBehavior pathBehavior = PathBehavior.OneWay)
        {
            characterMovement.FollowPath( pathBehavior );
        }

    }

}