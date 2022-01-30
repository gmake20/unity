using System.Collections.Generic;
using UnityEngine;

public class EquipmentCategory : MonoBehaviour
{
	[SerializeField]
	private	NavigationView			navigationView;		// ����/���� �̵��� ���� Push, Pop �޼ҵ�
	[SerializeField]
	private	RectTransform			rectDetails;		// ��� ���� ���� View (���� View)

	[SerializeField]
	private	GameObject				equipmentPrefab;	// ��� ��ư ������
	[SerializeField]
	private	Transform				equipmentParent;	// ��� ��ư ������Ʈ�� ��ġ�Ǵ� �θ� (Content)
	[SerializeField]
	private	List<EquipmentTemplate> equipmets;			// ��� ��ư�鿡 ���� ���� ����Ʈ

	private void Awake()
	{
		foreach ( var equip in equipmets )
		{
			// equipmentPrefab ������Ʈ�� ���� �����ؼ� equipmentParent�� �ڽ����� ��ġ
			GameObject clone = Instantiate(equipmentPrefab, equipmentParent);

			clone.GetComponent<Equipment>().Setup(equip);
			clone.GetComponent<EquipmentButton>().Setup(equip, navigationView, rectDetails);
		}
	}
}




// EquipmentTemplate.cs : ��� ��� ������, ��� �ϳ��� �����Ǵ� ����
// EquipmentCategory.cs : ī�װ� ���� ��� ������Ʈ���� ����
// Equipment.cs : ��� ������Ʈ ���� (�̹���, ������ �̸�, ���� ��)