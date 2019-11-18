using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipesFactory
{

    private List<PipeGroup> pipeGroupPool;

    public static int count = 0;

    public PipesFactory()
    {
        pipeGroupPool=new List<PipeGroup>();
    }


    public PipeGroup CreatePipeGroup()
    {
        var pipeGroup = pipeGroupPool.FirstOrDefault(p => p.Idle);
        if (pipeGroup == null)
        {
            //如果没有闲置的，就加载预制创建
            PipeGroup newPipeGroup=new PipeGroup();
            pipeGroupPool.Add(newPipeGroup);
            Debug.Log("池中没有闲置管子，去创建管子");
            newPipeGroup.GenratePipeGroup(RandomAdjustPipeGroupPos());
            count++;
            return newPipeGroup;
        }
        else
        {
            Debug.Log("池中有闲置管子，初始化位置即可");
            pipeGroup.GenratePipeGroup(RandomAdjustPipeGroupPos());
            return pipeGroup;
        }
    }

    public float RandomAdjustPipeGroupPos()
    {
     
        float offset = Random.Range(GloableValue.bottomPipePosYMinValue - GloableValue.bottomPipePosYCenterValue, GloableValue.upPipePosYMinValue - GloableValue.upPipePosYCenterValue);
        Debug.Log("随机挪动管子距离为"+offset);
        return offset;
    }


    public void ReplayInit()
    {
        pipeGroupPool.ForEach(p=>p.ReplayInit());
 
    }
}

public class Pipe
{
    

    public bool Idle
    {
        get => m_pipeBehaviour.Idle;
        set => m_pipeBehaviour.Idle = value;
    }

    private PipeBehaviour m_pipeBehaviour;

    public Pipe(int type)
    {
        //GameObject obj = Resources.Load<GameObject>( type==0? "管子-4" : "管子-4（倒）");
      
        var  m_pipeNode=GameObject.Find("PipeNode");
        GameObject obj = m_pipeNode.transform.Find(type == 0 ? "管子-4" : "管子-4（倒）").gameObject;
       var newPipeObj = GameObject.Instantiate(obj);
       newPipeObj.name = type == 0 ? ("gz_" + PipesFactory.count):("gz_dao_"+ PipesFactory.count);
        newPipeObj.gameObject.SetActive(true);
        newPipeObj.transform.SetParent(m_pipeNode.transform);
        
        m_pipeBehaviour = newPipeObj.AddComponent<PipeBehaviour>();
        m_pipeBehaviour.Type = type;
    }

    public void GenratePipeInGameScene(float offset=0)
    {
        m_pipeBehaviour.StartMove(offset);
    }


    public void EnterIdleState()
    {
        m_pipeBehaviour.EnterIdleState();
    }
}

public class PipeGroup
{
    private Pipe m_bottomPipe;
    private Pipe m_upPipe;


    public bool Idle
    {
        get => m_bottomPipe.Idle&&m_upPipe.Idle;
    }

    public PipeGroup()
    {

        m_bottomPipe=new Pipe(1);
        m_upPipe=new Pipe(0);
    }

    public void GenratePipeGroup(float offset=0)
    {
        m_bottomPipe.GenratePipeInGameScene(offset);
        m_upPipe.GenratePipeInGameScene(offset);
    }


    public void ReplayInit()
    {
        m_bottomPipe.Idle = true;
        m_upPipe.Idle = true;
        m_bottomPipe.EnterIdleState();
        m_upPipe.EnterIdleState();
    }
}


