using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryToast : MonoBehaviour {
	public Animator animator;
	public Text content;

	void OnEnable () {
		GameController.MessageHub.Subscribe<GameOverMessage>(Toast);
	}

	public void Toast (GameOverMessage message) {
		content.text = message.Winner == Player.None ? "Draw!" : string.Format("{0} wins!", message.Winner);
		animator.SetTrigger("doToast");
	}

	public void Reset () {
		animator.SetTrigger("doReset");
		GameController.MessageHub.Publish(new ResetMessage(this));
	}
}
