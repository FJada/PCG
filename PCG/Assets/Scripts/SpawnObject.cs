using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject[] objects;


    // Start is called before the first frame update
    private void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
