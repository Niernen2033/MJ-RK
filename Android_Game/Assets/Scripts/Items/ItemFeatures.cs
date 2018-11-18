using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Items
{
    public enum ItemFeaturesType { IsSellAble, IsEatAble, IsEquipAble, IsInfoAble, IsUpgradeAble, IsRepairAble, IsDeleteAble };

    public class ItemFeatures
    {
        [XmlElement(ElementName = "FeatureItem")]
        public bool[] GetFeatures { get; set; }

        public void EnableFeatures(params ItemFeaturesType[] features)
        {
            foreach(ItemFeaturesType featureType in features)
            {
                this.GetFeatures[(int)featureType] = true;
            }
        }

        public void DisableFeatures(params ItemFeaturesType[] features)
        {
            foreach (ItemFeaturesType featureType in features)
            {
                this.GetFeatures[(int)featureType] = false;
            }
        }

        public void EnableFeatures(List<ItemFeaturesType> features)
        {
            foreach (ItemFeaturesType featureType in features)
            {
                this.GetFeatures[(int)featureType] = true;
            }
        }

        public void DisableFeatures(List<ItemFeaturesType> features)
        {
            foreach (ItemFeaturesType featureType in features)
            {
                this.GetFeatures[(int)featureType] = false;
            }
        }

        public bool GetFeatureStatus(ItemFeaturesType featureType)
        {
            return this.GetFeatures[(int)featureType];
        }

        public void EnableAllFeatures()
        {
            for (int i = 0; i < (Enum.GetValues(typeof(ItemFeaturesType))).Length; i++)
            {
                this.GetFeatures[i] = true;
            }
        }

        public ItemFeatures()
        {
            this.GetFeatures = new bool[Enum.GetValues(typeof(ItemFeaturesType)).Length];
            for (int i = 0; i < (Enum.GetValues(typeof(ItemFeaturesType))).Length; i++)
            {
                this.GetFeatures[i] = false;
            }
        }

        public ItemFeatures(ItemFeatures itemFeatures)
        {
            this.GetFeatures = new bool[Enum.GetValues(typeof(ItemFeaturesType)).Length];
            for (int i = 0; i < (Enum.GetValues(typeof(ItemFeaturesType))).Length; i++)
            {
                this.GetFeatures[i] = itemFeatures.GetFeatures[i];
            }
        }

        public List<ItemFeaturesType> GetAvailableFeatures()
        {
            List<ItemFeaturesType> result = new List<ItemFeaturesType>();

            for (int i = 0; i < Enum.GetValues(typeof(ItemFeaturesType)).Length; i++)
            {
                if(this.GetFeatures[i])
                {
                    result.Add((ItemFeaturesType)i);
                }
            }

            return result;
        }
    }
}
