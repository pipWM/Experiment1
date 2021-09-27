using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugController : MonoBehaviour
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
            NextScene();
        }
    }
    void NextScene()
    {
        if (StartExperiment.NowExperiment < 4)
        {
            SceneManager.LoadScene("Wait");
        }
        else
        {
            SceneManager.LoadScene("Final");
        }

    }
}
