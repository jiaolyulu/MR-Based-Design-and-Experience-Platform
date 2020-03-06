using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_ItemManager : MonoBehaviour {
    public GameObject AssetItemPrefab;
    public List<GameObject> AssetItems;
	// Use this for initialization
	void Start () {
	}
	
    void OnEnable()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        }
    }

	// Update is called once per frame
	
    public void AddAssetItems(string name)
    {
        GameObject Curr = Instantiate(AssetItemPrefab);
        Curr.gameObject.SetActive(true);
        Curr.transform.name = name;
        Curr.transform.SetParent(transform);
        Curr.transform.localEulerAngles = Vector3.zero;
        Curr.transform.localPosition = - new Vector3(0.0f, (AssetItems.Count + 1) * 0.05f, 0.0f);
        Curr.transform.localScale = Vector3.one * 0.001f;
        Curr.GetComponentInChildren<Text>().text = name;

        AssetItems.Add(Curr);
    }

    public void RemoveAllItems()
    {
        for(int i = 0; i< AssetItems.Count; i++)
        {
            Destroy(AssetItems[i]);
        }
        AssetItems.Clear();
    }
}
