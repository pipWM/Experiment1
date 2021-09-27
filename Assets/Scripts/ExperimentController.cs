using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperimentController : MonoBehaviour {
    public GameObject whiteBall;
    public GameObject blackBall;

    public GameObject create2;
    DotCreate DotCreate1;
    DotCreate DotCreate2;
    int ExperimentNumber;
    bool isCreateDot2 = true;
    bool canStart = false;
    float startTime;
	// Use this for initialization
	void Start () {

        DotCreate1 = GameObject.Find("DotCreateObject1").GetComponent<DotCreate>();
        DotCreate2 = GameObject.Find("DotCreateObject2").GetComponent<DotCreate>();
        float speed1 = 48f;
        float speed2 = 6f;
        bool IsReverse1 = false;
        bool IsReverse2 = true;
        
        GameObject obj;
        ExperimentNumber = StartExperiment.ExperimentNumber[StartExperiment.NowExperiment];
        switch (ExperimentNumber)
        {
            case 1:
                //白のみ
                create2.SetActive(false);
                isCreateDot2 = false;
                //DotCreateScript1.BallCount = 480;
                break;
            case 2:
                //白、逆黒
                break;
            case 3:
                //白、黒
                IsReverse2 = false;
                break;
            case 4:
                //白、逆白
                DotCreate2.dot = whiteBall;
                break;
            case 5:
                //白、白
                DotCreate2.dot = whiteBall;
                IsReverse2 = false;
                break;
            case 6:
                //黒
                DotCreate1.dot = blackBall;
                create2.SetActive(false);
                isCreateDot2 = false;
                break;
            case 7:
                //黒、逆白
                DotCreate1.dot = blackBall;
                DotCreate2.dot = whiteBall;
                break;
            case 8:
                //黒、白
                DotCreate1.dot = blackBall;
                DotCreate2.dot = whiteBall;
                IsReverse2 = false;
                break;
        }
        DotCreate1.IsReverse = IsReverse1;
        DotCreate2.IsReverse = IsReverse2;
        GameObject.Find("DotObject2").GetComponent<DotMoveController>().IsReverse = IsReverse2;

        startCreate();
    }
	
	// Update is called once per frame
	void Update () {
        if (canStart && Time.time - startTime >= StartExperiment.waitTime)
        {
            startMove();
            canStart = false;
        }
    }
    public void startCreate()
    {
        DotCreate1.startCreate();
        if(isCreateDot2) DotCreate2.startCreate();
    }

    public void startCount()
    {
        DotCreate1.startCount();
        if (isCreateDot2) DotCreate2.startCount();
    }

    public void startMoveWait()
    {
        canStart = true;
        startTime = Time.time;
    }
    public void startMove()
    {
        DotMoveController dotMoveCon1 = GameObject.Find("DotObject1").GetComponent<DotMoveController>();
        DotMoveController dotMoveCon2 = GameObject.Find("DotObject2").GetComponent<DotMoveController>();

        dotMoveCon1.startMove();
        dotMoveCon2.startMove();
        Player player = GameObject.Find("OVRCameraRig").GetComponent<Player>();
        Debug.Log("スタート");
        player.startMove();
    }
}
