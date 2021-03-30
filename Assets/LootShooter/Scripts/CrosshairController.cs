using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
	public RectTransform Crosshair;

	public float sizeIncreasingSpeed = 1.1f;
	public float sizeDecreasingSpeed = 1.02f;

	public float sizeIncreasingAmountPerShot = 0.3f;

	private Vector3 _defaultSize = Vector3.one;
	private Vector3 _maxSize = Vector3.one * 1.1f;

	// max dispersion = 7

	private Coroutine _increaseSizeCoroutine;
	private Coroutine _decreaseSizeCoroutine;


	private void Awake()
	{
		_defaultSize = Crosshair.localScale;
		_decreaseSizeCoroutine = StartCoroutine(DecreaseSize());
	}
	public void IncreaseCrosshairSise()
	{
		_increaseSizeCoroutine = StartCoroutine(IncreaseSize());
	}
	private IEnumerator IncreaseSize()
	{
		float increasedAmount = 0;

		while (increasedAmount < sizeIncreasingAmountPerShot)
		{
			if (Crosshair.localScale.x >= _maxSize.x)
			{
				yield break;
			}

			increasedAmount += (Crosshair.localScale.x * sizeIncreasingSpeed - Crosshair.localScale.x);
			Crosshair.localScale *= sizeIncreasingSpeed;

			yield return new WaitForFixedUpdate();
		}
	}
	private IEnumerator DecreaseSize()
	{
		while (true)
		{
			if (Crosshair.localScale.x > _defaultSize.x)
			{
				Crosshair.localScale *= 1 / sizeDecreasingSpeed;
			}

			yield return new WaitForFixedUpdate();
		}
	}
}
