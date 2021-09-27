using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstPlayer : MonoBehaviour
{
    float StartTime;
    public GameObject dotCreateObject;
    public GameObject dotObject;
    public GameObject rHand;
    DotCreate dotCreate;
    DotMoveController dotMoveCon;
    Text vecText;
    Image vecIm;
    int vecLevel;
    Transform rHandTr;
    // Use this for initialization
    void Start()
    {
        if (dotCreateObject == null) Debug.Log("nullだよー");
        dotCreate = dotCreateObject.GetComponent<DotCreate>();
        dotMoveCon = dotObject.GetComponent<DotMoveController>();
        vecText = GameObject.Find("vection").GetComponent<Text>();

        rHandTr = rHand.GetComponent<Transform>();
        vecIm = GameObject.Find("vecGraph").GetComponent<Image>();

        dotMoveCon.startMoveWait();
        dotCreate.startCreate();
        dotCreate.startCount();
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        getVecLevel();
        checkNextScene();
    }
    void getVecLevel()
    {
        if (rHandTr == null) return;
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
    private void checkNextScene()
    {
        if (Time.time - StartTime > 60f + StartExperiment.waitTime || OVRInput.GetDown(OVRInput.RawButton.A))
        {
            SceneManager.LoadScene("PreExperiment");
        }
    }
}
