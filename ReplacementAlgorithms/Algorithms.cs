namespace ReplacementAlgorithms;

public static class Algorithms
{
    public static int Optimal(int[] currentFrames, int[] pages, int current)
    {
        var nextPages = pages[current..];
        var uniqueValues = nextPages.Where(currentFrames.Contains).Distinct();
        return !uniqueValues.Any() ? 0 : Array.IndexOf(currentFrames, uniqueValues.Last());
    }

    public static int MostUnused(int[] currentFrames, int[] pages, int current)
    {
        var previousPages = pages[..current];
        var uniqueValues = previousPages.Where(currentFrames.Contains).Distinct();
        return !uniqueValues.Any() ? 0 : Array.IndexOf(currentFrames, uniqueValues.First());
    }

    public static int FirstInFirstOut(int[] currentFrames, int[] pages, int current)
    {
        var previousPages = pages[..current];
        var uniqueValues = previousPages.Where(currentFrames.Contains);
        return !uniqueValues.Any() ? 0 : Array.IndexOf(currentFrames, uniqueValues.First());
    }
}