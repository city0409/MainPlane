using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup gameOverGroup;
    private MainLevelDirector director;

    private void Start()
    {
        director = MainLevelDirector.Instance;
        director.GameWinAction += DisplayText;
        gameOverGroup.alpha = 0;
    }

    public void DisplayText()
    {
        gameOverGroup.alpha = 1;
    }
}
