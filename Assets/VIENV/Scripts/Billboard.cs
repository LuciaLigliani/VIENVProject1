using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
        if (Camera.main != null) {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0); // flip if backwards
        }
    }
}
