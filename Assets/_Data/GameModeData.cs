using UnityEngine;

[System.Serializable]
public class GameModeData
{
    [Header("Classic Mode")]
    public int shuffleClassic = 22;
    public int hintClassic = 9;

    [Header("Full Mode")]
    public int shuffleFull = 12;
    public int hintFull = 7;
    public int shuffleEachLevel = 3;
    public int hintEachLevel = 2;
}
