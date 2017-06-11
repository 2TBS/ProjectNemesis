using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Grid : MonoBehaviour {
   
	public int mapHeight; //Height in units of map
	public int mapWidth; //Width in units of map
	public int tileSize; //Height and width in units of each tile
	public GameObject tilePrefab; //Prefab for an individual tile
    private GameObject tempUnitTile;
    private int i;
    private int columns;
    private int rows;
    public Camera m_Camera; //main camera
   
    public ArrayList GridArray; //complete list of grid tiles. Use with a foreach loop for maximum efficiency.
 
private void Awake() {      
        GridArray = new ArrayList();
        for (i = 0; i < (mapHeight * mapWidth) / (tileSize * tileSize); i++)
        {
            tempUnitTile = GameObject.Instantiate(tilePrefab, new Vector2(-(mapHeight/2) + (tileSize * rows) , -(mapWidth/2) + (tileSize * columns)), Quaternion.identity) as GameObject;
            tempUnitTile.name = "Tile " + (i + 1);
			tempUnitTile.transform.SetParent(GameObject.Find("The Map").transform);
           
            GridArray.Add(tempUnitTile);
            columns++;
           
            if (tileSize * columns == mapWidth)
            {
                columns = 0;
                rows++;
            }
        }
    }
   
   
    public GameObject Raycast () {
        RaycastHit hit;
        string colliderName;
        GameObject m_TileHit;
       
        if (!Physics.Raycast(m_Camera.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            return null;
       
        colliderName = hit.collider.name;
        m_TileHit = hit.collider.gameObject;
        Debug.Log(colliderName);
        return m_TileHit;
    }
   
}
 