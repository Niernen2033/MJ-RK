using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveLoad
{
    static class SaveInfo
    {
        //class
        public static class Paths
        {
            public static readonly string GlobalFolder = @"Assets/Saves/";
            public static readonly string ResourcesFolder = @"Assets/Resources/";
            public static class Resources
            {
                public static readonly string ImagesFolder = @"Images/";
                public static readonly string MusicFolder = @"Music/";
                public static class Images
                {
                    public static readonly string InventoryFolder = @"Images/Inventory/";
                    public static class Inventory
                    {
                        public static readonly string AllItems = @"Images/Inventory/all_items";
                    }
                }            
            }
        }
    }
}
