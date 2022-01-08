using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollBar;    // Scrollbar�� ��ġ�� �������� ���� ������ �˻�

    [SerializeField]
    private float swipeTime = 0.2f; // �������� Swipe �ϴ� �ð�

    [SerializeField]
    private float swipeDistance = 50.0f;    // �������� Swipe�Ǳ� ���� ��������ϴ� �ּҰŸ�

    private float[] scrollPageValues;   // �� �������� ��ġ��
    private float valueDistance = 0;    // �� ������ ������ �Ÿ�
    private int currentPage = 0;    // ���� ������
    private int maxPage = 0;        // �ִ�������
    private float startTouchX;      // ��ġ������ġ
    private float endTouchX;        // ��ġ ������ġ
    private bool isSwipeMode = false;   // ���� swipe�� �ǰ� �ִ��� üũ 

    // Swipe���� ����ϴ� Circle Group
    [SerializeField]
    private Transform[] circleContents;
    private float circleContentScale = 1.6f;

    private void Awake()
    {
        // ��ũ�ѵǴ� �������� �� value���� �����ϴ� �迭 �޸��Ҵ�
        scrollPageValues = new float[transform.childCount];

        // ��ũ�� �Ǵ� ������ ������ �Ÿ�
        valueDistance = 1f / (scrollPageValues.Length-1f);

        // ��ũ�� �Ǵ� �������� �� value��ġ ���� (0 <= value <= 1)
        for(int i=0;i<scrollPageValues.Length;++i)
        {
            scrollPageValues[i] = valueDistance * i;
        }

        // �ִ� ��������
        maxPage = transform.childCount;
    }


    // Start is called before the first frame update
    void Start()
    {
        // ���������� ����
        SetScrollBarValue(0);
    }

    public void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollBar.value = scrollPageValues[index];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();

        UpdateCircleContent();
    }

    private void UpdateInput()
    {
        if (isSwipeMode == true) return;

#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            startTouchX = Input.mousePosition.x;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
#endif




#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                startTouchX = touch.position.x;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                endTouchX = Input.mousePosition.x;
                UpdateSwipe();
            }

        }
#endif
    }


    private void UpdateSwipe()
    {
        if(Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        // swipe ���� 
        bool isLeft = startTouchX < endTouchX ? true : false;

        // �̵������� ����
        if(isLeft == true)
        {
            if (currentPage == 0) return;   // ���� �������� �ǿ����̸� ����
            currentPage--;
        }
        // �̵������� ������ 
        else
        {
            if (currentPage == maxPage - 1) return; // ���� �������� �ǿ������̸� ���� 
            currentPage++;
        }

        // currentPage��° �������� Swipe�ؼ� �̵�
        StartCoroutine(OnSwipeOneStep(currentPage));

    }

    /// <summary>
    /// �������� ���� ������ �ѱ�� Swipeȿ�� ���
    /// </summary>
    /// <param name="index">Swipe�ϴ� ������</param>
    /// <returns></returns>
    IEnumerator OnSwipeOneStep(int index)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;

        isSwipeMode = true;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);
            yield return null;
        }

        isSwipeMode = false;
    }

    private void UpdateCircleContent()
    {
        for(int i=0;i<scrollPageValues.Length;i++)
        {
            circleContents[i].localScale = Vector2.one;
            circleContents[i].GetComponent<Image>().color = Color.white;

            // �������� ������ �Ѿ�� ���� ������ ���� �ٲ۴�.
            if(scrollBar.value < scrollPageValues[i] + (valueDistance/2) && 
                scrollBar.value > scrollPageValues[i] - (valueDistance/2))
            {
                circleContents[i].localScale = Vector2.one * circleContentScale;
                circleContents[i].GetComponent<Image>().color = Color.black;
            }
        }
    }
}
