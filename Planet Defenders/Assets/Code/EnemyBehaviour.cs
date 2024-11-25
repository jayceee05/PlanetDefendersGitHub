using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    GameManager gameManager;
    public int enemyType;
    float shootRate;
    float enemyZPos;
    public float speed;

    float zRange = 49.4f;
    bool isOnRight = false;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (enemyType == 0)
        {
            shootRate = Random.Range(3, 8.5f);
        }
        else if (enemyType == 1)
        {
            shootRate = Random.Range(11, 16.5f);
        }
        enemyZPos = transform.position.z;

        StartCoroutine(ShootProtocol());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == 0)
        {
            MoveEnemy();
        }
    }
    
    private void MoveEnemy()
    {
        switch (isOnRight)
        {
            case false:
                if (transform.position.z < zRange)
                {
                    enemyZPos += speed;
                }
                else
                {
                    isOnRight = true;
                }
                break;
            case true:
                if (transform.position.z > -zRange)
                {
                    enemyZPos -= speed;
                }
                else
                {
                    isOnRight = false;
                }
                break;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, enemyZPos);
    }
    
    IEnumerator ShootProtocol()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(shootRate);
            Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    
}
