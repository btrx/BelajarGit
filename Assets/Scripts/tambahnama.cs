using UnityEngine;

public class tambahnama : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debug.Log("Nama saya adalah " + PlayerPrefs.GetString("nama"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
