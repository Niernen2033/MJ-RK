using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public class EatableItem : Item
    {
        public int HealValue { get; set; }

        public EatableItem() : base()
        {
            this.HealValue = 0;
        }

        public EatableItem(int healValue, ItemClass itemClass, ItemIcon itemIcon, string name, int goldValue, double weight) 
            : base(itemClass, ItemType.None, itemIcon, name, goldValue, weight)
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
