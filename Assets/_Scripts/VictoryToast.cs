using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryToast : MonoBehaviour {
	public Animator animator;
	public Text content;

	public UnityEvent onReset;

	public void Toast (Player winner) {
		content.text = winner == Player.None ? "Draw!" : string.Format("{0} wins!", winner);
		animator.SetTrigger("doToast");
	}

	public void Reset () {
		animator.SetTrigger("doReset");
		onReset.Invoke();
	}
}
