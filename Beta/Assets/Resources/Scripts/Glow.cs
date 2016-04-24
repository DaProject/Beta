using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {


public Color color;
public float maxGlow, minGlow;
public float smooth;
public bool glowOn;
public Material mat;

	// Use this for initialization
	void Start () {
	
	glowOn = true;
	mat = GetComponent<MeshRenderer>().material;
	color = mat.GetColor("_TintColor");
	color.a = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	if(glowOn)
	{
		color.a += Time.deltaTime * smooth;
		if(color.a >= maxGlow) glowOn = false;
	}
	
	else
	{
		color.a -= Time.deltaTime * smooth/2;
		if(color.a <= minGlow) glowOn = true;
	}
	
	mat.SetColor ("_TintColor", color);
	
	}
}
