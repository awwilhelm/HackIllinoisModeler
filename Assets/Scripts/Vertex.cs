using UnityEngine;
using System.Collections;

namespace Modeler
{
    public class Vertex : GameBehavior{
        
        private bool isSelected;
        public Material normalMat;
        public Material selectedMat;

        private MeshRenderer meshRenderer;
        
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            isSelected = false;
        }

        public void SetSelected(bool selected)
        {
            isSelected = selected;
            if(isSelected)
            {
                meshRenderer.sharedMaterial = selectedMat;
            } else
            {
                meshRenderer.sharedMaterial = normalMat;
            }
        }

        public bool GetSelected()
        {
            return isSelected;
        }

    }
}
