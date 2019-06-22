using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorDisplay : MonoBehaviour
{
    public static void SetWordColourBlue(TextMeshProUGUI text)
    {
        VertexGradient textGradient = text.colorGradient;
        textGradient.bottomLeft = new Color32(33, 255, 0, 255);
        textGradient.bottomRight = new Color32(112, 215, 167, 255);
        textGradient.topLeft = new Color32(18, 230, 154, 255);
        textGradient.topRight = new Color32(169, 223, 255, 255);
        text.colorGradient = textGradient;
    }

    public static void SetTextColourRed(TextMeshProUGUI text)
    {
        VertexGradient textGradient = text.colorGradient;
        textGradient.bottomLeft = new Color32(255, 185, 0, 255);
        textGradient.bottomRight = new Color32(255, 84, 84, 255);
        textGradient.topLeft = new Color32(255, 49, 96, 255);
        textGradient.topRight = new Color32(255, 0, 0, 255);
        text.colorGradient = textGradient;
    }
}
