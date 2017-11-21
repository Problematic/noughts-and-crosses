using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	public Player currentPlayer = Player.X;
	public Player winner;
	public Board board;
	int claimedSquares = 0;

	public PlayerEvent onVictory;

	readonly int[][] victoryChecks = new int[][] {
		new int[] { 0, 1, 2 },
		new int[] { 3, 4, 5 },
		new int[] { 6, 7, 8 },
		new int[] { 0, 3, 6 },
		new int[] { 1, 4, 7 },
		new int[] { 2, 5, 8 },
		new int[] { 0, 4, 8 },
		new int[] { 6, 4, 2 },
	};

	public void HandleSquareClick (Square square) {
		if (winner != Player.None) {
			return;
		}

		if (square.IsUnclaimed) {
			square.Owner = currentPlayer;
			currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;

			winner = DetermineWinner();
			claimedSquares++;

			if (claimedSquares == 9 || winner != Player.None) {
				onVictory.Invoke(winner);
			}
		}
	}

	public void OnResetRequested () {
		currentPlayer = Player.X;
		winner = Player.None;
		claimedSquares = 0;

		for (int i = 0; i < 9; i++) {
			board[i].Owner = Player.None;
		}
	}

	Player DetermineWinner () {
		var players = new HashSet<Player>();

		foreach (var check in victoryChecks) {
			players.Clear();

			foreach (var idx in check) {
				players.Add(board[idx].Owner);
			}

			if (players.Count == 1 && !players.Contains(Player.None)) {
				return players.Contains(Player.X) ? Player.X : Player.O;
			}
		}

		return Player.None;
	}
}
