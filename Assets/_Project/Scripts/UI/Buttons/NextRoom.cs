using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField, Header("�������� �������, ������� ����� ������������")]
    private GameObject[] activatedGO;

    [SerializeField, Header("�������� �������, ������� ����� ��������������")]
    private GameObject[] deactivatedGO;

    private bool lockButton = false;

    private void OnMouseDown()
    {
        if (gameObject.tag == "NextRoom")
        {
            Activated();
            Deactivated();
        }
    }

    public void Locker(bool set)
    {
        lockButton = set;
    }

    public void Deactivated()
    {
        if (!lockButton)
        {
            if (deactivatedGO != null & deactivatedGO.Length > 0)
            {
                foreach (GameObject go in deactivatedGO)
                {
                    go?.SetActive(false);
                }
            }
            Inventory.Instance.isNextRoomStep = true;
            Inventory.Instance.OnDisableInventory();
            Inventory.Instance.isNextRoomStep = false;
        }
    }

    public void Activated()
    {
        if (!lockButton)
        {
            SFX_Main.Instance.PlayAudio("Select"); //���� ��� ������� �� ��� �������
            if (activatedGO != null && activatedGO.Length > 0)
            {
                foreach (GameObject go in activatedGO)
                {
                    go?.SetActive(true);
                }
            }
            RoomsController.Instance.RoomCurrentUpdate();
        }
    }
}
