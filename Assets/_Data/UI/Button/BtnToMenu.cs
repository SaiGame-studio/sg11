using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnToMenu : BaseButton
{
    protected override void OnClick()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        SceneManager.LoadScene(GameState.MainMenu.ToString());
    }
}
