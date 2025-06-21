using UnityEngine;

public class BonusHPController : MonoBehaviour
{
    public int healAmount = 10;

    public float hpMinLifeTime = 2f;
    public float hpMaxLifeTime = 5f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("Player")) {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null) {
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate() {
        Destroy(gameObject,Random.Range(hpMinLifeTime,hpMaxLifeTime));
    }
}
