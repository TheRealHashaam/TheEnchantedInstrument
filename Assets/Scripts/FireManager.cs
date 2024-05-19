using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public float RestartDelay;
    public ParticleSystem Fire;
    public AudioSource FireSound;
    BoxCollider boxCollider;
    public float damageAmount = 10;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(Flame_Delay());
    }
    IEnumerator Flame_Delay()
    {
        Fire.Play();
        FireSound.Play();
        boxCollider.enabled = true;
        StartCoroutine(Fire_Sound());
        yield return new WaitForSeconds(RestartDelay + Fire.main.duration);
        StartCoroutine(Flame_Delay());
    }
    IEnumerator Fire_Sound()
    {
        yield return new WaitForSeconds(Fire.main.duration);
        FireSound.Stop();
        boxCollider.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<SnakeCharmer>())
        {
            other.gameObject.GetComponent<SnakeCharmer>().TakeDamage(damageAmount * Time.deltaTime);
        }
    }
}
