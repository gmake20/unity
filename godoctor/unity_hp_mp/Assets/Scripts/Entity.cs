using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	private	Stats	stats;		// ĳ���� ����
	public	Entity	target;		// ���� ��� (�÷��̾��� ��� ����� Ŭ���ϴ� ������� ����)
	
	// ü��(HP) ������Ƽ : 0~MaxHP ������ ���� �Ѿ �� ������ ����
	public	float	HP
	{
		set => stats.HP = Mathf.Clamp(value, 0, MaxHP);
		get => stats.HP;
	}
	// ����(MP) ������Ƽ : 0~MaxMP ������ ���� �Ѿ �� ������ ����
	public	float	MP
	{
		set => stats.MP = Mathf.Clamp(value, 0, MaxMP);
		get => stats.MP;
	}

	// ���� ������Ƽ�� �߻�(abstract)���� ����Ǿ��� ������
	// ���� �۵��ϴ� ������ �÷��̾�, ���� ���� �Ļ� Ŭ�������� ����
	public	abstract float	MaxHP		{ get; }	// �ִ� ü��
	public	abstract float	HPRecovery	{ get; }	// �ʴ� ü�� ȸ����
	public	abstract float	MaxMP		{ get; }	// �ִ� ����
	public	abstract float	MPRecovery	{ get; }	// �ʴ� ���� ȸ����

	protected void Setup()
	{
		HP = MaxHP;
		MP = MaxMP;

		StartCoroutine("Recovery");
	}

	/// <summary>
	/// �ʴ� ü��, ���� ȸ��
	/// </summary>
	protected IEnumerator Recovery()
	{
		while ( true )
		{
			if ( HP < MaxHP ) HP += HPRecovery;
			if ( MP < MaxMP ) MP += MPRecovery;

			yield return new WaitForSeconds(1);
		}
	}

	// ������ ������ �� ������ TakeDamage() ȣ��
	// �Ű����� damage�� �����ϴ� ���� ���ݷ�
	public abstract void TakeDamage(float damage);
}

[System.Serializable]
public struct Stats
{
	// �̸�, ����, ����, ����(��, ���� ��), ü��/����, ����ġ ���� ĳ���� ��ġ

	[HideInInspector]
	public	float	HP;
	[HideInInspector]
	public	float	MP;
}

