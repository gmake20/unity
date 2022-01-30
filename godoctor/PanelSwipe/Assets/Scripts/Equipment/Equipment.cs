using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Equipment : MonoBehaviour
{
	[SerializeField]
	private	Image				imageIcon;		// ������ ������
	[SerializeField]
	private	TextMeshProUGUI		textName;		// ������ �̸� (��޿� ���� ���� ����)
	[SerializeField]
	private	TextMeshProUGUI		textPrice;		// ������ ����
	[SerializeField]
	private	TextMeshProUGUI		textDetails;	// ������ ���� ����

	public void Setup(EquipmentTemplate equipment)
	{
		// ������ ������ ����
		imageIcon.sprite = Resources.Load<Sprite>(equipment.iconFile);
		// ������ �̸� ���� (����, �ؽ�Ʈ)
		textName.color	 = GradeToColor(equipment.grade);
		textName.text	 = equipment.name;
		// ������ ����
		textPrice.color	 = GradeToColor(equipment.grade);
		textPrice.text	 = equipment.price.ToString();

		// ������ ���� ���� ����
		if ( textDetails != null )
		{
			textDetails.text = equipment.details;
		}
	}

	public Color GradeToColor(EquipmentGrade grade)
	{
		// Normal, Magic, Rare, Set, Unique �������� ����
		Color[] colors = new Color[(int)EquipmentGrade.Count] {
				Color.white, Color.blue, Color.yellow, Color.green, new Color(1, 0.6f, 0)
			};

		return colors[(int)grade];
	}
}

