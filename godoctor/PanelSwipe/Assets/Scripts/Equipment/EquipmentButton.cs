using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour
{
	private	EquipmentTemplate	equipment;		// 현재 장비에 대한 정보
	private	NavigationView		navigationView;	// 뷰 이동을 위한 NavigationView
	private	RectTransform		rectDetails;	// 하위 View RectTransform

	public void Setup(EquipmentTemplate equipment, NavigationView navigationView, RectTransform rectDetails)
	{
		// 버튼을 눌렀을 때 호출될 이벤트 메소드 등록 (OnClick)
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

