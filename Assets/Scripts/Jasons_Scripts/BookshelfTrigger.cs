using UnityEngine;
using System.Collections;
using System;

public class BookshelfTrigger : MonoBehaviour {

	private GameObject[] books;
	private bool[] booksOnShelf = new bool[4]{false, false, false, false};
	private bool triggerComplete = false;
	private GameObject book1, book2, book3, book4;

	void Start() {
		books = GameObject.FindGameObjectsWithTag ("Book");
		GameObject book1 = books [0];
		GameObject book2 = books [1];
		GameObject book3 = books [2];
		GameObject book4 = books [3];
	}

	void OnTriggerEnter(Collider other) {
		GameObject go = other.gameObject;

		if (go.tag != "Book") {
			return;
		}

		int i = 0;
		while (go.name != books [i].name && i<books.Length) {
			i++;
		}
		booksOnShelf[i] = true;
	}

	void OnTriggerStay(Collider other) {
		if (!triggerComplete && booksOnShelf [0] && booksOnShelf [1] && booksOnShelf [2] && booksOnShelf [3]) {
			triggerComplete = true;
			GameObject audio = GameObject.Find ("Books Placed Voice Over");
			audio.GetComponent<AudioSource>().Play ();
		}
	}

	void OnTriggerLeave(Collider other) {
		GameObject go = other.gameObject;

		if (go.tag != "Book") {
			return;
		}

		int i = 0;
		while (go.name != books [i].name && i<books.Length) {
			i++;
		}
		booksOnShelf[i] = false;
	}
}
