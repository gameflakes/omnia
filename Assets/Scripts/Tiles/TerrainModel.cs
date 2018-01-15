using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TerrainModel : Tile
{
    [SerializeField]
    private Sprite[] terrainSprites;

    [SerializeField]
    private Sprite preview;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    // When the tile is painted, refresh all it's neighbors
    {
        for (int j = 1; j >= -1; j--)
        {
            for (int i = -1; i <= 1; i++)
            {
                Vector3Int neighbor = new Vector3Int(position.x + i, position.y + j, position.z);

                if ((i != 0 || j != 0) &&
                    HasTerrain(tilemap, neighbor))
                {
                    tilemap.RefreshTile(neighbor);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    // Bitmasks the neighbor information of a tile and sets the specific sprite to that
    {
        int dependencies = 0;

        for (int j = 1; j >= -1; j--)
        {
            for (int i = -1; i <= 1; i++)
            {
                if (i != 0 || j != 0)
                {
                    if (HasTerrain(tilemap, new Vector3Int(position.x + i, position.y + j, position.z)))
                    {
                        dependencies += 1;
                    }
                    dependencies = dependencies << 1;
                }
            }
        }

        dependencies = dependencies >> 1;
        int spriteIndex = GetTileSpriteWithDependencies(dependencies);
        tileData.sprite = terrainSprites[spriteIndex];

        // Colisão não pegava e preview não estava pegando, mas isso aqui resolveu
        tileData.colliderType = ColliderType.Sprite;
        preview = terrainSprites[16];
    }

    private bool HasTerrain(ITilemap tilemap, Vector3Int position)
    // Verify if it's a Terrain Tile
    {
        return tilemap.GetTile(position) == this;
    }

    private int GetTileSpriteWithDependencies(int dependencies)
    // Return specific sprite for the dependencies encoded by using bitmask
    /*
     * Bitmask is using the bits in a number to encode information.
     * Example:
     *    Let's say we want to give the answer to 5 yes or no questions.
     *    We can change the yes to 1 and no to 0, and get 1, 0, 0, 1 and 0 as the answers.
     *    If we group the numbers 1 and 0 in a sequence, we get 10010, which is a natural number,
     *    but it is represented in binary* instead of decimal.
     *    *(more info on binary to decimal: https://www.rapidtables.com/convert/number/binary-to-decimal.html)
     * 
     *    this binary: 10010 converts to decimal: 18.
     *    
     *    If the answer was 01100 it would be 12.
     *    If the answer was 00010 it would be 2, and so on.
     *    
     *    This means we can encode all answers possibilities in a number, and save memory space or execution time.
     *    
     *    Well, the code under this comment is using Bitmask to encode 8 questions:
     *     - Is your left neighbor a Terrain Tile?
     *     - Is your left/top neighbor a Terrain Tile?
     *     - Is your top neighbor a Terrain Tile?
     *     ...
     *     - Is your left/bottom neighbor a Terrain Tile?
     *     
     *    This result in a 8 bit number.
     *    That means it has 2 to the power of 8(256) possible distinct values and therefore the number range is 0 to 255.
     *    Using the same technique, we can arrive at the numbers that are needed to set a specific sprite to a tile.
     *    Example:
     *      
     *         The water sprite, with no borders, is only used when all of it's neighbors are water too.
     *         That means it has to answer yes(1) to all of the 8 questions: 11111111.
     *         This binary is converted to 255 in decimal.
     *         
     *   Now, we can check if the number variable is 255, and if so change the tile sprite to the water without borders.
     *   
     */
    {
        switch (dependencies)
        {
            case 26:
            case 58:
            case 154:
            case 186:
                return 0;
            case 82:
            case 83:
            case 114:
            case 115:
                return 1;
            case 74:
            case 78:
            case 202:
            case 206:
                return 2;
            case 88:
            case 89:
            case 92:
            case 93:
                return 3;
            case 10:
            case 14:
            case 42:
            case 46:
                return 4;
            case 18:
            case 19:
            case 146:
            case 147:
                return 5;
            case 72:
            case 73:
            case 200:
            case 201:
                return 6;
            case 80:
            case 84:
            case 112:
            case 116:
                return 7;
            case 254:
                return 8;
            case 251:
                return 9;
            case 223:
                return 10;
            case 127:
                return 11;
            case 11:
            case 15:
            case 43:
            case 47:
                return 12;
            case 31:
            case 63:
            case 159:
            case 191:
                return 13;
            case 22:
            case 23:
            case 150:
            case 151:
                return 14;
            case 107:
            case 111:
            case 235:
            case 239:
                return 15;
            case 255:
                return 16;
            case 214:
            case 215:
            case 246:
            case 247:
                return 17;
            case 104:
            case 105:
            case 232:
            case 233:
                return 18;
            case 248:
            case 249:
            case 252:
            case 253:
                return 19;
            case 208:
            case 212:
            case 240:
            case 244:
                return 20;
            case 0:
            case 1:
            case 4:
            case 32:
            case 128:
            case 5:
            case 33:
            case 129:
            case 36:
            case 132:
            case 160:
            case 37:
            case 133:
            case 161:
            case 164:
            case 165:
                return 21;
            case 8:
            case 9:
            case 40:
            case 41:
                return 22;
            case 24:
            case 25:
            case 28:
            case 56:
            case 152:
            case 29:
            case 57:
            case 153:
            case 60:
            case 156:
            case 184:
            case 61:
            case 157:
            case 188:
            case 189:
                return 23;
            case 2:
            case 3:
            case 6:
            case 7:
                return 24;
            case 66:
            case 67:
            case 70:
            case 98:
            case 194:
            case 71:
            case 99:
            case 195:
            case 102:
            case 198:
            case 226:
            case 103:
            case 199:
            case 230:
            case 231:
                return 25;
            case 90:
                return 26;
            case 64:
            case 96:
            case 192:
            case 224:
                return 27;
            case 16:
            case 20:
            case 144:
            case 148:
                return 28;
            default:
                return 16;
        }

    }

#if UNITY_EDITOR

    [MenuItem("Assets/Create/Tiles/TerrainModel")]
    public static void CreateTerrainTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save TerrainModel", "New TerrainModel", "asset", "Save TerrainModel", "Assets");
        if (path == "") return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TerrainModel>(), path);
    }

#endif

}
