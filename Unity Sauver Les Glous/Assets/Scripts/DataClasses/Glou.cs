using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glou
{
    public float hue { get; } // [0.0f, 1.0f]
    public float sizeMultiplier { get; } //Normal size = 1.0f
    public int? houseID { get; set; }

    Glou(float hue, float sizeMultiplier)
    {
        this.hue = hue;
        this.sizeMultiplier = sizeMultiplier;
    }
}