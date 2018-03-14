using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerNetwork : PersistentSingleton<Photon.PunBehaviour> 
{
    private PhotonView photonView;
    private int playersInGame;


	protected override  void Awake() 
	{
        base.Awake();
        photonView = GetComponent<PhotonView>();
        SceneManager.sceneLoaded += OnSceneFinishedLoaded;
	}
	
	private void OnSceneFinishedLoaded( Scene scene,LoadSceneMode mode) 
	{
        if (scene.name == "MainPlane")
        {
            if (PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
	}

    private void MasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        StartCoroutine(LoadGameOthers());
    }
    private IEnumerator LoadGameOthers()
    {
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene("MainPlane");
    }
    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
    }
}
