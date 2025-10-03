using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ScreenSaver
{
    public class Snowflake
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Image Image { get; set; }
        public float SizeMultiplier { get; set; }
        public float FallSpeed { get; set; }
        public float SwaySpeed { get; set; }
        public float SwayAmount { get; set; }
        public float SwayOffset { get; set; }
    }
}
