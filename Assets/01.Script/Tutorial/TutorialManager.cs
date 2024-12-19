using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // 텍스트 출력용 UI
    public float typingSpeed = 0.05f;    // 타이핑 속도 (글자당 딜레이)
    [SerializeField] private Image enterImage; // 엔터 이미지

    private int currentIndex = 0;        // 현재 메시지 인덱스
    private bool isWaveStarted = false;  // 웨이브 시작 여부
    private bool isTyping = false;       // 텍스트가 타이핑 중인지 여부
    private Coroutine typingCoroutine;   // 현재 실행 중인 타이핑 코루틴 참조

    private string[] messages = {
        "환영합니다!",
        "게임을 시작하기 전에 게임 설명과 조작방법을 알려드리겠습니다!",
        "먼저 게임 설명입니다.",
        "당신은 지금부터 타워를 수호하는 보호코어입니다.",
        "wasd로 레일을 타고 모서리로 이동할 수 있습니다.",
        "가운데 큰 마름표 안의 파란색이 체력, 작은 마름표 안의 빨간색이 노 리미트 게이지입니다!",
        "가운데 숫자는 점수판입니다. 적을 해치울 수록\n 점수가 올라갑니다!",
        "타워를 침략하는 로봇 군단으로부터 타워를 지키고 살아남으세요!",
        "마침 저기 적 허수아비가 있네요. 좌클릭해서 공격해봅시다.",
        "적 처치 시마다 게이지가 일정량 충전됩니다. 100% 충전 시 노 리미트 타임이 발동됩니다!",
        "노 리미트 타임(15초)동안에는 이동 제한이 해제됩니다.",
        "그리고 모든 적을 한방에 처치할 수 있습니다!",
        "F키로 상호작용 시 아이템을 획득할 수 있습니다!",
        "스킬은 E키와 Q키를 이용해 사용할 수 있습니다",
        "먼저 획득한 스킬은 E키, 나중에 획득한 스킬은 Q키로 들어갑니다",
        "준비를 다 마쳤습니다. 이제 게임을 시작해볼까요?"
    };

    void Start()
    {
        StartTyping();
        enterImage.enabled = false; // 초기에는 이미지 숨김
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바: 현재 타이핑 완료
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine); // 타이핑 중지
                tutorialText.text = messages[currentIndex]; // 전체 텍스트 출력
                isTyping = false;
                enterImage.enabled = true; // 엔터 이미지 표시
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // 엔터: 다음 메시지로 이동
        {
            if (!isTyping && currentIndex < messages.Length - 1)
            {
                currentIndex++;
                StartTyping();
                enterImage.enabled = false; // 다음 메시지가 출력되면 이미지 숨김
            }
        }
    }

    private void StartTyping()
    {
        typingCoroutine = StartCoroutine(TypeMessage(messages[currentIndex]));
        enterImage.enabled = false; // 타이핑 중에는 이미지 숨김
    }

    private IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        tutorialText.text = ""; // 기존 텍스트 초기화

        foreach (char letter in message.ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 한 글자 출력 후 대기
        }

        isTyping = false; // 타이핑 완료
        enterImage.enabled = true; // 타이핑이 끝나면 이미지 표시

    }
}
