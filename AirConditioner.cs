using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {


    public class AirConditioner : MonoBehaviour {

        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip acSound;


        void Start ()
        {
            audioSource = GetComponent<AudioSource>();

            float chance = Random.Range( 0f, 1f );
            if (chance <= 0.52f)
            {
                //StartCoroutine( PlayACSound() );
                audioSource.clip = acSound;
                audioSource.pitch = Random.Range( 0.84f, 1.1f );
                audioSource.Play();
            }
        }


        //private IEnumerator PlayACSound ()
        //{
        //    while (true)
        //    {
        //        float chance = Random.Range( 0f, 1f );
        //        if (chance <= 0.1f)
        //        {
        //            audioSource.clip = acSound;
        //            audioSource.pitch = 1f;
        //            audioSource.Play();
        //            yield return new WaitForSeconds( 12 );
        //            audioSource.clip = null;
        //        }

        //        yield return new WaitForSeconds( Random.Range( 30, 50 ) );
        //    }
        //}

    }

}