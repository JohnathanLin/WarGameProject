using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameApp.CameraManager.SetPos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
