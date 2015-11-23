using UnityEngine;
using System.Collections;
using System.Xml;


public class LevelBuilder : MonoBehaviour {
	 
	private int _levelWidth;
	private int _levelHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
					
				case 2:
					_player = new Player((MyGame)_MG, this);
					_player.SetXY(w * 64, (h * 64) + _player.height);
					break;
				case 3:
					_spike = new Spike();
					AddChild(_spike);
					
					_spike.SetXY(w * 64, h * 64);
					_spikeList.Add(_spike);
					break;
				case 4:
					
				case 5:
					_nextLevel = new NextLevel(_currentLevel, (MyGame)game);
					AddChild(_nextLevel);
					_nextLevel.SetXY(w * 64, h * 64);
					break;
				case 6:
					_torch = new Torch();
					AddChild(_torch);
					_torch.SetXY(w * 64, h * 64);
					_torch.Mirror(true, false);
					break;
				case 7:
					_torch = new Torch();
					AddChild(_torch);
					_torch.SetXY(w * 64, h * 64);
					_torch.Mirror(false, false);
					break;
				case 8:
					_fadingBlock = new FadingBlock();
					AddChild(_fadingBlock);
					_fadingBlock.SetXY(w * 64, h * 64);
					_fadingBlockList.Add(_fadingBlock);
					break;
				case 9:
					_enemy = new Skeleton(w * 64, (h * 64) + 64);
					_enemyList.Add(_enemy);
					break;
				case 10:
					_bat = new Bat(w * 64, (h * 64), _MG, this);
					_bat.y = _bat.y + _bat.height;
					AddChild(_bat);
					_batList.Add(_bat);
					break;
				case 11:
					//_ground = new Ground(1);
					//AddChild(_ground);
					//_ground.SetXY(w * 64, h * 64);
					//_groundList.Add(_ground);
					//break;
				case 12:
					_ground = new Ground(0);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 13:
					_ground = new Ground(1);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 14:
					_ground = new Ground(2);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 15:
					_ground = new Ground(3);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 16:
					_ground = new Ground(4);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 17:
					_ground = new Ground(5);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 18:
					_ground = new Ground(6);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 19:
					_ground = new Ground(7);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 20:
					_ground = new Ground(8);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 21:
					_ground = new Ground(9);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 22:
					_ground = new Ground(10);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 23:
					_ground = new Ground(11);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 24:
					_ground = new Ground(12);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 25:
					_ground = new Ground(13);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 26:
					_ground = new Ground(14);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 27:
					_ground = new Ground(15);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 28:
					_ground = new Ground(16);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 29:
					_ground = new Ground(17);
					AddChild(_ground);
					_ground.SetXY(w * 64, h * 64);
					_groundList.Add(_ground);
					break;
				case 30:
					_coin = new Coin();
					AddChild(_coin);
					_coin.SetXY(w * 64, h * 64);
					_coinList.Add(_coin);
					break;
				case 31:
					_brokenRock = new BrokenRock();
					AddChild(_brokenRock);
					_brokenRock.SetXY(w * 64, h * 64);
					_brokenRockList.Add(_brokenRock);
					break;
				case 32:
					_boss = new Boss (_MG, this, w * 64, h * 64);
					AddChild (_boss);
					break;
					
				}
				
			}
		}
	}*/
}
