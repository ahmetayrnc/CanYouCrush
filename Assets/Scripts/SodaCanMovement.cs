using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SodaCanMovement : MonoBehaviour
{
	private AnimationCurve curve;
	private AnimationCurve[] curves;
	
	public AnimationCurve[] EZCurves;
	public AnimationCurve[] MIDCurves;
	public AnimationCurve[] HARDCurves;
	
	public SodaCan can;
	public float baseSpeed;
	public float duration;
	public float firstMilestone;
	public float secondMilestone;
	
	[HideInInspector]
	public float speed;
	private bool doMovement;

	private float t;
	private float time;
	private int randomIndex;
	void Start()
	{	
		doMovement = true;
		curves = EZCurves;
		curve = curves[0];
	}
	
	void Update()
	{
		if (GameManager.Instance.gamePaused)
		{
			return;
		}
		
		if (!doMovement)
		{
			return;
		}

		time += Time.deltaTime;
		if (time > firstMilestone)
		{
			curves = MIDCurves;
		}
		
		if (time > secondMilestone)
		{
			curves = HARDCurves;
		}
		
		t += Time.deltaTime * (1 / duration);
		if (t >= 1f)
		{
			t = 0f;
			
			randomIndex = Random.Range(0, curves.Length);
			curve = curves[randomIndex];
		}
		
		Vector3 dir = Vector3.right;
		speed = curve.Evaluate(t) * baseSpeed + 1 ;
		transform.Translate(dir.normalized * speed * Time.deltaTime);
	}
	
	public void StopMovement()
	{
		doMovement = false;
	}

	public void NewLevel()
	{
		Random.InitState(DateTime.Now.Ticks.GetHashCode());
		curves = MIDCurves;
		time = 0f;
	}
}
