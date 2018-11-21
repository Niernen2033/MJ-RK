using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPC;

namespace Items
{
    public class ConsumeableItem : Item
    {
        public int HealValue { get; set; }

        public ConsumeableItem() : base()
        {
            this.HealValue = 0;
        }

        public ConsumeableItem(int healValue, ItemClass itemClass, ItemType itemType, ItemSubType itemSubType, ItemIcon itemIcon, ItemFeatures itemFeatures,
            string basicName, string additionalName, int goldValue, double weight)
            : base(itemClass, itemType, itemSubType, itemIcon, itemFeatures, basicName, additionalName, goldValue, weight, 0)
        {
            this.HealValue = healValue;
        }

        public void Eat(Champion champion)
        {
            int tragetVitalityValue = (int)champion.Vitality.Acctual + this.HealValue;
            if(tragetVitalityValue <= champion.Vitality.Basic)
            {
                champion.Vitality.ChangeAcctualValue(tragetVitalityValue);
            }
            else
            {
                champion.Vitality.ChangeAcctualValue(champion.Vitality.Basic);
            }
        }
    }
}
