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
        dataVolume = Resources.Load<GameData>("GameData");
        data = Resources.Load<PlayerData>("PlayerData");
        tankPrefab= Resources.Load<TankEnemy>("Prefab/Enemys/Tank");
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
    }

    public void GameWin()
    {
        if (GameWinAction != null)
        {
            GameWinAction();
        }
        StartCoroutine(BackToMenu());
    }

    public IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
