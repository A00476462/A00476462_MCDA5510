using System;
using System.IO;
using System.Text;
using System.Linq;


public class Logs
{

    static string fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Directory.GetCurrentDirectory() + @"\..\..\..\..\logs\lg-" + DateTime.Now.ToString("yyyyMMdd") + ".lg");
    static bool has_removed = false;

    public static void Info(string Msg)
    {
        try
        {
            if (!has_removed) 
            {
                delFile(fileName);
                has_removed = true;
            }

            string path = Path.GetDirectoryName(fileName);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                System.IO.File.CreateText(fileName).Dispose();
            }
            else if (!System.IO.File.Exists(fileName))
            {
                System.IO.File.CreateText(fileName).Dispose();
            }
            using (TextWriter writer2 = System.IO.File.AppendText(fileName))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                sb.AppendLine("Time：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendLine(Msg);
                sb.AppendLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                sb.AppendLine("");
                writer2.WriteLine(sb.ToString());
            }
            Console.WriteLine(Msg);
        }
        catch (Exception ex)
        {

        }
    }


    public static void delFile(string url)
    {
        try
        {
            FileInfo file = new FileInfo(url);
            if (!file.Exists)
            {
                return;
            }

            file.Delete();

            if (!new FileInfo(file.DirectoryName + "/" + file.Name).Exists)
            {
                return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
