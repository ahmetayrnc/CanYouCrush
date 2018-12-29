using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
	public SpriteRenderer sprite;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}
	
	void Update()
	{
		if (transform.position.x >= 9f)
		{
			transform.position -= Vector3.right * 5 * 4.5f;
		}
	}

	public void SetColor(Color _color)
	{
		sprite.color = _color;
	}
}
