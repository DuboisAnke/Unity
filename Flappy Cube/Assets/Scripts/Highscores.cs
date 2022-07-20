using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Highscore", menuName = "ScriptableObjects/Highscore", order = 2)]
[System.Serializable]
public class Highscores : ScriptableObject
{
    public List<HighscoreEntry> entries = new List<HighscoreEntry>();

    [System.Serializable]
    public class HighscoreEntry
    {
        public string playerName;
        public int playerScore;

    }

    public bool IsPlayerNotANoob(int score)
    {
        bool newHighscore = false;
        foreach (var entry in entries)
        {

            if (score <= entry.playerScore)
            {
                newHighscore = false;
                return newHighscore;
            }
            else if (score > entry.playerScore)
            {
                newHighscore = true;
                return newHighscore;
            }
        }
        return newHighscore;

    }

}
