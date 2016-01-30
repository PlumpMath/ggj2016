﻿using UnityEngine;
using System.Collections.Generic;
using Common.Signal;
using Game;

public class MonsterManager : MonoBehaviour
{
	[SerializeField]
	private Monster[] Monsters;

	private int CurrentMonsterIndex = -1;
	private Monster CurrentMonster;

	[SerializeField]
	private float TowerTopY;
	[SerializeField]
	private float TowerBottomY;

	void Awake()
	{
		// subscribe to signals
		GameSignals.START_GAME.AddListener (OnStartGame);
		GameSignals.ON_BUBBLE_POPPED.AddListener (OnBubblePopped);
		GameSignals.ON_BUBBLE_MISSED.AddListener (OnBubbleMissed);
	}

	void OnDestroy()
	{
		// subscribe to signals
		GameSignals.START_GAME.RemoveListener (OnStartGame);
		GameSignals.ON_BUBBLE_POPPED.RemoveListener (OnBubblePopped);
		GameSignals.ON_BUBBLE_MISSED.RemoveListener (OnBubbleMissed);
	}

	// TEMP FOR TEST
	void Update()
	{
		if (Input.GetKeyUp (KeyCode.W)) {
			GameSignals.ON_BUBBLE_MISSED.Dispatch ();
		}

		if (Input.GetKeyUp (KeyCode.S)) {
			GameSignals.ON_BUBBLE_POPPED.Dispatch ();
		}

		if (Input.GetKeyUp (KeyCode.I)) {
			GameSignals.START_GAME.Dispatch ();
		}
	}

	#region Event listener
	private void OnStartGame(ISignalParameters @params)
	{
		Debug.Log ("OnStartGame");
		// hide current monster if applicable
		HideCurrentMonster ();

		// reset monster index
		CurrentMonsterIndex = -1;

		// show next monster
		ShowNextMonster ();
	}

	private void OnBubbleMissed(ISignalParameters @params)
	{
		// raise monster
		CurrentMonster.Rise ();

		// check if monster has reached character
		bool isMonsterOnTop = CurrentMonster.YPosition >= TowerTopY;
		if (isMonsterOnTop) {
			// dispatch end game
			GameSignals.END_GAME.Dispatch ();
		}
	}

	private void OnBubblePopped(ISignalParameters @params)
	{
		// lower monster
		CurrentMonster.Lower ();

		// check if monster has been defeated
		bool monsterDefeated = CurrentMonster.YPosition < TowerBottomY;
		Debug.Log ("monsterDefeated: " + monsterDefeated);
		if (monsterDefeated) {
			// kill current monster
			HideCurrentMonster();

			// dispatch monster dead
			GameSignals.ON_MONSTER_DEAD.Dispatch();

			// replace with new monster
			ShowNextMonster();
		}
	}
	#endregion

	private void ShowNextMonster()
	{
		// incremenet monster index
		CurrentMonsterIndex = (CurrentMonsterIndex == Monsters.Length - 1) ? 0 : ++CurrentMonsterIndex;

		// update current monster
		CurrentMonster = Monsters[CurrentMonsterIndex];

		// set monster speed
		float towerHeight = TowerTopY - TowerBottomY;
		int heightInBubbles = CurrentMonster.BubblesToReachTop + CurrentMonster.BubblesToReachBottom;
		float monsterSpeed = towerHeight / ((float)heightInBubbles);
		CurrentMonster.SetSpeed (monsterSpeed);

		// show monster
		Vector3 initialPosition = Vector3.zero;
		initialPosition.y = TowerBottomY + (monsterSpeed * CurrentMonster.BubblesToReachBottom);
		CurrentMonster.Show (initialPosition);
	}

	private void HideCurrentMonster()
	{
		if (CurrentMonster == null)
			return;
		
		// hide monster
		CurrentMonster.Hide ();
	}
}