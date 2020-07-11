using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MobSpawner : MonoBehaviour
{
    public float spawnCounter = 2f;
    public float spawnInterval = 5f;
    public Transform mobPrefab;
    public Transform spawnPoint;
    private int waveIndex = 0;
    public Slider spawnTimerBar;

    public Text spawnTimerText;

    private GameMaster gameMaster;
    private void Start()
    {
        gameMaster = GameMaster.instance;
    }
    // Update is called once per frame
    void Update()
    {
        
        if(spawnCounter <= 0)
        {
            waveIndex++;
            StartCoroutine(SpawnWave());
            spawnCounter = spawnInterval;
            gameMaster.survivedRounds++;
        }
        spawnCounter -= Time.deltaTime;
        spawnCounter = Mathf.Clamp(spawnCounter, 0, Mathf.Infinity);
        spawnTimerText.text = string.Format("{0:00.00}",spawnCounter);
        spawnTimerBar.value = spawnCounter / spawnInterval;
        
    }
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnMob();
            yield return new WaitForSeconds(0.3f);
        }
    }
    void SpawnMob()
    {
        Instantiate(mobPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
