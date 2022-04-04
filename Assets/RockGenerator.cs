using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public int x = 128;
    public int z = 128;

    public Vector3 randomPosition;
    public Vector3 randomRotation;
    public LayerMask layers;
    [SerializeField] private GameObject[] rocks;

    void Start()
    {
        for (int currentX = -x; currentX < x; currentX += 5)
        {
            for (int currentZ = -z; currentZ < z; currentZ += 5)
            {
                RaycastHit hit;
                if (Physics.Raycast(
                        new Vector3(currentX + Random.Range(-randomPosition.x, randomPosition.x), 20,
                            currentZ + Random.Range(-randomPosition.z, randomPosition.z)), Vector3.down, out hit, 100, layers))
                {
                    GameObject rock = Instantiate(rocks[0], hit.point,
                        Quaternion.Euler(Random.Range(0, randomRotation.x), Random.Range(0, randomRotation.y), Random.Range(0, randomRotation.z)));
                    float scale = Random.Range(0.1f, 1f);
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

    // Update is called once per frame
    void Update()
    {
    }
}