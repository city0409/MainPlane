using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPannel : MonoBehaviour 
{
    [SerializeField]
    private InputField inputField = null;
    private PlayerData playerData;

    [SerializeField]
    private string loadSceneName;

	private void Awake() 
	{
        playerData = Resources.Load<PlayerData>("PlayerData");
	}
	
    public void SetPlayerName()
    {
        playerData.playerName = inputField.text;
        PhotonNetwork.playerName = playerData.playerName;
        StartCoroutine(Login());
    }

	public IEnumerator Login() 
	{
        UIManager.Instance.FaderOn(true, 1f);
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene(loadSceneName);
	}
}
