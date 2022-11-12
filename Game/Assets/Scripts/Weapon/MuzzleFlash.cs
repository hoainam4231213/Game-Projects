using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    public void FireHandle()
    {
        particleSystem.Simulate(0, true, true);
        particleSystem.Play();
    }
}
