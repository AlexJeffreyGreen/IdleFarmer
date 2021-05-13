using System.IO;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;

namespace SaveSystem
{
    public class SaveFile
    {
        private FileStream _fileStream;
        
        public FileStream FileStream
        {
            get { return this._fileStream; }
            set { this._fileStream = value; }
        }

        public SaveFile(FileStream fileStream)
        {
            this._fileStream = fileStream;
        }

        public SaveFile()
        {
            
        }
    }
}