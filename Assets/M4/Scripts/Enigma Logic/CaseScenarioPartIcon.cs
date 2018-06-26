using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseScenarioPartIcon {
	private GameObject prefab;
	private CaseScenarioPart scenarioPart;
	// Use this for initialization

	public CaseScenarioPartIcon(CaseScenarioPart _scenarioPart, GameObject _prefab){
		scenarioPart = _scenarioPart;
		prefab = _prefab;
	}
}
