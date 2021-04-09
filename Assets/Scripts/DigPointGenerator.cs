using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.ProBuilder;

public class DigPointGenerator : MonoBehaviour
{
    public GameObject DigPointPrefab;
    private GameObject[] walls;
    private List<Vector3> DigPointList=new List<Vector3>();
    private List<GameObject> DigPointObjList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");

        var token = this.GetCancellationTokenOnDestroy();
        DigPointGenerate(token).Forget();
    }

    private async UniTask DigPointGenerate(CancellationToken token = default)
    {
        while (true)
        {
            for(int i = 0; i < 20; i++)
            {
                int randomWallNum = Random.Range(0, walls.Length);
                float wallSizeX = walls[randomWallNum].transform.localScale.x * 2 - 0.5f;
                float wallSizeZ = walls[randomWallNum].transform.localScale.z * 2 - 0.5f;
                int n = Random.Range(0, 4);
                float x=0, z=0;
                switch (n)
                {
                    case 0:
                        x = wallSizeX;
                        z = Random.Range(-wallSizeZ, wallSizeZ);
                        break;
                    case 1:
                        x = -wallSizeX;
                        z = Random.Range(-wallSizeZ, wallSizeZ);
                        break;
                    case 2:
                        x = Random.Range(-wallSizeX, wallSizeX);
                        z = wallSizeX;
                        break;
                    case 3:
                        x = Random.Range(-wallSizeX, wallSizeX);
                        z = -wallSizeX;
                        break;
                    default:
                        x = 0;
                        z = 0;
                        break;
                }

                if (DigPointObjList.Count < i + 1)
                {
                    var obj = Instantiate(DigPointPrefab);
                    DigPointObjList.Add(obj);
                    DigPointList.Add(Vector3.zero);
                }

                Vector3 pos = walls[randomWallNum].transform.position+new Vector3(x, 0.5f, z);
                DigPointList[i] = pos;
                DigPointObjList[i].transform.position = DigPointList[i];
            }

            await UniTask.Delay(300 * 1000);
        }
    }
}
