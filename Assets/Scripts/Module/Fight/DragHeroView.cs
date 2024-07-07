using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class DragHeroView : BaseView
{
    private void Update()
    {

        if (_canvas.enabled == false)
        {
            return;
        }


        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPos;
    }
    public override void Open(params object[] args)
    {
        transform.GetComponent<Image>().SetIcon(args[0].ToString());
    }
}
