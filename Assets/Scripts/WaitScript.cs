using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitScript : MonoBehaviour
{
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GameObject.Find("WaitText").GetComponent<Text>();
        int ExperimentNumber = StartExperiment.ExperimentNumber[StartExperiment.NowExperiment - 1];
        if(StartExperiment.NowExperiment >= StartExperiment.ExperimentCount)
        {
            text.text = "お疲れさまでした。これで実験は終了です。HMDをはずし、アンケートを記入してください。\nこの実験の条件は" + ExperimentNumber + "です。";
        }
        else
        {
            text.text = "お疲れさまでした。HMDをはずし、アンケートを記入してください。\nこの実験の条件は" + ExperimentNumber + "です。\n十分な休憩をとったら次の実験に移ってください。";
        }
       
    }

    public void Clicked()
    {
        /*
        if (StartExperiment.NowExperiment % 2 == 0)
        {
            SceneManager.LoadScene("PreFirstCondition");
        }
        else
        {
            SceneManager.LoadScene("PreExperiment");
        }*/
        SceneManager.LoadScene("PreFirstCondition");
    }

    public void QuitAplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
#endif
        UnityEngine.Application.Quit();
    }
}
