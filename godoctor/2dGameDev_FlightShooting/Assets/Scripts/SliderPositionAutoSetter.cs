using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    // 거리는 해당 스프라이트크기때라서 다름. 현재 스프라이트에서는 35 아래쪽으로 위치한다.
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
        // 적이 파괴되면 UI도 삭제
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // 스프라이트의 위치가 변결될때마다 해당 UI의 위치를 변경한다. 
        // 오브젝트의 월드좌표를 화면좌표로 구한다.
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        // 화면내에서의 좌표에 distance만큼 떨어진곳에UI 위치를 설정한다.
        rectTransform.position = screenPosition + distance;
    }

}
