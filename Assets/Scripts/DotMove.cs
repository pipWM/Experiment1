using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotMove : MonoBehaviour {
    Transform t;
    Rigidbody rb;
    //Color color;
    GameObject FOE;
    int dir;
    float StartPos;
    public float speed;
    public bool IsReverse;
    public bool IsRotate;
    Material material;
	// Use this for initialization
	void Start () {
        material = gameObject.GetComponent<MeshRenderer>().material;
        dir = Random.Range(0, 359);
        FOE = GameObject.Find("FOE");
        StartPos = transform.position.z;
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

        var tmpC = material.color;
        tmpC.a = 0f;
        material.color = tmpC;
        /*
        Vector3 v;
        if (!IsReverse)
        {
            //ドットが前から飛んでくる
            v = new Vector3(0, 0, -speed);
        }
        else
        {
            //ドットが後ろから飛んでくる
            v = new Vector3(0, 0, speed);
        }

        rb.velocity = v;*/
    }
	
	// Update is called once per frame
	void Update () {
        //CheckAlpha();
        var tmpC = material.color;
        tmpC.a = Mathf.Min(1f, material.color.a + 0.1f);
        material.color = tmpC;
        CheckDestroy();
	}

    void CheckDestroy()
    {

        //遠くまで行ったらドットを消す
        if(Mathf.Abs(transform.position.z - StartPos) > 100 )
        {
            Destroy(this.gameObject);
        }
        if (transform.position.z < -1 || transform.position.z > 100)
        {
            Destroy(this.gameObject);
        }

    }
}
