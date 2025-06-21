using UnityEngine;

public class BossEnemyController : MonoBehaviour {
    GameObject player;
    GameObject gameManager;
    public int maxHealth = 50;
    private int currentHealth;
    public int damage = 20;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager");
        currentHealth = maxHealth;
    }

    void Update() {
        MoveToPlayer();
    }

    private void MoveToPlayer() {
        if (player == null)
            return;
        // Boss moves slightly faster towards the player
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Random.Range(0.5f, 1.2f) * Time.deltaTime);
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            if (gameManager != null) {
                GameManager gm = gameManager.GetComponent<GameManager>();
                gm.Score += 100;
                gm.scoreText.text = "Point : " + gm.Score;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("Player")) {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
