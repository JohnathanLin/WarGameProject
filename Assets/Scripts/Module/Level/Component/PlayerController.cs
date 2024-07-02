using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.5f;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        GameApp.CameraManager.SetPos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0)
        {
            ani.Play("idle");
        } else
        {
            if ((h > 0 && transform.localScale.x < 0) || (h < 0 && transform.localScale.x > 0))
            {
                Flip();
            }

            //Translate���ƶ�, Vector3.right * h�ǳ���, moveSpeed * Time.deltaTime���ٶȳ���ʱ��
            //transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
            //��������Ϊ�˿���Player�ƶ���Χ����ʹ��Translate��
            
            Vector3 pos = transform.position + Vector3.right * h * moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -32, 24);
            transform.position = pos;

            GameApp.CameraManager.SetPos(transform.position);

            ani.Play("move");
        }
    }

    //ת��
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
