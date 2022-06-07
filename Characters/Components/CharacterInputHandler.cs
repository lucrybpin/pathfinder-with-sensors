using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    [System.Serializable]
    public class CharacterInputHandler : CharacterComponent {

        PlayerInput.TopdownActions input;

        public CharacterInputHandler (CharacterCommander commander) : base( commander )
        {
            input = GameManager.Instance.InputManagerComponent.GetTopdownActions();
        }

        public override void Update ()
        {
            if (!IsEnabled) return;

            if (input.PrimaryAction.triggered)
            {
                RaycastHit [ ] hits = Utils.GetWorldHitsFrom2DPosition( input.CursorPosition.ReadValue<Vector2>() );
                if (hits != null)
                {
                    this.Commander.CharacterMovement.Move( hits [ 0 ].point );
                }
                
            }

            if (input.SecondaryAction.triggered)
            {
                //Debug.Log( "Secondary Action. Position " + input.CursorPosition.ReadValue<Vector2>() );
            }
        }
    }

}