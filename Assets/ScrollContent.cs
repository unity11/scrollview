using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollContent : MonoBehaviour
{

    public int count = 100;

    public RectTransform rect;
    RectTransform grid;
    public List<RectTransform> itemList;

    //public float height;

    public GameObject prefab;
    float itemHeight = 50.0f;
    float itemSpace = 10.0f;
    float Height
    {
        get { return itemHeight + itemSpace; }
    }
    float gridHeight;
    int itemCount;
    void Start()
    {
        
        float height = rect.sizeDelta.y;
        itemCount = Mathf.CeilToInt(height / (Height))+1;

        grid = rect.GetComponent<ScrollRect>().content;
        Vector2 size = grid.sizeDelta;

        gridHeight = Height * count;
        size.y = gridHeight- itemSpace;
        grid.sizeDelta = size;
        for (int i = 0; i < itemCount; i++)
        {
            RectTransform item = GameObject.Instantiate<GameObject>(prefab, grid).GetComponent<RectTransform>();
            item.gameObject.SetActive(true);
            item.GetComponent<Item>().Index = -i;
            itemList.Add(item);
        }

        rect.GetComponent<ScrollRect>().onValueChanged.AddListener((vec) =>
        {
            Reflesh();
        });
        Reflesh();
    }
    public int id;
    private void Reflesh()
    {
        float height = grid.anchoredPosition.y;
        height = Mathf.Clamp(height, 0, gridHeight- itemCount* (Height));
        id = Mathf.FloorToInt(height / (Height));

        for (int i = 0; i < itemList.Count; i++)
        {
            RectTransform r = itemList[i];
            Item item = r.GetComponent<Item>();
            int k = id - item.Index;
            int x = k;
            r.GetChild(0).GetComponent<Text>().text = x.ToString();
            r.anchoredPosition = new Vector2(0, -x * (Height));
        }
       
    }
  
    void Update()
    {
        
    }
}
