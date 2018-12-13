using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Items
{
    sealed class ItemIndex
    {
        public static class MelleArmor
        {
            public enum Body
            {
                Body1_1 = 1,//44,
                Body1_2 = 1,//43,
                Body1_3 = 1,//44,
                Body2_1 = 1,//56,
                Body2_2 = 1,//55,
                Body2_3 = 1,//54,
                Body3_1 = 1,//62,
                Body3_2 = 1,//61,
                Body3_3 = 1,//60,
            }
            public enum Helmet
            {
                Helmet1 = 1,//53,
                Helmet2 = 1,//59,
                Helmet3 = 1,//41,
                Helmet4 = 1,//47,
            }
            public enum Gloves
            {
                Gloves1 = 1,//46,
                Gloves2 = 1,//52,
                Gloves3 = 1,//58,
                Gloves4 = 1,//40,
            }
            public enum Boots
            {
                Boots1 = 1,//39,
                Boots2 = 1,//45,
                Boots3 = 1,//51,
                Boots4 = 1,//57,
            }
            public enum Weapon
            {
                Sword1 = 1,//103,
                Sword2 = 1,//571,
                Sword3 = 1,//589,
                Sword4 = 1,//608,
                Axe1 = 1,//105,
                Axe2 = 1,//122,
                Axe3 = 1,//563,
                Axe4 = 1,//571,
            }
            public enum Shield
            {
                Shield1 = 1,//113,
                Shield2 = 1,//114,
                Shield3 = 1,//119,
                Shield4 = 1,//120,
                Shield5 = 1,//645,
                Shield6 = 1,//637,
            }
        }

        public static class RangedArmor
        {
            public enum Body
            {
                Body1 = 1,//666,
                Body2 = 1,//670,
                Body3 = 1,//443,
                Body4_1 = 1,//23,
                Body4_2 = 1,//24,
            }
            public enum Helmet
            {
                Helmet1 = 1,//33,
                Helmet2 = 1,//34,
                Helmet3 = 1,//35,
                Helmet4 = 1,//37,
                Helmet5 = 1,//41,
                Helmet6 = 1,//442,
            }
            public enum Gloves
            {
                Gloves1 = 1,//46,
                Gloves2 = 1,//441,
                Gloves3 = 1,//669,
            }
            public enum Boots
            {
                Boots1 = 1,//25,
                Boots2 = 1,//19,
                Boots3 = 1,//20,
                Boots4 = 1,//39,
            }
            public enum Weapon
            {
                Bow1 = 1,//614,
                Bow2 = 1,//615,
                Bow3 = 1,//616,
                Bow4 = 1,//617,
                Bow5 = 1,//826,
                Crossbow1 = 1,//619,
                Crossbow2 = 1,//624,
                Crossbow3 = 1,//816,
                Crossbow4 = 1,//817,
            }
        }

        public static class MagicArmor
        {
            public enum Body
            {
                Body1 = 1,//66,
                Body2 = 1,//67,
                Body3 = 1,//68,
                Body4 = 1,//69,
                Body5 = 1,//70,
                Body6 = 1,//71,
                Body7 = 1,//72,
                Body8 = 1,//73,
            }
            public enum Helmet
            {
                Helmet1 = 1,//42,
                Helmet2 = 1,//41,
                Helmet3 = 1,//33,
                Helmet4 = 1,//27,
                Helmet5 = 1,//22,
            }
            public enum Gloves
            {
                Gloves1 = 1,//46,
                Gloves2 = 1,//441,
                Gloves3 = 1,//669,
            }
            public enum Boots
            {
                Boots1 = 1,//25,
                Boots2 = 1,//19,
                Boots3 = 1,//20,
                Boots4 = 1,//39,
            }
            public enum Weapon
            {
                Staff1 = 1,//675,
                Staff2 = 1,//678,
                Staff3 = 1,//680,
                Staff4 = 1,//693,
                Staff5 = 1,//699,
                Staff6 = 1,//701,
                Staff7 = 1,//695,
                Staff8 = 1,//683,
            }
            public enum Shield
            {
                Shield1 = 1,//120,
                Shield2 = 1,//702,
                Shield3 = 1,//703,
                Shield4 = 1,//705,
                Book1 = 1,//371,
                Book2 = 1,//413,
                Book3 = 1,//412,
                Book4 = 1,//506,
                Book5 = 1,//505,
            }
        }

        public enum ItemClass
        {
            Basic = 2,//809,
            Common = 3,//810,
            Uncommon = 4,//7,
            Epic = 5,//808,
            Legendary = 6,//12,
        }

        public enum Trinket
        {
            Amulet1 = 1,//455,
            Amulet2 = 1,//456,
            Amulet3 = 1,//457,
            Amulet4 = 1,//449,
            Amulet5 = 1,//450,
            Head1 = 1,//448,
            Head2 = 1,//460,
            Head3 = 1,//414,
        }

        public static class Potions
        {
            public enum Health
            {
                Small = 1,//229,
                Medium = 1,//230,
                Large = 1,//228,
            }
            public static int Empty = 1;//214;
            public enum Mana
            {
                Small = 1,//232,
                Medium = 1,//233,
                Large = 1,//231,
            }
            public enum Armor
            {
                Small = 1,//235,
                Medium = 1,//236,
                Large = 1,//234,
            }
            public enum Strength
            {
                Small = 1,//220,
                Medium = 1,//221,
                Large = 1,//219,
            }
            public enum Dexterity
            {
                Small = 1,//216,
                Medium = 1,//217,
                Large = 1,//215,
            }
            public enum Intelligence
            {
                Small = 1,//211,
                Medium = 1,//212,
                Large = 1,//210,
            }
        }

        public static class Junk
        {
            public enum Gems
            {
                Gem1 = 1,//361,
                Gem2 = 1,//360,
                Gem3 = 1,//403,
            }
            public enum Gold
            {
                Gold1 = 1,//300,
                Gold2 = 1,//301,
                Gold3 = 1,//302,
                Gold4 = 1,//304,
                Gold5 = 1,//305,
            }
            public enum Minerals
            {
                Mineral1 = 1,//379,
                Mineral2 = 1,//380,
                Mineral3 = 1,//381,
                Mineral4 = 1,//382,
                Mineral5 = 1,//383,
                Mineral6 = 1,//384,
                Mineral7 = 1,//385,
                Mineral8 = 1,//386,
                Mineral9 = 1,//387,
                Mineral10 = 1,//388,
            }
            public enum BodyParts
            {
                BodyPart1 = 1,//422,
                BodyPart2 = 1,//451,
                BodyPart3 = 1,//775,
                BodyPart4 = 1,//776,
                BodyPart5 = 1,//777,
                BodyPart6 = 1,//778,
                BodyPart7 = 1,//779,
                BodyPart8 = 1,//780,
                BodyPart9 = 1,//781,
            }
            public enum Generic
            {
                Generic1 = 1,//272,
                Generic2 = 1,//274,
                Generic3 = 1,//280,
                Generic4 = 1,//283,
                Generic5 = 1,//270,
                Generic6 = 1,//255,
                Generic7 = 1,//278,
                Generic8 = 1,//285,
                Generic9 = 1,//303,
                Generic10 = 1,//312,
                Generic11 = 1,//333,
                Generic12 = 1,//341,
                Generic13 = 1,//355,
            }
        }

        public enum Special
        {
            No_Item = -1,//439,
            Bagpack1 = 1,//83,
            Bagpack2 = 1,//84,

        }

        public enum Gold
        {
            Small = 0,//366,
            Medium = 0,//365,
            Large = 0,//363,
        }

        public enum Food
        {
            Food1 = 1,//144,
            Food2 = 1,//145,
            Food3 = 1,//146,
            Food4 = 1,//147,
            Food5 = 1,//148,
            Food6 = 1,//149,
            Food7 = 1,//150,
            Food8 = 1,//151,
            Food9 = 1,//152,
            Food10 = 1,//153,
            Food11 = 1,//154,
            Food12 = 1,//155,
            Food13 = 1,//156,
            Food14 = 1,//157,
            Food15 = 1,//158,
        }
    }
}
