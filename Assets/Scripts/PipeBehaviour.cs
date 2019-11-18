using System;
using System.Collections;
using System.Collections.Generic;
using App.Dispatch;
using GameApp;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeBehaviour : MonoBehaviour
{
    public int Type;//0是上管子 1是下管子

    public bool Idle;

    public bool CanMoving;

    private bool m_havePassed;
    /// <summary>
    /// 初始化
    /// </summary>

    public void StartMove(float offset = 0)
    {
        Debug.Log("管子初始化");
        if (Type == 0)
        {
            this.transform.position= new Vector3(7.07f, GloableValue.bottomPipePosYCenterValue, 0f);
        }
        else
        {
            this.transform.position = new Vector3(7.07f, GloableValue.upPipePosYCenterValue, 0f);
        }
        this.transform.position+=new Vector3(0, offset, 0);
        Idle = false;
        m_havePassed = false;
        CanMoving = true;
    }

    public void EnterIdleState()
    {
        if (Type == 0)
        {
            this.transform.position = new Vector3(7.07f, GloableValue.bottomPipePosYCenterValue, 0f);
        }
        else
        {
            this.transform.position = new Vector3(7.07f, GloableValue.upPipePosYCenterValue, 0f);
        }
        Idle = true;
        m_havePassed = false;
        CanMoving = false;
    }

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManeger.Instance.IsOver)
            return;

        if (GameManeger.Instance.IsPauseGame)
            return;

        if(Idle)
            return;
        if(CanMoving)
           this.transform.position-=new Vector3(1*GloableValue.MoveSpeed*Time.deltaTime,0,0);

        //超出屏幕置为闲置状态,停止移动

        if (this.transform.position.x < -7f)
        {
            Idle = true;
            CanMoving = false;
        }



        if (this.Type == 0 && !Idle&&!m_havePassed&&CanMoving)
        {
            if (this.transform.position.x + this.GetComponent<BoxCollider2D>().size.x / 2 <
                GameManeger.Instance.GetBirdTailPosX())
            {
                Dispatcher.DoWork<int>("AddScore",1);
                m_havePassed = true;
            
            }
     

        }
    }

    void OnBecameInvisible()
    {
        //Vector3 pos = Camera.main.ScreenToViewportPoint(this.transform.position);
        ////超出屏幕置为闲置状态


        //Debug.LogError(string.Format("pos:({0},{1},{2})", pos.x, pos.y, pos.z));
        //Debug.LogError("超出屏幕！");
        //Idle = true;
    }

}
