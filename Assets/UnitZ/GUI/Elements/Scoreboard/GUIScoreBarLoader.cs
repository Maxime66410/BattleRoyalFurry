﻿//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class GUIScoreBarLoader : MonoBehaviour
{

	public RectTransform Canvas;
	public RectTransform GUIScorePrefab;
	public RectTransform GUITeamPrefab;

	
	void Start ()
	{

	}
	
	void OnEnable ()
	{
		DrawScoreboard ();
	}
	
	void ClearCanvas ()
	{
		if (Canvas == null)
			return;

		foreach (Transform child in Canvas.transform) {
			GameObject.Destroy (child.gameObject);
		}
	}

	public void DrawScoreboard ()
	{
		// just draw a GUIScorePrefab to canvas.
		if (UnitZ.NetworkGameplay == null || UnitZ.NetworkGameplay.playersManager == null || Canvas == null || GUIScorePrefab == null)
			return;

		PlayersManager playersManager = UnitZ.NetworkGameplay.playersManager;

		if (playersManager.PlayerList != null) {	
			ClearCanvas ();
			int i = 0;
			// draw team first.
			foreach (string tm in playersManager.TeamsList) {
				if (tm != "") {
					GameObject team = (GameObject)GameObject.Instantiate (GUITeamPrefab.gameObject, Vector3.zero, Quaternion.identity);
					team.transform.SetParent (Canvas.transform);
					GUITeamBar teammbar = team.GetComponent<GUITeamBar> ();
					RectTransform teamtransform = team.GetComponent<RectTransform> ();
					if (teamtransform) {
						teamtransform.localScale = GUITeamPrefab.gameObject.transform.localScale;
						teamtransform.anchoredPosition = new Vector2 (0, -((GUITeamPrefab.sizeDelta.y * i)));
					}
					if (teammbar) {
						teammbar.TeamName.text = tm;	
					}
					i++;
				}
				// draw players inside.
				foreach (PlayerData player in playersManager.PlayerList) {
					if (tm == player.Team) {
						GameObject obj = (GameObject)GameObject.Instantiate (GUIScorePrefab.gameObject, Vector3.zero, Quaternion.identity);
						obj.transform.SetParent (Canvas.transform);
						GUIScoreBar scorebar = obj.GetComponent<GUIScoreBar> ();
				
						RectTransform scoretransform = obj.GetComponent<RectTransform> ();
						if (scoretransform) {
							scoretransform.localScale = GUIScorePrefab.gameObject.transform.localScale;
							scoretransform.anchoredPosition = new Vector2 (0, -((GUIScorePrefab.sizeDelta.y * i)));
						}
						if (scorebar) {
							scorebar.Player = player;
						}
						i++;	
					}
				}
			}
			Canvas.sizeDelta = new Vector2 (Canvas.sizeDelta.x, GUIScorePrefab.sizeDelta.y * i);
		}
		
	}

	void Update ()
	{
	
	}
}
