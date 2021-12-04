using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    // �Ÿ��� �ش� ��������Ʈũ�⶧�� �ٸ�. ���� ��������Ʈ������ 35 �Ʒ������� ��ġ�Ѵ�.
    private Vector3 distance = Vector3.down * 35.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ���� �ı��Ǹ� UI�� ����
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // ��������Ʈ�� ��ġ�� ����ɶ����� �ش� UI�� ��ġ�� �����Ѵ�. 
        // ������Ʈ�� ������ǥ�� ȭ����ǥ�� ���Ѵ�.
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        // ȭ�鳻������ ��ǥ�� distance��ŭ ����������UI ��ġ�� �����Ѵ�.
        rectTransform.position = screenPosition + distance;
    }

}
