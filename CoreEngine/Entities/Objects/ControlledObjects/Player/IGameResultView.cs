namespace CoreEngine.Entities.Objects.ControlledObjects.Player;

public interface IGameResultView
{
    void OnPlayerDead(object sender);
    void ScoreUpdate(int score);
}