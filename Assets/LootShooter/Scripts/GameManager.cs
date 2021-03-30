using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float SceneReloadDelay = 2f;
	public float QuitWindow = 2;
	public bool IsQutting;
	public vHUDController HudController;

	private void Start()
	{
		HudController.ShowText("В конце пещеры должны быть разбившиеся дроны", 5f, 0.5f);
		HudController.ShowText("Найдите уцелевший дрон и активируйте на нём маяк", 5f, 0.5f);
	}
	private void Update()
	{

		if(Input.GetKeyDown(KeyCode.T))
		{
			SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(!IsQutting)
			ShowExitWarning();
		}
		if (Input.GetKeyDown(KeyCode.Return) && IsQutting)
		{
			Application.Quit();
		}
	}  
	public void ShowExitWarning()
	{
		HudController.ShowText("Чтобы выйти, нажмите Enter");
		IsQutting = true;
		StartCoroutine(StopQuitting());
	}
	private IEnumerator StopQuitting()
	{
		yield return new WaitForSeconds(QuitWindow);
		IsQutting = false;
	}
	public void ReloadScene()
	{
		StartCoroutine(ReloadSceneDelayed());
	}
	private IEnumerator ReloadSceneDelayed()
	{
		yield return new WaitForSeconds(SceneReloadDelay);
		SceneManager.LoadScene(0);		
	}
}

