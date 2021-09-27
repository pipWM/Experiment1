using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotMoveController : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public bool IsReverse;
    public bool AddAccele;
    float StartTime;

    float acceleMax;
    float accele;

    bool canStart = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (!IsReverse)
        {
            //ドットが前から飛んでくる
            speed = -speed;
        }
        

        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart && Time.time - StartTime >= StartExperiment.waitTime)
        {
            startMove();
            canStart = false;
        }

        /*
        if (IsStart && !FootGround && Time.time - StartTime >= 0.5f)
        {
            if (!IsReverse)
            {
                //ドットが前から飛んでくる
                accele = Random.Range(-1.0f, -0.5f);
            }
            else
            {
                accele = Random.Range(0.5f, 1f);
            }
            
            ChangeAccele();
            StartTime = Time.time;
            FootGround = !FootGround;
        }
        if (IsStart && FootGround && Time.time - StartTime >= 0.3f)
        {
            RemoveAccele();
            StartTime = Time.time;
            FootGround = !FootGround;
        }*/
    }

    public void startMoveWait()
    {
        //動き始める前の5秒待機スタート
        StartTime = Time.time;
        canStart = true;
    }

    public void startMove()
    {
        StartTime = Time.time;
        Vector3 v = new Vector3(0f, 0f, speed);
        rb.velocity = v;
    }

    void ChangeAccele()
    {
        if (!AddAccele) return;
        Vector3 force = new Vector3(0.0f, 0.0f, accele);
        rb.AddForce(force, ForceMode.Impulse);

    }
    void RemoveAccele()
    {
        if (!AddAccele) return;
        Vector3 force = new Vector3(0.0f, 0.0f, -accele);
        Vector3 v = rb.velocity;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
