using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    public class ConsoleController : MonoBehaviour {

        bool showConsole;
        bool showHelp;

        PlayerInput.StandardActions standardInput;

        string input;
        public static ConsoleCommand OPUS_COMMAND;
        public static ConsoleCommand MOTHERLODE;
        public static ConsoleCommand<int> PRINT_NUMBER;
        public static ConsoleCommand<string> PRINT_TEXT;
        public static ConsoleCommand HELP;

        public List<object> commandList;

        private void Awake ()
        {
            OPUS_COMMAND = new ConsoleCommand(
                "opus_command",
                "The very first console command ever created by Mulx.",
                "opus_command", () => {
                    Debug.Log( "Opus Command Executed!" );
                } );

            MOTHERLODE = new ConsoleCommand(
                "motherlode",
                "Just a nostalgic word, huh?",
                "motherlode", () => {
                    Debug.Log( "Motherlode Executed!" );
                } );

            PRINT_NUMBER = new ConsoleCommand<int>(
                "print_number",
                "Print a number using the parameter.",
                "print_number <number>", (x) => {
                    Debug.Log( x );
                } );

            PRINT_TEXT = new ConsoleCommand<string>(
                "print_text",
                "Print a text using the parameter.",
                "print_text <text>", (x) => {
                    Debug.Log( x );
                } );

            HELP = new ConsoleCommand(
                "help",
                "Show list of console commands available ",
                "help", () => {
                    showHelp = true;
                } );

            commandList = new List<object>
            {
                OPUS_COMMAND,
                MOTHERLODE,
                PRINT_NUMBER,
                PRINT_TEXT,
                HELP
            };
        }

        private void Start ()
        {
            standardInput = GameManager.Instance.InputManagerComponent.GetStandardActions(); //InputManager.Instance.GetStandardActions();
            input = "";
        }


        private void Update ()
        {
            if (standardInput.Console.triggered)
                OnToggleConsole();

            if (standardInput.Enter.triggered)
                HandleInput();
        }

        private void OnEnter ()
        {
            if (showConsole)
            {
                HandleInput();
                input = "";
            }
        }

        private void HandleInput ()
        {
            string [ ] properties = input.Split( ' ' );

            for (int i = 0; i < commandList.Count; i++)
            {
                ConsoleCommandBase commandBase = commandList [ i ] as ConsoleCommandBase;
                if (input.Contains( commandBase.CommandId ))
                {
                    if (commandList [ i ] as ConsoleCommand != null)
                    {
                        ( commandList [ i ] as ConsoleCommand ).Execute();
                    }
                    else if (commandList [ i ] as ConsoleCommand<int> != null)
                    {
                        ( commandList [ i ] as ConsoleCommand<int> ).Execute( int.Parse( properties [ 1 ] ) );
                    }
                    else if (commandList [ i ] as ConsoleCommand<string> != null)
                    {
                        ( commandList [ i ] as ConsoleCommand<string> ).Execute( properties [ 1 ] );
                    }
                }
            }
        }

        public void OnToggleConsole ()
        {
            showConsole = !showConsole;

            if (showConsole)
            {
                CameraController.Instance.Disable();
            } else
            {
                CameraController.Instance.Enable();
            }

        }

        Vector2 scroll;

        private void OnGUI ()
        {
            if (!showConsole) { return; }

            float y = 0f;

            if (showHelp)
            {
                GUI.Box( new Rect( 0, y, Screen.width, 100 ), "" );

                Rect viewport = new Rect( 0, 0, Screen.width - 30, 20 * commandList.Count );

                scroll = GUI.BeginScrollView( new Rect( 0, y + 5f, Screen.width, 89 ), scroll, viewport );

                for (int i = 0; i < commandList.Count; i++)
                {
                    ConsoleCommandBase command = commandList [ i ] as ConsoleCommandBase;

                    string label = $"{command.CommandFormat} - {command.CommandDescription}";

                    Rect labelRect = new Rect( 5, 20 * i, viewport.width - 100, 20 );

                    GUI.Label( labelRect, label );
                }

                GUI.EndScrollView();

                y += 100;
            }

            GUI.Box( new Rect( 0, y, Screen.width, 30 ), "" );
            input = GUI.TextField( new Rect( 10f, y + 5f, Screen.width - 20f, 20f ), input );
        }
    }

}