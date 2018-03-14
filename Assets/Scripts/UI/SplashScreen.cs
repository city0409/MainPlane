using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using UnityEngine.SceneManagement;

public class SplashScreen : Photon.PunBehaviour
{
    [SerializeField]
    private GameObject Spinner;

    //[SerializeField]
    //private string loadSceneName;

    [SerializeField]
    private CanvasGroup loginPannelGroup;
    [SerializeField]
    private CanvasGroup logoPannelGroup;

    private bool luaInjected;
    private bool joinedLobby;
    //private bool canLoad;

    private void Start()
    {
        UIManager.Instance.FaderOn(false, 1f);

        NetworkManager.Instance.OnLuaInjected(OnLuaInjected);
        StartCoroutine(ShowLoginPannel());
    }

    private void OnLuaInjected()
    {
        luaInjected = true;
        //Spinner.SetActive(false);
    }
    public override void OnJoinedLobby()
    {
        joinedLobby = true;
    }

    private IEnumerator ShowLoginPannel()
    {
        yield return new WaitForSeconds(2f);
        while (!luaInjected || !joinedLobby)
        {
            yield return null;
        }
        Spinner.SetActive(false);
        DisplayMenu();
        //UIManager.Instance.FaderOn(true, 1f);
        //yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene(loadSceneName);
    }

    private void DisplayMenu()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(DOTween.To(() => logoPannelGroup.alpha, x => logoPannelGroup.alpha = x, 0,
            1).OnComplete(() =>
            {
                logoPannelGroup.interactable = false;
                logoPannelGroup.blocksRaycasts = false;
            }));
        mySequence.Append(DOTween.To(() => loginPannelGroup.alpha, x => loginPannelGroup.alpha = x, 1, 1).OnComplete(() =>
        {
            loginPannelGroup.interactable = true;
            loginPannelGroup.blocksRaycasts = true;
        }));
    }
}
