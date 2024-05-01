using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject
{
    public Vector2 position;
    public Vector2 direction;
    public float ChanceToChange;

    // Start is called before the first frame update
    public WalkerObject(Vector2 pos, Vector2 dir, float chanceToChange)
    {
        position = pos;
        direction = dir;
        ChanceToChange = chanceToChange;
    }
}
