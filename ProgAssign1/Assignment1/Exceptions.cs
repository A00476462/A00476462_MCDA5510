using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1
{

    public class Exceptions
    {

        static void Main()
        {

            DateTime start = System.DateTime.Now;

            // step1
            String outDir = Directory.GetCurrentDirectory() + @"\..\..\..\..\output\out.csv";
            Logs.delFile(outDir);
            Logs.Info(String.Format("Output dir:\n{0}", outDir));
            var sw = OpenStream(outDir);
            if (sw is null)
                return;

            // step2
            DirWalker fw = new DirWalker();
            String csvDirs = Directory.GetCurrentDirectory() + @"\..\..\..\..\..\Sample Data";
            string[] fileList = fw.walk_getFileList(csvDirs);
            //Console.WriteLine(fileList.Length);

            // step3
            SimpleCSVParser parser = new SimpleCSVParser();
            long skip_rows_num = 0, valid_rows_num = -1;
            for (int i = 0; i < fileList.Length; i++)
            {
                string filepath = fileList[i];
                //Console.WriteLine("File:" + filepath);
                List<string[]> csvContent = parser.parse_getContent(filepath);
                for (int j = 0; j < csvContent.Count; j++)
                {
                    if (i != 0 && j == 0) 
                    {
                        continue;
                    }
                    string[] row = csvContent[j];
                    //Console.WriteLine(string.Join(",", row));
                    sw.WriteLine(string.Join(",", row));
                    valid_rows_num += 1;
                    //break;
                }
                skip_rows_num += parser.skip_rows_num;
                //if (valid_rows_num > 184) { break; }
                //break;
            }
            sw.Close();

            DateTime end = System.DateTime.Now;
            TimeSpan ts = end.Subtract(start);
            Logs.Info(String.Format("Total execution time: {0}s", ts.TotalSeconds));
            Logs.Info(String.Format("Total number of valid rows: {0}", valid_rows_num));
            Logs.Info(String.Format("Total number of skipped rows: {0}", skip_rows_num));
        }

        static StreamWriter OpenStream(string path)
        {
            if (path is null)
            {
                Console.WriteLine("You did not supply a file path.");
                return null;
            }

            try
            {
                var fs = new FileStream(path, FileMode.CreateNew);
                return new StreamWriter(fs);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file or directory cannot be found.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The file or directory cannot be found.");
            }
            catch (DriveNotFoundException)
            {
                Console.WriteLine("The drive specified in 'path' is invalid.");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("'path' exceeds the maxium supported path length.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to create this file.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                Console.WriteLine("There is a sharing violation.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                Console.WriteLine("The file already exists.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
            }
            return null;
        }

    }

}
