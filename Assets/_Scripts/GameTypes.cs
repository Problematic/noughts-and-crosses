using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TinyMessenger;

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

public class GameOverMessage : ITinyMessage {
    public object Sender { get; set; }
	public Player Winner { get; private set; }

	public GameOverMessage (object sender, Player winner) {
		Sender = sender;
		Winner = winner;
	}
}

public class ResetMessage : TinyMessageBase
{
    public ResetMessage(object sender) : base(sender) {}
}
