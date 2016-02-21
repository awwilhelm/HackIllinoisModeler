using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Modeler
{
    public class SelectVertex : GameBehavior
    {
        public List<GameObject> selectedVertices;

        void Start()
        {
            selectedVertices = new List<GameObject>();
        }
        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShootRay();
            }
        }

        void ShootRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawLine(hitInfo.point, hitInfo.point.normalized*5, Color.red);
                if (hitInfo.transform.tag == "Vertex")
                {
                    Vertex vertexRef = hitInfo.transform.GetComponent<Vertex>();
                    vertexRef.SetSelected(!vertexRef.GetSelected());
                    if(vertexRef.GetSelected())
                    {
                        AddVertexToSelected(hitInfo.transform.gameObject);
                    }
                    else
                    {
                        RemoveVertextFromSelected(hitInfo.transform.gameObject);
                    }

                }
            } else
            {
                ClearVertexFromSelected();
            }
        }

        void AddVertexToSelected(GameObject vertex)
        {
            selectedVertices.Add(vertex);
        }

        void RemoveVertextFromSelected(GameObject vertex)
        {
            selectedVertices.Remove(vertex);
        }

        void ClearVertexFromSelected()
        {
            for(int i = 0; i< selectedVertices.Count; i++)
            {
                selectedVertices[i].GetComponent<Vertex>().SetSelected(false);
            }
            selectedVertices.Clear();
        }

        public List<GameObject> GetSelected()
        {
            return selectedVertices;
        }
    }
}
