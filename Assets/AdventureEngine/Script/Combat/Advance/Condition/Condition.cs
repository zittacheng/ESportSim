using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition : Mark {

        public virtual bool Pass(KeyBase KB)
        {
            return true;
        }

        public virtual bool Pass(Card Source)
        {
            return true;
        }

        public override void CommonKeys()
        {
            base.CommonKeys();
        }
    }
}