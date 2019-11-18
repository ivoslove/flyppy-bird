using System.Collections;
using System.Collections.Generic;
using App.Dispatch;
using GameApp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : BaseView
{

    private TextMeshProUGUI m_resultText;

    private TextMeshProUGUI m_newRecordTipTxt;//新的纪录要显示标语

    private Button m_replayButton;

    private Button m_exitBtn;

    public ResultView()
    {
       
    }

    public override void InitPreLoadView()
    {
        GameObject cacheGameObject = Resources.Load<GameObject>("ResultView");
        GameObject instance = GameObject.Instantiate(cacheGameObject, GameObject.Find("Canvas").transform);
        if (CanvasTransform == null)
            CanvasTransform = GameObject.Find("Canvas").transform;
        instance.transform.SetParent(CanvasTransform);
        viewNode = instance.transform;
        m_resultText = instance.transform.Find("ResultPanelView/ResultText").GetComponent<TextMeshProUGUI>();
        instance.SetActive(false);//先隐藏

        m_newRecordTipTxt= instance.transform.Find("ResultPanelView/NewRecordTipTxt").GetComponent<TextMeshProUGUI>();
        m_newRecordTipTxt.gameObject.SetActive(false);
        m_exitBtn = instance.transform.Find("ResultPanelView/ExitButton").GetComponent<Button>();
        m_exitBtn.onClick.AddListener(OnClickExitBtn);

        m_replayButton = instance.transform.Find("ResultPanelView/ReplayButton").GetComponent<Button>();
        m_replayButton.onClick.AddListener(OnClickReplayBtn);
    }

    private void OnClickReplayBtn()
    {
        viewNode.gameObject.SetActive(false);

        GameManeger.Instance.Replay();

    }

    public void OpenView()
    {
        viewNode.gameObject.SetActive(true);
        viewNode.SetAsLastSibling();
        //刷新纪录面板
        int highest = GameManeger.Instance.GetRecord();
        int currentScore = GameManeger.Instance.GetCurrentScore();
        m_resultText.text = currentScore.ToString();

    
        m_newRecordTipTxt.gameObject.SetActive(currentScore > highest);

    }
    

    public void OnClickExitBtn()
    {
        Application.Quit();
    }
}