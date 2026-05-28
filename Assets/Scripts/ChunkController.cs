using AuxiliarClasses;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public static ChunkObject[] chunkList { get; set; }

    public GameObject chunkAsset;

    [SerializeField] Transform player;
    [Range(5, 100)]
    public int radius = 5;
    public static int chunkSide { get; private set; } // Cantidad de chunks de lado a lado del mapa, se asigna desde el método CreateChunks al crear los chunks, se utiliza para gestionar los chunks y sus LoDs dentro del código chunkObject

    int baseSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false; // Deshabilitado para que no ejecute el método Update, que se encargará de cargar los LoDs según la distancia a jugador luego de la primera carga
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateChunks(WorldVertex[] worldVertices, int _chunkSide, int density) // Primera carga de LoDs con el centro o pos de jugador guardada como referencia
    {
        chunkSide = _chunkSide;
        ChunkObject chunkAssetScript;
        
        int chunkSideVertices = baseSize * density + 1;
        int mapSideVertices = chunkSide * baseSize * density + 1;
        WorldVertex[] _vertices = new WorldVertex[chunkSideVertices * chunkSideVertices];

        for (int z = 0; z < chunkSide; z++)
        {
            for (int x = 0; x < chunkSide; x++)
            {
                for (int cZ = 0; cZ < chunkSideVertices; cZ++)
                {   for (int cX = 0; cX < chunkSideVertices; cX++)
                    {
                        _vertices[cX + cZ * mapSideVertices] = worldVertices[z * mapSideVertices * (chunkSideVertices - 1) + x * (chunkSideVertices - 1) + cZ * mapSideVertices + cX]; // Añade el rango de vértices correspondiente al chunk a la lista de vértices del chunk
                    }                        
                }

                chunkAssetScript = Instantiate(chunkAsset, transform.position + new Vector3(x * baseSize, 0, z * baseSize), Quaternion.identity).GetComponent<ChunkObject>(); // Instancia el chunk en la posición correspondiente
                chunkAssetScript.DataUpdate(_vertices, chunkSideVertices, chunkSideVertices); // Actualiza los datos del chunk con la lista de vértices del chunk y sus dimensiones
                chunkAssetScript.indexPos = z * chunkSide + x; // Guarda la posición del chunk en la lista de chunks para su posterior gestión en la unión y separación de chunks
                chunkAssetScript.density = density;

                chunkList[x + z * chunkSide] = chunkAssetScript; // Añade el chunk a la lista de chunks para su posterior gestión
            }
        }

        ChunkFirstLoad(); // Carga de Lods según distancia a jugador
    }

    void ChunkFirstLoad() // Carga de Lods según distancia a jugador
    {   
        int chunkListCenter = chunkList.Count - chunkList.Count / 2;
        if (radius < chunkSide / 2)
        {
            for
        }
        // chunkList.ForEach(chunk => chunk.gameObject.SetActive(Vector3.Distance(player.position, chunk.transform.position) < radius * baseSize)); // Activa o desactiva el chunk según la distancia a jugador y el radio de carga
        enabled = true; // Habilita el script para que se ejecute el método Update, que se encargará de cargar los LoDs según la distancia a jugador luego de la primera carga
    }

    void LoDCheck() // Comprueba posición de jugador para cargar LoDs según distancia
    {

    }


    void ChunkLoad() // Renderizado de los Chunks
    {

    }
}