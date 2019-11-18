using System.Collections;
using System.Collections.Generic;
using App.Dispatch;
using GameApp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopView : BaseView
{

    private TextMeshProUGUI m_scoreTextMesh;

    private TextMeshProUGUI m_recordSoreText;//历史纪录

    private Toggle m_musicToggle;

    private Button m_exitBtn;

    public TopView()
    {
        Dispatcher.Listener<int>("UpdateScoreView",UpdateScoreView);
    }

    public override void InitPreLoadView()
    {
        GameObject cacheGameObject = Resources.Load<GameObject>("TopView");
        GameObject instance = GameObject.Instantiate(cacheGameObject,GameObject.Find("Canvas").transform);
        if (CanvasTransform == null)
            CanvasTransform = GameObject.Find("Canvas").transform;
        instance.transform.SetParent(CanvasTransform);
        viewNode = instance.transform;
        m_scoreTextMesh=instance.transform.Find("ScoreTip/ScoreText").GetComponent<TextMeshProUGUI>();
        m_scoreTextMesh.text = "0";
        instance.SetActive(true);
        m_musicToggle= instance.transform.Find("MusicToggle").GetComponent<Toggle>();
  
        m_musicToggle.onValueChanged.AddListener(OnMusicTgValueChanged);

        m_exitBtn = instance.transform.Find("ExitBtn").GetComponent<Button>();
        m_exitBtn.onClick.AddListener(OnClickExitBtn);

        m_recordSoreText=instance.transform.Find("RecordSoreText").GetComponent<TextMeshProUGUI>();
        m_recordSoreText.text = GameManeger.Instance.GetRecord().ToString();
    }

    #region private method

    private void UpdateScoreView(int score)
    {
        m_scoreTextMesh.text = score.ToString();

    }

    private void OnMusicTgValueChanged(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("打开音乐");
        }
        else
        {
            Debug.Log("静音模式");
        }
        Dispatcher.DoWork<bool>("OpenOrCloseMusic", isOn);
    }

    private void OnClickExitBtn()
    {
        Application.Quit();
    }



    #endregion

    #region public method

    public void ReplayInit()
    {
        m_recordSoreText.text = GameManeger.Instance.GetRecord().ToString();
        m_scoreTextMesh.text = "0";

    }

    #endregion

}
