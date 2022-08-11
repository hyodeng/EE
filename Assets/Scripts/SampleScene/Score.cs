using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoerText = null;
    private float TimeScore;
    public float Timecurrent;
    private float MaxScore = 100f;
    private bool isEnded;

    private void Start()
    {
        ResetTime();
    }
    private void Update()
    {
        if (! isEnded)
        {
            CheckTimer();
        }
      
        return;

        
    }

    private void CheckTimer()
    {
        Timecurrent += Time.deltaTime;
        if(Timecurrent<MaxScore)
        {
            scoerText.text = $"Score : {Timecurrent}";
        }
        else if (!isEnded)
        {
            EndTimer();
        }
    }

    private void EndTimer()
    {

    }

    private void ResetTime()
    {
        TimeScore = Time.time;
        Timecurrent = 0;
        scoerText.text = $"Score : {Timecurrent}";
        isEnded = false;
    }
}
