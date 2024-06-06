namespace Framework.Lib.DataReaders;

public class CsvFileDataReader : IDataReader
{
    private readonly string filePath;

    private readonly int[] targetColumnIndexes;

    public CsvFileDataReader(string filePath, int[] targetColumnIndexes) 
    {
        this.filePath = filePath;
        this.targetColumnIndexes = targetColumnIndexes;
    }

    public (double[,], double[,]) Read()
    {
        var lines = File.ReadAllLines(filePath);
        var x = new double[lines[0].Split(',').Length - targetColumnIndexes.Length, lines.Length - 1];
        var y = new double[targetColumnIndexes.Length, lines.Length - 1];
        int lineNumber = 0;
        foreach (var str in lines[1..])
        {
            var row = str.Split(',').ToArray();
            int xAddedCount = 0;
            int yAddedCount = 0;
            for (int i = 0; i < row.Length; i++)
            {
                if (targetColumnIndexes.Contains(i))
                {
                    y[yAddedCount++, lineNumber] = double.Parse(row[i]);
                }
                else
                {
                    x[xAddedCount++, lineNumber] = double.Parse(row[i]);
                }
            }

            lineNumber++;
        }

        return (x, y);

    }
}
