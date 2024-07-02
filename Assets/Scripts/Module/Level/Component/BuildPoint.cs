using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelId; //¹Ø¿¨id

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameApp.MessageCenter.PostEvent(Defines.ShowLevelDesEvent, LevelId);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameApp.MessageCenter.PostEvent(Defines.HideLevelDesEvent);
    }
}
