using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCodeChecker
{
    private readonly string konamiCode = "↑↑↓↓←→←→BA";
    private string inputSequence = "";

    private bool isCheckingKonamiCode = false;

    public bool IsCodeEntered { get; private set; } = false;

    public void CheckInput()
    {
        if (IsCodeEntered) return;

        string keyPressed = GetPressedKey();
        if (!string.IsNullOrEmpty(keyPressed))
        {
            inputSequence += keyPressed;

            if (inputSequence == konamiCode)
            {
                IsCodeEntered = true;
                Debug.Log("Konami Code Entered!");
            }
            else if (!konamiCode.StartsWith(inputSequence))
            {
                Reset();
            }
        }
    }

    private string GetPressedKey()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)) return "↑";
        if (Input.GetKeyUp(KeyCode.DownArrow)) return "↓";
        if (Input.GetKeyUp(KeyCode.LeftArrow)) return "←";
        if (Input.GetKeyUp(KeyCode.RightArrow)) return "→";
        if (Input.GetKeyUp(KeyCode.A)) return "A";
        if (Input.GetKeyUp(KeyCode.B)) return "B";
        return null;
    }

    public void Reset()
    {
        inputSequence = "";
        IsCodeEntered = false;
    }

    public void SetCheckingKonamiCode(bool active)
    {
        isCheckingKonamiCode = active;
    }

    public bool IsCheckingKonamiCode() => isCheckingKonamiCode;

    public string GetInputSequence() => inputSequence;
}
