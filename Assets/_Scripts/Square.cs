using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour, IPointerClickHandler {
	[SerializeField]
	Text content;

	Player owner;
	public Player Owner {
		get {
			return owner;
		}
		set {
			owner = value;
			content.text = owner == Player.None ? string.Empty : owner.ToString();
		}
	}

	public bool IsUnclaimed {
		get {
			return owner == Player.None;
		}
	}

	public SquareEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
		onClick.Invoke(this);
    }
}
