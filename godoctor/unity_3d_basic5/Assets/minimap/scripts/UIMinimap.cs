using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMinimap : MonoBehaviour
{
	[SerializeField]
	private	Camera			minimapCamera;
	[SerializeField]
	private	float			zoomMin	= 1;		// 카메라의 orthographicSize 최소 크기
	[SerializeField]
	private	float			zoomMax = 30;		// 카메라의 orthographicSize 최대 크기
	[SerializeField]
	private	float			zoomOneStep = 1;	// 1회 줌 할 때 증가/감소되는 수치
	[SerializeField]
	private	TextMeshProUGUI	textMapName;

	private void Awake()
	{
		// 맵 이름을 현재 씬 이름으로 설정 (원하는 이름으로 설정)
		textMapName.text = SceneManager.GetActiveScene().name;
	}

	public void ZoomIn()
	{
		// 카메라의 orthographicSize 값을 감소시켜 카메라에 보이는 사물 크기 확대
		minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize-zoomOneStep, zoomMin);
	}

	public void ZoomOut()
	{
		// 카메라의 orthographicSize 값을 증가시켜 카메라에 보이는 사물 크기 축소
		minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize+zoomOneStep, zoomMax);
	}
}

