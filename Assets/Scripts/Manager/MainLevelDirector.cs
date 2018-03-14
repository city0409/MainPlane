using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainLevelDirector : Singleton<MainLevelDirector>
{
    //private static MainLevelDirector instance;
    //public static MainLevelDirector Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            throw new NullReferenceException("No MainLevelDirector in scene");
    //        }
    //        return instance;
    //    }
    //}
    private MainPlane mainPlane;
    private MainEnemy mainEnemy;
    private MainBoss bossPlane;
    private TankEnemy tankPrefab;

    private PlayerData data;
    private GameData dataVolume;

    private int score;
    private int maxScore;
    private int playerLifeCount = 3;
    
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (maxScore < score)
            {
                data.maxScore = value;
                maxScore = value;
            }
        }
    }

    public int MaxScore
    {
        get { return maxScore; }
        //private  set { maxScore = value; }
    }
    public int PlayerLifeCount { get { return playerLifeCount; } }
    public MainPlane CurrentAirPlane { get; private set; }

    public Action GameStartAction;
    public Action GameOverAction;
    public Action GameWinAction;

    protected override void Awake()
    {
        Init();
    }
    private void Start()
    {
        if (GameStartAction != null)
        {
            GameStartAction();
        }
        if (UIManager.Instance != null)
            UIManager.Instance.FaderOn(false, 1f);
        StartCoroutine(Decorate());
    }

    private void Init()
    {
        mainPlane = Resources.Load<MainPlane>("Prefab/MainPlane");
        mainEnemy = Resources.Load<MainEnemy>("Prefab/Enemys/Enemy");
        bossPlane = Resources.Load<MainBoss>("Prefab/Enemys/Boss");
        tankPrefab= Resources.Load<TankEnemy>("Prefab/Enemys/Tank");



        dataVolume = Resources.Load<GameData>("GameData");
        data = Resources.Load<PlayerData>("PlayerData");
        maxScore = data.maxScore;
    }

    private IEnumerator Decorate()
    {
        yield return new WaitForSeconds(2);
        CurrentAirPlane = Instantiate(mainPlane, mainPlane.transform.position, Quaternion.identity);
        Instantiate(mainEnemy, mainEnemy.transform.position, Quaternion.identity);
        GameManager.Instance.Player = CurrentAirPlane;
        CurrentAirPlane.OnDeadEvent += OnMainPlaneDead;
        yield return new WaitForSeconds(10);
        Instantiate(tankPrefab, tankPrefab.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(10);
        Instantiate(bossPlane, bossPlane.transform.position, Quaternion.identity);

    }
    private IEnumerator RebornPlayer()
    {
        yield return new WaitForSeconds(2);
        CurrentAirPlane=Instantiate(mainPlane, mainPlane.transform.position, Quaternion.identity);
        GameManager.Instance.Player = CurrentAirPlane;
        CurrentAirPlane.OnDeadEvent += OnMainPlaneDead;
    }

    private void OnMainPlaneDead()
    {
        playerLifeCount--;
        if (playerLifeCount > 0)
        {
            StartCoroutine(RebornPlayer());
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (GameOverAction != null)
        {
            GameOverAction();
        }
        AddHistoryScore();
    }

    public void GameWin()
    {
        if (GameWinAction != null)
        {
            GameWinAction();
        }
        StartCoroutine(BackToMenu());
        AddHistoryScore();
    }

    public IEnumerator BackToMenu(float dalayTime=2.0f)
    {
        AddHistoryScore();
        yield return new WaitForSecondsRealtime(dalayTime);
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(1);
    }

    private void AddHistoryScore()
    {
        if (score < 0) return;
        if (data.LeaderboardDatas.Count>=10)
        {
            for (int i = 0; i < data.LeaderboardDatas.Count; i++)
            {
                if (score>data.LeaderboardDatas[i].score)
                {
                    LeaderboardData leaderboardData = new LeaderboardData();
                    leaderboardData.score = score;
                    leaderboardData.date = System.DateTime.Now.ToString("yy-MM-dd,h:mm:ss tt");
                    data.LeaderboardDatas.Add(leaderboardData);
                    break;
                }
            }
            if (data.LeaderboardDatas.Count >10)
            {
                data.LeaderboardDatas.RemoveAt(data.LeaderboardDatas.Count-2);
            }
        }
        else
        {
            LeaderboardData leaderboardData = new LeaderboardData();
            leaderboardData.score = score;
            leaderboardData.date = System.DateTime.Now.ToString("yy-MM-dd,h:mm:ss tt");
            data.LeaderboardDatas.Add(leaderboardData);
        }
    }
}
