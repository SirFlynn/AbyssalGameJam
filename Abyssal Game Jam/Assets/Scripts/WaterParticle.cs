using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    public ParticleSystem water;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(water, transform.position, transform.rotation);
            AudioManager.Instance.PlayTap();
        }
    }
}
