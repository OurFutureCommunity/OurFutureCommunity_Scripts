namespace SolarQuest
{
    public class GridSouth : GridGenerator
    {

        public override void UpdateGridScore()
        {
            int count = 0;
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
            gridScore = count;
            solarGame.GetComponent<SolarGame>().UpdateScore();
        }
    }
}

