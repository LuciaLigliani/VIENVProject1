using UnityEngine;
using System.Collections;

// Code modified from: http://docs.unity3d.com/Documentation/ScriptReference/Vector3.Lerp.html

/// <summary>
/// Gives a door the ability to slide its panel from a starting position to an end position
/// </summary>
public class SlideMechanism : MonoBehaviour
{
	// Transforms to act as start and end markers for the journey.
	public Transform startMarker;
	public Transform endMarker;

    // Uncomment this if you want a non-linear panel movement
    // public AnimationCurve myCurve; 
	
	// Movement speed in units/sec.
	public float speed = 1.0f;
	
	// Time when the movement started.
	private float startTime;
	
	// Total distance between the markers.
	private float journeyLength;

	//private float smooth = 5.0f;
    // The state of the door (sliding or not sliding)
	public bool sliding = false;

    // The panel/mesh that needs to be moved 
    private Transform paneltransform;

    private void Awake() {
    }

	void Start () {
        // Retreives a reference to the panel mesh that needs to move
        // Assumes the object "DoorPanel" is a child of this object
        paneltransform = transform.Find("DoorPanel");
	}

	void StartSliding () {
		// Keep a note of the time the movement started.
		startTime = Time.time;	
		// Calculate the journey length.
		journeyLength = Vector3.Distance (startMarker.position, endMarker.position);
        // Changing to a sliding state
		sliding = true;
	}

	// If sliding moves along the sliding vector according to movement speed
	void Update () {
		if (sliding) {
			// Distance moved = time * speed.
			float distCovered = (Time.time - startTime) * speed;			
			// Fraction of journey completed = current distance divided by total distance.
			float fracJourney = distCovered / journeyLength;
            // Set our position as a fraction of the distance between the markers.
            paneltransform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            // Swap the above line with the following line to use a non-linear motion curve (and uncomment variable up above)
            //paneltransform.position = Vector3.Lerp (startMarker.position, endMarker.position, myCurve.Evaluate(fracJourney));
		}
	}
}
