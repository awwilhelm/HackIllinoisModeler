using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Modeler
{
    [RequireComponent(typeof(SelectVertex))]
    public class MoveVertex : GameBehavior
    {
        public GameObject moveTool;
        public GameObject meshGameObject;
        private List<GameObject> selectedVerts;
        private GameObject moveToolInstance;
        private bool buttonDown;
        private bool moveXAxis;
        private bool moveYAxis;
        private bool moveZAxis;
        private Vector3 mouseStartingSpot;

        private const float MOVE_MULTIPLIER = 12;

        void Start()
        {
            selectedVerts = GetComponent<SelectVertex>().GetSelected();
            buttonDown = false;
            moveXAxis = moveYAxis = moveZAxis = false;

        }

        void Update()
        {
            
            if(Input.GetMouseButtonDown(0))
            {
                buttonDown = true;
                if (selectedVerts.Count > 0)
                {

                    Vector3 v3 = Input.mousePosition;
                    v3.z = selectedVerts[0].transform.position.z;
                    v3.z = Camera.main.nearClipPlane;
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    mouseStartingSpot = v3;
                    for(int i = 0; i<selectedVerts.Count; i++)
                    {
                        selectedVerts[i].GetComponent<Vertex>().SetStartingPosition();
                    }
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                buttonDown = false;
                moveXAxis = moveYAxis = moveZAxis = false;
            }

            InstantiateMoveTool();
            SetMoveToolSelection();
            MoveSelected();
            if(buttonDown)
            {
                meshGameObject.GetComponent<GenerateMesh>().RecalcVertices();
            }
            
            
        }

        void MoveSelected()
        {
            if (selectedVerts.Count > 0)
            {

                if (moveXAxis || moveYAxis || moveZAxis)
                {
                    StartCoroutine(MoveAxisBasedOnMouse());
                }

                Vector3 sumVec = Vector3.zero;
                for(int i = 0; i<selectedVerts.Count; i++)
                {
                    sumVec = new Vector3(sumVec.x + selectedVerts[i].transform.position.x, sumVec.y + selectedVerts[i].transform.position.y, sumVec.z + selectedVerts[i].transform.position.z);
                }
                moveToolInstance.transform.position = new Vector3(sumVec.x/selectedVerts.Count, sumVec.y/selectedVerts.Count, sumVec.z/selectedVerts.Count);
                //moveTool.transform.position = new Vector3(0, 0, 0);
            }
        }
        IEnumerator MoveAxisBasedOnMouse()
        {
            //Waits 1 frame
            yield return 0;


            for (int i = 0; i < selectedVerts.Count; i++)
            {
                Vector3 v3 = Input.mousePosition;
                print("hiiii      " + v3);
                v3.z = selectedVerts[i].transform.position.z;
                v3.z = Camera.main.nearClipPlane;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                if (moveXAxis)
                {
                    selectedVerts[i].transform.position = new Vector3(selectedVerts[i].GetComponent<Vertex>().GetStartingPosition().x - (mouseStartingSpot.x - v3.x) * MOVE_MULTIPLIER, selectedVerts[i].transform.position.y, selectedVerts[i].transform.position.z);
                }
                else if (moveYAxis)
                {
                    selectedVerts[i].transform.position = new Vector3(selectedVerts[i].transform.position.x, selectedVerts[i].GetComponent<Vertex>().GetStartingPosition().y - (mouseStartingSpot.y - v3.y)*MOVE_MULTIPLIER, selectedVerts[i].transform.position.z);
                }
                else if (moveZAxis)
                {
                    selectedVerts[i].transform.position = new Vector3(selectedVerts[i].transform.position.x, selectedVerts[i].transform.position.y, 
                        selectedVerts[i].GetComponent<Vertex>().GetStartingPosition().z - (mouseStartingSpot.z - v3.z) * MOVE_MULTIPLIER);

                }
            }


        }

        void SetMoveToolSelection()
        {
            if (moveToolInstance && buttonDown && !moveXAxis && !moveYAxis && !moveZAxis)
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
                if (hitInfo.transform.tag == "xAxis")
                {
                    moveXAxis = true;
                } else
                {
                    moveXAxis = false;
                }

                if (hitInfo.transform.tag == "yAxis")
                {
                    moveYAxis = true;
                }
                else
                {
                    moveYAxis = false;
                }

                if (hitInfo.transform.tag == "zAxis")
                {
                    moveZAxis = true;
                }
                else
                {
                    moveZAxis = false;
                }
            }
        }

        void InstantiateMoveTool()
        {
            selectedVerts = GetComponent<SelectVertex>().GetSelected();
            if (selectedVerts.Count > 0 && !moveToolInstance)
            {
                moveToolInstance = Instantiate(moveTool, selectedVerts[0].transform.position, Quaternion.identity) as GameObject;
            }
            else if (selectedVerts.Count == 0)
            {
                if (moveToolInstance)
                {
                    Destroy(moveToolInstance);
                }
            }
        }
    }
}
