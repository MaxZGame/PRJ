using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [field: SerializeField, Header("Вставить ItemSo")]
    public ItemSO itemSO { get; private set; }


    //Коллайдер предмета
    private Collider2D col2d;

    //Спрайт-рендерер на предмете
    private SpriteRenderer spriteRenderer;


    public int IndexItem { get; private set; }

    //Флаг находится ли в инвентаре?
    private bool isInInventory = false;

    //Блок для перетаскивания
    /// <summary>
    /// Флаг - перетаскиваем?
    /// </summary>
    private bool isDragging = false;
    /// <summary>
    /// Главная камера игры
    /// </summary>
    private Camera cam;
    /// <summary>
    /// оффсет между курсором и центром объекта
    /// </summary>
    private Vector3 grabOffset;
    //Конец блока для перетаскивания


    [SerializeField, Header("===Если интерактивный, то вставляем всё ниже===")]
    private GameObject objInteractive;


    private void Awake()
    {
        IndexItem = -1;
    }

    private void OnEnable()
    {
        if (objInteractive != null && objInteractive.activeSelf == true)
        {
            objInteractive.SetActive(false);
        }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = itemSO.SpriteItem;
        col2d = GetComponent<Collider2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        CloseInteractiveObj();
    }

    void OnMouseDown()
    {
        if (isInInventory)
        {
            DragItemDown();
        }
        else
        {
            if (itemSO.IsInteractive)
            {
                objInteractive.SetActive(true);
            }
            else
            {
                Inventory.Instance.AddItems(itemSO, gameObject);
            }
        }
    }


    private void OnMouseDrag()
    {
        DragItemDrag();
    }

    private void OnMouseUp()
    {
        DragItemUp();
    }

    private void CloseInteractiveObj()
    {
        if (objInteractive != null && objInteractive.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                objInteractive.SetActive(false);
            }
        }
    }

    private void DragItemDown()
    {
        Vector3 worldCoor = cam.ScreenToWorldPoint(Input.mousePosition);
        grabOffset = transform.position - worldCoor;
        isDragging = true;
    }

    private void DragItemDrag()
    {
        if (!isDragging)
        {
            return;
        }
        Inventory.Instance.RemoveItems(itemSO, this, this.gameObject);
        Vector3 worldCoor = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = worldCoor + grabOffset;
        transform.position = target;
    }

    private void DragItemUp()
    {
        isDragging = false;
    }

    public void SetIsInInventory(bool set, int index)
    {
        isInInventory = set;
        IndexItem = index;
    }

    private void OnValidate()
    {
        if (itemSO != null)
        {
            if (itemSO.IsInteractive)
            {
                if (objInteractive == null)
                {
                    Debug.LogError($"[Item,Validate]Нет интерактивной панели для интерактивного объекта!{gameObject.name}");
                }
            }
            else
            {
                objInteractive = null;
            }
        }

        if (this.gameObject.activeSelf && itemSO == null)
        {
            Debug.LogError($"[Item,Validate] Не назначен ItemSO в {gameObject.name}!");
        }

    }
}
