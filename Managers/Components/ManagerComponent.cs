using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    [System.Serializable]
    public class ManagerComponent {

        GameManager manager;
        [SerializeField] bool isEnabled;

        public GameManager Manager { get => manager; }

        public ManagerComponent (GameManager manager)
        {
            this.manager = manager;
            Enable();
        }

        public void Enable ()
        {
            this.isEnabled = true;
        }

        public void Disable ()
        {
            this.isEnabled = false;
        }

    }

}