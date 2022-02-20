using UnityEngine;

public class PlayerController2DFreeformD : MonoBehaviour
{
	private	Animator animator;
	//private	float	 walkSpeed = 4.0f;
	//private	float	 runSpeed = 8.0f;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");		// 좌, 우 방향키 입력
		float vertical	 = Input.GetAxis("Vertical");		// 위, 아래 방향키 입력
		// Shft키를 안누르면 최대 0.5, Shft키를 누르면 최대 1까지 값이 바뀌게 된다
		float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;

		// horizontal 값에 따라 애니메이션 재생 (-1:왼쪽, 0:가운데, 1:오른쪽)
		animator.SetFloat("Horizontal", horizontal * offset);
		// vertical 값에 따라 애니메이션 재생 (-1:뒤, 0:가운데, 1:앞)
		animator.SetFloat("Vertical", vertical * offset);

		// 이동속도 : Shift키를 안눌렀을 땐 walkSpeed, Shift키를 눌렀을 땐 runSpeed값이 moveSpeed에 저장
		//float moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, Input.GetAxis("Sprint"));
		// 실제 이동
		//transform.position += new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
	}
}

