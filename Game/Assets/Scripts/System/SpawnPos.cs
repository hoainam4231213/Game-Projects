using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : BYSingleton<SpawnPos>
{
    public List<Transform> pos;
    // Start is called before the first frame update
    public Transform GetPos()
    {
        return pos[0];
    }
}
