using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TinyMessenger;

public class GameController : MonoBehaviour {
	public Board board;
	public GameState state;

	static TinyMessengerHub messageHub;
	public static TinyMessengerHub MessageHub {
		get {
			if (messageHub == null) {
				messageHub = new TinyMessengerHub();
			}

			return messageHub;
		}
	}

	TinyMessageSubscriptionToken resetToken;

	void OnEnable () {
		resetToken = MessageHub.Subscribe<ResetMessage>(OnResetRequested);
	}

	void OnDisable () {
		MessageHub.Unsubscribe(resetToken);
	}

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
		if (state.winner != Player.None) {
			return;
		}

		if (square.IsUnclaimed) {
			square.Owner = state.currentPlayer;
			state.claimedSquares++;

			state.NextPlayer();
			state.winner = DetermineWinner();

			if (state.claimedSquares == 9 || state.winner != Player.None) {
				MessageHub.Publish(new GameOverMessage(this, state.winner));
			}
		}
	}

	public void OnResetRequested (ResetMessage message) {
		state.Reset();
		board.Reset();
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
