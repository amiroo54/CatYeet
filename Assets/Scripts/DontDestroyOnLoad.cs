using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        
        DontDestroyOnLoad(this);

        
    }
    private void OnLevelWasLoaded(int level)
    {
        if (FindObjectsOfType<DontDestroyOnLoad>().Length > 1)
        {
            for (int i = 0; i < FindObjectsOfType<DontDestroyOnLoad>().Length; i++)
            {
                if (!FindObjectsOfType<DontDestroyOnLoad>()[i] == this)
                {
                    Destroy(FindObjectsOfType<DontDestroyOnLoad>()[i]);
                }
            }
        }
    }
}
