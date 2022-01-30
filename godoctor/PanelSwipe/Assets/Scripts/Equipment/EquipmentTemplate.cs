using UnityEngine;

public enum EquipmentGrade { Normal=0, Magic, Rare, Set, Unique, Count }

[System.Serializable]
public class EquipmentTemplate
{
	public	EquipmentGrade	grade;		// 장비 등급
	public	string			iconFile;	// 장비 아이콘 파일 이름(경로 포함)
	public	string			name;		// 장비 이름
	public	int				price;		// 장비 가격
	[TextArea(0, 30)]
	public	string			details;	// 장비에 대한 세부 설명
}

