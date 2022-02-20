using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField]
	private	GameObject	attackCollision;
	private	Animator	animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void OnMovement(float horizontal, float vertical)
	{
		animator.SetFloat("horizontal", horizontal);
		animator.SetFloat("vertical", vertical);
	}

	public void OnJump()
	{
		animator.SetTrigger("onJump");
	}

	public void OnKickAttack()
	{
		animator.SetTrigger("onKickAttack");
	}

	public void OnWeaponAttack()
	{
		animator.SetTrigger("onWeaponAttack");
	}

	public void OnAttackCollision()
	{
		attackCollision.SetActive(true);
	}
}

