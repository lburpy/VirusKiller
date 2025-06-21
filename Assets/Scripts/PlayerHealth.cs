using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    GameObject gameManager;

    public int maxHealth = 100;
    private int currentHealth;
    public GameObject failPanel;

    void Start() {
        gameManager = GameObject.Find("GameManager");
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount) {
        if (gameObject.GetComponent<PlayerMovement>().isDashing)
            return;

        currentHealth -= damageAmount;
        gameManager.GetComponent<GameManager>().playerHPText.text = "Player HP : " + currentHealth;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void Heal(int healAmount) {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        gameManager.GetComponent<GameManager>().playerHPText.text = "Player HP : " + currentHealth;
    }

    void Die() {
        Debug.Log("Oyuncu Öldü");
        Time.timeScale = 0;
        gameManager.GetComponent<GameManager>().pointText.text = "Total Point : " + gameManager.GetComponent<GameManager>().Score;
        failPanel.SetActive(true);
    }

}
