using System.Collections;
using UnityEngine;
using TMPro;

public class DialogTest : MonoBehaviour
{
	[SerializeField]
	private	DialogSystem	dialogSystem01;
	[SerializeField]
	private	TextMeshProUGUI	textCountdown;
	[SerializeField]
	private	DialogSystem	dialogSystem02;

	private IEnumerator Start()
	{
		textCountdown.gameObject.SetActive(false);

		// 첫 번째 대사 분기 시작
		yield return new WaitUntil(()=>dialogSystem01.UpdateDialog());

		// 대사 분기 사이에 원하는 행동을 추가할 수 있다.
		// 캐릭터를 움직이거나 아이템을 획득하는 등의.. 현재는 5-4-3-2-1 카운트 다운 실행
		textCountdown.gameObject.SetActive(true);
		int count = 5;
		while ( count > 0 )
		{
			textCountdown.text = count.ToString();
			count --;

			yield return new WaitForSeconds(1);
		}
		textCountdown.gameObject.SetActive(false);

		// 두 번째 대사 분기 시작
		yield return new WaitUntil(()=>dialogSystem02.UpdateDialog());

		textCountdown.gameObject.SetActive(true);
		textCountdown.text = "The End";

		yield return new WaitForSeconds(2);

		UnityEditor.EditorApplication.ExitPlaymode();
	}
}

