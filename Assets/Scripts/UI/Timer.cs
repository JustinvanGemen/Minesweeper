using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

	[SerializeField] private Text _text;
	private int _secondsPassed;
	private int _minutesPassed;

	private void Start()
	{
		StartCoroutine(Clock());
	}

	private IEnumerator Clock()
	{
		yield return new WaitForSeconds(1);
		_secondsPassed++;
		if (_secondsPassed == 60)
		{
			_minutesPassed++;
			_secondsPassed = 0;
		}
		
		if (_secondsPassed < 10)
		{
			_text.text = _minutesPassed.ToString() + ":0" + _secondsPassed.ToString();

		}
		else
		{
			_text.text = _minutesPassed.ToString() + ":" + _secondsPassed.ToString();
		}
		StartCoroutine(Clock());
	}
}
