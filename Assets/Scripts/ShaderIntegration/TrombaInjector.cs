using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrombaInjector : ShaderPasser
{
	public float succPower1;
	public float succPower2;
	public float succSpeed1 = 3;
	public float succSpeed2 = 3;
	public float[] poundDist = {
		100, 100
	};
	private float succReduction = 24f;
	private float poundReduction = 5f;

	private Transform brother1;
	private Transform brother2;
	private Transform tromba1;
	private Transform tromba2;
	protected override void FakeStart()
	{
		Brother[] brothers = FindObjectsOfType<Brother>();
		//get each brother
		brother1 = brothers[0].transform;
		brother2 = brothers[1].transform;
		//get tromba from each player
		tromba1 = brother1.GetComponentInChildren<Tromba>().transform;
		tromba2 = brother2.GetComponentInChildren<Tromba>().transform;
	}

	protected override void FakeUpdate()
	{
		//remove sucking power
		if (succPower1 < 0)
		{
			succPower1 = Mathf.Min(0, succPower1 + succReduction * Time.deltaTime);
		}
		else
		{
			succPower1 = Mathf.Max(0, succPower1 - succReduction * Time.deltaTime);
		}

		if (succPower2 < 0)
		{
			succPower2 = Mathf.Min(0, succPower2 + succReduction * Time.deltaTime);
		}
		else
		{
			succPower2 = Mathf.Max(0, succPower2 - succReduction * Time.deltaTime);
		}

		//update groundpound animation
		if (poundDist[0] < 100)
		{
			poundDist[0] += poundReduction * Time.deltaTime;
		}
		if (poundDist[1] < 100)
		{
			poundDist[1] += poundReduction * Time.deltaTime;
		}
	}

	protected override void BakePropertyNames()
	{
		PropertyNames.Add("GroundpoundPos1");
		PropertyNames.Add("GroundpoundPos2");
		PropertyNames.Add("TrombaPos1");
		PropertyNames.Add("TrombaPos2");
		PropertyNames.Add("SuccPower1");
		PropertyNames.Add("SuccPower2");
		PropertyNames.Add("SuccSpeed1");
		PropertyNames.Add("SuccSpeed2");
		PropertyNames.Add("PoundDist1");
		PropertyNames.Add("PoundDist2");
	}

	protected override void PassToRender(int j)
	{
		shadedMaterials[j].SetVector(PropertyIDs[0], brother1.position);
		shadedMaterials[j].SetVector(PropertyIDs[1], brother2.position);
		shadedMaterials[j].SetVector(PropertyIDs[2], tromba1.position);
		shadedMaterials[j].SetVector(PropertyIDs[3], tromba2.position);
		shadedMaterials[j].SetFloat(PropertyIDs[4], succPower1);
		shadedMaterials[j].SetFloat(PropertyIDs[5], succPower2);
		shadedMaterials[j].SetFloat(PropertyIDs[6], succSpeed1);
		shadedMaterials[j].SetFloat(PropertyIDs[7], succSpeed2);
		shadedMaterials[j].SetFloat(PropertyIDs[8], poundDist[0]);
		shadedMaterials[j].SetFloat(PropertyIDs[9], poundDist[1]);
	}
}
