using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Equipment : MonoBehaviour
{
	[SerializeField]
	private	Image				imageIcon;		// 아이템 아이콘
	[SerializeField]
	private	TextMeshProUGUI		textName;		// 아이템 이름 (등급에 따라 색상 변경)
	[SerializeField]
	private	TextMeshProUGUI		textPrice;		// 아이템 가격
	[SerializeField]
	private	TextMeshProUGUI		textDetails;	// 아이템 세부 설명

	public void Setup(EquipmentTemplate equipment)
	{
		// 아이템 아이콘 설정
		imageIcon.sprite = Resources.Load<Sprite>(equipment.iconFile);
		// 아이템 이름 설정 (색상, 텍스트)
		textName.color	 = GradeToColor(equipment.grade);
		textName.text	 = equipment.name;
		// 아이템 가격
		textPrice.color	 = GradeToColor(equipment.grade);
		textPrice.text	 = equipment.price.ToString();

		// 아이템 세부 설명 설정
		if ( textDetails != null )
		{
			textDetails.text = equipment.details;
		}
	}

	public Color GradeToColor(EquipmentGrade grade)
	{
		// Normal, Magic, Rare, Set, Unique 아이템의 색상
		Color[] colors = new Color[(int)EquipmentGrade.Count] {
				Color.white, Color.blue, Color.yellow, Color.green, new Color(1, 0.6f, 0)
			};

		return colors[(int)grade];
	}
}

