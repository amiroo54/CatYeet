using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    //for anyone reading my code, i wrote all of it. i was just in the mood to use m_ this time.
    [SerializeField]TMP_Text m_TextMeshPro;
    float m_Timer;
    void Start()
    {
          //m_TextMeshPro = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (!GameManager.HasWon)
        {
            m_TextMeshPro.text = m_Timer.ToString();
        }
       
    }
}
