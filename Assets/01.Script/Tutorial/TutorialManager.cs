using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // 텍스트 출력용 UI
    public float messageInterval = 2f;  // 메시지 출력 간격
    public float typingSpeed = 0.05f;   // 타이핑 속도 (글자당 딜레이)

    private int currentIndex = 0;       // 현재 메시지 인덱스
    public GameObject waveManager;      // 웨이브 매니저 오브젝트
    private bool isWaveStarted = false; // 웨이브 시작 여부

    private string[] messages = {
        "환영합니다!",
        "게임을 시작하기 전에 게임 설명과 조작방법을 알려드리겠습니다!",
        "먼저 게임 설명입니다.",
        "당신은 지금부터 타워를 수호하는 보호코어입니다.",
        "wasd로 레일을 타고 모서리로 이동할 수 있습니다.",
        "타워를 침략하는 로봇 군단으로부터 타워를 지키고 살아남으세요!",
        "가운데 큰 마름표 안의 파란색이 체력, 작은 마름표 안의 빨간색이 노 리미트 게이지입니다!",
        "가운데 숫자는 점수판입니다. 적을 해치울 수록 점수가 올라갑니다!",
        "마침 저기 중앙에서 로봇들이 몰려오네요. 좌클릭해서 공격해봅시다.",
        "적 처치 시마다 게이지가 일정량 충전됩니다. 100% 충전 시 노 리미트 타임이 발동됩니다!",
        "노 리미트 타임(15초)동안에는 이동 제한이 해제됩니다.",
        "그리고 모든 적을 한방에 처치할 수 있습니다!",
        "체험을 위해 게이지를 전부 충전해드리겠습니다.", //
        "다음으로 조작방법입니다.",
        "준비를 다 마쳤습니다. 이제 게임을 시작해볼까요?"
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

            // 인덱스 7에서 웨이브 매니저를 시작
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
        tutorialText.text = ""; // 기존 텍스트 초기화

        foreach (char letter in message.ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 한 글자 출력 후 대기
        }
    }
}
