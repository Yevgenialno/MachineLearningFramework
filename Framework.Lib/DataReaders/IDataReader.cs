namespace Framework.Lib.DataReaders;

public interface IDataReader
{
    (double[,], double[,]) Read();
}
