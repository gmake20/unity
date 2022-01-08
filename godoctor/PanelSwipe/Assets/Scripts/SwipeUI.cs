using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollBar;    // Scrollbar의 위치를 바탕으로 현재 페이지 검사

    [SerializeField]
    private float swipeTime = 0.2f; // 페이지가 Swipe 하는 시간

    [SerializeField]
    private float swipeDistance = 50.0f;    // 페이지가 Swipe되기 위해 움직어야하는 최소거리

    private float[] scrollPageValues;   // 각 페이지의 위치값
    private float valueDistance = 0;    // 각 페이지 사이의 거리
    private int currentPage = 0;    // 현재 페이지
    private int maxPage = 0;        // 최대페이지
    private float startTouchX;      // 터치시작위치
    private float endTouchX;        // 터치 종료위치
    private bool isSwipeMode = false;   // 현재 swipe가 되고 있는지 체크 

    // Swipe에서 사용하는 Circle Group
    [SerializeField]
    private Transform[] circleContents;
    private float circleContentScale = 1.6f;

    private void Awake()
    {
        // 스크롤되는 페이지의 각 value값을 저장하는 배열 메모리할당
        scrollPageValues = new float[transform.childCount];

        // 스크롤 되는 페이지 사이의 거리
        valueDistance = 1f / (scrollPageValues.Length-1f);

        // 스크롤 되는 페이지의 각 value위치 설정 (0 <= value <= 1)
        for(int i=0;i<scrollPageValues.Length;++i)
        {
            scrollPageValues[i] = valueDistance * i;
        }

        // 최대 페이지수
        maxPage = transform.childCount;
    }


    // Start is called before the first frame update
    void Start()
    {
        // 최초페이지 설정
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

        // swipe 방향 
        bool isLeft = startTouchX < endTouchX ? true : false;

        // 이동방향이 왼쪽
        if(isLeft == true)
        {
            if (currentPage == 0) return;   // 현재 페이지가 맨왼쪽이면 종료
            currentPage--;
        }
        // 이동방향이 오른쪽 
        else
        {
            if (currentPage == maxPage - 1) return; // 현재 페이지가 맨오른쪽이면 종료 
            currentPage++;
        }

        // currentPage번째 페이지로 Swipe해서 이동
        StartCoroutine(OnSwipeOneStep(currentPage));

    }

    /// <summary>
    /// 페이지를 한장 옆으로 넘기는 Swipe효과 재생
    /// </summary>
    /// <param name="index">Swipe하는 페이지</param>
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

            // 페이지의 절반을 넘어가면 현재 페이지 원을 바꾼다.
            if(scrollBar.value < scrollPageValues[i] + (valueDistance/2) && 
                scrollBar.value > scrollPageValues[i] - (valueDistance/2))
            {
                circleContents[i].localScale = Vector2.one * circleContentScale;
                circleContents[i].GetComponent<Image>().color = Color.black;
            }
        }
    }
}
