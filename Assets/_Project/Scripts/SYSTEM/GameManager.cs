using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField, Header("Кнопка запуска инвентаря")]
    public Inventory_Start inventory_Start { get; private set; }

    [SerializeField, Header("Вставить сюда скрипт инвентаря")]
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
