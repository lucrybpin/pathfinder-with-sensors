using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    public class Tree : MonoBehaviour {
        [SerializeField] AudioClip cicadaSound;
        [SerializeField] AudioClip [ ] birdsSounds;

        [SerializeField] AudioSource audioSource;

        private void Start ()
        {
            audioSource = GetComponent<AudioSource>();
            StartCoroutine( PlayCicadaSound() );
            StartCoroutine( PlayBirdsSound() );
        }

        private IEnumerator PlayCicadaSound ()
        {
            while (true)
            {
                float chance = Random.Range( 0f, 1f );
                if (chance <= 0.1f)
                {
                    audioSource.clip = cicadaSound;
                    audioSource.pitch = 1f;
                    audioSource.Play();
                    yield return new WaitForSeconds( 12 );
                    audioSource.clip = null;
                }

                yield return new WaitForSeconds( Random.Range( 30, 50 ) );
            }
        }

        private IEnumerator PlayBirdsSound ()
        {
            while (true)
            {
                float chance = Random.Range( 0f, 1f );
                if (chance <= 0.3f)
                {
                    if (audioSource.clip != cicadaSound)
                    {
                        int index = Random.Range( 0, birdsSounds.Length );
                        audioSource.clip = birdsSounds [ index ]; ;
                        audioSource.pitch = Random.Range( 0.8f, 1.2f );
                        audioSource.Play();
                    }
                }

                yield return new WaitForSeconds( Random.Range( 3, 7 ) );
            }
        }
    }

}