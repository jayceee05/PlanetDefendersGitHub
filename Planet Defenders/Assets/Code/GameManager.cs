using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text ammoText;
    public Text levelText;
    public GameObject gameoverScreen;
    public List<GameObject> enemyShips; 
    int score;
    int lives;
    float zPos;
    float zSpawnRange = 49.4f;
    int level;
    int enemiesToSpawn;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        score = 0; lives = 5; level = 1;
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
        livesText.text = "Lives: " + lives;
        ammoText.text = "Ammo: 0";
        isGameActive = true;
        SpawnWave(level);
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<EnemyBehaviour>().Length;
        if (enemyCount == 0)
        {
            UpdateLevel();
            SpawnWave(level);
        }

        if (lives == 0)
        {
            gameoverScreen.SetActive(true);
            isGameActive = false;
        }
    }

    public void UpdateScore()
    {
        score += 5;
        scoreText.text = "Score: " + score;
    }

    public void UpdateAmmo(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }

    public void UpdateLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives;
    }

    public void UpdateLevel()
    {
        level++;
        levelText.text = "Level: " + level;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SpawnWave(int level)
    {
        enemiesToSpawn = (level + 1) / 2; 
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyShips[0], new Vector3(-45, 30, GenerateRandomZPos()), enemyShips[0].transform.rotation);
        }

        enemiesToSpawn = level / 2;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyShips[1], new Vector3(-45, 45, GenerateRandomZPos()), enemyShips[1].transform.rotation);
        }
    }

    float GenerateRandomZPos()
    {
        zPos = Random.Range(-zSpawnRange, zSpawnRange);
        
        return zPos;
    }
}
