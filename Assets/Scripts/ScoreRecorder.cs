using System.Collections;
using System.Collections.Generic;
using App.Dispatch;
using GameApp;
using UnityEngine;

public class ScoreRecorder
{

    private int m_currentScore;

    public ScoreRecorder()
    {
        m_currentScore = 0;
        Dispatcher.Listener<int>("AddScore",AddScore);
    }

    public void AddScore(int score)
    {
        Debug.Log("得分");
        this.m_currentScore += score;

        Dispatcher.DoWork<int>("UpdateScoreView",m_currentScore);
        GameManeger.Instance.ScoreEvent();

        GameManeger.Instance.AdjustmLevel(m_currentScore);

    }

    public int GetRecord()
    {
       int score= PlayerPrefs.GetInt("Record");
       Debug.Log("历史最高分"+score);
       return score;
    }

    public int CurrentScore
    {
        get=>m_currentScore;
    }


    public void ReplayInit()
    {
        m_currentScore = 0;
    }

    public void RefreshRecord()
    {
        int score = PlayerPrefs.GetInt("Record");
        if (m_currentScore > score)
        {
            //刷新纪录
            PlayerPrefs.SetInt("Record",m_currentScore);
            PlayerPrefs.Save();
        }

    }
}
