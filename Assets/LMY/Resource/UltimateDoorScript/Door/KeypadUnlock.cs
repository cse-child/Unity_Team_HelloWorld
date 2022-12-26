using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class KeypadUnlock : MonoBehaviour
{
	public string combination = string.Empty;
	public bool showingCombo = false;
	public string currentCombo = string.Empty;
	public GUIStyle guessFontStyle = null;
	
	public AudioClip unlockSound = null;
	
	public Rect displayPosition = new Rect(15, 15, 100, 25);
	public Rect clearAnswerPosition = new Rect(15, 15, 100, 25);
	
	public Texture2D zero = null;
	public Texture2D one = null;
	public Texture2D two = null;
	public Texture2D three = null;
	public Texture2D four = null;
	public Texture2D five = null;
	public Texture2D six = null;
	public Texture2D seven = null;
	public Texture2D eight = null;
	public Texture2D nine = null;
	public Texture2D star = null;
	public Texture2D pound = null;
	
	public Rect zeroPosition = new Rect(484, 500, 100, 100);
	public Rect onePosition = new Rect(336, 125, 100, 100);
	public Rect twoPosition = new Rect(484, 125, 100, 100);
	public Rect threePosition = new Rect(631, 125, 100, 100);
	public Rect fourPosition = new Rect(336, 255, 100, 100);
	public Rect fivePosition = new Rect(484, 255, 100, 100);
	public Rect sixPosition = new Rect(631, 255, 100, 100);
	public Rect sevenPosition = new Rect(336, 379, 100, 100);
	public Rect eightPosition = new Rect(484, 379, 100, 100);
	public Rect ninePosition = new Rect(631, 379, 100, 100);
	public Rect starPosition = new Rect(336, 500, 100, 100);
	public Rect poundPosition = new Rect(631, 500, 100, 100);
	
	private Door attachedDoor = null;
	
	private void Start()
	{
		attachedDoor = gameObject.GetComponent<Door>();
		
		if (attachedDoor == null)
			Debug.LogError("This script needs to be on a gameObject that has the Door script attached.");
	}
	
	private void Update()
	{
		if (attachedDoor.doorState == Door.DoorState.StartOpen)
			showingCombo = true;
		
		if (showingCombo)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
				
				if (zeroPosition.Contains(mouse))
					currentCombo += "0";
				else if (onePosition.Contains(mouse))
					currentCombo += "1";
				else if (twoPosition.Contains(mouse))
					currentCombo += "2";
				else if (threePosition.Contains(mouse))
					currentCombo += "3";
				else if (fourPosition.Contains(mouse))
					currentCombo += "4";
				else if (fivePosition.Contains(mouse))
					currentCombo += "5";
				else if (sixPosition.Contains(mouse))
					currentCombo += "6";
				else if (sevenPosition.Contains(mouse))
					currentCombo += "7";
				else if (eightPosition.Contains(mouse))
					currentCombo += "8";
				else if (ninePosition.Contains(mouse))
					currentCombo += "9";
				else if (poundPosition.Contains(mouse))
					currentCombo += "#";
				else if (starPosition.Contains(mouse))
					currentCombo += "*";
				
				if (clearAnswerPosition.Contains(mouse))
					currentCombo = string.Empty;
			}
		}
		else if (!string.IsNullOrEmpty(currentCombo))
			currentCombo = string.Empty;
		
		// This is when the door unlocks
		if (currentCombo == combination && !string.IsNullOrEmpty(combination))
		{
			attachedDoor.locked = false;
			currentCombo = string.Empty;
			showingCombo = false;
			
			// Here is where you would play a sound effect or something to indicate unlock.
			AudioSource.PlayClipAtPoint(unlockSound, transform.position);
		}
	}
	
	private void OnGUI()
	{
		// Draw all the keypad
		if (showingCombo && attachedDoor != null)
		{
			GUI.DrawTexture(zeroPosition, zero);
			GUI.DrawTexture(starPosition, star);
			GUI.DrawTexture(poundPosition, pound);
			GUI.DrawTexture(onePosition, one);
			GUI.DrawTexture(twoPosition, two);
			GUI.DrawTexture(threePosition, three);
			GUI.DrawTexture(fourPosition, four);
			GUI.DrawTexture(fivePosition, five);
			GUI.DrawTexture(sixPosition, six);
			GUI.DrawTexture(sevenPosition, seven);
			GUI.DrawTexture(eightPosition, eight);
			GUI.DrawTexture(ninePosition, nine);
			
			GUI.Label(displayPosition, currentCombo, guessFontStyle);
		}
	}
}