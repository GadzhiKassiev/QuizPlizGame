using System;

namespace QuizPlizGame
{
    /// <summary>
    /// Класс для перетасовки вопросов игры.
    /// </summary>
    internal class QuizQuestionShuffle
    {
        /// <summary>
        /// Метод перестановкий использующий алгоритм тасования Фишера — Йетса
        /// </summary>
        /// <param name="qp">массив вопросов</param>
        /// <returns>перетасованный массив вопросов</returns>
        public static QuizQuestion[] SimpleShuffle(QuizQuestion[] qp)
        {
            Random rnd = new Random();

            for (int i = 0; i < qp.Length; i++)
            {
                int r = rnd.Next(0, i + 1);
                QuizQuestion swap = qp[i];
                qp[i] = qp[r];
                qp[r] = swap;
            }
            return qp;
        }
    }
}
