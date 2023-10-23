using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;


namespace Assignment1
{
    public class SimpleCSVParser
    {
        public long skip_rows_num = 0;
        public long valid_rows_num = 0;


        void reset_count_rows_num() 
        {
            valid_rows_num = 0;
            skip_rows_num = 0;
        }

        public void parse(String fileName)
        {
            reset_count_rows_num();
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        foreach (string field in fields)
                        {
                            Console.WriteLine(field);
                        }
                    }
                }

            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.StackTrace);
            }
        }

        public List<string[]> parse_getContent(String fileName)
        {
            reset_count_rows_num();
            List<string[]> tempList = new List<string[]>();
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        long skip_rows_num_old = skip_rows_num;
                        string[] fields = parser.ReadFields();
                        for (int i = 0; i < fields.Length; i++ )
                        {
                            if (fields[i] == "" && skip_rows_num == skip_rows_num_old)
                            {
                                skip_rows_num += 1;
                            }
                            if (fields[i].ToLower().Contains(",".ToLower())) {
                                fields[i] = "\"" + fields[i] + "\"";
                            }
                        }
                        if (skip_rows_num_old == skip_rows_num)
                        {
                            tempList.Add(fields);
                            valid_rows_num += 1;
                        }
                    }
                    return tempList;
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.StackTrace);
                return tempList;
            }

        }
    }
}
