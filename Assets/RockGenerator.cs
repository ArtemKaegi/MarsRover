using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public int x = 128;
    public int z = 128;

    public Vector3 randomPosition;
    public Vector3 randomRotation;
    public float maxScale;
    public float minScale;
    public LayerMask layers;
    [SerializeField] private GameObject[] rocks;

    void Start()
    {
        for (int currentX = -x; currentX < x; currentX += 2)
        {
            for (int currentZ = -z; currentZ < z; currentZ += 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(
                        new Vector3(currentX + Random.Range(-randomPosition.x, randomPosition.x), 20,
                            currentZ + Random.Range(-randomPosition.z, randomPosition.z)), Vector3.down, out hit, 100, layers))
                {
                    GameObject rock = Instantiate(rocks[Random.Range(0, rocks.Length)], hit.point,
                        Quaternion.Euler(Random.Range(0, randomRotation.x), Random.Range(0, randomRotation.y), Random.Range(0, randomRotation.z)));
                    float scale = Random.Range(minScale, maxScale);
                    rock.transform.localScale = new Vector3(scale, scale, scale);
                    rock.transform.position += Vector3.down * scale * 0.7f;
                }
                else
                {
                    Debug.Log("NoHit");
                }
            }
        }
    }
}