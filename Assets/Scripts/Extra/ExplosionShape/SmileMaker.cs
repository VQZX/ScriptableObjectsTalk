using UnityEngine;
using System.Collections;

public class SmileMaker : MonoBehaviour
{
  public GameObject m_SmilePrefab;
  public ExplosionShape m_SmileExplosionSmall;
  public ExplosionShape m_SmileExplosionBig;

  void Update()
  {
    if (Input.GetMouseButtonUp(0))
      MakeSmileExplosion(false);
    else if (Input.GetMouseButtonUp(1))
      MakeSmileExplosion(true);	
  }

  void MakeSmileExplosion(bool isBig)
  {
    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitInfo;
    if (Physics.Raycast(mouseRay, out hitInfo, 10000.0f))
    {
      ExplosionShape es = isBig ? m_SmileExplosionBig : m_SmileExplosionSmall;
      for (int x = 0; x < es.m_width; x++)
        for (int y = 0; y < es.m_height; y++)
          if (es.m_data [x + y * es.m_width] > 0)
          {
            GameObject go = Instantiate(m_SmilePrefab, 
                        hitInfo.point + new Vector3(x, 1, y) * 2.0f,
                        Random.rotation) as GameObject;
            go.GetComponent<Rigidbody>().AddForce(Vector3.up * es.m_data [x + y * es.m_width] * 1.5f, ForceMode.Impulse);

            StartCoroutine(DestoryInTime(go, es.m_data [x + y * es.m_width]));
          }
    }
  }

  IEnumerator DestoryInTime(GameObject go, int Timer)
  {
    yield return new WaitForSeconds(Timer * 1.0f);
    Destroy(go);
  }
}
