using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour
{
	private	EquipmentTemplate	equipment;		// ���� ��� ���� ����
	private	NavigationView		navigationView;	// �� �̵��� ���� NavigationView
	private	RectTransform		rectDetails;	// ���� View RectTransform

	public void Setup(EquipmentTemplate equipment, NavigationView navigationView, RectTransform rectDetails)
	{
		// ��ư�� ������ �� ȣ��� �̺�Ʈ �޼ҵ� ��� (OnClick)
		this.equipment		= equipment;
		this.navigationView	= navigationView;
		this.rectDetails	= rectDetails;
		
		GetComponent<Button>().onClick.AddListener(OnClickBtnEvent);
	}

	public void OnClickBtnEvent()
	{
		rectDetails.GetComponent<Equipment>().Setup(equipment);
		navigationView.Push(rectDetails);
	}
}

