using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotCreate : MonoBehaviour {

    public GameObject dot;
    public Material material;
    GameObject CenterEye;
    Transform CenterEyeTransform;
    Transform DotObjectTransform;
    public int ObjectNumber;
    public bool IsReverse;
    public float RMin;
    public float RMax;

    public int BallCount;

    // Use this for initialization
    void Start () {
        if(material != null)
        {
            dot.GetComponent<Renderer>().material = material;
        }
        CenterEye = GameObject.Find("CenterEyeAnchor");
        CenterEyeTransform = CenterEye.GetComponent<Transform>();
        if (CenterEyeTransform == null) Debug.Log("transformがnullだよー");
        DotObjectTransform = GameObject.Find("DotObject" + ObjectNumber).GetComponent<Transform>();
    }

    public void startCreate()
    {
        //BallCount = (int)(Frequence * 100f / speed);
        for (int i = 0; i < BallCount; i++)
        {
            CreateFirst();
        }
    }

    public void startCount()
    {
        InvokeRepeating("Count", 1, 0.1f);
    }

    void Create()
    {
        //ドットを作る
        //var EyeRotation = CenterEyeTransform.rotation;
        int dir = Random.Range(0, 359);
        float len = Random.Range(RMin, RMax);
        Vector3 pos;
        if (!IsReverse)
        {
            //ドットが前から飛んでくる
            pos = this.transform.position + new Vector3(len * Mathf.Cos(dir * Mathf.Deg2Rad), len * Mathf.Sin(dir * Mathf.Deg2Rad), 0);
        }
        else
        {
            //ドットが後ろから飛んでくる
            pos = transform.position + new Vector3(0, 0, -100) + new Vector3(len * Mathf.Cos(dir * Mathf.Deg2Rad), len * Mathf.Sin(dir * Mathf.Deg2Rad), 0);
        }
        GameObject instance = (GameObject)Instantiate(dot, pos, Quaternion.identity);

        instance.GetComponent<DotMove>().IsReverse = IsReverse;
        instance.transform.parent = DotObjectTransform;
    }

    void CreateFirst()
    {
        //初期ドットを作る
        //Quaternion EyeRotation = CenterEyeTransform.rotation;
        int dir = Random.Range(0, 359);
        float len = Random.Range(RMin, RMax);
        Vector3 pos;
        float AddDis;
        if (!IsReverse)
        {
            //ドットが前から飛んでくる
            pos = this.transform.position + new Vector3(len * Mathf.Cos(dir * Mathf.Deg2Rad), len * Mathf.Sin(dir * Mathf.Deg2Rad), 0);
            AddDis = Random.Range(-100f, 0f);
        }
        else
        {
            //ドットが後ろから飛んでくる
            pos = transform.position + new Vector3(0, 0, -100) + new Vector3(len * Mathf.Cos(dir * Mathf.Deg2Rad), len * Mathf.Sin(dir * Mathf.Deg2Rad), 0);
            AddDis = Random.Range(0, 100f);
        }
        
        
        GameObject instance = (GameObject)Instantiate(dot, pos + new Vector3(0, 0, AddDis), Quaternion.identity);
        DotObjectTransform = GameObject.Find("DotObject" + ObjectNumber).GetComponent<Transform>();
        instance.transform.parent = DotObjectTransform;

        /*
        instance.GetComponent<DotMove>().speed = speed;
        instance.GetComponent<DotMove>().IsReverse = IsReverse;
        instance.GetComponent<DotMove>().IsRotate = IsRotate;*/

    }


    void Count()
    {
        int count = 0;
        if (GameObject.Find("DotObject" + ObjectNumber) != null)
        {
            count = GameObject.Find("DotObject" + ObjectNumber).GetComponent<Transform>().childCount;
        }
        //Debug.Log(ObjectNumber+": "+count+"個");

        for (int i = 0; i < BallCount - count; i++)
        {
            Create();
        }
    }

    public void Cancel()
    {
        CancelInvoke();
    }

}
