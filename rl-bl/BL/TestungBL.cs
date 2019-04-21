using System;
using rl_contract.Models;

namespace rl_bl
{
    public class TestungBL
    {
        public TestungBL()
        {

        }

        public void calculateScore(Testung _testung)
        {
            _testung.Chapters.ForEach(chapter =>
            {
                var scoreForChapter = 0;
                var index = 0;
                chapter.Questions.ForEach(question =>
                {
                    if (question.Type == "radio" && question.Value != "")
                    {
                        int qScore = -1;
                        Int32.TryParse(question.Value, out qScore);
                        if (qScore >= 0)
                        {
                            scoreForChapter += qScore;
                            index++;
                        }
                    }
                });
                chapter.Score = (scoreForChapter > 0) ? scoreForChapter / index: -1;
            });
        }
    }
}
