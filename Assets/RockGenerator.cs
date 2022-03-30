using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    private int x = 128;
    private int z = 128;

    public Vector3 randomization;
    public int current;
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
                        new Vector3(currentX + Random.Range(-randomization.x, randomization.x), 20,
                            currentZ + Random.Range(-randomization.z, randomization.z)), Vector3.down, out hit, 100, layers))
                {
                    GameObject rock = Instantiate(rocks[0], hit.point,
                        Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
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