using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHPMP : MonoBehaviour
{
	[SerializeField]
	private	Entity			entity;
	[SerializeField]
	private	Slider			sliderHP;
	[SerializeField]
	private	TextMeshProUGUI	textHP;
	[SerializeField]
	private	Slider			sliderMP;
	[SerializeField]
	private	TextMeshProUGUI	textMP;

	private void Update()
	{
		if ( sliderHP != null ) sliderHP.value	= Utils.Percent(entity.HP, entity.MaxHP);
		if ( textHP != null )	textHP.text		= $"{entity.HP:F0}/{entity.MaxHP:F0}";

		if ( sliderMP != null )	sliderMP.value	= Utils.Percent(entity.MP, entity.MaxMP);
		if ( textMP != null )	textMP.text		= $"{entity.MP:F0}/{entity.MaxMP:F0}";
	}
}

