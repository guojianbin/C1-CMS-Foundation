using Composite.Data.Streams;


namespace Composite.Data.Plugins.DataProvider.Streams
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public class FileSystemFileBase
    {
        private CachedMemoryStream _currentWriteStream;
        private string _systemPath;

        /// <exclude />
        public CachedMemoryStream CurrentWriteStream
        {
            get { return _currentWriteStream; }
            set { _currentWriteStream = value; }
        }


        /// <exclude />
        public virtual string SystemPath
        {
            get { return _systemPath; }
            set { _systemPath = value; }
        }
    }
}
