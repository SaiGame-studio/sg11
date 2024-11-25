using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnRestartGame : BaseButton
{
    protected override void OnClick()
    {
        GameManager.Instance.ResetGameOverState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
