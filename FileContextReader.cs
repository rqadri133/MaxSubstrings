
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

namespace ManySubstrings.Classes.Util
{
    
public class FileContextReader
{
    public static List<Line> ReadFile(string fileName)
    {
        
        List<Line> lines = new List<Line>();
        using (StreamReader sr = File.OpenText(fileName))
        {
            int x = 0;
            while (!sr.EndOfStream)
            {
                //we're just testing read speeds
                var line = new Line();
                line.LineContent  = sr.ReadLine();
                line.Index = x;
                x += 1;
                lines.Add(line);
            }
        }
       return lines; 

     }  


 }  

 

}





