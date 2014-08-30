﻿using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	private static InventoryManager s_Instance = null;

	public ArrayList itemList;

	private GameObject[] itemUIObj = new GameObject[6];

	private int currentPage = 0;

	public string currentChooserItemId = null;

	public string itemToCombineWith = null;

	public string itemToCombineWith2 = null;

	public static InventoryManager instance
	{
		get
		{
			if (s_Instance == null)
			{
				InventoryManager tmpInst = FindObjectOfType(typeof(InventoryManager)) as InventoryManager;
				if (tmpInst != null)
					tmpInst.Start();
				s_Instance = tmpInst;
				
				if (s_Instance == null && Application.isEditor)
					Debug.LogError("Could not locate a object. You have to have exactly one object in the scene.");
			}
			
			return s_Instance;
		}
	}

	public void RemoveItem(Item item)
	{
		itemList.Remove(item);
	}

	public void RemoveItemString(string itemId)
	{
		foreach(Item i in itemList)
		{
			if(i.m_id == itemId)
			{
				itemList.Remove(i);
				return;
			}
		}

		Debug.Log("cannot find item");
	}

	public void AddItem(Item item)
	{
		itemList.Add(item);

		//update item
		UpdateInventoryUI();

	}

	public int getCurrentPage()
	{
		return currentPage;
	}

	public void setCurrentPage(int value)
	{
		if(value >= 0)
		{
			currentPage = value;
		}
	}

	public void UpdateInventoryUI()
	{
		//inventory ui hidden dont do anything
		if(itemUIObj[0] && !itemUIObj[0].activeSelf)
		{
			return;
		}

		//check if we have gameobjects
		for(int x = 0; x < 6 ; x++)
		{
			if(!itemUIObj[x])
			{
				itemUIObj[x] = GameObject.Find("Item" + x);
			}
			
			if(itemUIObj[x])
			{
				itemUIObj[x].transform.FindChild("Label").GetComponent<UILabel>().text = getItemName(x+(6*currentPage));
			}
		}
	}

	public void AddItemByName(string room, string id)
	{

	}

	public string getItemName(int id)
	{
		if(itemList.Count <= id)
		{
			return "";
		}
		Item i = (Item)itemList[id];
		return i.m_id;
	}

	public GameObject FindById(string id)
	{
		for(int x = 0; x < itemList.Count; x++)
		{
			Item i = (Item)itemList[x];
			if(i.m_id == id)
			{
				return i.m_itemObj;
			}
		}

		return null;
	}

	public bool CombineCurrentSelection()
	{
		bool success = false;

		if((itemToCombineWith2 == "knife1" && itemToCombineWith == "cork")  ||
		   (itemToCombineWith2 == "cork" && itemToCombineWith == "knife1"))
		{
			//add new item
			var item = new Item("corkPieces",null);
			//RemoveItemString("knife1");
			RemoveItemString("cork");
			AddItem(item);
			success = true;
		}
		else if((itemToCombineWith2 == "corkPieces" && itemToCombineWith == "brokenBucket")  ||
		        (itemToCombineWith2 == "brokenBucket" && itemToCombineWith == "corkPieces"))
		{
			var item = new Item("Bucket",null);
			RemoveItemString("corkPieces");
			RemoveItemString("brokenBucket");
			AddItem(item);
			success = true;
		}

		itemToCombineWith2 = "";
		itemToCombineWith = "";

		if(success)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public int GetNumberOfItems()
	{
		return itemList.Count;
	}

	// Use this for initialization
	void Start () {
		itemList = new ArrayList();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			//show inventory list
			Debug.Log("Current Inventory:");
			for(int x = 0 ; x<itemList.Count; x++)
			{
				Debug.Log(x + ". " + ((Item)itemList[x]).m_id);
			}
		}
	}

	public void UseItem(string itemId)
	{
		if(itemId == "flashLight")
		{
			bool isTorchOn = GameObject.Find("Main Camera").transform.FindChild("torchlight").GetComponent<TorchLightControl>().isTorchOn;

			if(isTorchOn)
			{
				GameObject.Find("Main Camera").transform.FindChild("torchlight").GetComponent<TorchLightControl>().isTorchOn = false;
			}
			else
			{
				GameObject.Find("Main Camera").transform.FindChild("torchlight").GetComponent<TorchLightControl>().isTorchOn = true;
			}
		}
		else if(itemId == "charcole")
		{
			
		}
		else if(itemId == "paper")
		{
			
		}
		else if(itemId == "knife1")
		{
			
		}
		else if(itemId == "TimeDevice")
		{
			bool isUsingTDevice = GameObject.Find("Main Camera").GetComponent<cameraEffectController>().isUsingTDevice;

			if(isUsingTDevice)
			{
				GameObject.Find("Main Camera").GetComponent<cameraEffectController>().isUsingTDevice = false;
			}
			else
			{
				GameObject.Find("Main Camera").GetComponent<cameraEffectController>().isUsingTDevice = true;
			}
		}	
		else if(itemId == "screwDriver")
		{
			
		}
		else if(itemId == "wrench1")
		{
			
		}
		else if(itemId == "brokenBucket")
		{
			
		}
		else if(itemId == "cell_mirror")
		{
			
		}
		else if(itemId == "key")
		{
			
		}
	}
}
