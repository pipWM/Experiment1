using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class StartPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            NextCondition();
        }

    }
    public void NextCondition()
    {
        Debug.Log("clicked!");
        SceneManager.LoadScene("PreFirstCondition");
    }
}
