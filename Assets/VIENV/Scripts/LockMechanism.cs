using UnityEngine;
using System.Collections;

/// <summary>
/// Respond to player collision by opening lock/door if key present in inventory
/// </summary>
public class LockMechanism : MonoBehaviour {

    // The door to be affected
    public GameObject door; // <-- Should implement "StartSliding"
    // The sound to be played when lock opens
    public AudioClip opensound;

    // Private properties that change when lock opens
    private GameObject mybulbobject;
    private GameObject mylightobject;
    private Material bulbmaterialcomponent;
    private Light lightcomponent;

    void Start () {
        // Retrieve references to those things we need to access later
        // Assumes the objects "Bulb" and "Light" are both children of this object
        mybulbobject = transform.Find("Bulb").gameObject;
        bulbmaterialcomponent = mybulbobject.GetComponent<Renderer>().material;
        mylightobject = transform.Find("LockLight").gameObject;
        lightcomponent = mylightobject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Change the color of the lock to a given color
    void SetColor(Color newcolor) {
        bulbmaterialcomponent.SetColor("_BaseColor", newcolor);
        lightcomponent.color = newcolor;
    }

    // Tell lock and door to open if user has key
	//void OnTriggerEnter (Collider col) {
	//	if (col.gameObject.tag == "Player") {
    //        KeyInventory inventory = col.gameObject.GetComponent<KeyInventory>();
    //        if (inventory != null)
    //        {
    //            if (inventory.HasKey())
    //            {
    //                door.SendMessage("StartSliding");
    //                AudioSource.PlayClipAtPoint(opensound, transform.position);
    //                SetColor(Color.green);
    //            }
    //        }
	//	}
		
	//}
}

