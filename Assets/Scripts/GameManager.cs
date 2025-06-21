using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject Player;
    public GameObject Enemy;
    public GameObject BonusHP;

    // Enemy Spawn Variables
    public int enemiesPerWave = 10;
    public float timeBetweenWaves = 5f;
    private int currentWave = 0;

    // HP Spawn Variables
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 15f;

    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI pointText;

    public int Score;

    void Start() {
        Time.timeScale = 1f;
        StartCoroutine(WaveLoop());
        StartCoroutine(RandomHealthSpawnLoop());
    }

    IEnumerator WaveLoop() {
        while (true) {
            yield return new WaitForSeconds(timeBetweenWaves);
            StartNewWave();
        }
    }

    IEnumerator RandomHealthSpawnLoop() {
        while (true) {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            RandomHPSpawner();
        }
    }

    void RandomEnemySpawner() {
        if (Player == null) return;
        Vector3 camMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 camMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float spawnBuffer = Random.Range(0.5f, 3f);

        Vector3 spawnPos = Vector3.zero;
        int side = Random.Range(0, 4); // 0=üst, 1=alt, 2=sað, 3=sol

        switch (side) {
            case 0: // ÜST
                spawnPos = new Vector3(
                    Random.Range(camMin.x, camMax.x),
                    camMax.y + spawnBuffer,
                    0);
                break;
            case 1: // ALT
                spawnPos = new Vector3(
                    Random.Range(camMin.x, camMax.x),
                    camMin.y - spawnBuffer,
                    0);
                break;
            case 2: // SAÐ
                spawnPos = new Vector3(
                    camMax.x + spawnBuffer,
                    Random.Range(camMin.y, camMax.y),
                    0);
                break;
            case 3: // SOL
                spawnPos = new Vector3(
                    camMin.x - spawnBuffer,
                    Random.Range(camMin.y, camMax.y),
                    0);
                break;
        }

        Instantiate(Enemy, spawnPos, Quaternion.identity);
    }

    void StartNewWave() {
        currentWave++;
        int totalEnemies = enemiesPerWave + currentWave * 2;

        Debug.Log($"* Dalga {currentWave} baþlýyor! ({totalEnemies} düþman)");
        waveText.text = "Wave " + currentWave;
        StartCoroutine(SpawnEnemiesWithDelay(totalEnemies));
    }

    IEnumerator SpawnEnemiesWithDelay(int count) {
        for (int i = 0; i < count; i++) {
            RandomEnemySpawner();
            yield return new WaitForSeconds(0.3f);
        }
    }

    void RandomHPSpawner() {
        Instantiate(BonusHP, Random.insideUnitCircle * 10, Quaternion.identity);
    }

    public void RetryButton() {
        SceneManager.LoadScene(0);
    }
}
