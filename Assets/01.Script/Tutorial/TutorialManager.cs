using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // �ؽ�Ʈ ��¿� UI
    public float typingSpeed = 0.05f;    // Ÿ���� �ӵ� (���ڴ� ������)
    [SerializeField] private Image enterImage; // ���� �̹���

    private int currentIndex = 0;        // ���� �޽��� �ε���
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
        "��ħ ���� �� ����ƺ� �ֳ׿�. ��Ŭ���ؼ� �����غ��ô�.",
        "�� óġ �ø��� �������� ������ �����˴ϴ�. 100% ���� �� �� ����Ʈ Ÿ���� �ߵ��˴ϴ�!",
        "�� ����Ʈ Ÿ��(15��)���ȿ��� �̵� ������ �����˴ϴ�.",
        "�׸��� ��� ���� �ѹ濡 óġ�� �� �ֽ��ϴ�!",
        "FŰ�� ��ȣ�ۿ� �� �������� ȹ���� �� �ֽ��ϴ�!",
        "��ų�� EŰ�� QŰ�� �̿��� ����� �� �ֽ��ϴ�",
        "���� ȹ���� ��ų�� EŰ, ���߿� ȹ���� ��ų�� QŰ�� ���ϴ�",
        "�غ� �� ���ƽ��ϴ�. ���� ������ �����غ����?"
    };

    void Start()
    {
        StartTyping();
        enterImage.enabled = false; // �ʱ⿡�� �̹��� ����
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
                enterImage.enabled = true; // ���� �̹��� ǥ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // ����: ���� �޽����� �̵�
        {
            if (!isTyping && currentIndex < messages.Length - 1)
            {
                currentIndex++;
                StartTyping();
                enterImage.enabled = false; // ���� �޽����� ��µǸ� �̹��� ����
            }
        }
    }

    private void StartTyping()
    {
        typingCoroutine = StartCoroutine(TypeMessage(messages[currentIndex]));
        enterImage.enabled = false; // Ÿ���� �߿��� �̹��� ����
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
        enterImage.enabled = true; // Ÿ������ ������ �̹��� ǥ��

    }
}
