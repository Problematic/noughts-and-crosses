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
public class SquareEvent : UnityEvent<Square> {}

[System.Serializable]
public class PlayerEvent : UnityEvent<Player> {}
