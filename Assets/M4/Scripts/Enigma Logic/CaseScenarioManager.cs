using UnityEngine;
using System.Collections;

public class CaseScenarioManager : MonoBehaviour
{
    CaseScenarioPart[] parts;
    int activePartIndex = 0;

    // Use this for initialization
    
    void Awake()
    {
        EventManager.instance.AddListener<RequestNextQuestionEvent>(show);
    }
    private void OnDestroy()
    {
        EventManager.instance.RemoveListener<RequestNextQuestionEvent>(show);
    }
    void Start()
    {
        parts = GameObject.Find("Parts").GetComponentsInChildren<CaseScenarioPart>();
        foreach(CaseScenarioPart part in parts)
        {
            part.init();
            if(part.id != 0)
            {
                part.gameObject.SetActive(false);
            }

        }
        foreach (CaseScenarioPart part in parts)
        {
            part.locked();

        }
    }
    void show(RequestNextQuestionEvent e)
    {
        parts[e.choiceId].gameObject.SetActive(true);
        if(activePartIndex != e.choiceId)
        {
            parts[activePartIndex].gameObject.SetActive(false);
        }
        activePartIndex = e.choiceId;
    }
}
