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

    //������λ���Ƿ���2D��ײ����
    public static void ScreenPointToRay2D(Camera cam, Vector2 mousePos, System.Action<Collider2D> callback)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
        callback?.Invoke(col);
    }
}
