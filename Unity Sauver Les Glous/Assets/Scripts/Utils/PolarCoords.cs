using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PolarCoords2D
{
    public const float _PI = 3.1459f;
    public const float _TWOPI = 2.0f * 3.1459f;
    public float theta; //angle in radians
    public float r; //radial distance

    //Avoid -180 deg to +180 etc...
    //https://gamemath.com/book/polarspace.html
    public void HandleAliasing()
    {
        if (r == 0.0f)
        {
            theta = 0.0f;
        }
        else
        {
            if (r < 0.0f)
            {
                r = -r;
                theta += _PI;
            }
            if (Mathf.Abs(theta) > _PI)
            {
                //offset PI
                theta += _PI;

                //Wrap on range [0, 2PI]
                theta -= Mathf.Floor(theta / _TWOPI) * _TWOPI;

                //Undo offset
                theta -= _PI;
            }
        }
    }

    //Constructor from cartesian coordinates
    public PolarCoords2D(float x, float y)
    {
        if(x == 0.0f && y == 0.0f)
        {
            r = 0.0f;
            theta = 0.0f;
        }
        else
        {
            r = Mathf.Sqrt(x * x + y * y);
            theta = Mathf.Atan2(y, x);
        }
    }

    public Vector2 ToCartesian()
    {
        return new Vector2(
            r * Mathf.Cos(theta), 
            r* Mathf.Sin(theta)
        );
    }
}