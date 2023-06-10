using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    public GameObject player;
    public GameObject platformPrefab;
    public GameObject springPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.name == "Player")
        if (collision.gameObject.name.StartsWith("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.name.StartsWith("Platform"))
        {
            if (Random.Range(1, 7) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(springPrefab, new Vector2(Random.Range(-1.7f, 1.7f), transform.position.y + 10f + Random.Range(-0.3f, 0.3f)), Quaternion.identity);
            }
            else
            {
                collision.gameObject.transform.position = new Vector2(Random.Range(-1.7f, 1.7f), transform.position.y + 10f + Random.Range(-0.3f, 0.3f));
            }
        }
        else if (collision.gameObject.name.StartsWith("Spring"))
        {
            if (Random.Range(1, 7) == 1)
            {
                collision.gameObject.transform.position = new Vector2(Random.Range(-1.7f, 1.7f), transform.position.y + 10f + Random.Range(-0.3f, 0.3f));
            }
            else
            {
                Destroy(collision.gameObject);
                Instantiate(platformPrefab, new Vector2(Random.Range(-1.7f, 1.7f), transform.position.y + 10f + Random.Range(-0.3f, 0.3f)), Quaternion.identity);
            }
        }   
    }
}
