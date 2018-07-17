using System;
using System.Collections.Generic;

namespace Textadventure
{
    class GameStep
    {
        public Func<bool> NextStepCondition;
        
        public string DescriptionForReachingNextStep; 
        public GameStep(Func<bool> b, string s)
        {
            NextStepCondition = b;
            DescriptionForReachingNextStep = s;
        }
    }
}
