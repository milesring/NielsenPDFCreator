using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nielsen_PDF_Creator
{
    class FilePathObject
    {
        private String contractName;
        private String path;

        public FilePathObject(String n, String p)
        {
            contractName = n;
            path = p;
        }

        public String getPath() { return path; }
        public String getName() { return contractName; }
        public void setPath(String p)
        {
            path = p;
        }
    }
}
