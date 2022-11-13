using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactControl : MonoBehaviour
{
    public float timeDelay = 1.5f;
    public ParticleSystem particle;
    public string pool_nameImpact;
    // Start is called before the first frame update
    public void OnSpawned()
    {
        particle.Simulate(0,true,true);
        particle.Play();
        StartCoroutine("DelayImpact");
    }

    IEnumerator DelayImpact()
    {
        yield return new WaitForSeconds(timeDelay);
        BYPoolManager.instance.DeSpawn(pool_nameImpact,transform);
    }
}
