using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    [SerializeField] Vector2 EffectMultiplayer;
    Transform CamTransform;
    Vector3 lastcampos;
    float TextureSizeX;
   
    void Start()
    {
        CamTransform = Camera.main.transform;
        lastcampos = Vector3.zero;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        TextureSizeX = texture.width / sprite.pixelsPerUnit;
    }
    void FixedUpdate()
    {
        Vector3 DeltaMovement = CamTransform.position - lastcampos;
        transform.position += new Vector3(DeltaMovement.x * EffectMultiplayer.x, DeltaMovement.y * EffectMultiplayer.y, 0);
        lastcampos = CamTransform.position;

        if (Mathf.Abs(CamTransform.position.x - transform.position.x) >= TextureSizeX)
        {
            float offsetPositionX = (CamTransform.position.x - transform.position.x) % TextureSizeX;
            transform.position = new Vector3(CamTransform.position.x + offsetPositionX, transform.position.y);
        }
        
    }
}
