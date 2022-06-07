using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    [System.Serializable]
    public abstract class CharacterComponent {

        CharacterCommander commander;
        [SerializeField][ReadOnly] bool isEnabled;

        public CharacterCommander Commander { get => commander; }
        public bool IsEnabled { get => isEnabled; }

        protected CharacterComponent (CharacterCommander commander)
        {
            this.commander = commander;
            Enable();
        }

        public void Enable()
        {
            this.isEnabled = true;
        }

        public void Disable ()
        {
            this.isEnabled = false;
        }

        public abstract void Update ();
    }

}