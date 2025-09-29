using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory_UI))]
[RequireComponent(typeof(Inventory_Sound))]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //Основное хранилище предметов
    public Dictionary<string, GameObject> inventoryItemsDIC = new Dictionary<string, GameObject>();

    //Хранилище позиций для предметов
    private Dictionary<string, Vector2> posItemsDIC = new Dictionary<string, Vector2>();

    [SerializeField, Header("Вставьте объекты позиций слотов")]
    private Transform[] transformsSlots;
    private List<Vector2> posSlots = new List<Vector2>();

    [SerializeField, Header("Укажите максимальное количество предметов в инвентаре"), Range(1, 9)]
    private int maxItemsInInventory = 5;
    [SerializeField, Header("Укажите за какое время предметы вылезают из инвентаря"), Range(0f, 2f)]
    private float timeSpeedItem = 0.5f;


    [SerializeField, Header("Укажите насколько увеличивается инвентарь при открытии"), Range(1, 4)]
    private float scaleInventoryOpen = 2;
    private Vector3 beginScale; //Начальный размер кнопки инвентаря

    [SerializeField, Header("Вставьте начальную позицию предмета в инвентаре")]
    private Transform transformPointBegin;
    private Vector2 posPointBegin;

    [SerializeField, Header("Вставьте объект, который станет родительским для предметов в инвентаре")]
    private GameObject newParentItems;

    //Объект, который будет родительским при удалении из инвентаря
    private GameObject oldParentItems;

    [SerializeField, Header("Вставьте объект визуала инвентаря")]
    private GameObject objInventory;

    [SerializeField, Header("Вставьте позицию инвентаря")]
    private Transform transformInventory;

    [SerializeField, Header("Вставьте кнопку активации инвентаря")]
    private ActivatedInventoryButton activatedInventoryButton;

    private Inventory_Sound inventory_Sound; //Звуковой скрипт инвентаря


    //Флаг, если переходим в другую комнату
    [HideInInspector]
    public bool isNextRoomStep = false;

    //Скрипт, который отвечает за управление интерфейсом инвентаря
    private Inventory_UI inventory_UI;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItems(ItemSO itemSO, GameObject objItem)
    {
        if (posItemsDIC.Count < maxItemsInInventory)
        {

            objItem.transform.SetParent(newParentItems.transform); // Устанавливаем нового родителя
            objItem.SetActive(false); //Деактивируем
            objItem.transform.position = posPointBegin; //Присваиваем новую позицию

            Item itemScript = objItem.GetComponent<Item>(); //Инициализируем скрипт предмета

            inventoryItemsDIC[itemSO.NameItem] = objItem; // Добавляем в хранилище
            CompareNameItem(itemScript);    //Проверяем совпадение имен

            int index = GetFreeIndex(); //Присваиваем индекс

            itemScript.SetIsInInventory(true, index);

            //Добавляем слот предмету
            posItemsDIC[itemSO.NameItem] = posSlots[itemScript.IndexItem];

            if (activatedInventoryButton.IsActivated)
            {
                objItem.SetActive(true);
                objItem.transform.position = posItemsDIC[itemSO.NameItem];
            }

            inventory_Sound.PlayAddItem(); //Звук добавления предмета
        }
        else
        {
            inventory_Sound.PlayAudio("FullInventory");
        }

        inventory_UI.UpdateCount(inventoryItemsDIC.Count, maxItemsInInventory); //Обновляем UI отображение количества предметов
    }

    public void RemoveItems(ItemSO itemSO, Item itemScript, GameObject objItem)
    {
        //Получаем все объекты с тегом ITEMS
        GameObject[] oldParentItemsALL = GameObject.FindGameObjectsWithTag("ITEMS");

        //Перебираем все объекты с тегом ITEMS
        foreach (GameObject parents in oldParentItemsALL)
        {
            if (parents.activeSelf) // Если объект активен, то даем переменной этот объект
            {
                oldParentItems = parents;
            }
        }

        objItem.transform.SetParent(oldParentItems.transform); // Устанавливаем старого родителя
        oldParentItems = null; //Сбрасываем старого родителя

        itemScript.SetIsInInventory(false, -1);

        inventoryItemsDIC.Remove(itemSO.NameItem);
        posItemsDIC.Remove(itemSO.NameItem);

        inventory_UI.UpdateCount(inventoryItemsDIC.Count, maxItemsInInventory); //Обновляем UI отображение количества предметов
    }

    public void OnEnableInventory()
    {
        
        objInventory.transform.position = posPointBegin; //Ставим на позицию
        RectTransform rectTransform = objInventory.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(scaleInventoryOpen, scaleInventoryOpen, scaleInventoryOpen);//Меняем размер инвентаря
        //Закончили

        foreach (var kvp in inventoryItemsDIC)
        {
            string key = kvp.Key;
            GameObject item = kvp.Value;
            if (!posItemsDIC.TryGetValue(key, out var pos))
            {
                Debug.LogWarning($"[Inventory] Нет позиции для ключа: {key}");
                continue;
            }
            StartCoroutine(MoveWithLerp.Instance.Move(item.transform, pos, timeSpeedItem)); //Плавно расставляем предметы по слотам
            item.SetActive(true);
        }
        if (!isNextRoomStep)
        {
            inventory_Sound.PlayAudio("OpenClose"); // Звук открытия,закрытия инвентаря
        }
    }

    public void OnDisableInventory()
    {
        //Меняем размер инвентаря
        objInventory.transform.position = transformInventory.position;
        RectTransform rectTransform = objInventory.GetComponent<RectTransform>();
        rectTransform.localScale = beginScale;
        //Закончили

        foreach (var kvp in inventoryItemsDIC)
        {
            string key = kvp.Key;
            GameObject item = kvp.Value;
            item.SetActive(false);
            item.transform.position = posPointBegin;
            activatedInventoryButton.SetActivated(false);
        }
        if (!isNextRoomStep)
        {
            inventory_Sound.PlayAudio("OpenClose"); // Звук открытия,закрытия инвентаря
        }
    }

    private int GetFreeIndex() //Метод поиска свободных слотов
    {
        for (int i = 0; i < posSlots.Count; i++)
        {
            bool slotBusy = false;
            foreach (var kvp in inventoryItemsDIC)
            {
                Item item = kvp.Value.GetComponent<Item>();
                if (item.IndexItem == i)
                {
                    slotBusy = true;
                }
            }
            if (!slotBusy)
            {
                return i;
            }
        }
        return -1; //Если свободных слотов не нашлось
    }

    private void CompareNameItem(Item itemScript)
    {
        int countCompared = 0;
        foreach (var kvp in inventoryItemsDIC)
        {
            Item compareItemScripts = kvp.Value.GetComponent<Item>();
            if (itemScript.IndexItem == compareItemScripts.IndexItem)
            {
                countCompared++;
            }
            if (countCompared > 1)
            {
                Debug.LogError($"У {itemScript.gameObject.name} и {compareItemScripts.gameObject.name} одинаковое имя {itemScript.IndexItem}, измените имя у ItemSO, чтобы не возникало конфликтов!");
            }
        }

    }

    public void Init() //Метод инициализации
    {
        for (int i = 0; i < transformsSlots.Length; i++)
        {
            posSlots.Add(transformsSlots[i].transform.position);
        }
        if (posSlots.Count < maxItemsInInventory)
        {
            Debug.LogError($"Слотов меньше, чем указанное максимальное количество предметов!");
        }
        posPointBegin = transformPointBegin.transform.position;
        beginScale = objInventory.transform.localScale;
        inventory_Sound = GetComponent<Inventory_Sound>();
        inventory_UI = GetComponent<Inventory_UI>();
        inventory_UI.UpdateCount(inventoryItemsDIC.Count, maxItemsInInventory); //Обновляем UI отображение количества предметов
        ActivatedInventory();
    }

    public void ActivatedInventory()
    {
        if (!GameManager.Instance.inventory_Start.isStartInventory) //Если кнопка старта инвентаря не запущена, то объект инвентаря неактивен
        {
            objInventory.SetActive(false);
        }
        else
        {
            objInventory.SetActive(true);
        }
    }

#if UNITY_EDITOR
    private void OnValidate() //Защита от дурака (в том числе и от себя)
    {
        if (transformsSlots.Length < maxItemsInInventory)
        {
            Debug.LogError($"{transformsSlots} меньше, чем - максимально назначенное количество слотов! Создайте больше слотов!");
        }
    }
#endif
}
