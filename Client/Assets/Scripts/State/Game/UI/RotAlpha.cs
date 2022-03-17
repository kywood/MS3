using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Defines;

public class RotAlpha : MonoBehaviour
{
    // Start is called before the first frame update


    public float RotSpeed = 0.2f;

    float _alpha = 1.0f;
    float _disAlpha = 1.0f;

    eDir _dir = eDir.Left;

    RawImage texture;

    void Start()
    {
        texture = GetComponent<RawImage>();
        texture.texture.wrapMode = TextureWrapMode.Repeat;
    }

    // Update is called once per frame
    void Update()
    {
        if(_dir == eDir.Left)
            _alpha -= Time.deltaTime * RotSpeed;
        else
            _alpha += Time.deltaTime * RotSpeed;

        if (_alpha <= 0f && _dir == eDir.Left)
        {
            _alpha = 0f;
            _dir = eDir.Right;
        }
        else if (_alpha >= 1.5f && _dir == eDir.Right)
        {
            _alpha = 1.5f;
            _dir = eDir.Left;
        }

        _disAlpha = _alpha;


        if (_disAlpha <= 0.5f)
        {
            _disAlpha = 0.5f;
        }
        else if (_disAlpha >= 1f)
        {
            _disAlpha = 1f;
        }

        //Color c = new Color(texture.color);

        texture.color = new Color(
            texture.color.r,
            texture.color.g,
            texture.color.b,
            _disAlpha
            );

        //texture.color.a = _disAlpha;

        //Rect currentUV = texture.uvRect;
        //currentUV.x -= Time.deltaTime * RotSpeed;

        //if (currentUV.x <= -1f || currentUV.x >= 1)
        //{
        //    currentUV.x = 0f;
        //}

        //texture.uvRect = currentUV;
    }
}
