using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameObject scammer;
    
    public Transform player;
    public float spawnRadius = 2f;
    public float frontSpawnChance = 0.1f; 
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    public void EndGame()
    {
        Debug.Log("Game Ended");
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
    }

    public void SpawnScammer()
    {
        if (scammer == null || player == null)
        {
            Debug.LogWarning("Scammer or Player not assigned!");
            return;
        }

        float angle;
        
        if (Random.value < frontSpawnChance)
        {
            
            angle = Random.Range(-45f, 45f);
        }
        else
        {
            if (Random.value < 0.5f)
            {
                angle = Random.Range(45f, 180f);
            }
            else
            {
                angle = Random.Range(-180f, -45f);
            }
        }
        
        Vector3 direction = Quaternion.Euler(0, angle, 0) * player.forward;
        Vector3 spawnPos = player.position + direction.normalized * spawnRadius + new Vector3(0, Random.Range(2f, 3f), 0);
        Instantiate(scammer, spawnPos, Quaternion.identity);
    }
}
