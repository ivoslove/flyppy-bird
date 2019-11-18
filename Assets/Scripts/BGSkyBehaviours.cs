using System.Collections;
using System.Collections.Generic;
using GameApp;
using UnityEngine;
using UnityEngine.UI;

public class BGSkyBehaviours : MonoBehaviour
{
    private Transform m_bgImage1;
    public Transform m_bgImage2;

    public Vector3 m_bgImage1InitPos;

    public Vector3 m_bgImage2InitPos;

    void Start()
    {
        m_bgImage1 = GameObject.Find("SkyBgImageNode/星空1").transform;
        m_bgImage2 = GameObject.Find("SkyBgImageNode/星空2").transform;
        m_bgImage1InitPos = m_bgImage1.position;
        m_bgImage2InitPos = m_bgImage2.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManeger.Instance.IsOver)
        {
            return;
        }

        if (GameManeger.Instance.IsPauseGame)
        {
            return;
        }

        //画布移动
        m_bgImage1.transform.position+=new Vector3(-1f*GloableValue.MoveSpeed*Time.deltaTime,0,0);
        m_bgImage2.transform.position += new Vector3(-1f * GloableValue.MoveSpeed * Time.deltaTime, 0, 0);

        if (Vector3.Distance(m_bgImage2.transform.position, m_bgImage1InitPos) <= 0.1f)
        {
            m_bgImage1.transform.position = m_bgImage2InitPos;
        }

        if (Vector3.Distance(m_bgImage1.transform.position, m_bgImage1InitPos) <= 0.1f)
        {
            m_bgImage2.transform.position = m_bgImage2InitPos;
        }

    }


    public void ReplayInit()
    {
        m_bgImage1.position = m_bgImage1InitPos ;
        m_bgImage2.position = m_bgImage2InitPos ;
    }
}
