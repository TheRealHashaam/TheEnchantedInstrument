using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flute : MonoBehaviour
{
    public ParticleSystem FluteParticle;
    public AudioSource FluteSound;

    public void PlayFlute()
    {
        FluteParticle.Play();
        FluteSound.Play();
    }
    public void StopFlute()
    {
        FluteParticle.Stop();
        FluteSound.Stop();
    }
}
