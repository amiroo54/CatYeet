using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Transform UpperPoint;
    [SerializeField] Vector2 CatCheckRadius;
    [SerializeField] LayerMask Cat;
    private void Start()
    {
        transform.localPosition = new Vector3(map(Settings.MusicVolume, 0, 1, 0, 18), 0, 0);
    }
    void Update()
    {
        if (SliderCheck())
        {
            transform.position = new Vector3(GameManager.Cat.transform.position.x, transform.position.y, 0);
        }
        if (transform.localPosition.x <= 0)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
        if (transform.localPosition.x >= 18)
        {
            transform.localPosition = new Vector3(18, 0, 0);
        }
        Settings.MusicVolume = map(transform.localPosition.x, 0, 18, 0, 1);
    }
    bool SliderCheck()
    {
        bool IsCatTouched;
        if (Physics2D.OverlapBox(UpperPoint.position, CatCheckRadius, 0f, Cat))
        {
            IsCatTouched = true;
        }
        else
        {
            IsCatTouched = false;
        }
        return IsCatTouched;
    }
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
