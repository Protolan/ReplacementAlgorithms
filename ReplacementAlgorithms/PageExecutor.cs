namespace ReplacementAlgorithms;

public class PageExecutor
{
    public enum Algorithm
    {
        Optimal,
        MostUnused,
        FirstInFirstOut,
    }
    private readonly int[] _currentFrames;
    private readonly int[] _pages;

    public PageExecutor(int framesCount, int[] pages)
    {
        _currentFrames = new int[framesCount];
        _pages = pages;
    }

    public void Execute(Algorithm algorithm, PageLogger logger)
    {
        for (var i = 0; i < _currentFrames.Length; i++)
        {
            _currentFrames[i] = _pages[i];
            logger.AddPage(_currentFrames, false);
        }
        for (int i = _currentFrames.Length; i < _pages.Length; i++)
        {
            if (_currentFrames.Contains(_pages[i]))
            {
                logger.AddPage(_currentFrames, false);
                continue;
            }
            var index = algorithm switch
            {
                Algorithm.Optimal => Optimal(_currentFrames, _pages, i),
                Algorithm.MostUnused => MostUnused(_currentFrames, _pages, i),
                Algorithm.FirstInFirstOut => FirstInFirstOut(_currentFrames, _pages, i),
            };
            _currentFrames[index] = _pages[i];
            logger.AddPage(_currentFrames, true);
        }
        
    }
    
    public static int Optimal(int[] currentFrames, int[] pages, int current)
    {
        var nextPages = pages[current..];
        var uniqueValues = nextPages.Where(currentFrames.Contains).Distinct();
        return !uniqueValues.Any() ? 0 : Array.IndexOf(currentFrames, uniqueValues.Last());
    }

    public static int MostUnused(int[] currentFrames, int[] pages, int current)
    {
        return 0;
    }

    public static int FirstInFirstOut(int[] currentFrames, int[] pages, int current)
    {
        var previousPages = pages[..current];
        var uniqueValues = previousPages.Where(currentFrames.Contains);
        return !uniqueValues.Any() ? 0 : Array.IndexOf(currentFrames, uniqueValues.First());
    }
}