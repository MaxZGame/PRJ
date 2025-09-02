using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField, Header("������ ������� ���������")]
    public Inventory_Start inventory_Start { get; private set; }

    [SerializeField, Header("�������� ���� ������ ���������")]
    private Inventory inventory;

    private void Awake()
    {
        Instance = this;
        inventory.Init();
    }

    private void Start()
    {
        StoryManager.Instance.Message();
        RoomsController.Instance.FirstStart();
    }
}
