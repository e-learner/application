using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElearnerApp.Utilities
{
    public class FileTools
    {
        public static string RemoveSpacesFromFilename(string filename)
        {
            string finalName = "";

            foreach (var item in filename.Split(' '))
            {
                finalName += item; 
            }
            return finalName.Replace("#", "_");
        }
    }
}