using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Player {
	None,
	X,
	O,
}

[System.Serializable]
public class GameState {
	public Player currentPlayer = Player.X;
	public Player winner;
	public int claimedSquares;

	public void NextPlayer () {
		currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
	}

	public void Reset () {
		currentPlayer = Player.X;
		winner = Player.None;
		claimedSquares = 0;
	}
}

[System.Serializable]
public class SquareEvent : UnityEvent<Square> {}

[System.Serializable]
public class PlayerEvent : UnityEvent<Player> {}
