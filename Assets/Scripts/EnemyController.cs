using UnityEngine;

public class EnemyController : MonoBehaviour {
    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        MoveToPlayer();
    }

    private void MoveToPlayer() {

        if (player == null)
            return;

        transform.position = Vector3.Lerp(transform.position, player.transform.position, Random.Range(0.3f, 1f) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("Player")) {
            collision.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
}
