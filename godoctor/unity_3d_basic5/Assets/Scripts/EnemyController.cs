using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private	Animator			animator;
	private	SkinnedMeshRenderer	meshRenderer;
	private	Color				originColor;

	private void Awake()
	{
		animator	 = GetComponent<Animator>();
		meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		originColor	 = meshRenderer.material.color;
	}

	public void TakeDamage(int damage)
	{
		// 체력이 감소되거나 피격 애니메이션이 재생되는 등의 코드를 작성
		Debug.Log(damage+"의 체력이 감소합니다.");
		// 피격 애니메이션 재생
		animator.SetTrigger("onHit");
		// 색상 변경
		StartCoroutine("OnHitColor");
	}

	private IEnumerator OnHitColor()
	{
		// 색을 빨간색으로 변경한 후 0.1초 후에 원래 색상으로 변경
		meshRenderer.material.color = Color.red;

		yield return new WaitForSeconds(0.1f);

		meshRenderer.material.color = originColor;
	}
}

