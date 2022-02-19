using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private float rotateSpeedX = 3;
	private float rotateSpeedY = 5;
	private float limitMinX = -80;
	private float limitMaxX = 50;
	private float eulerAngleX;
	private float eulerAngleY;

	public void RotateTo(float mouseX, float mouseY)
	{
		// ���콺�� ��/��� �����̴� mouseX ���� y�࿡ �����ϴ� ������
		// ���콺�� ��/��� ������ �� ī�޶� ��/�츦 ������ ī�޶� ������Ʈ��
		// y���� ȸ���Ǿ�� �ϱ� ����
		eulerAngleY += mouseX * rotateSpeedX;
		// ���� �������� ī�޶� ��/�Ʒ��� ������ ī�޶� ������Ʈ�� x���� ȸ��!
		eulerAngleX -= mouseY * rotateSpeedY;

		// x�� ȸ�� ���� ��� �Ʒ�, ���� �� �� �ִ� ���� ������ �����Ǿ� �ִ�
		eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

		// ���� ������Ʈ�� ���ʹϿ� ȸ���� ����
		transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
	}

	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360) angle += 360;
		if (angle > 360) angle -= 360;

		// Mathf.Clamp()�� �̿��� angle�� min <= angle <= max�� �����ϵ��� ��
		return Mathf.Clamp(angle, min, max);
	}
}
