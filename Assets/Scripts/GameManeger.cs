using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TaskExtension;


namespace GameApp
{
    public class GameManeger : MonoBehaviour
    {


        public static GameManeger Instance;

        public bool IsPauseGame = false;

        public bool IsOver = false;

        #region private  number

        private PipesFactory m_pipesFactory;

        private float initTime = 0;



        private BGSkyBehaviours bgSkyBehaviours;

        private TopView m_topView;

        private ResultView m_resultView;

        private ScoreRecorder m_scoreRecorder;

        private BirdController m_birdController;

        private AudioManager m_audioManager;

        private List<IGameBehaviour> m_gameBehaviours;

        #endregion

        #region monolife

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            //积分器初始化
            m_scoreRecorder = new ScoreRecorder();

            //获取小鸟控制器
            m_birdController = FindObjectOfType<BirdController>();

            //声音控制器
            m_audioManager=new AudioManager();
            m_audioManager.PlayBGMusic();

            m_gameBehaviours =new List<IGameBehaviour>();
            m_gameBehaviours.Add(m_audioManager);

            //UI初始化
            m_topView =new TopView();
            m_topView.InitPreLoadView();
            m_resultView=new ResultView();
            m_resultView.InitPreLoadView();
       

            //画布管理器
            bgSkyBehaviours = this.gameObject.AddComponent<BGSkyBehaviours>();
            m_pipesFactory =new PipesFactory();
            //先创建一组管子
            m_pipesFactory.CreatePipeGroup();
        }


        void Update()
        {
            if (IsOver)
            {


                return;
            }

            if (initTime > GloableValue.spanTime)
            {
                m_pipesFactory.CreatePipeGroup();
                initTime = 0f;
            }
            else
            {
                initTime += Time.deltaTime;
            }

        }

        public void JumpEvent()
        {
            m_gameBehaviours.ForEach(p=>p.Jump());
        }
        public void ScoreEvent()
        {
            m_gameBehaviours.ForEach(p => p.Score());
        }

        public void GameOver()
        {
            m_gameBehaviours.ForEach(p => p.GameOver());

            UnityTask.RunDelay(TimeSpan.FromSeconds(3.5)).ContinueWith(t =>
            {
                m_resultView.OpenView();
                m_scoreRecorder.RefreshRecord();
            });

   
        }

        public void Pause()
        {
            m_gameBehaviours.ForEach(p => p.Pause());
        }


        #endregion

        #region public method

        public float GetBirdTailPosX()
        {
            float x = m_birdController.transform.position.x - m_birdController.GetComponent<CircleCollider2D>().radius;
            return x;
        }

        public int GetRecord()
        {
            return m_scoreRecorder.GetRecord();
        }

        public int GetCurrentScore()
        {
            return m_scoreRecorder.CurrentScore;
        }
        #endregion

        /// <summary>
        /// 重新游戏
        /// </summary>
        public void Replay()
        {
            //难度将为初级
            //Time.timeScale = 1;
            //m_audioManager.ScalePlaySpeed(1f);

                GloableValue.MoveSpeed = 2f;
                GloableValue.spanTime = 3f;
                m_audioManager.ScalePlaySpeed(1f);

                //分数清零
                m_scoreRecorder.ReplayInit();
                //刷新UI面板
                m_topView.ReplayInit();
                //小鸟初始化位置
                m_birdController.Init();
                //重新播放
                m_audioManager.PlayBGMusic();
                //管子重玩初始化
                m_pipesFactory.ReplayInit();
                //画布重玩初始化
                bgSkyBehaviours.ReplayInit();

                IsOver = false;

                initTime = 0f;

                m_pipesFactory.CreatePipeGroup();//游戏开始先创建一个




        }

        public void AdjustmLevel(int score)
        {
            if (score == 20)
            {
                GloableValue.MoveSpeed *= 1.2f;
                GloableValue.spanTime *= (1 / 1.2f);

            }

            if (score == 40)
            {
                GloableValue.MoveSpeed *= 1.2f;
                GloableValue.spanTime *= (1 / 1.2f);
                m_audioManager.ScalePlaySpeed(1.14f);
            }

            if (score == 80)
            {
                GloableValue.MoveSpeed *= 1.2f;
                GloableValue.spanTime *= (1 / 1.2f);

            }

            if (score == 160)
            {
                GloableValue.MoveSpeed *= 1.2f;
                GloableValue.spanTime *= (1 / 1.2f);
                m_audioManager.ScalePlaySpeed(1.28f);
            }

            //if (score == 25)
            //{
            //    Time.timeScale += 0.2f;

            //}

            //if (score == 50)
            //{
            //    Time.timeScale += 0.5f;
            //    m_audioManager.ScalePlaySpeed(1.14f);
            //}
            //if (score == 100 )
            //{
            //    Time.timeScale += 0.7f;
            //    m_audioManager.ScalePlaySpeed(1.28f);
            //}
        }
    }

}

