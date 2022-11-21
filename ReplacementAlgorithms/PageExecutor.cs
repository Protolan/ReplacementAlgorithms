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
        var buffer = new Queue<int>();
        int currentIndex = 0;
        for (int i = 0; i < _pages.Length; i++)
        {
            if (_currentFrames.Contains(_pages[i]))
            {
                logger.AddPage(_currentFrames, false);
                continue;
            }

            if (currentIndex < _currentFrames.Length)
            {
                buffer.Enqueue(currentIndex);
                _currentFrames[currentIndex] = _pages[i];
                currentIndex++;
                logger.AddPage(_currentFrames, false);
                continue;
            }
            var index = algorithm switch
            {
                Algorithm.Optimal => Optimal(_currentFrames, _pages, i),
                Algorithm.MostUnused => MostUnused(_currentFrames, _pages, i),
                Algorithm.FirstInFirstOut => FirstInFirstOut(buffer)
            };
            _currentFrames[index] = _pages[i];
            logger.AddPage(_currentFrames, true);
        }
        
    }
    
    public static int Optimal(int[] currentFrames, int[] pages, int current)
    {
        
        var nextPages = pages[current..];
        int farFrameIndex = 0;
        int maxDistant = 0;
        
        for (var j = 0; j < currentFrames.Length; j++)
        {
            var frame = currentFrames[j];
            int i;
            for (i = 0; i < nextPages.Length; i++)
            {
                if (frame != nextPages[i]) continue;
                if (i > maxDistant)
                {
                    maxDistant = i;
                    farFrameIndex = j;
                }
                break;
            }
    
            if (i == nextPages.Length)
                return j;
        }
        return farFrameIndex;
    }
    
    public static int MostUnused(int[] currentFrames, int[] pages, int current)
    {
        var previousPages = pages[..current];
        int mostUnusedPosition = current;
        int mostUnusedElementIndex = currentFrames[0];
        for (var i = 0; i < currentFrames.Length; i++)
        {
            var frame = currentFrames[i];
            for (int j = previousPages.Length - 1; j >= 0; j--)
            {
                if (frame != previousPages[j]) continue;
                if (j < mostUnusedPosition)
                {
                    mostUnusedPosition = j;
                    mostUnusedElementIndex = i;
                }  
                break;
            }
        }

        return mostUnusedElementIndex;
    }

    public static int FirstInFirstOut(Queue<int> buffer)
    {
        var frame =  buffer.Dequeue();
        buffer.Enqueue(frame);
        return frame;
    }
}