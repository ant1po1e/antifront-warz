using System.Collections.Generic;
using UnityEngine;

public class WaveCube : MonoBehaviour
{
    public int gridWidth = 10; 
    public int gridHeight = 10;
    public float spacing = 1.5f;
    public GameObject cubePrefab;
    public float waveSpeed = 2f; 
    public float waveHeight = 2f; 
    public float updateInterval = 0.05f; 
    public bool showGizmos = true;

    private List<Transform> cubes = new List<Transform>();
    private List<float> randomOffsets = new List<float>();

    private void Start()
    {
        GenerateWaveGrid();
        InvokeRepeating(nameof(UpdateWave), 0f, updateInterval); 
    }

    private void GenerateWaveGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 localPosition = new Vector3(x * spacing, 0, z * spacing);
                GameObject cube = Instantiate(cubePrefab, transform);

                cube.transform.localPosition = localPosition;
                cubes.Add(cube.transform);

                randomOffsets.Add(Random.Range(0f, Mathf.PI * 2f));
            }
        }
    }

    private void UpdateWave()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i] != null)
            {
                Vector3 localPosition = cubes[i].localPosition;

                float waveOffset = Mathf.Sin(Time.time * waveSpeed + randomOffsets[i]) * waveHeight;

                cubes[i].localPosition = new Vector3(localPosition.x, waveOffset, localPosition.z);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.cyan;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 start = transform.position + new Vector3(x * spacing, 0, z * spacing);
                Vector3 end = start + Vector3.up * waveHeight;

                Gizmos.DrawLine(start, end);

                Gizmos.DrawSphere(start, 0.1f);
            }
        }
    }
}
