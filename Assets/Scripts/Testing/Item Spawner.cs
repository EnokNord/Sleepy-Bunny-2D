using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float timerLength;
    float timer;
    public GameObject PrefabToSpawn;

    private void Start()
    {
        timer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timerLength)
        {
            Instantiate(PrefabToSpawn, transform);
            timer = 0f;
        }
    }
}
