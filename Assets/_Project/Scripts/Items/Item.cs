using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [field: SerializeField, Header("�������� ItemSo")]
    public ItemSO itemSO { get; private set; }

    private Collider2D col2d;

    private SpriteRenderer spriteRenderer;

    [SerializeField, Header("===���� �������������, �� ��������� �� ����===")]
    private GameObject objInteractive;

    //���� ��������� �� � ���������?
    private bool isInInventory = false;


    private void OnEnable()
    {
        if (objInteractive != null && objInteractive.activeSelf == true)
        {
            objInteractive.SetActive(false);
        }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = itemSO.SpriteItem;
        col2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CloseInteractiveObj();
    }

    void OnMouseDown()
    {
        if (isInInventory)
        {
            DragItem(); //������ �����!!!
            Debug.Log("�������� ��� ����������� �� ���������");
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

    private void DragItem()
    {
        //����� ����� ������ ����������� �������
    }

    public void SetIsInInventory(bool set)
    {
        isInInventory = set;
    }

    private void OnValidate()
    {
        if (itemSO != null)
        {
            if (itemSO.IsInteractive)
            {
                if (objInteractive == null)
                {
                    Debug.LogError($"[Item,Validate]��� ������������� ������ ��� �������������� �������!{gameObject.name}");
                }
            }
            else
            {
                objInteractive = null;
            }
        }

        if (this.gameObject.activeSelf && itemSO == null)
        {
            Debug.LogError($"[Item,Validate] �� �������� ItemSO � {gameObject.name}!");
        }

    }
}
