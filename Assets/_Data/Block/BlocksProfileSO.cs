using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/BlocksProfile", order = 1)]
public class BlocksProfileSO : ScriptableObject
{
    public List<Sprite> sprites = new List<Sprite>();
}
