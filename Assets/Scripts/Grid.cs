using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
 
public class Grid : MonoBehaviour {
   
	public int mapHeight; //Height in units of map
	public int mapWidth; //Width in units of map
	public int tileSize; //Height and width in units of each tile
	public GameObject tilePrefab; //Prefab for an individual tile
    private GridTile tempUnitTile;
    private int i;
    private int columns;
    private int rows;
   
    public List<GridTile> gridArray; //complete list of grid tiles. Use with a foreach loop for maximum efficiency.
 
private void Awake() {      
        gridArray = new List<GridTile>();
        for (i = 0; i < (mapHeight * mapWidth) / (tileSize * tileSize); i++)
        {
            tempUnitTile = GameObject.Instantiate(tilePrefab, new Vector2(-(mapHeight/2) + (tileSize * rows) , -(mapWidth/2) + (tileSize * columns)), Quaternion.identity).GetComponent<GridTile>();
            tempUnitTile.name = "Tile " + (i + 1) + "(" + columns + "," + rows + ")";
			tempUnitTile.transform.SetParent(GameObject.Find("Grid").transform);
            tempUnitTile.x = columns;
            tempUnitTile.y = rows;
            tempUnitTile.theGrid = this;
            gridArray.Add(tempUnitTile);
            columns++;
           
            if (tileSize * columns == mapWidth)
            {
                columns = 0;
                rows++;
            }
        }
    }
   
   ///Returns the GridTile that the mouse is over at this time.
    public GridTile GetGridTile (Player owner) {
        RaycastHit hit;
        string colliderName;
        GridTile m_TileHit;
       
        if (!Physics.Raycast(owner.cam.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            return null;
       
        colliderName = hit.collider.name;
        m_TileHit = hit.collider.GetComponent<GridTile> ();
        return m_TileHit;
    }
   
}
 