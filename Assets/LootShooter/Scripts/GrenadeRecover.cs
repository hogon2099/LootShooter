using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(vThrowManager))]
public class GrenadeRecover : MonoBehaviour
{
    public float CooldownTime = 10f;
    public Image CooldownIndicator;
    private vThrowManager _throwManager;

	private void Start()
	{
		_throwManager = GetComponent<vThrowManager>();
	}
	public void StartGrenadeRecovery()
	{
		StartCoroutine(RecoverGrenade());	
	}
	private IEnumerator RecoverGrenade()
	{
		CooldownIndicator.fillAmount = 0;
		float restoringSpeed = 1 / CooldownTime;

		while (CooldownIndicator.fillAmount < 1)
		{
			CooldownIndicator.fillAmount += restoringSpeed * Time.deltaTime;
			yield return null;
		}

		_throwManager.CanUseThrow(true);
		_throwManager.SetAmount(1);
	}
}
