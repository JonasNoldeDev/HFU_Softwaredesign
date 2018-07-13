using System;
using System.Collections.Generic;

namespace Textadventure
{
    class GameStep
    {
        public bool NextStepCondition;
        public string DescriptionForReachingNextStep; 
        public GameStep(bool b, string s)
        {
            NextStepCondition = b;
            DescriptionForReachingNextStep = s;
        }
    }
}
