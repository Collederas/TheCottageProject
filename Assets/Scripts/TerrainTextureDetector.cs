using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class TerrainTextureDetector : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    private UnityEvent Event;

    private void Start()
    {
        GetAlphaMapsAtObjectLocation();
    }

    public float[,,] GetAlphaMapsAtObjectLocation()
    {
        var terrainData = terrain.terrainData;
        var ownerLocation = transform.position;

        Vector3 objectTerrainLocation = new Vector3(ownerLocation.x / terrainData.size.x,
            0, ownerLocation.z / terrainData.size.z);
        Vector2 objectAlphaMapLocation = new Vector2(objectTerrainLocation.x * terrainData.alphamapWidth,
            objectTerrainLocation.z * terrainData.alphamapHeight);
        
       return terrainData.GetAlphamaps((int)objectAlphaMapLocation.x, (int)objectAlphaMapLocation.y, 1, 1);
    }

    /// <summary>
    /// Get index of the texture that has the higher alphamap weight.
    /// See Terrain's Layers for index reference.
    /// </summary>
    public int GetPrevalentTextureIndex()
    {
        float[] textureWeights = new float[terrain.terrainData.alphamapLayers];

        if (!terrain)
            return 0;

        var aMap = GetAlphaMapsAtObjectLocation();

        for (int i = 0; i < terrain.terrainData.alphamapLayers; i++)
        {
            textureWeights[i] = aMap[0, 0, i];
        }
        
        return Array.IndexOf(textureWeights, textureWeights.Max());
    }

    /// <summary>
    /// Print debug values at current owner's position.
    /// </summary> 
    public void DebugAtOwnerLocation()
    {
        var ownerLocation = transform.position;
        float[,,] alphamaps = GetAlphaMapsAtObjectLocation();
        Debug.LogFormat("Alpha map for grass at {0} is: {1}", ownerLocation, alphamaps[0,0,0]);
        Debug.LogFormat("Alpha map for moss at {0} is: {1}", ownerLocation, alphamaps[0,0,1]);
        Debug.LogFormat("Alpha map for mulch at {0} is: {1}", ownerLocation, alphamaps[0,0,2]);
        Debug.LogFormat("Alpha map for rocky terrain at {0} is {1}", ownerLocation, alphamaps[0,0,3]);

        print("Prevalent texture index: " + GetPrevalentTextureIndex());
    }
}
