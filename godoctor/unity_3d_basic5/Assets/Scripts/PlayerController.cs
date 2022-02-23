using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private	KeyCode			jumpKeyCode = KeyCode.Space;
	[SerializeField]
	private	Transform		cameraTransform;
	private	Movement3D		movement3D;
	private	PlayerAnimator	playerAnimator;

	private void Awake()
	{
		// Cursor.visible	 = false;					// 마우스 커서를 보이지 않게
		// Cursor.lockState = CursorLockMode.Locked;	// 마우스 커서 위치 고정

		movement3D		= GetComponent<Movement3D>();
		playerAnimator	= GetComponentInChildren<PlayerAnimator>();
	}

	private void Update()
	{
		// 방향키를 눌러 이동
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		// 이동 속도 설정 (앞으로 이동할때만 5, 나머지는 2)
		movement3D.MoveSpeed = z > 0 ? 5.0f : 2.0f;
		// 이동 함수 호출 (카메라가 보고있는 방향을 기준으로 방향키에 따라 이동)
		movement3D.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));


		// 회전 설정 (항상 앞만 보도록 캐릭터의 회전은 카메라와 같은 회전 값으로 설정)
		transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

		// 애니메이션 파라미터 설정 (horizontal, vertical)
		playerAnimator.OnMovement(x, z);


		// Space키를 누르면 점프
		if ( Input.GetKeyDown(jumpKeyCode) )
		{
			playerAnimator.OnJump();	// 애니메이션 파라미터 설정 (onJump)
			movement3D.JumpTo();		// 점프 함수 호출
		}


		// 마우스가 UI위에 있을때는 아래코드를 실행하지 않도록 설정
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

		// 마우스 왼쪽 버튼을 누르면 발차기 공격
		if ( Input.GetMouseButtonDown(0) )
		{
			playerAnimator.OnKickAttack();
		}

		// 마우스 오른쪽 버튼을 누르면 무기 공격 (연계)
		if ( Input.GetMouseButtonDown(1) )
		{
			playerAnimator.OnWeaponAttack();
		}
	}
}

