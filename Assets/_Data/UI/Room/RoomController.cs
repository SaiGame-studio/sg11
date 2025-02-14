using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : SaiMonoBehaviour
{
    [SerializeField] protected TMP_InputField input;
    [SerializeField] protected Button createRoomBtn;
    [SerializeField] protected Button joinRoomBtn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInput();
        this.LoadCreateBtn();
        this.LoadJointBtn();
    }

    private void LoadJointBtn()
    {
        if (this.joinRoomBtn != null) return;
        //this.joinRoomBtn = GetComponentInChildren<BtnJoinRoom>().GetComponent<Button>();
        Debug.LogWarning(transform.name + " LoadJointBtn", gameObject);
    }

    private void LoadCreateBtn()
    {
        if (this.createRoomBtn != null) return;
        //this.createRoomBtn = GetComponentInChildren<BtnCreateRoom>().GetComponent<Button>();
        Debug.LogWarning(transform.name + " LoadCreateBtn", gameObject);
    }

    private void LoadInput()
    {
        if (this.input != null) return;
        this.input = GetComponentInChildren<TMP_InputField>();
        Debug.LogWarning(transform.name + " LoadInput", gameObject);
    }
}
