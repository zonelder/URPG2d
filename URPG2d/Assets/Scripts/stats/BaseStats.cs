using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stats
{
    [System.Serializable]
    public class BaseStats//тут не производиться никаких расчетов, а просто храняться базовые значения для апределленных юнитов
    {
        //<abstract stats>
        public AbstractStrip HP;
        public AbstractStrip MP;
        public AbstractStrip SP;
        public BaseStats()
        {

        }

        public BaseStats(float hp, float mp,float sp)
        {
            HP = new AbstractStrip(hp);
            MP = new AbstractStrip(mp);
            SP = new AbstractStrip(sp);
        }
    }
}

