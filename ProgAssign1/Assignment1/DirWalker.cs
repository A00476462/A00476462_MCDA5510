using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1
{
  

    public class DirWalker
    {

        public void walk(String path)
        {

            string[] list = Directory.GetDirectories(path);


            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                    Console.WriteLine("Dir:" + dirpath);
                }
            }
            string[] fileList = Directory.GetFiles(path);
            foreach (string filepath in fileList)
            {

                    Console.WriteLine("File:" + filepath);
            }
        }

        public string[] walk_getFileList(String path)
        {

            string[] fileList = Directory.GetFiles(path, "*.csv");

            string[] list = Directory.GetDirectories(path);
            if (list == null) return fileList;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    List<string> tempList = new List<string>();
                    tempList.AddRange(fileList);
                    tempList.AddRange(walk_getFileList(dirpath));
                    fileList = tempList.ToArray();
                }
            }

            return fileList;
        }

    }
}
