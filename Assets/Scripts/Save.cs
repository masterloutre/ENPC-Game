using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Save{

    //ScoreEnigme[] scores;
    public List<ScoreEnigme> scores;

    public Save()
    {
        scores = new List<ScoreEnigme>();
    }
    public List<ScoreEnigme> Scores()
    {
        return scores;
    }

    public void setScores(List<ScoreEnigme> newScores)
    {
        scores = new List<ScoreEnigme>();
        scores = newScores;
    }
}
