using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saiph_Level_Editor
{
    internal class Node
    {
        //Node attributes
        float x;
        float y;
        float wait;
        float interval;
        int enemy;
        int amount;

        //accessors
        public float GetX() {return x;}
        public float GetY() {return y;}
        public float GetWait() {return wait;}
        public float GetInterval() {return interval;}
        public int GetEnemy() {return enemy;}
        public int GetAmount() {return amount;}
        
        //mutators
        private void SetX(int x) {this.x = x;}
        private void SetY(int y) {this.y = y;}
        private void SetWait(int wait) {this.wait = wait;}
        private void SetInterval(float interval) {this.interval = interval;}
        private void SetEnemy(int enemy) {this.enemy = enemy;}
        private void SetAmount(int amount) {this.amount = amount;}
        

        //constructor
        public Node(float x, float y, float wait, float interval, int enemy, int amount)
        {
            this.x = x;
            this.y = y;
            this.wait = wait;
            this.interval = interval;
            this.enemy = enemy;
            this.amount = amount;
        }
        
        //copy constructor
        public Node(Node _copy)
        {
            this.x = _copy.GetX();
            this.y = _copy.GetY();
            this.wait = _copy.GetWait();
            this.interval = _copy.GetInterval();
            this.enemy = _copy.GetEnemy();
            this.amount = _copy.GetAmount();
        }
        


        //Overrides

        //To String
        public override string ToString()
        {
            if (amount > 1)
            {
                return "Spawn " + amount + " enemies at (" + x + ", " + y + ") after " + wait + "ms every " + interval + " ms.";
            }
            else
            {
                return "Spawn " + amount + " enemy at (" + x + ", " + y + ") after " + wait / 1000 + "ms.";
            }
        }


        //Equals
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Node other = obj as Node;
            if ((System.Object)other == null)
            {
                return false;
            }
            return (this.x == other.x) && (this.y == other.y) && (this.wait == other.wait) && (this.interval == other.interval) && (this.enemy == other.enemy) && (this.amount == other.amount);
        }
        
        //==
        public static bool operator ==(Node n1, Node n2)
        {
            return (n1.x == n2.x) && (n1.y == n2.y) && (n1.wait == n2.wait) && (n1.interval == n2.interval) && (n1.enemy == n2.enemy) && (n1.amount == n2.amount);
        }
        public static bool operator !=(Node n1, Node n2)
        {
            return !n1.Equals(n2);
        }

        //override the get hash code
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
