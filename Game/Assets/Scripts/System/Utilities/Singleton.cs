using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BYSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    // Start is called before the first frame update
    private static T instance_;
    public static T instance
    {
        get
        {
            if(BYSingleton<T>.instance_==null)
            {
                BYSingleton<T>.instance_ = (T)FindObjectOfType<T>();
                if(BYSingleton<T>.instance_==null)
                {
                    GameObject go = new GameObject();
                    go.name = "[@" + typeof(T).Name + "]";
                    BYSingleton<T>.instance_ = go.AddComponent<T>();
                }
            }
            return BYSingleton<T>.instance_;
        }

    }

    private void Reset()
    {
        gameObject.name = typeof(T).Name;
    }


}
