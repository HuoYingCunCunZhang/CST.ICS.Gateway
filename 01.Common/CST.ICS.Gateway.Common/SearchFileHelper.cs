namespace CST.ICS.Gateway.Common
{
    public class SearchFileHelper
    {
        public IList<string> SearchAllDllFile(string directory)
        {
            var fileNames = Directory.GetFiles(directory, "CST.ICS.Gateway*.dll");
            return fileNames;
        }
    }
}