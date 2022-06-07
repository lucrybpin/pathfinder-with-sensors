using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {
    

    public class GameManager : MonoBehaviour {

        public enum GameMode {
            FREE,
            TOPDOWN
        }

        [SerializeField][ReadOnly] GameMode gameMode;
        [SerializeField] InputManagerComponent inputManagerComponent;

        public static GameManager Instance { get; private set; }
        public InputManagerComponent InputManagerComponent { get => inputManagerComponent; }

        private void Awake ()
        {
            SingletonSetup();
            inputManagerComponent = new InputManagerComponent( this );
        }

        private void Start ()
        {
            SwitchGameMode( GameMode.FREE );
        }

        private void Update ()
        {
            HandleCommandSwitch();
        }

        public void SwitchGameMode (GameMode newGameMode)
        {
            
            switch (newGameMode)
            {
                case GameMode.FREE:
                InputManagerComponent.EnableCameraInputs();
                InputManagerComponent.EnableStandardInputs();
                InputManagerComponent.DisableTopdownInputs();
                this.gameMode = GameMode.FREE;
                break;

                case GameMode.TOPDOWN:
                InputManagerComponent.DisableCameraInputs();
                InputManagerComponent.EnableStandardInputs();
                InputManagerComponent.EnableTopdownInputs();
                this.gameMode = GameMode.TOPDOWN;
                break;

                default:
                break;
            }
        }

        private void HandleCommandSwitch ()
        {
            if (InputManagerComponent.GetStandardActions().F1.triggered)
            {
                Debug.Log("Switching to Free mode");
                SwitchGameMode( GameMode.FREE );
            }
            else if (InputManagerComponent.GetStandardActions().F2.triggered)
            {
                Debug.Log( "Switching to Topdown mode" );
                SwitchGameMode( GameMode.TOPDOWN );
            }
        }

        private void SingletonSetup ()
        {
            if (Instance != null && Instance != this)
                Destroy( gameObject );
            else
                Instance = this;

            DontDestroyOnLoad( this.gameObject );
        }
    }

}