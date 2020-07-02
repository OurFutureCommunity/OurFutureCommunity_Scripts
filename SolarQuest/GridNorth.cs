namespace SolarQuest
{
    public class GridNorth : GridGenerator
    {
        public override void UpdateGridScore()
        {
            float count = 0;
            for (int r = 1; r < row + 1; r++)
            {
                for (int c = 1; c < col + 1; c++)
                {
                    if (occupied[r, c] > 0)
                    {
                        count++;
                    }
                }
            }
            gridScore = UnityEngine.Mathf.RoundToInt(count * 0.1f);
            solarGame.GetComponent<SolarGame>().UpdateScore();
        }
    }

}
