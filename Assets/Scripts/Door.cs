using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool IsOpen = false;
    [SerializeField] bool IsOpenAtStart;
    [SerializeField] Sprite Close;
    [SerializeField] Sprite Open;
    private void Start()
    {
        if (IsOpenAtStart)
        {
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        if (!IsOpen)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = Open;
            IsOpen = true;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = Close;
            IsOpen = false;
        }
       
    }
}
