using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    GameObject DotObject1, DotObject2, DotCreateObject1, DotCreateObject2;

    DataSend dataSend;
    Transform tr, rHandTr;
    float StartTime, rStick;
    int ExperimentNumber, vecLevel;
    bool isMove = false;
    bool canFinish = false;
    Image vecIm;
    Text vecText;
    
    public List<Data> dataList = new List<Data>();

    // Use this for initialization
    void Start () {
        getCom();
        StartTime = Time.time;
        ExperimentNumber = StartExperiment.ExperimentNumber[StartExperiment.NowExperiment];
    }
	
	// Update is called once per frame
	void Update () {
        getVecLevel();

        if(canStart())
        {
            ExperimentController exCon = GameObject.Find("ExperimentController").GetComponent<ExperimentController>();
            //exCon.startCreate();
            exCon.startMoveWait();
            exCon.startCount();
            changeTextColor();
            isMove = true;
            canFinish = true;
            StartTime = Time.time;
        }

        /*
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Object1Delete();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            Object2Delete();
        }*/


        checkNextScene();
    }

    void getVecLevel()
    {
        float rotZ = rHandTr.eulerAngles.z;
        if (rotZ > 180)
        {
            rotZ -= 360f;
            rotZ = Mathf.Max(-40, rotZ);
        }
        else
        {
            rotZ = Mathf.Min(40, rotZ);
        }

        vecLevel = Mathf.RoundToInt(rotZ) * 5;
        //Debug.Log("z: " + rotZ);
        /*
        rStick = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
        if (rStick >= 0)
        {
            vecLevel = 10 * Mathf.CeilToInt(rStick * 10f);
        }
        else
        {
            vecLevel = 10 * Mathf.FloorToInt(rStick * 10f);
        }
        vecLevel += 100;*/
        vecText.text = "" + vecLevel;
        vecIm.fillAmount = ((vecLevel + 200) / 400f);
    }

    void checkNextScene()
    {
        if (canNextScene())
        {
            dataSend.startSendData();
            StartExperiment.NowExperiment++;
            if (StartExperiment.NowExperiment >= StartExperiment.ExperimentCount)
            {
                SceneManager.LoadScene("Finish");
            }
            else
            {
                SceneManager.LoadScene("Wait");
            }
        }
    }

    bool canNextScene()
    {
        return (Time.time - StartTime > 300 && canFinish) || (OVRInput.GetDown(OVRInput.RawButton.A) && StartExperiment.isDebug);
    }

    bool canStart()
    {
        return vecLevel == 0 && !isMove && OVRInput.GetDown(OVRInput.RawButton.RHandTrigger);
    }
    void Object2Delete()
    {
        if (ExperimentNumber == 1) return;

        DotCreateObject2.GetComponent<DotCreate>().Cancel();


        DotObject2.SetActive(!DotObject2.activeSelf);
    }

    void Object1Delete()
    {

        DotCreateObject1.GetComponent<DotCreate>().Cancel();

        DotObject1.SetActive(!DotObject1.activeSelf);
    }

    void makeData()
    {
        float time = Time.time - StartTime;
        Data data = new Data(time,vecLevel);
        dataSend.addData(data);
        dataList.Add(data);
    }

    public void startMove()
    {
        StartTime = Time.time;
        InvokeRepeating("makeData", 0, 0.1f);
    }
    public void startMakeData()
    {
        InvokeRepeating("makeData", 10, 0.1f);
    }

    private void getCom()
    {
        dataSend = GameObject.Find("ExperimentController").GetComponent<DataSend>();
        tr = this.transform;
        DotObject1 = GameObject.Find("DotObject1");
        DotObject2 = GameObject.Find("DotObject2");
        DotCreateObject1 = GameObject.Find("DotCreateObject1");
        DotCreateObject2 = GameObject.Find("DotCreateObject2");     
        rHandTr = GameObject.Find("RightHandAnchor").GetComponent<Transform>();
        vecIm = GameObject.Find("vecGraph").GetComponent<Image>();
        vecText = GameObject.Find("vection").GetComponent<Text>();
    }

    private void changeTextColor()
    {
        //Textを見えなくする 
        Color tmpC = vecText.color;
        tmpC.a = 0;
        vecText.color = tmpC;
    }
}

public class Data
{
    public float time;
    public int vection;
    public Data(float time, int vection)
    {
        this.time = time;
        this.vection = vection;
    }
}
