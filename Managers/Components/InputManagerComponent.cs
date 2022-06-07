using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    [System.Serializable]
    public class InputManagerComponent : ManagerComponent {

        PlayerInput playerInput;
        [SerializeField] [ReadOnly] bool cameraInputs = false;
        [SerializeField] [ReadOnly] bool standardInputs = false;
        [SerializeField] [ReadOnly] bool topdownInputs = false;


        public InputManagerComponent (GameManager manager) : base( manager )
        {
            this.playerInput = new PlayerInput();
        }

        public void EnableCameraInputs ()
        {
            playerInput.camera.Enable();
            cameraInputs = true;
        }

        public void EnableStandardInputs ()
        {
            playerInput.standard.Enable();
            standardInputs = true;
        }

        public void EnableTopdownInputs ()
        {
            playerInput.topdown.Enable();
            topdownInputs = true;
        }

        public void DisableCameraInputs ()
        {
            playerInput.camera.Disable();
            cameraInputs = false;
        }

        public void DisableStandardInputs ()
        {
            playerInput.standard.Disable();
            standardInputs = false;
        }

        public void DisableTopdownInputs ()
        {
            playerInput.topdown.Disable();
            topdownInputs = false;
        }

        public PlayerInput.CameraActions GetCameraActions ()
        {
            return playerInput.camera;
        }

        public PlayerInput.StandardActions GetStandardActions ()
        {
            return playerInput.standard;
        }

        public PlayerInput.TopdownActions GetTopdownActions ()
        {
            return playerInput.topdown;
        }
    }

}