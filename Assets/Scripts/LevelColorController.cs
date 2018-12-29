using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelColorController : MonoBehaviour
{
	public Camera cam;
	public Transform conveyorParent;
	public LevelColor[] colors;

	public string currentColorString;
	public int colorIndex = 0;
	
	void Start()
	{	
		SetColor();
	}
		
	/// <summary>
	/// Picks the next color in the color array
	/// Changes the color of the level
	/// </summary>
	public void ChangeColor()
	{
		colorIndex++;

		if (colorIndex >= colors.Length)
		{
			colorIndex = 0;
		}
		
		LevelColor newColor = colors[colorIndex];
		
		cam.backgroundColor = newColor.backgroundColor;

		foreach (Transform t in conveyorParent)
		{
			if (t.CompareTag("Conveyor"))
			{
				t.GetComponent<Conveyor>().SetColor(newColor.conveyorColor);
			}
		}
		currentColorString = newColor.ColorString;
		
		
		PlayerPrefs.SetString("Color", currentColorString);
		PlayerPrefs.Save();
	}

	/// <summary>
	/// Gets the current level's color from the save
	/// Sets the color to the level
	/// </summary>
	void SetColor()
	{
		if (!PlayerPrefs.HasKey("Color"))
		{
			PlayerPrefs.SetString("Color", "Blue");
			PlayerPrefs.Save();
		}

		currentColorString = PlayerPrefs.GetString("Color");

		LevelColor newColor = new LevelColor();

		for (int i = 0; i < colors.Length; i++)
		{
			newColor = colors[i];
			if (newColor.ColorString == currentColorString)
			{
				colorIndex = i;
				break;
			}
		}
		
		cam.backgroundColor = newColor.backgroundColor;
		foreach (Transform t in conveyorParent)
		{
			if (t.CompareTag("Conveyor"))
			{
				t.GetComponent<Conveyor>().SetColor(newColor.conveyorColor);
			}
		}
	}
}
