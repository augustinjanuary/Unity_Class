using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SOscore : ScriptableObject
{
    public int value = 0;
    [Range(0, 100)]public int health = 100;
    public int oddsOfDrop = 20;
}
