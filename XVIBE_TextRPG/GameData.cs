using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

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
    }
}
