using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using static XVIBE_TextRPG.Equipment;

namespace XVIBE_TextRPG
{
    public class GameData
    {
        public string Name { get; set; }
        public string Job { get; set; }

        public int Exp { get; set; }

        public int Level { get; set; }

        public int Gold { get; set; }

        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int MaxMP { get; set; }
        public int CurrentMP { get; set; }
        public int TotalATK { get; set; }
        public int TotalDEF { get; set; }        

        public List<WeaponData> Inventory { get; set; } = new List<WeaponData>();
        public WeaponData EquippedWeapon { get; set; }
    }

    public class WeaponData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int ATK { get; set; }
        public int Price { get; set; }
    }
}
