using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    public class ConsoleCommandBase {
        private string commandId;
        private string commandDescription;
        private string commandFormat;

        public string CommandId { get => commandId; }
        public string CommandDescription { get => commandDescription; }
        public string CommandFormat { get => commandFormat; }

        public ConsoleCommandBase (string id, string description, string format)
        {
            this.commandId = id;
            this.commandDescription = description;
            this.commandFormat = format;
        }
    }

    public class ConsoleCommand : ConsoleCommandBase {

        private Action command;

        public ConsoleCommand (string id, string description, string format, Action command) : base( id, description, format )
        {
            this.command = command;
        }

        public void Execute()
        {
            command.Invoke();
        }
    }

    public class ConsoleCommand<T1> : ConsoleCommandBase {

        private Action<T1> command;

        public ConsoleCommand (string id, string description, string format, Action<T1> command) : base( id, description, format )
        {
            this.command = command;
        }

        public void Execute(T1 value)
        {
            command.Invoke( value );
        }

    }
}