using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironManager : MonoBehaviour {
    public static EnvironManager instance = null;

    public GameObject environ; //In inspector, set default environ (should be template prefab)


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

}
