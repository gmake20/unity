using UnityEngine;

public class PlayerController2DSimple : MonoBehaviour
{
	private	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");		// 좌, 우 방향키 입력
		float vertical	 = Input.GetAxis("Vertical");		// 위, 아래 방향키 입력

		// horizontal 값에 따라 애니메이션 재생 (-1:왼쪽, 0:가운데, 1:오른쪽)
		animator.SetFloat("Horizontal", horizontal);
		// vertical 값에 따라 애니메이션 재생 (-1:뒤, 0:가운데, 1:앞)
		animator.SetFloat("Vertical", vertical);

		// 이동속도
		//float moveSpeed = 5.0f;
		// 실제 이동
		//transform.position += new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
	}
}


