using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public FlappyController flappy;
    public Pipe pipe;
    public float pipeSpeed;
    public Highscores highscores;
}
