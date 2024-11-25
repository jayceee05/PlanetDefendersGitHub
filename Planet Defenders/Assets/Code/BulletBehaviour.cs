using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int bulletType;
    float bulletSpeed;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        if (bulletType == 0)
        {
            bulletSpeed = 7.5f;
            direction = Vector3.down;
        }
        
        else if (bulletType == 1)
        {
            bulletSpeed = 4.5f;
            direction = Vector3.down;
        }

        else if (bulletType == 2)
        {
            bulletSpeed = 55;
            direction = Vector3.down;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);

        if (transform.position.y > 60 && bulletType == 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bulletType != 2)
        {
            if (other.CompareTag("Player Bullet"))
            {
                Destroy(gameObject);
            }
        }
    }
}
