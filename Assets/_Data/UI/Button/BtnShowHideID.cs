using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShowHideID : BaseButton
{
    private NodeObj[] listNode;

    private void SetupNodeObj()
    {
        listNode = BlockSpawner.Instance.Holder.GetComponentsInChildren<NodeObj>();
    }

    protected override void OnClick()
    {
        if(listNode == null)
        {
            SetupNodeObj();
        }

        foreach (NodeObj node in listNode)
        {
            bool active = !node.gameObject.activeInHierarchy;
            node.gameObject.SetActive(active);
        }
    }
}
