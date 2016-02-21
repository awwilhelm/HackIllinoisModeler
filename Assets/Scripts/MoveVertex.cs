using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Modeler
{
    [RequireComponent(typeof(SelectVertex))]
    public class MoveVertex : GameBehavior
    {
        public GameObject moveTool;
        private List<GameObject> selectedVerts;
        private bool isMoveInstantiated;

        void Start()
        {
            selectedVerts = GetComponent<SelectVertex>().GetSelected();
            isMoveInstantiated = false;
        }

        void Update()
        {
            selectedVerts = GetComponent<SelectVertex>().GetSelected();
            if (selectedVerts.Count > 0 && !isMoveInstantiated)
            {
                isMoveInstantiated = true;
                Instantiate(moveTool, selectedVerts[0].transform.position, Quaternion.identity);
            }
        }
    }
}
