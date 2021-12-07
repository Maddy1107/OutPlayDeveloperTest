using System;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    public static EffectPlayer instance;

    private void Awake() => instance = this;

    public ParticleSystem explosion;

    public AudioSource explosionSound;

    //Paly the effects
    public void PlayEffects(Vector3 position)
    {
        ParticleSystem particle = Instantiate(explosion, position, Quaternion.identity);
        Destroy(particle, 2);
        explosionSound.Play();
    }

    
}
