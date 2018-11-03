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
        public bool[] Features { get; set; }

        public void EnableFeatures(params ItemFeaturesType[] features)
        {
            foreach(ItemFeaturesType featureType in features)
            {
                this.Features[(int)featureType] = true;
            }
        }

        public void DisableFeatures(params ItemFeaturesType[] features)
        {
            foreach (ItemFeaturesType featureType in features)
            {
                this.Features[(int)featureType] = false;
            }
        }

        public void EnableFeatures(List<ItemFeaturesType> features)
        {
            foreach (ItemFeaturesType featureType in features)
            {
                this.Features[(int)featureType] = true;
            }
        }

        public void DisableFeatures(List<ItemFeaturesType> features)
        {
            foreach (ItemFeaturesType featureType in features)
            {
                this.Features[(int)featureType] = false;
            }
        }

        public bool GetFeatureStatus(ItemFeaturesType featureType)
        {
            return this.Features[(int)featureType];
        }

        public ItemFeatures()
        {
            this.Features = new bool[Enum.GetValues(typeof(ItemFeaturesType)).Length];
            for (int i = 0; i < (Enum.GetValues(typeof(ItemFeaturesType))).Length; i++)
            {
                this.Features[i] = false;
            }
        }

        public List<ItemFeaturesType> GetAvailableFeatures()
        {
            List<ItemFeaturesType> result = new List<ItemFeaturesType>();

            for (int i = 0; i < Enum.GetValues(typeof(ItemFeaturesType)).Length; i++)
            {
                if(this.Features[i])
                {
                    result.Add((ItemFeaturesType)i);
                }
            }

            return result;
        }
    }
}
