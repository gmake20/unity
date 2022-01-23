using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavigationView : MonoBehaviour
{
	[SerializeField]
	private	RectTransform			currentView;	// ���� View ����
	[SerializeField]
	private	TextMeshProUGUI			textTitle;		// ���� View �̸� ���
	[SerializeField]
	private	Button					buttonPrev;		// "���ư���" ��ư (���� View�� �̵�)
	[SerializeField]
	private	TextMeshProUGUI			textPrevName;	// ���� View �̸� (��ư �Ʒ��� ���)
	
	private	CanvasGroup				canvasGroup;	// View �̵��� �� �� UI�� ������ �� ������ �浹ó�� On/Off ����
	private	Stack<RectTransform>	stackViews;		// ���� View �������� ����Ǵ� Stack �ڷᱸ��

	private void Awake()
	{
		canvasGroup	= GetComponent<CanvasGroup>();
		stackViews	= new Stack<RectTransform>();

		// ��ư�� �̺�Ʈ �޼ҵ� ���
		buttonPrev.onClick.AddListener(Pop);
		// ó���� �����ϴ� View�� ���� ������ View�̱� ������ ���ư��� ��ư ��Ȱ��ȭ
		buttonPrev.gameObject.SetActive(false);

		// NavigationBar�� �ִ� ���� View�� �̸� ����
		SetNavigationBar(currentView.name);
	}

	/// <summary>
	/// ���� View���� ���� View�� �̵��� �� ȣ��
	/// </summary>
	public void Push(RectTransform newView)
	{
		// ���� �䰡 Ȱ��/��Ȱ��ȭ �Ǵ� �������� �� �̵��� ���� �ִϸ��̼��� ������
		// �ִϸ��̼ǵǴ� ���ȿ��� ��ȣ�ۿ��� �Ұ����ϵ��� CanvasGroup.blocksRaycasts�� false�� ����
		// (�ڽ����� �ִ� UI�� �ϳ��� �����ϱ� ��Ʊ� ������ CanvasGroup ������Ʈ ���)
		canvasGroup.blocksRaycasts = false;

		/// ������� �� (���� View) /////////////////////////////////////////////////////////
		RectTransform previousView = currentView;
		previousView.gameObject.SetActive(false);	// ������� �並 ��Ȱ��ȭ
		stackViews.Push(previousView);				// Back ��ư�� ���� ���� View�� ���ư� �� �ֵ��� ���ÿ� ����
		
		/// �����ϴ� �� (���� View) /////////////////////////////////////////////////////////
		currentView = newView;
		currentView.gameObject.SetActive(true);		// �����ϴ� �並 Ȱ��ȭ
		
		// �ִϸ��̼��� ����Ǹ� blockRaycasts�� true�� ����
		canvasGroup.blocksRaycasts = true;

		// ���� ���̴� View �̸��� ����ϰ�, ���� View �̸��� "���ư���" ��ư �Ʒ��� ���
		SetNavigationBar(currentView.name, previousView.name);
	}

	/// <summary>
	/// ���� View���� ���� View�� �̵��� �� ȣ��
	/// </summary>
	public void Pop()
	{
		// ���� ���ÿ� �����ִ� �������� ������ 0�̸� Pop �� �� ���� ������ return
		if ( stackViews.Count < 1 )
		{
			return;
		}

		// �ִϸ��̼��� ����Ǵ� ���� blockRaycasts�� false�� ����
		canvasGroup.blocksRaycasts = false;

		/// ������� �� (���� View) /////////////////////////////////////////////////////////
		RectTransform previousView = currentView;
		previousView.gameObject.SetActive(false);	// ������� �並 ��Ȱ��ȭ
		
		/// �����ϴ� �� (���� View) /////////////////////////////////////////////////////////
		currentView = stackViews.Pop();				// ������ �̵��� ���� ���ÿ��� ���� �����Ͱ� ���� View
		currentView.gameObject.SetActive(true);		// �����ϴ� �並 Ȱ��ȭ
		
		// �ִϸ��̼��� ����Ǹ� blockRaycasts�� true�� ����
		canvasGroup.blocksRaycasts = true;

		// ���� �䰡 ���������� "���ư���" ��ư�� Ȱ��ȭ�ϰ�, ��ư�� ǥ�õǴ� �̸� ����
		if ( stackViews.Count >= 1 )
		{
			SetNavigationBar(currentView.name, stackViews.Peek().name);
		}
		// ���� �䰡 ������ "���ư���" ��ư ��Ȱ��ȭ
		else
		{
			SetNavigationBar(currentView.name);
		}
	}

	public void SetNavigationBar(string title, string prevBtnName="")
	{
		// ���� View �̸� ���
		textTitle.text = title;

		// ���� View�� �ֻ��� View�̸�
		if ( prevBtnName.Equals("") )
		{
			// ���ư��� ��ư ��Ȱ��ȭ
			buttonPrev.gameObject.SetActive(false);
		}
		else
		{
			// ���ư��� ��ư Ȱ��ȭ, ���� View �̸� ���
			buttonPrev.gameObject.SetActive(true);
			textPrevName.text = prevBtnName;
		}
	}
}

