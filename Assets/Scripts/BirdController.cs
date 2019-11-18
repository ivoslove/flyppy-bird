using System.Collections;
using System.Collections.Generic;
using GameApp;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{

    private Rigidbody2D m_rigidbody;

    private KeyCode jumpKeyCode = KeyCode.Space;

    private bool m_enable = true;

    [SerializeField]
    private float m_jumpForce = 0;

    private Quaternion m_downQuaternion;

    private Quaternion m_forwardQuaternion;

    private float tileSooth=2;//摇摆平滑度系数
    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_downQuaternion=Quaternion.Euler(0,0,-70);
        m_forwardQuaternion=Quaternion.Euler(0,0,25);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManeger.Instance.IsPauseGame)
            return;

        if (m_enable)
        {
            ListeningInput();
        
        }
    }

    private void ListeningInput()
    {



#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
      
#else

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
#endif
        {
            transform.rotation = m_forwardQuaternion;
            m_rigidbody.velocity=Vector2.zero;
            m_rigidbody.AddForce(Vector2.up* m_jumpForce, ForceMode2D.Force);
            Debug.Log("跳！！！");
            GameManeger.Instance.JumpEvent();
        }
        transform.rotation=Quaternion.Lerp(transform.rotation,m_downQuaternion, tileSooth*Time.deltaTime);
    }

    void OnTriggerEnter2D()
    {
        GameManeger.Instance.IsOver = true;
        GameManeger.Instance.GameOver();
        m_enable = false;

        m_rigidbody.simulated = false;
        m_rigidbody.velocity = Vector2.zero;
    }

    private void OnBecameInvisible()
    {
        if (!GameManeger.Instance.IsOver)
        {
          
            //print("在摄像机视野外");
            Debug.Log("摄像机视野外");
            GameManeger.Instance.IsOver = true;
            GameManeger.Instance.GameOver();
            m_enable = false;
            m_rigidbody.simulated = false;
            m_rigidbody.velocity = Vector2.zero;
            Debug.Log("失去重力效果！！！");
        }




    }


    public void Init()
    {
        m_rigidbody.simulated = true;
        m_rigidbody.transform.rotation=Quaternion.identity;
        m_rigidbody.velocity = Vector2.zero;
        m_rigidbody.MovePosition(new Vector2(-3.46f, -0.54f));
 

       m_enable = true;
    }
}
