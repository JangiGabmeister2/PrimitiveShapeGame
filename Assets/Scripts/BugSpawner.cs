using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject cake;

    [Space(10)] public GameObject[] bugs;
    public GameObject gameArea;
    public float spawnRadius = 50f;

    [Space(10)] public int bugsPerFrame;
    public int bugCount;
    public int maxBugsToSpawn = 30;
    public int maxSpawnIncrease = 20;

    [Space(10)] public float newBugSpeed = 10;

    private List<GameObject> bugsInGame = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(nameof(Spawn));
    }

    private IEnumerator Spawn()
    {
        while (bugCount < maxBugsToSpawn)
        {
            for (int i = 0; i < bugsPerFrame; i++)
            {
                Vector3 position = GetRandomPosition();
                bugsInGame.Add(SpawnBug(position));

                yield return new WaitForSeconds(Random.Range(0f, 0.4f));
            }
        }
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
