using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	public Square squarePrefab;
	public Square[] squares;

	public SquareEvent onSquareClick;

	public Square this[int idx] {
		get {
			return squares[idx];
		}
	}

	void OnEnable () {
		foreach (var square in squares) {
			square.onClick.AddListener(onSquareClick.Invoke);
		}
	}

	void OnDisable () {
		foreach (var square in squares) {
			square.onClick.RemoveListener(onSquareClick.Invoke);
		}
	}

	void Start () {
		int squareCount = 9;
		squares = new Square[squareCount];

		for (int i = 0; i < squareCount; i++) {
			var square = Instantiate<Square>(squarePrefab);
			square.name = string.Format("Square {0}", i);
			square.transform.SetParent(transform, false);
			square.onClick.AddListener(onSquareClick.Invoke);

			squares[i] = square;
		}
	}

	void HandleSquareClick(Square square) {
		Debug.Log(square, this);
	}
}
