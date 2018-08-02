using UnityEngine;
using System.Collections;
using System;

public class CaseScenarioManager : MonoBehaviour
{
    CaseScenarioPart[] partList;
    int activePartIndex = 0;

    // Use this for initialization

    void Awake()
    {
      partList = GameObject.Find("Parts").GetComponentsInChildren<CaseScenarioPart>(true);
      EventManager.instance.AddListener<RequestUnlockNextPartsEvent>(unsealParts);
      EventManager.instance.AddListener<RequestShowPartEvent>(showPart);

    }
    private void OnDestroy()
    {
      EventManager.instance.RemoveListener<RequestUnlockNextPartsEvent>(unsealParts);
      EventManager.instance.RemoveListener<RequestShowPartEvent>(showPart);
    }
    void Start()
    {
        foreach(CaseScenarioPart part in partList)
        {
          part.createIcon();
          part.seal();
        }
      writeQuestionNumbers();
      setActiveUntilConditionalFrom(0);
      try{
        partList[activePartIndex].show();
      } catch (IndexOutOfRangeException) {
        Debug.Log("Caught IndexOutOfRangeException : Il n'y a pas de scenarioPart");
      }

    }

    void unsealParts(RequestUnlockNextPartsEvent e)
    {
      if(e.currentPartId >= partList.Length){
        return;
      }
      setActiveUntilConditionalFrom(e.currentPartId + 1);
    }

    void setActiveUntilConditionalFrom(int startId){
      for(int i = startId; i<partList.Length; i++){
        partList[i].unseal();
        if(partList[i].gameObject.GetComponent<ChoiceQuestionTimerConditional>() != null){
          break;
        }
      }
    }

    void showPart(RequestShowPartEvent e){
      partList[activePartIndex].hide();
      activePartIndex = e.partId;
      partList[activePartIndex].show();
    }

    void writeQuestionNumbers(){
      int count = 1;
      foreach(CaseScenarioPart part in partList){
        if(part.gameObject.GetComponent<ChoiceQuestion>() != null){
          part.writeInIcon(" " + count.ToString());
          count ++;
        }
      }
    }


}
