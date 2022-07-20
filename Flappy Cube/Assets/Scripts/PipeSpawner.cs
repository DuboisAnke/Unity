using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float time;
    public Pipe pipe;
    public bool isSpawning = false;

    void Init(LevelScriptableObject level)
    {
        pipe = level.pipe;
        pipe.speed = level.pipeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;

            }
            else if (time <= 0f)
            {
                SpawnPipe();
                time = 2f;
            }
        }

    }

    void SpawnPipe()
    {
        Pipe spawnedPipe = Instantiate<Pipe>(pipe, new Vector3(7, Random.Range(3, -3), 0), Quaternion.identity);

    }
}
