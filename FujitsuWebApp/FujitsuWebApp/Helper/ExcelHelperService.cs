namespace FujitsuWebApp.Helper
{
    public interface IExcelHelperService<out T> where T : class, new()
    {
        public IEnumerable<T> ReadFile(byte[] fileContent, string fileExtension, bool usingAttribute = false);
    }

    public class ExcelHelperService<T> : IExcelHelperService<T>
        where T : class, new()
    {
        public IEnumerable<T> ReadFile(byte[] fileContent, string fileExtension, bool usingAttribute = false)
        {
            return SpreadsheetProcessor<T>.Read(fileContent, fileExtension.ToLower(), usingAttribute);
        }
    }
}
