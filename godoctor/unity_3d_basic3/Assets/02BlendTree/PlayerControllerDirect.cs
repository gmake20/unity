using System.Collections;
using UnityEngine;

public class PlayerControllerDirect : MonoBehaviour
{
	private	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		KeyEvent(0, KeyCode.Q, "angry");	// Q키를 누르면 angry 파라미터 값 증가
		KeyEvent(1, KeyCode.A, "angry");	// A키를 누르면 angry 파라미터 값 감소

		KeyEvent(0, KeyCode.W, "eye");		// W키를 누르면 eye 파라미터 값 증가
		KeyEvent(1, KeyCode.S, "eye");		// S키를 누르면 eye 파라미터 값 감소

		KeyEvent(0, KeyCode.E, "sap");		// E키를 누르면 sap 파라미터 값 증가
		KeyEvent(1, KeyCode.D, "sap");		// D키를 누르면 sap 파라미터 값 감소

		KeyEvent(0, KeyCode.R, "smile");	// R키를 누르면 smile 파라미터 값 증가
		KeyEvent(1, KeyCode.F, "smile");	// F키를 누르면 smile 파라미터 값 감소
	}

	private void KeyEvent(int type, KeyCode key, string parameter)
	{
		// key 키를 누르면 파라미터 값 증가/감소 시작
		if ( Input.GetKeyDown(key) )
		{
			string coroutine = type==0 ? "ParameterUp" : "ParameterDown";
			StartCoroutine(coroutine, parameter);
		}
		// key 키를 떼면 파라미터 값 증가/감소 중지
		else if ( Input.GetKeyUp(key) )
		{
			string coroutine = type==0 ? "ParameterUp" : "ParameterDown";
			StopCoroutine(coroutine);
		}
	}

	private IEnumerator ParameterUp(string parameter)
	{
		// 현재 파라미터 값을 받아온다
		float percent = animator.GetFloat(parameter);

		// 파라미터 값을 증가시키는 코루틴이기 때문에 1이 될때까지 실행
		while ( percent < 1 )
		{
			percent += Time.deltaTime;	// percent 값 증가
			animator.SetFloat(parameter, percent);

			yield return null;
		}
	}

	private IEnumerator ParameterDown(string parameter)
	{
		// 현재 파라미터 값을 받아온다
		float percent = animator.GetFloat(parameter);

		// 파라미터 값을 감소시키는 코루틴이기 때문에 0이 될때까지 실행
		while ( percent > 0 )
		{
			percent -= Time.deltaTime;	// percent 값 감소
			animator.SetFloat(parameter, percent);

			yield return null;
		}
	}
}


