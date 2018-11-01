using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Items
{
    public enum FeaturesType { IsSellAble, IsEatAble, IsEquipAble, IsInfoAble, IsUpgradeAble, IsRepairAble };

    public class ItemFeatures
    {
        [XmlElement(ElementName = "FeatureItem")]
        List<bool> Features { get; set; }

        public void EnableFeatures(params FeaturesType[] features)
        {
            foreach(FeaturesType featureType in features)
            {
                this.Features[(int)featureType] = true;
            }
        }

        public void DisableFeatures(params FeaturesType[] features)
        {
            foreach (FeaturesType featureType in features)
            {
                this.Features[(int)featureType] = false;
            }
        }

        public void EnableFeatures(List<FeaturesType> features)
        {
            foreach (FeaturesType featureType in features)
            {
                this.Features[(int)featureType] = true;
            }
        }

        public void DisableFeatures(List<FeaturesType> features)
        {
            foreach (FeaturesType featureType in features)
            {
                this.Features[(int)featureType] = false;
            }
        }

        public ItemFeatures()
        {
            this.Features = new List<bool>();
            for (int i = 0; i < (Enum.GetValues(typeof(FeaturesType))).Length; i++)
            {
                this.Features.Add(false);
            }
        }

        public List<FeaturesType> GetAvailableFeatures()
        {
            List<FeaturesType> result = new List<FeaturesType>();

            for (int i = 0; i < Enum.GetValues(typeof(FeaturesType)).Length; i++)
            {
                if(this.Features[i])
                {
                    result.Add((FeaturesType)i);
                }
            }

            return result;
        }
    }
}
