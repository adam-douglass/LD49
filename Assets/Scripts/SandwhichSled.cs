using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwhichSled : MonoBehaviour
{
    public GameObject SandwhichPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var created = Instantiate(SandwhichPrefab, this.gameObject.transform.position, Quaternion.identity);
        created.transform.SetParent(this.transform);   
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(Time.deltaTime * 0.6f, 0, 0);   
    }
}
