using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavigationView : MonoBehaviour
{
	[SerializeField]
	private	RectTransform			currentView;	// 현재 View 정보
	[SerializeField]
	private	TextMeshProUGUI			textTitle;		// 현재 View 이름 출력
	[SerializeField]
	private	Button					buttonPrev;		// "돌아가기" 버튼 (이전 View로 이동)
	[SerializeField]
	private	TextMeshProUGUI			textPrevName;	// 이전 View 이름 (버튼 아래에 출력)
	
	private	CanvasGroup				canvasGroup;	// View 이동을 할 때 UI를 제어할 수 없도록 충돌처리 On/Off 제어
	private	Stack<RectTransform>	stackViews;		// 상위 View 정보들이 저장되는 Stack 자료구조

	private void Awake()
	{
		canvasGroup	= GetComponent<CanvasGroup>();
		stackViews	= new Stack<RectTransform>();

		// 버튼에 이벤트 메소드 등록
		buttonPrev.onClick.AddListener(Pop);
		// 처음에 등장하는 View가 가장 상위의 View이기 때문에 돌아가기 버튼 비활성화
		buttonPrev.gameObject.SetActive(false);

		// NavigationBar에 있는 현재 View의 이름 갱신
		SetNavigationBar(currentView.name);
	}

	/// <summary>
	/// 상위 View에서 하위 View로 이동할 때 호출
	/// </summary>
	public void Push(RectTransform newView)
	{
		// 만약 뷰가 활성/비활성화 되는 과정에서 뷰 이동과 같은 애니메이션이 있으면
		// 애니메이션되는 동안에는 상호작용이 불가능하도록 CanvasGroup.blocksRaycasts를 false로 설정
		// (자식으로 있는 UI를 하나씩 통제하기 어렵기 때문에 CanvasGroup 컴포넌트 사용)
		canvasGroup.blocksRaycasts = false;

		/// 사라지는 뷰 (상위 View) /////////////////////////////////////////////////////////
		RectTransform previousView = currentView;
		previousView.gameObject.SetActive(false);	// 사라지는 뷰를 비활성화
		stackViews.Push(previousView);				// Back 버튼을 눌러 상위 View로 돌아갈 수 있도록 스택에 저장
		
		/// 등장하는 뷰 (하위 View) /////////////////////////////////////////////////////////
		currentView = newView;
		currentView.gameObject.SetActive(true);		// 등장하는 뷰를 활성화
		
		// 애니메이션이 종료되면 blockRaycasts를 true로 설정
		canvasGroup.blocksRaycasts = true;

		// 현재 보이는 View 이름을 출력하고, 상위 View 이름을 "돌아가기" 버튼 아래에 출력
		SetNavigationBar(currentView.name, previousView.name);
	}

	/// <summary>
	/// 하위 View에서 상위 View로 이동할 때 호출
	/// </summary>
	public void Pop()
	{
		// 현재 스택에 남아있는 데이터의 개수가 0이면 Pop 할 수 없기 때문에 return
		if ( stackViews.Count < 1 )
		{
			return;
		}

		// 애니메이션이 재생되는 동안 blockRaycasts를 false로 설정
		canvasGroup.blocksRaycasts = false;

		/// 사라지는 뷰 (하위 View) /////////////////////////////////////////////////////////
		RectTransform previousView = currentView;
		previousView.gameObject.SetActive(false);	// 사라지는 뷰를 비활성화
		
		/// 등장하는 뷰 (상위 View) /////////////////////////////////////////////////////////
		currentView = stackViews.Pop();				// 상위로 이동할 때는 스택에서 꺼낸 데이터가 상위 View
		currentView.gameObject.SetActive(true);		// 등장하는 뷰를 활성화
		
		// 애니메이션이 종료되면 blockRaycasts를 true로 설정
		canvasGroup.blocksRaycasts = true;

		// 상위 뷰가 남아있으면 "돌아가기" 버튼을 활성화하고, 버튼에 표시되는 이름 설정
		if ( stackViews.Count >= 1 )
		{
			SetNavigationBar(currentView.name, stackViews.Peek().name);
		}
		// 상위 뷰가 없으면 "돌아가기" 버튼 비활성화
		else
		{
			SetNavigationBar(currentView.name);
		}
	}

	public void SetNavigationBar(string title, string prevBtnName="")
	{
		// 현재 View 이름 출력
		textTitle.text = title;

		// 현재 View가 최상위 View이면
		if ( prevBtnName.Equals("") )
		{
			// 돌아가기 버튼 비활성화
			buttonPrev.gameObject.SetActive(false);
		}
		else
		{
			// 돌아가기 버튼 활성화, 상위 View 이름 출력
			buttonPrev.gameObject.SetActive(true);
			textPrevName.text = prevBtnName;
		}
	}
}

