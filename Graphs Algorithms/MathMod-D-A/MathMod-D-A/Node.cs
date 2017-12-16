using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMod_D_A
{
    class Node
    {
        public int? Id = null;
        public bool Checked = false;
        public int Price = 0;
        public Node backNode = null;
        public List<Node> NodeYouMayGo = new List<Node>();
    }
}
