using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainLevelDirector : MonoBehaviour
{
    private static MainLevelDirector instance;
    public static MainLevelDirector Instance
    {
        get
        {
            if (instance == null)
            {
                throw new NullReferenceException("No MainLevelDirector in scene");
            }
            return instance;
        }
    }
    //[SerializeField  ]
    private MainPlane mainPlane;
    //[SerializeField]
    private GameObject mainEnemy;
    private GameObject bossPlane;
    //[SerializeField]
    private PlayerData data;
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
    private MainPlane currentPlane;

    public Action GameStartAction;
    public Action GameOverAction;

    private void Awake()
    {

        instance = this;
        Init();
    }
    private void Start()
    {
        if (GameStartAction != null)
        {
            GameStartAction();
        }
        StartCoroutine(Decorate());
        StartCoroutine(DecorateBoss());
    }

    private void Init()
    {
        mainPlane = Resources.Load<MainPlane>("Prefab/Player/MainPlane");
        mainEnemy = Resources.Load<GameObject>("Prefab/Enemys/Enemy");
        bossPlane = Resources.Load<GameObject>("Prefab/Boss");

        data = Resources.Load<PlayerData>("PlayerData");
        maxScore = data.maxScore;
    }

    private IEnumerator Decorate()
    {

        yield return new WaitForSeconds(2);
        currentPlane = Instantiate(mainPlane, mainPlane.transform.position, Quaternion.identity);
        Instantiate(mainEnemy, mainEnemy.transform.position, Quaternion.identity);

        currentPlane.OnDeadEvent += OnMainPlaneDead;
    }
    private IEnumerator DecorateBoss()
    {
        yield return new WaitForSeconds(2);
        Instantiate(bossPlane, mainEnemy.transform.position, Quaternion.identity);
    }

    private void OnMainPlaneDead()
    {
        playerLifeCount--;
        if (playerLifeCount > 0)
        {
            StartCoroutine(Decorate());
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
}
