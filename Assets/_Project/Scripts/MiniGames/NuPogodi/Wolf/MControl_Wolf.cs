using UnityEngine;

public class MControl_Wolf : MonoBehaviour
{
    [SerializeField, Header("�������� 4 ����� ��� ����������� ''�����''"), Tooltip("����� ������ �������")]
    private Transform leftDTransfrom;
    [SerializeField, Tooltip("����� ������� �������")]
    private Transform leftUTransfrom;
    [SerializeField, Tooltip("������ ������ �������")]
    private Transform rightDTransfrom;
    [SerializeField, Tooltip("������ ������� �������")]
    private Transform rughtUTransfrom;

    //������� � �����������
    private Vector2 leftDPos;
    private Vector2 leftUPos;
    private Vector2 rightUPos;
    private Vector2 rightDPos;

    private void OnEnable()
    {
        gameObject.transform.position = leftDPos;
    }

    private void Awake()
    {
        InitPos();
    }

    private void Update()
    {
        UpdateInput();
    }


    private void UpdateInput()
    {
        if (gameObject.activeSelf)
        {
            Vector2 currentPos = gameObject.transform.position;
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentPos == rightUPos)
                {
                    gameObject.transform.position = leftUPos;
                }
                if (currentPos == rightDPos)
                {
                    gameObject.transform.position = leftDPos;
                }
                MG_NuPogodi_AudioManager.Instance.PlayOneShotAudio("FootStep");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentPos == leftUPos)
                {
                    gameObject.transform.position = rightUPos;
                }
                if (currentPos == leftDPos)
                {
                    gameObject.transform.position = rightDPos;
                }
                MG_NuPogodi_AudioManager.Instance.PlayOneShotAudio("FootStep");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (currentPos == rightDPos)
                {
                    gameObject.transform.position = rightUPos;
                }
                if (currentPos == leftDPos)
                {
                    gameObject.transform.position = leftUPos;
                }
                MG_NuPogodi_AudioManager.Instance.PlayOneShotAudio("FootStep");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (currentPos == rightUPos)
                {
                    gameObject.transform.position = rightDPos;
                }
                if (currentPos == leftUPos)
                {
                    gameObject.transform.position = leftDPos;
                }
                MG_NuPogodi_AudioManager.Instance.PlayOneShotAudio("FootStep");
            }
        }
    }

    private void InitPos()
    {
        leftDPos = leftDTransfrom.position;
        leftUPos = leftUTransfrom.position;
        rightUPos = rughtUTransfrom.position;
        rightDPos = rightDTransfrom.position;
        gameObject.transform.position = leftDPos;
    }
}
