using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public static class CSVParser
{
    static public object CreateArray_K(Type type, string text) 
    {
        List<string[]> text_textline = ParseTXTtoLine(text);
        List<List<string>> text_textline_lst = new List<List<string>>();
        for (int i = 0; i < text_textline.Count; i++)
        {
            List<string> lsttring = new List<string>();
            string txtline = text_textline[i][0];
            string value = "";
            for (int j = 0; j < txtline.Length; j++)
            {
                if ((txtline[j] >= 'a' && txtline[j] <= 'z') || (txtline[j] >= '0' && txtline[j] <= '9') || txtline[j] == ',' || txtline[j] == ':' || txtline[j] == ' ')
                {
                    value += (txtline[j]).ToString();
                }
                else if (txtline[j] >= 'A' && txtline[j] <= 'Z') 
                {
                    //value += ((char)(txtline[j] - 'A' + 'a')).ToString();// upper to lower
                    value += (txtline[j]).ToString(); ;
                }
                else // /t
                {
                    lsttring.Add(value);
                    value = "";
                }
                if (j == (txtline.Length - 1))
                {
                    lsttring.Add(value);
                    value = "";
                }
            }
            text_textline_lst.Add(lsttring);
        }
        List<string[]> row = new List<string[]>();
        foreach (var lst in text_textline_lst)
        {
            row.Add(lst.ToArray());
        }

        Dictionary<string, List<string>> Dict = new Dictionary<string, List<string>>();
        for (int j = 0; j < row[0].Length; j++)
        {
            Dict[row[0][j]] = new List<string>();
        }
        for (int i = 1; i < row.Count; i++) 
        {
            for (int j = 0; j < row[0].Length; j++) 
            {
                Dict[row[0][j]].Add(row[i][j]);
            }
        }
        //
        Array finalArray = Array.CreateInstance(type, row.Count - 1);
        for (int i = 0; i < (row.Count - 1); i++)
        {
            object v = Activator.CreateInstance(type);
            for (int j = 0; j < Dict.Count; j++) 
            {
                FieldInfo[] fieldinfo = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance); // [wave, monster]
                string a = Dict.ElementAt(j).Key;
                foreach (FieldInfo tmp in fieldinfo)
                {
                    if (tmp.Name == Dict.ElementAt(j).Key)
                    {
                        string b = Dict.ElementAt(j).Value[i];
                        //tmp.SetValue(v, Dict.ElementAt(j).Value[i]);

                        if (tmp.FieldType == typeof(string))
                        {
                            tmp.SetValue(v, Dict.ElementAt(j).Value[i]);
                        }
                        if (tmp.FieldType == typeof(int))
                        {
                            int.TryParse(Dict.ElementAt(j).Value[i], out int value);
                            tmp.SetValue(v, value);
                        }
                        if (tmp.FieldType == typeof(float))
                        {
                            float.TryParse(Dict.ElementAt(j).Value[i], out float value);
                            tmp.SetValue(v, value);
                        }


                    }
                }
            }
            finalArray.SetValue(v, i);
        }
        return finalArray;
    }

    static public T[] Deserialize<T>(string text)
    {
        return (T[])CreateArray_K(typeof(T), text);
    }

    static public List<string[]> ParseTXTtoLine(string text, char separator = ',') 
    {
        List<string[]> lines = new List<string[]>();
        List<string> line = new List<string>();
        StringBuilder token = new StringBuilder();
        bool quotes = false;

        for (int i = 0; i < text.Length; i++)
        {
            if (quotes == true)
            {
                if ((text[i] == '\\' && i + 1 < text.Length && text[i + 1] == '\"') || (text[i] == '\"' && i + 1 < text.Length && text[i + 1] == '\"'))
                {
                    token.Append('\"');
                    i++;
                }
                else if (text[i] == '\\' && i + 1 < text.Length && text[i + 1] == 'n')
                {
                    token.Append('\n');
                    i++;
                }
                else if (text[i] == '\"')
                {
                    line.Add(token.ToString());
                    token = new StringBuilder();
                    quotes = false;
                    if (i + 1 < text.Length && text[i + 1] == separator)
                        i++;
                }
                else
                {
                    token.Append(text[i]);
                }
            }
            else if (text[i] == '\r' || text[i] == '\n')
            {
                if (token.Length > 0)
                {
                    line.Add(token.ToString());
                    token = new StringBuilder();
                }
                if (line.Count > 0)
                {
                    lines.Add(line.ToArray());
                    line.Clear();
                }
            }
            else if (text[i] == separator)
            {
                line.Add(token.ToString());
                token = new StringBuilder();
            }
            else if (text[i] == '\"')
            {
                quotes = true;
            }
            else
            {
                token.Append(text[i]);
            }
        }

        if (token.Length > 0)
        {
            line.Add(token.ToString());
        }
        if (line.Count > 0)
        {
            lines.Add(line.ToArray());
        }
        return lines;
    }
}
