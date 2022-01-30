using System.Collections.Generic;
using UnityEngine;

public class EquipmentCategory : MonoBehaviour
{
	[SerializeField]
	private	NavigationView			navigationView;		// 상위/하위 이동을 위한 Push, Pop 메소드
	[SerializeField]
	private	RectTransform			rectDetails;		// 장비 세부 설정 View (하위 View)

	[SerializeField]
	private	GameObject				equipmentPrefab;	// 장비 버튼 프리팹
	[SerializeField]
	private	Transform				equipmentParent;	// 장비 버튼 오브젝트가 배치되는 부모 (Content)
	[SerializeField]
	private	List<EquipmentTemplate> equipmets;			// 장비 버튼들에 대한 정보 리스트

	private void Awake()
	{
		foreach ( var equip in equipmets )
		{
			// equipmentPrefab 오브젝트를 복제 생성해서 equipmentParent의 자식으로 배치
			GameObject clone = Instantiate(equipmentPrefab, equipmentParent);

			clone.GetComponent<Equipment>().Setup(equip);
			clone.GetComponent<EquipmentButton>().Setup(equip, navigationView, rectDetails);
		}
	}
}




// EquipmentTemplate.cs : 장비 등급 열거형, 장비 하나에 설정되는 내용
// EquipmentCategory.cs : 카테고리 별로 장비 오브젝트들을 생성
// Equipment.cs : 장비 오브젝트 설정 (이미지, 아이템 이름, 가격 등)