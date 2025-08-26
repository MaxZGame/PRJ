using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        SFX_Main.Instance.PlayAudio("Select"); //Звук при нажатии на эту клавишу
        Application.Quit();
    }
}
