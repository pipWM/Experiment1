using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreExperiment : MonoBehaviour {

    Text text;
	// Use this for initialization
	void Start () {
        //StartExperiment.NowExperiment++;
        text = GameObject.Find("PreText").GetComponent<Text>();
        text.text = "この条件は" + StartExperiment.ExperimentNumber[StartExperiment.NowExperiment]+"です";
	}

    public void Clicked()
    {
        SceneManager.LoadScene("Experiment");
    }

    public void QuitAplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    
}
