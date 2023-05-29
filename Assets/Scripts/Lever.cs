using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Lever : MonoBehaviour
{
    [SerializeField] UnityEvent ActiveEvent;
    [SerializeField] UnityEvent DeactiveEvent;
    [SerializeField] Transform Handle;
    [SerializeField] Rigidbody2D MoveableObject;
    [SerializeField] float Speed;
    bool HandleActive = false;
    AudioSource source;
    [SerializeField] bool Reactivable;
    private void Start()
    {
        source = this.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Handle.localEulerAngles.z < 50 && !HandleActive)
        {     
            ActiveEvent.Invoke();
            HandleActive = true;
            source.Play();
        }
        if (Handle.localEulerAngles.z > 310 && HandleActive && Reactivable)
        {
            DeactiveEvent.Invoke();
            HandleActive = false;
            source.Play();
        }
    }
    IEnumerator _MoveObject(float Pos)
    {
        for (int i = 0; i < Speed; i++)
        {
            MoveableObject.MovePosition(new Vector2(MoveableObject.position.x, Pos / Speed + MoveableObject.position.y));
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    public void MoveObject(float Pos)
    {
        StartCoroutine(_MoveObject(Pos));
    }
}
