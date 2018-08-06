using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnigmaSequence : MonoBehaviour {

    public int sceneToLoadIndex = 0; //build index
    public bool sequenceComplete = false;
    public int solvedEnigmas = 0;
    public int enigmasInSequence = 3;
    
    //Enigma Selection
    //key: enigmaId, value: scene buildIndex
    public Dictionary<int, int> enigmaScenes;
    public List<EnigmaStatus> statusList;
    public SuccessStatus successStatus;
    List<int> selectedEnigmas;
    public System.Random randomIndex;

    //FILTERS
    public List<Enigme_Data> m_enigmaDatas;
    public List<Enigme_Data> selectedEnigmaDatas;
    public enum Filter { NONE, TYPE, TIME, DIFFICULTY };
    public Filter filter = Filter.NONE;
    public float time = 5;
    public EnigmaType type = EnigmaType.INPUT;
    public EnigmaDifficulty difficulty = EnigmaDifficulty.Easy;

    public void loadEnigmaDatas()
    {
        DataControl.control.LoadEnigmaDatas();
        m_enigmaDatas = DataControl.control.enigmaDatas;
    }
      
    public void updateSceneToLoad() {
        DataControl.control.sceneToLoad = sceneToLoadIndex;
    }

    public int EnigmaSceneFromList()
    {
        int sceneId = 0;
        sceneId = DrawEnigma();
        return sceneId;
    }
  
    public List<int> enigmasbyStatus(SuccessStatus status)
    {
        List<EnigmaStatus>  statuses = statusList.FindAll(enigmaStatus => enigmaStatus.successStatus == status);
        List<int> enigmasId = new List<int>();
        foreach(EnigmaStatus eStatus in statuses)
        {
            enigmasId.Add(eStatus.enigmaId);
        }
        return enigmasId;
    }

    public int DrawEnigma()
    {
        int index = randomIndex.Next(selectedEnigmas.Count);
        return selectedEnigmas[index];
    }

    public bool selectEnigmas()
    {
        selectedEnigmas = enigmasbyStatus(SuccessStatus.NOT_TRIED);
        if (selectedEnigmas.Count == 0)
        {
            selectedEnigmas = enigmasbyStatus(SuccessStatus.FAILED);
            if (selectedEnigmas.Count == 0) return false;
        }
        return true;
    }

    public void selectScene()
    {
        if (selectEnigmas())
        {
            int enigmaId = DrawEnigma();
            sceneToLoadIndex = enigmaScenes[enigmaId];
        }
        else print("No enigmas left to play");
    }

/***********************************************************************/
    public void FilterEnigmasExclusive()
    {
        switch (filter)
        {
            case Filter.NONE:
                resetEnigmaFilters();
                break;
            case Filter.TIME:
                SelectEnigmasByTime(m_enigmaDatas);
                break;
            case Filter.DIFFICULTY:
                SelectEnigmasByDifficulty(m_enigmaDatas);
                break;
            case Filter.TYPE:
                SelectEnigmasByType(m_enigmaDatas);
                break;
            default:
                print("no valid filter selected, resetting filters");
                resetEnigmaFilters();
                break;
        }
    }
//FILTRES
    public void SelectEnigmasByTime(List<Enigme_Data> enigmaDatas)
    {
        selectedEnigmaDatas = enigmaDatas.FindAll(enigmeData => enigmeData.time <= time);
    }

    public void SelectEnigmasByType(List<Enigme_Data> enigmaDatas)
    {
        selectedEnigmaDatas = enigmaDatas.FindAll(enigmeData => enigmeData.enigmaType == type);
    }

    public void SelectEnigmasByDifficulty(List<Enigme_Data> enigmaDatas)
    {
        selectedEnigmaDatas = enigmaDatas.FindAll(enigmeData => enigmeData.enigmaDifficulty == difficulty);
    }

    public void resetEnigmaFilters()
    {
        selectedEnigmaDatas = m_enigmaDatas;
    }

    public List<int> SelectedEnigmaDatasId()
    {
        List<int> enigmasId = new List<int>();
        foreach (Enigme_Data ed in selectedEnigmaDatas)
        {
            enigmasId.Add(ed.enigmaId);
        }
        return enigmasId;
    }
    //***************************************************************************//
}
