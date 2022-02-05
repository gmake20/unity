using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ChatType { Normal = 0, Party, Guild, Whisper, System, Count }

public class ChatController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		textChatPrefab;			// ��ȭ�� ����ϴ� Text UI ������
	[SerializeField]
	private	Transform		parentContent;			// ��ȭ�� ��µǴ� ScrollView�� Content
	[SerializeField]
	private	TMP_InputField	inputField;				// ��ȭ �Է�â

	[SerializeField]
	private	Sprite[]		spriteChatInputType;	// ��ȭ �Է� �Ӽ� ��ư�� ������ �̹��� ����
	[SerializeField]
	private	Image			imageChatInputType;		// ��ȭ �Է� �Ӽ� ��ư�� �̹���
	[SerializeField]
	private	TextMeshProUGUI	textInput;				// ��ȭ �Է� �Ӽ��� ���� ��ȭ �Է�â�� �ۼ��Ǵ� �ؽ�Ʈ ���� ����
	
	private	ChatType		currentInputType;		// ���� ��ȭ �Է� �Ӽ� (Normal, Party, Guild)
	private	Color			currentTextColor;		// ��ȭ �Է� �Ӽ��� ���� �Է�/��� �ؽ�Ʈ ���� ����

	private	List<ChatCell>	chatList;				// ��ȭâ�� ��µǴ� ��� ��ȭ�� �����ϴ� ����Ʈ
	private	ChatType		currentViewType;		// ���� ��ȭ ���� �Ӽ� (Normal, Party, Guild, Whisper, System)

	private	string			lastChatData = "";		// �������� �ۼ��� ��ȭ ����
	private	string			lastWhisperID = "";		// �������� �Ӹ��� ���� ���

	private	string			ID = "DoctorKO";        // ���� ���̵� (�ӽ�)
	private	string			friendID = "Noname";	// ģ�� ���̵� (�ӽ�)

	private void Awake()
	{
		chatList		 = new List<ChatCell>();

		currentInputType = ChatType.Normal;
		currentTextColor = Color.white;
	}

	private void Update()
	{
		// ��ȭ �Է�â�� ��Ŀ�� �Ǿ����� ���� �� EnterŰ�� ������
		if ( Input.GetKeyDown(KeyCode.Return) && inputField.isFocused == false )
		{
			// ��ȭ �Է�â�� ��Ŀ���� Ȱ��ȭ
			inputField.ActivateInputField();
		}

		// ��ȭ �Է�â�� ��Ŀ�� �Ǿ����� �� TabŰ�� ������
		if ( Input.GetKeyDown(KeyCode.Tab) && inputField.isFocused == true )
		{
			// ���� ��ȭ �Է� �Ӽ� ����
			SetCurrentInputType();
		}
	}

	/// <summary>
	/// InputField �Է� ���� �� EnterŰ ���� �̿��� Focus�� ��Ȱ��ȭ�� �� ȣ��
	/// </summary>
	public void OnEndEditEventMethod()
	{
		Debug.Log("OnEndEditEventMethod");
		// EnterŰ�� ������ ��ȭ �Է�â�� �Էµ� ������ ��ȭâ�� ���
		if ( Input.GetKeyDown(KeyCode.Return) )
		{
			Debug.Log("Enter Key");
			UpdateChat();
		}
	}

	/// <summary>
	/// EnterŰ or ��ư�� ���� InputField�� �ۼ��� ������ ��ȭâ�� ���
	/// </summary>
	public void UpdateChat()
	{
		// InputField�� ��������� ����
		if ( inputField.text.Equals("") ) return;

		// ��ȭ �Է�â�� �Էµ� ������ ��ȭâ�� ��� (��ɾ ���ԵǾ� ���� �� ��ɾ� ó��)
		UpdateChatWithCommand(inputField.text);
	}

	private Color ChatTypeToColor(ChatType type)
	{
		// ��ȭ �Ӽ��� ���� ���� �� ��ȯ (�Ϲ�, ��Ƽ, ���, �Ӹ�, �ý���)
		Color[] colors = new Color[(int)ChatType.Count] {
			Color.white, Color.blue, Color.green, Color.magenta, Color.yellow };

		return colors[(int)type];
	}

	public void SetCurrentInputType()
	{
		// ���� ��ȭ �Ӽ��� �� �ܰ辿 ��ȭ (�Ӹ�, �ý����� �Է� �Ӽ��� ���� ������ ����)
		currentInputType = (int)currentInputType < (int)ChatType.Count-3 ? currentInputType+1 : 0;
		// ���� ��ȭ �Ӽ��� ���� ��ư �̹��� ����
		imageChatInputType.sprite = spriteChatInputType[(int)currentInputType];
		// ���� ��ȭ �Ӽ��� ���� ��µǴ� �ؽ�Ʈ ���� ����
		currentTextColor = ChatTypeToColor(currentInputType);
		// ��ȭ �Է�â�� �ؽ�Ʈ ���� ���� (Normal�� �� �Է�â�� �Ͼ�� ��� ���������� ���)
		textInput.color = currentTextColor == Color.white ? Color.black : currentTextColor;
	}

	public void SetCurrentViewType(int newType)
	{
		// Button UI�� OnClick �̺�Ʈ�� �������� �Ű������� ó���� �ȵǼ� int�� �޾ƿ´�.
		currentViewType = (ChatType)newType;

		// ���� ��ȭ ���� �Ӽ��� �Ϲ��̸�
		if ( currentViewType == ChatType.Normal )
		{
			// ��� ��ȭ ��� Ȱ��ȭ
			for ( int i = 0; i < chatList.Count; ++ i )
			{
				chatList[i].gameObject.SetActive(true);
			}
		}
		// ���� ��ȭ ���� �Ӽ��� �Ϲ��� �ƴϸ�
		else
		{
			for ( int i = 0; i < chatList.Count; ++ i )
			{
				// ���� ��ȭ ���� �Ӽ��� �ش��ϴ� ��ȭ�� Ȱ��ȭ�ϰ�, �������� ��Ȱ��ȭ
				chatList[i].gameObject.SetActive(chatList[i].ChatType == currentViewType);
			}
		}
	}

	private void PrintChatData(ChatType type, Color color, string text)
	{
		// ��ȭ ���� ����� ���� Text UI ���� (textChatPrefab�� ���� �����ؼ� parentContent�� �ڽ����� ��ġ)
		GameObject	clone	= Instantiate(textChatPrefab, parentContent);
		ChatCell	cell	= clone.GetComponent<ChatCell>();

		// ��ȭ �Է�â�� �ִ� ������ ��ȭâ�� ��� (ID : ����)
		cell.Setup(type, color, $"{ID} : {text}");
		// ��ȭ �Է�â�� �ִ� ���� �ʱ�ȭ
		inputField.text = "";
		// ��ȭâ�� ����� ��ȭ�� ����Ʈ�� ����
		chatList.Add(cell);
	}

	public void UpdateChatWithCommand(string chat)
	{
		// '/'�� �������� �ʱ� ������ ��ɾ� ó�� ���� ��ȭ ���� ���
		if ( !chat.StartsWith("/") )
		{
			// ���� ��ȭ ������ lastChatData�� ���� (/re�� ������ ��ȭ ������ ����� �� ���)
			lastChatData = chat;
			// ��ȭâ�� ��ȭ ���� ��� (���� �Է� �Ӽ�, ����, ��ȭ ����)
			PrintChatData(currentInputType, currentTextColor, lastChatData);
			return;
		}

		/// '/'�� ������ ���� ��ɾ�� �ν��ϰ� ��ɾ� ó�� �߰�

		// �������� �ۼ��� ���� �ٽ� ���
		if ( chat.StartsWith("/re") )
		{
			// ������ ��ȭ ������ ������ ����
			if ( lastChatData.Equals("") )
			{
				inputField.text = "";
				return;
			}

			// ������ ��ȭ ���뿡 ��ɾ ���ԵǾ� ���� ���� �ֱ� ������
			// ������ ��ȭ ������ �Ű������� �ٽ� UpdateChatWithCommand() �޼ҵ� ȣ��
			UpdateChatWithCommand(lastChatData);
		}
		// �Ӹ�
		else if ( chat.StartsWith("/w ") )
		{
			// ���� ��ȭ ������ lastChatData�� ����
			lastChatData = chat;


			
			// ' '(����)�� �������� ���ڿ��� 3���� ����
			// ��ɾ�(/w), �Ӹ� ���, ����

			string[] whisper = chat.Split(' ', (char)3);

			// ��� ������ ���̵� �˻��� ������ ���̵� �ִ��� �˻� ��
			// ����� ������ �Ӹ��� ������, ����� ������ �ý��� �޼��� ���
			// (����� �ӽ÷� friendID �ϳ��� �����߱� ������ �ϳ��� ��)
			if ( whisper[1] == friendID )
			{
				// �Ӹ� ������ ���� ���� (/r�� ������ �Ӹ� ��󿡰� �Ӹ� ������ �� ���)
				lastWhisperID = whisper[1];
				// �Ӹ� �Է� �Ӽ����� �Ӹ� ������ ������ ���� ���
				// ��ɾ �ۼ��� ���� ������ ������ ���� �Է� �Ӽ�, ���� ������ ���� �������� �ʰ� ����
				PrintChatData(ChatType.Whisper, ChatTypeToColor(ChatType.Whisper), $"[to {whisper[1]}] {whisper[2]}");
			}
			else
			{
				// �ý��� �Է� �Ӽ����� �Ӹ��� �������� ����� ���ٰ� ���
				PrintChatData(ChatType.System, ChatTypeToColor(ChatType.System), $"Do not find [{whisper[1]}]");
			}
			
		}
		// �������� �Ӹ��� ���� ��󿡰� �ٽ� �Ӹ� ������
		else if ( chat.StartsWith("/r ") )
		{
			
			// ������ �Ӹ� ����� ������ ����
			if ( lastWhisperID.Equals("") )
			{
				inputField.text = "";
				return;
			}

			// ���� ��ȭ ������ lastChatData�� ����
			lastChatData = chat;

			// ' '(����)�� �������� ���ڿ��� 2���� ����
			// ��ɾ�(/r), ����
			string[] whisper = chat.Split(' ', (char)2);

			// �Ӹ� �Է� �Ӽ����� �Ӹ� ������ ������ ���� ���
			PrintChatData(ChatType.Whisper, ChatTypeToColor(ChatType.Whisper), $"[to {lastWhisperID}] {whisper[1]}");
			
		}

	}
}

