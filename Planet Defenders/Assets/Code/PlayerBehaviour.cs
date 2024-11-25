using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public int ammo;
    public GameObject bulletPrefab;
    float zRange = 57.5f;
    int collisions;
    GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
       gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive )
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && ammo > 0)
            {
                Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
                ammo -= 1;
                gameManager.UpdateAmmo(ammo);
            }
        }
    }

    private void LateUpdate()
    {
        if (transform.position.z >  zRange || transform.position.z < -zRange) 
        {
            Vector3 pos = transform.position;
            pos.z = Mathf.Clamp(pos.z, zRange, -zRange);
            transform.position = pos;
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy Bullet"))
        {
            if (gameManager.isGameActive) 
            {
                gameManager.UpdateScore();

                collisions += 1;
            }
            Destroy(other.gameObject);
        }
       
        if (collisions == 3)
        {
            ammo += 2;
            gameManager.UpdateAmmo(ammo);
            collisions = 0;
        }
    }
}
