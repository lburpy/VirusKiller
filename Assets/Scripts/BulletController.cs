using TMPro;
using UnityEngine;

public class BulletController : MonoBehaviour {

    GameObject gameManager;

    private void Start() {
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.name.Equals("Player") && !collision.name.Equals("Walls")) {
            if (collision.name.Contains("Enemy")) {
                gameManager.GetComponent<GameManager>().Score += 10;
                gameManager.GetComponent<GameManager>().scoreText.text = "Point : " + gameManager.GetComponent<GameManager>().Score;
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
