using Bowling;
using Xunit.Abstractions;
using static System.Formats.Asn1.AsnWriter;

namespace BowlingGameTests;

public class GameTest
{
    private readonly ITestOutputHelper output;

    public GameTest(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void TestInitGame_ThrowsArgumentException(int frames)
    {
        //Arrange //Act //Assert
        Assert.Throws<ArgumentException>( () =>
        {
            var game = new BowlingGame(frames);
        });
    }

    [Fact]
    public void TestInitGame()
    {
        //Arrange //Act
        var game = new BowlingGame(10);

        //Assert
        Assert.NotNull(game);

        game = null;
    }

    [Fact]
    public void TestCreateScoresheet()
    {
        //Arrange
        var game = new BowlingGame(10);

        //Act
        game.CreateScoresheet();

        //Assert
        Assert.NotNull(game);

        game = null;
    }

    [Fact]
    public void TestGame()
    {
        //Arrange
        var game = new BowlingGame(10);
        game.CreateScoresheet();

        //Act
        var score = game.GetFramesScores();

        //Assert
        Assert.NotNull(score);

        game = null;
    }

    [Fact]
    public void OutputGame()
    {
        //Arrange
        var game = new BowlingGame(10);
        game.CreateScoresheet();

        //Act
        var scoresheet = game.GetScoresheet();
        var scores = game.GetFramesScores();

        //Assert
        foreach (var score in scoresheet)
        {
            output.WriteLine(score.ToString());
        }

        output.WriteLine("frame scores");
        foreach (var score in scores)
        {
            output.WriteLine(score.ToString());
        }       

        game = null;
    }
}