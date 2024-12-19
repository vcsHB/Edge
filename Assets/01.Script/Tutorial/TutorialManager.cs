using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // �ؽ�Ʈ ��¿� UI
    public float messageInterval = 2f;  // �޽��� ��� ����
    public float typingSpeed = 0.05f;   // Ÿ���� �ӵ� (���ڴ� ������)

    private int currentIndex = 0;       // ���� �޽��� �ε���
    public GameObject waveManager;      // ���̺� �Ŵ��� ������Ʈ
    private bool isWaveStarted = false; // ���̺� ���� ����

    private string[] messages = {
        "ȯ���մϴ�!",
        "������ �����ϱ� ���� ���� ����� ���۹���� �˷��帮�ڽ��ϴ�!",
        "���� ���� �����Դϴ�.",
        "����� ���ݺ��� Ÿ���� ��ȣ�ϴ� ��ȣ�ھ��Դϴ�.",
        "wasd�� ������ Ÿ�� �𼭸��� �̵��� �� �ֽ��ϴ�.",
        "Ÿ���� ħ���ϴ� �κ� �������κ��� Ÿ���� ��Ű�� ��Ƴ�������!",
        "��� ū ����ǥ ���� �Ķ����� ü��, ���� ����ǥ ���� �������� �� ����Ʈ �������Դϴ�!",
        "��� ���ڴ� �������Դϴ�. ���� ��ġ�� ���� ������ �ö󰩴ϴ�!",
        "��ħ ���� �߾ӿ��� �κ����� �������׿�. ��Ŭ���ؼ� �����غ��ô�.",
        "�� óġ �ø��� �������� ������ �����˴ϴ�. 100% ���� �� �� ����Ʈ Ÿ���� �ߵ��˴ϴ�!",
        "�� ����Ʈ Ÿ��(15��)���ȿ��� �̵� ������ �����˴ϴ�.",
        "�׸��� ��� ���� �ѹ濡 óġ�� �� �ֽ��ϴ�!",
        "ü���� ���� �������� ���� �����ص帮�ڽ��ϴ�.", //
        "�������� ���۹���Դϴ�.",
        "�غ� �� ���ƽ��ϴ�. ���� ������ �����غ����?"
    };

    void Start()
    {
        StartCoroutine(DisplayMessage());
    }

    private IEnumerator DisplayMessage()
    {
        while (currentIndex < messages.Length)
        {
            yield return StartCoroutine(TypeMessage(messages[currentIndex]));

            // �ε��� 7���� ���̺� �Ŵ����� ����
            if (currentIndex == 7 && !isWaveStarted)
            {
                isWaveStarted = true;
                Instantiate(waveManager);
            }

            currentIndex++;
            yield return new WaitForSeconds(messageInterval);
        }
    }

    private IEnumerator TypeMessage(string message)
    {
        tutorialText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in message.ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // �� ���� ��� �� ���
        }
    }
}
