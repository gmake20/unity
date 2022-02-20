using UnityEngine;

public class PlayerController1D : MonoBehaviour
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
		float vertical = Input.GetAxis("Vertical");		// 위, 아래 방향키 입력
		// Shft키를 안누르면 최대 0.5, Shft키를 누르면 최대 1까지 값이 바뀌게 된다
		float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;
		// 오른쪽 방향키를 누르면 forward가 +이지만 왼쪽 방향키를 누르면 forwad가 -이기 때문에
		// 애니메이션 파라미터를 설정할 땐 절대값으로 적용한다
		float moveParameter = Mathf.Abs(vertical * offset);

		// moveParameter 값에 따라 애니메이션 재생 (0:대기, 0.5:걷기, 1:뛰기)
		animator.SetFloat("moveSpeed", moveParameter);
		
		// 이동속도 : Shift키를 안눌렀을 땐 walkSpeed, Shift키를 눌렀을 땐 runSpeed값이 moveSpeed에 저장
		//float moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, Input.GetAxis("Sprint"));
		// 실제 이동
		//transform.position += new Vector3(vertical, 0, 0) * moveSpeed * Time.deltaTime;
	}
}

