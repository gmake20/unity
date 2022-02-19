﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private	Transform	target;				// 카메라가 추적하는 대상
	[SerializeField]
	private	float		minDistance = 3;	// 카메라와 target의 최소 거리
	[SerializeField]
	private	float		maxDistance = 30;	// 카메라와 target의 최대 거리
	[SerializeField]
	private	float		wheelSpeed = 500;	// 마우스 휠 스크롤 속도
	[SerializeField]
	private	float		xMoveSpeed = 500;	// 카메라의 y축 회전 속도
	[SerializeField]
	private	float		yMoveSpeed = 250;	// 카메라의 x축 회전 속도
	private	float		yMinLimit = 5;		// 카메라 x축 회전 제한 최소 값
	private	float		yMaxLimit = 80;		// 카메라 x축 회전 제한 최대 값
	private	float		x, y;				// 마우스 이동 방향 값
	private	float		distance;			// 카메라와 target의 거리

	private void Awake()
	{
		// 최초 설정된 target과 카메라의 위치를 바탕으로 distance 값 초기화
		distance = Vector3.Distance(transform.position, target.position);
		// 최초 카메라의 회전 값을 x, y 변수에 저장
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}

	private void Update()
	{
		if ( target == null ) return;	// target이 존재하지 않으면 실행 하지 않는다

		// 오른쪽 마우스를 누르고 있을 때
		if ( Input.GetMouseButton(1) )
		{
			// 마우스를 x, y축 움직임 방향 정보
			x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
			y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;
			// 오브젝트의 위/아래(x축) 한계 범위 설정
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			// 카메라의 회전(Rotation) 정보 갱신
			transform.rotation = Quaternion.Euler(y, x, 0);
		}

		// 마우스 휠 스크롤을 이용해 target과 카메라의 거리 값(distance) 조절
		distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
		// 거리는 최소, 최대 거리를 설정해서 그 값을 벗어나지 않도록 한다
		distance = Mathf.Clamp(distance, minDistance, maxDistance);
	}

	private void LateUpdate()
	{
		if ( target == null ) return;	// target이 존재하지 않으면 실행 하지 않는다

		// 카메라의 위치(Position) 정보 갱신
		// target의 위치를 기준으로 distacne만큼 떨어져서 쫓아간다
		transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
	}
	
	private float ClampAngle(float angle, float min, float max)
	{
		if ( angle < -360 )	angle += 360;
		if ( angle > 360 )	angle -= 360;

		return Mathf.Clamp(angle, min, max);
	}
}

