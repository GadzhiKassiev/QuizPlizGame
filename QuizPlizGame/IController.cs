using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizPlizGame
{
    public interface IController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="act"></param>
        void WaitForUserChoiceOption(Action<int> act);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="act"></param>
        void WaitForUserChoiceAnswer(Action<int> act);

    }
}
