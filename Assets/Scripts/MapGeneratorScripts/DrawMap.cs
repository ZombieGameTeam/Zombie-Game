using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawMap : Singleton<DrawMap> {

    public float sizeOfTile = 2f;
   
    private Map map = null;
    private Vector2 size = new Vector2(50, 50);
    private MapGenerator mapGenerator = null;


    public List<GameObject> walls, floors;

    private int angle = 0;

    void Awake() {


        List<Room> rooms = new List<Room>();
        for (int i = 0; i < 10; i++)
            rooms.Add(new MediumRoom(Vector2.zero));
        
        mapGenerator = new MapGenerator(size, rooms);

        map = mapGenerator.getMap();
        this.drawMap();

    }

  

    private void drawMap() {
        if (map == null)
        {
            Debug.Log("map variable is null");
            return;
        }
        
        
        for (int i = 0; i < size.x; i++) {
        
            for (int j = 0; j < size.y; j++) {
        
                if (map[i, j] == Cell.Block){
        
                    GameObject plane = Instantiate(floors[0], Vector3.zero, Quaternion.identity) as GameObject;
                    plane.transform.position = new Vector3(i * sizeOfTile, j * sizeOfTile, 0);
                    plane.transform.localScale = new Vector3(sizeOfTile / 2f, sizeOfTile / 2f, 1f);
                    plane.transform.parent = gameObject.transform;
                    drawWalls(new Vector2(i, j));
                    
                }
            }
        }
    
    }
    private void drawWalls(Vector2 poz) {
        GameObject wall = null;
        int typeOfWall = 0;
        float wallWidth = sizeOfTile / 10f;
        float wallHeight = sizeOfTile / 2f;
        int x, y;
        x = (int)poz.x - 1;
        y = (int)poz.y;
        if (this.map[x, y] != Cell.Block) {
            wall = Instantiate(walls[typeOfWall], Vector3.zero, Quaternion.identity) as GameObject;
            wall.transform.localScale = new Vector3(wallWidth, wallHeight, 1f);
            wall.transform.position = new Vector3(x * sizeOfTile + sizeOfTile / 2, y * sizeOfTile, -1.0f);
            wall.transform.parent = gameObject.transform;
        }
        x = (int)poz.x + 1;
        y = (int)poz.y;
        if (this.map[x, y] != Cell.Block) {
            wall = Instantiate(walls[typeOfWall], Vector3.zero, Quaternion.identity) as GameObject;
            wall.transform.localScale = new Vector3(wallWidth, wallHeight, 1f);
            wall.transform.position = new Vector3(x * sizeOfTile - sizeOfTile / 2, y * sizeOfTile, -1.0f);
            wall.transform.parent = gameObject.transform;
        }
        x = (int)poz.x;
        y = (int)poz.y - 1;
        if (this.map[x, y] != Cell.Block) {
            wall = Instantiate(walls[typeOfWall], Vector3.zero, Quaternion.identity) as GameObject;
            wall.transform.localScale = new Vector3(wallWidth, wallHeight, sizeOfTile);
            wall.transform.rotation = Quaternion.Euler(0, 0, 90);
            wall.transform.position = new Vector3(x * sizeOfTile, y * sizeOfTile + sizeOfTile / 2, -1.0f);
            wall.transform.parent = gameObject.transform;
        }
        x = (int)poz.x;
        y = (int)poz.y + 1;
        if (this.map[x, y] != Cell.Block) {
            wall = Instantiate(walls[typeOfWall], Vector3.zero, Quaternion.identity) as GameObject;
            wall.transform.localScale = new Vector3(wallWidth, wallHeight, sizeOfTile);
            wall.transform.rotation = Quaternion.Euler(0, 0, 90);
            wall.transform.position = new Vector3(x * sizeOfTile, y * sizeOfTile - sizeOfTile / 2, -1.0f);
            wall.transform.parent = gameObject.transform;
        }
    }

	public Vector3 getStartPosition() {
		Vector3 pos = mapGenerator.getStartPosition();
		pos.x *= sizeOfTile;
		pos.z *= sizeOfTile;
		return pos;
	}


}
