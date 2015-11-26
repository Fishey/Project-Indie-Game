using UnityEngine;
using System.Collections;
using System.Xml;


public class LevelBuilder : MonoBehaviour {
	 
	public Transform Enemies;

	private EnemyClass[] enemyList;
	private PickUpScript[] pickupList;

	private int _levelWidth;
	private int _levelHeight;

	// Use this for initialization
	void Start () {
		if (!Enemies)
			Enemies = GameObject.Find("Enemies").transform;
		enemyList = Enemies.GetComponentsInChildren<EnemyClass>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RespawnEnemies()
	{
		foreach (EnemyClass enemy in enemyList)
		{
			enemy.Respawn();
		}
	}
	/*
	public string XMLreader(string slevel)
	{
		XmlDocument doc = new XmlDocument();
		doc.Load(@"levels\" + slevel);
		
		XmlElement root = doc.DocumentElement;
		if (root.HasAttribute("width"))
		{
			_levelWidth = Convert.ToInt16(root.GetAttribute("width"));
		}
		if (root.HasAttribute("height"))
		{
			_levelHeight = Convert.ToInt16(root.GetAttribute("height"));
		}
		
		string level = "";
		
		foreach (XmlNode node in doc.DocumentElement.ChildNodes)
		{
				
			level = node.InnerText;
		}
		
		return level;
		
	}
	
	public int[,] LevelArrayBuilder(string level)
	{
		string[] aLevelString = level.Split(',');
		int[,] aLevelInt = new int[_levelHeight, _levelWidth];
		
		int indexOfaLevelString = 0;
		for (int h = 0; h < _levelHeight; h++)
		{
			for (int w = 0; w < _levelWidth; w++)
			{
				aLevelInt[h, w] = Convert.ToInt16(aLevelString[indexOfaLevelString]);
				indexOfaLevelString++;
			}
		}
		
		return aLevelInt;
	}
	
	public void BuildGameLevel(int[,] levelArray)
	{
		for (int h = 0; h < _levelHeight; h++)
		{
			for (int w = 0; w < _levelWidth; w++)
			{
				int tile = levelArray[h, w];
				
				switch (tile)
				{
					
				}
			}
		}
	}*/
}
