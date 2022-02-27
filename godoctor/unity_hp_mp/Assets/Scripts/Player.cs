using System.Collections;
using UnityEngine;

public class Player : Entity
{
	private	SpriteRenderer	spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		// Entity�� ���ǵǾ� �ִ� Setup() �޼ҵ� ȣ��
		base.Setup();
	}

	private void Update()
	{
		// �⺻ ����
		if ( Input.GetKeyDown("1") )
		{
			target.TakeDamage(20);
		}
		// ��ų ����
		else if ( Input.GetKeyDown("2") )
		{
			MP -= 100;
			target.TakeDamage(55);
		}
	}

	// �⺻ ü�� + ���� ���ʽ� + ���� ��� ���� ���
	public override float MaxHP => MaxHPBasic + MaxHPAttrBonus;
	// 100 + ���緹�� * 30
	public float MaxHPBasic		=> 100 + 1 * 30;
	// �� * 10
	public float MaxHPAttrBonus => 10 * 10;
	
	public override float HPRecovery => 10;
	public override float MaxMP => 200;
	public override float MPRecovery => 25;

	public override void TakeDamage(float damage)
	{
		HP -= damage;

		StartCoroutine("HitAnimation");
	}

	private IEnumerator HitAnimation()
	{
		Color color = spriteRenderer.color;
		
		color.a = 0.2f;
		spriteRenderer.color = color;

		yield return new WaitForSeconds(0.1f);

		color.a = 1;
		spriteRenderer.color = color;
	}
}

