using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public int max = 2;
	private List<Texture> items = new List<Texture>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool IsDuplicate(Texture aTexture){
		bool isDuplicate = false;
		for(int i=0; i<items.Count; i++){
			if(aTexture.ToString() == items[i].ToString()){
				Debug.Log("You already have this item");
				isDuplicate = true;
				return isDuplicate;
			}
		}
		return isDuplicate;
	}

	void Pickup(Texture aTexture) {
		Debug.Log ("Got Pickup: "+aTexture.ToString());
		if(items.Count<max && IsDuplicate(aTexture)==false)
			items.Add(aTexture);
		else
			Debug.Log("Inventory full");
	}

	void OnGUI() {
		for (int i = 0; i < items.Count; i++)
        {
			GUI.DrawTexture(new Rect(10 + (110 * i), 10, 100, 100),
                            items[i],
                            ScaleMode.ScaleToFit,
                            true, 1.0f);
        }
	}

}
