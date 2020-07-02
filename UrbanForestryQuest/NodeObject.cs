using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class NodeObject : MonoBehaviour
    {
        public int posX;
        public int posZ;
        public int textureId;
        public int multiplier;

        // Function called when loading object
        public void UpdatedNodeObject(Node curNode, NodeObjectSaveable saveable)
        {
            posX = saveable.posX;
            posZ = saveable.posZ;
            textureId = saveable.textureId;
            multiplier = saveable.multiplier;

            ChangeMaterial(curNode);
        }

        void ChangeMaterial(Node curNode)
        {
            Material getMaterial = ResourceManager.GetInstance().GetMaterial(textureId);
            curNode.tileRenderer.material = getMaterial;
        }


        // Get object to be saved
        public NodeObjectSaveable GetSaveable()
        {
            NodeObjectSaveable saveable = new NodeObjectSaveable();
            saveable.posX = this.posX;
            saveable.posZ = this.posZ;
            saveable.textureId = this.textureId;
            saveable.multiplier = this.multiplier;

            return saveable;
        }

    }

    [System.Serializable]
    public class NodeObjectSaveable
    {
        public int posX;
        public int posZ;
        public int textureId;
        public int multiplier;
    }
}
