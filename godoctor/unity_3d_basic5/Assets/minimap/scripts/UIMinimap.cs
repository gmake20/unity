using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMinimap : MonoBehaviour
{
	[SerializeField]
	private	Camera			minimapCamera;
	[SerializeField]
	private	float			zoomMin	= 1;		// ī�޶��� orthographicSize �ּ� ũ��
	[SerializeField]
	private	float			zoomMax = 30;		// ī�޶��� orthographicSize �ִ� ũ��
	[SerializeField]
	private	float			zoomOneStep = 1;	// 1ȸ �� �� �� ����/���ҵǴ� ��ġ
	[SerializeField]
	private	TextMeshProUGUI	textMapName;

	private void Awake()
	{
		// �� �̸��� ���� �� �̸����� ���� (���ϴ� �̸����� ����)
		textMapName.text = SceneManager.GetActiveScene().name;
	}

	public void ZoomIn()
	{
		// ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
		minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize-zoomOneStep, zoomMin);
	}

	public void ZoomOut()
	{
		// ī�޶��� orthographicSize ���� �������� ī�޶� ���̴� �繰 ũ�� ���
		minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize+zoomOneStep, zoomMax);
	}
}

