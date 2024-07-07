using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Tools
{
    public static void SetIcon(this UnityEngine.UI.Image img, string res)
    {
        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }
}
