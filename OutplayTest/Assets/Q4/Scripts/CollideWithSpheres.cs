using UnityEngine;

public class CollideWithSpheres : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            EffectPlayer.instance.PlayEffects(transform.position);
        }
    }
}
