using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // �ؽ�Ʈ ��¿� UI
    public float typingSpeed = 0.05f;    // Ÿ���� �ӵ� (���ڴ� ������)

    private int currentIndex = 0;        // ���� �޽��� �ε���
    public GameObject waveManager;       // ���̺� �Ŵ��� ������Ʈ
    private bool isWaveStarted = false;  // ���̺� ���� ����
    private bool isTyping = false;       // �ؽ�Ʈ�� Ÿ���� ������ ����
    private Coroutine typingCoroutine;   // ���� ���� ���� Ÿ���� �ڷ�ƾ ����

    private string[] messages = {
        "ȯ���մϴ�!",
        "������ �����ϱ� ���� ���� ����� ���۹���� �˷��帮�ڽ��ϴ�!",
        "���� ���� �����Դϴ�.",
        "����� ���ݺ��� Ÿ���� ��ȣ�ϴ� ��ȣ�ھ��Դϴ�.",
        "wasd�� ������ Ÿ�� �𼭸��� �̵��� �� �ֽ��ϴ�.",
        "��� ū ����ǥ ���� �Ķ����� ü��, ���� ����ǥ ���� �������� �� ����Ʈ �������Դϴ�!",
        "��� ���ڴ� �������Դϴ�. ���� ��ġ�� ����\n ������ �ö󰩴ϴ�!",
        "Ÿ���� ħ���ϴ� �κ� �������κ��� Ÿ���� ��Ű�� ��Ƴ�������!",
        "��ħ ���� �߾ӿ��� �κ����� �������׿�. ��Ŭ���ؼ� �����غ��ô�.",
        "�� óġ �ø��� �������� ������ �����˴ϴ�. 100% ���� �� �� ����Ʈ Ÿ���� �ߵ��˴ϴ�!",
        "�� ����Ʈ Ÿ��(15��)���ȿ��� �̵� ������ �����˴ϴ�.",
        "�׸��� ��� ���� �ѹ濡 óġ�� �� �ֽ��ϴ�!",
        "ü���� ���� �������� ���� �����ص帮�ڽ��ϴ�.",
        "�������� ���۹���Դϴ�.",
        "�غ� �� ���ƽ��ϴ�. ���� ������ �����غ����?"
    };

    void Start()
    {
        StartTyping();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽���: ���� Ÿ���� �Ϸ�
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine); // Ÿ���� ����
                tutorialText.text = messages[currentIndex]; // ��ü �ؽ�Ʈ ���
                isTyping = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // ����: ���� �޽����� �̵�
        {
            if (!isTyping && currentIndex < messages.Length - 1)
            {
                currentIndex++;
                StartTyping();
            }
        }
    }

    private void StartTyping()
    {
        typingCoroutine = StartCoroutine(TypeMessage(messages[currentIndex]));
    }

    private IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        tutorialText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in message.ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // �� ���� ��� �� ���
        }

        isTyping = false; // Ÿ���� �Ϸ�

        // �ε��� 7���� ���̺� �Ŵ��� ����
        if (currentIndex == 7 && !isWaveStarted)
        {
            isWaveStarted = true;
            Instantiate(waveManager);
        }
    }
}
