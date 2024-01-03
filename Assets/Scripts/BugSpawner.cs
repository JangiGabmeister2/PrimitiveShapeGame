using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugSpawner : MonoBehaviour
{
    public GameObject cake;
    public Text waveText;

    [Space(10)] public GameObject[] bugs;
    public GameObject gameArea;
    public float spawnRadius = 50f;

    [Space(10)] public int bugsPerFrame;
    public int bugCount;
    public int maxBugsToSpawn = 30;
    public int maxSpawnIncrease = 50;
    public int maxSpawnRateIncrease = 10;

    [Space(10)] public float newBugSpeed = 10;

    private int wave = 1;
    private List<GameObject> _bugsInGame = new List<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(nameof(Spawn));
    }

    private void Update()
    {
        foreach (var bug in _bugsInGame)
        {
            if (bug != null)
            {
                if (Vector3.Distance(bug.transform.position, gameArea.transform.position) > spawnRadius + 1
                    || GameHandler.instance.CurrentState == GameStates.Menu
                    || GameHandler.instance.CurrentState == GameStates.End)
                {
                    _bugsInGame.Remove(bug);
                    Destroy(bug.gameObject);
                }
            }
        }
    }

    private IEnumerator Spawn()
    {
        if (GameHandler.instance.CurrentState == GameStates.Game)
        {
            while (bugCount < maxBugsToSpawn)
            {
                waveText.text = $"Wave {wave}";

                for (int i = 0; i < bugsPerFrame; i++)
                {
                    Vector3 position = GetRandomPosition();
                    _bugsInGame.Add(SpawnBug(position));

                    yield return new WaitForSeconds(Random.Range(0f, 0.4f));
                }

                if (bugCount >= maxBugsToSpawn)
                {
                    while (bugCount > 5)
                    {
                    }

                    waveText.text = "Intermission";
                    maxBugsToSpawn += maxSpawnIncrease;
                    bugsPerFrame += maxSpawnRateIncrease;
                    bugCount = 0;

                    yield return new WaitForSeconds(30f);

                    wave++;
                }
            }
        }

        if (GameHandler.instance.CurrentState == GameStates.End)
        {
            wave = 1;
            bugCount = 0;
        }

        yield return null;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 pos = Random.insideUnitCircle.normalized;

        pos.z = pos.y;
        pos.y = 0;
        pos *= spawnRadius;
        pos += gameArea.transform.position;

        return pos;
    }

    private GameObject SpawnBug(Vector3 position)
    {
        bugCount++;

        int i = Random.Range(0, bugs.Length);

        GameObject newBug = Instantiate(
            bugs[i], //the type of bug to spawn
            position, //where it will spawn
            Quaternion.FromToRotation(
                bugs[i].transform.forward,
                gameArea.transform.position - position), //make it face the center of game area
            transform); //parent the bug to this transform

        newBug.transform.LookAt(gameArea.transform.position);
        newBug.GetComponent<BugBehaviour>().moveSpeed += newBugSpeed;

        return newBug;
    }
}
