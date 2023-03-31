using System;

namespace Bowling
{
    /// <inheritdoc cref="IBowlingGame"/>
    public class BowlingGame: IBowlingGame
    {
        const int pinCount = 10;

        int frameCount;
        List<int> scoreTable;

        /// <summary>
        /// Init a new game.
        /// </summary>
        /// <param name="frameCount">The number of frames.</param>
        public BowlingGame(int frameCount)
        {
            if (frameCount < 2)
            {
                throw new ArgumentException($"'{nameof(frameCount)}' cannot be less than 2.", nameof(frameCount));
            }

            this.scoreTable = new List<int>(frameCount * 2 + 1);
            this.frameCount = frameCount;
        }
       
        public void CreateScoresheet()
        {
            Random r = new Random();

            // Add spare
            DoRoll(8);
            DoRoll(2);

            for (int i = 1; i < frameCount; i++)
            {
                var firstTry = r.Next(0, 10);
                DoRoll(firstTry);

                var secondTry = r.Next(0, 10 - firstTry);
                DoRoll(secondTry);
            }

            if (IsStrike(scoreTable[frameCount * 2 - 1]))
            {
                var bonusTry = r.Next(0, 10);
                DoRoll(bonusTry);
            }

            if (IsSpare(scoreTable[frameCount * 2 - 1]))
            {
                var firstBonusTry = r.Next(0, 10);
                DoRoll(firstBonusTry);

                var secondBonusTry = r.Next(0, 10 - firstBonusTry);
                DoRoll(secondBonusTry);
            }
        }
       
        public List<int> GetScoresheet()
        {
            return scoreTable;
        }
      
        public List<int> GetFramesScores()
        {
            if (scoreTable is null || !scoreTable.Any())
            {
                throw new ArgumentException($"'{nameof(scoreTable)}' cannot be empty.", nameof(scoreTable));
            }

            var framesScore = new List<int>();
            var roll = 0;

            for (int i = 0; i < frameCount; i++)
            {
                if (IsStrike(roll))
                {
                    framesScore.Add(SumStrike(roll));
                    roll++;
                }
                if (IsSpare(roll))
                {
                    framesScore.Add(SumSpare(roll));
                    roll += 2;
                }
                else
                {
                    // 'Open frame'
                    framesScore.Add(SumOpenFrame(roll));
                    roll += 2;
                }
            }

            return framesScore;

            int SumStrike(int roll)
            {
                return pinCount + scoreTable[roll + 1] + scoreTable[roll + 2];
            }

            int SumSpare(int roll)
            {
                return pinCount + scoreTable[roll + 2];
            }

            int SumOpenFrame(int roll)
            {
                return scoreTable[roll] + scoreTable[roll + 1];
            }   
        }

        /// <summary>
        /// It is called each time the player rolls a ball.
        /// </summary>
        /// <param name="pins">The number of pins knocked down.</param>
        private void DoRoll(int pins)
        {
            scoreTable.Add(pins);
        }

        /// <summary>
        /// A spare is when the player knocks down all 10 pins in two tries.
        /// </summary>
        /// <param name="roll">The roll try.</param>
        /// <returns>If the roll is spare.</returns>
        private bool IsSpare(int roll)
        {
            return scoreTable[roll] + scoreTable[roll + 1] == pinCount;
        }

        /// <summary>
        /// A strike is when the player knocks down all 10 pins on his first try.  
        /// </summary>
        /// <param name="roll">The roll try.</param>
        /// <returns>If the roll is strike.</returns>
        private bool IsStrike(int roll)
        {
            return scoreTable[roll] == pinCount;
        }
    }
}