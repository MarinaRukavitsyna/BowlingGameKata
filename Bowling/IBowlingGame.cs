namespace Bowling
{
    /// <summary>
    /// The Bowling game interface.
    /// </summary>
    public interface IBowlingGame
	{
        /// <summary>
        /// Create Scoresheet with random rolls.
        /// </summary>
        void CreateScoresheet();

        /// <summary>
        /// Get scoresheet.
        /// </summary>
        /// <returns>The scoresheet list.</returns>
        List<int> GetScoresheet();

        /// <summary>
        /// Returns the score for each frame.
        /// </summary>
        /// <returns>The score for each frame</returns>
        List<int> GetFramesScores();
    }
}