using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame
{
    public class CharacterSpawner : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine( StartCitizensCO() );
        }

        private IEnumerator StartCitizensCO()
        {
            yield return new WaitForSeconds( 2.5f );
            StartCitizens();
        }

        private static void StartCitizens ()
        {
            List<CharacterCommander> citizens = new List<CharacterCommander>();
            citizens.AddRange( GameObject.FindObjectsOfType<CharacterCommander>() );
            foreach (CharacterCommander citizen in citizens)
            {
                citizen.FindRandomDestination();
                citizen.FollowPath(PathBehavior.Explorer);
            }
        }
    }
}
