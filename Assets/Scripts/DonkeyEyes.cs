using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyEyes : MonoBehaviour
{
    MainInput i;
    [SerializeField] Transform EyeSocket;
    [SerializeField] float MaxCap;
    private void Start()
    {
        i = new MainInput();
        i.Enable();
    }

    private void Update()
    {
        Vector2 Mousepos = EyeSocket.position - Camera.main.ScreenToWorldPoint(i.MoveHorse.Aim.ReadValue<Vector2>());
        Mousepos.Normalize();
        transform.position = EyeSocket.position - new Vector3(Mousepos.x, Mousepos.y, 0) / MaxCap;
    }
}
