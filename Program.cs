using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Diagnostics;
using ManySubstrings.Classes;
using ManySubstrings.Classes.Util;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Calculate Many Substrings, World!");



Process proc = Process.GetCurrentProcess();
var memory = proc.WorkingSet64 /(1024*1024);
var available = proc.MaxWorkingSet/(Math.Pow(1024,8)) ;

Console.WriteLine($"Available Memory in GB {available} and current occupied is  MB {memory}  ");


 List<Line> currentlines  = FileContextReader.ReadFile(Environment.CurrentDirectory + "//input/input003.txt");
// data index starts from 2 in our test case
string s = currentlines[1].LineContent;

currentlines = currentlines.GetRange(2, currentlines.Count-2);

int startIndex = 0 ;
int endIndex = 0;
string[] startIndexrange; 
int total_distance = 0;
List<SubSet> lstsets = new List<SubSet>();
List<int> nums = new List<int>();
foreach (var line in currentlines)
{
   startIndexrange =line.LineContent.Split(" ");
   startIndex = Convert.ToInt32(startIndexrange[0]);
   endIndex = Convert.ToInt32(startIndexrange[1]) - startIndex + 1;
   total_distance = startIndex + endIndex;
    if( (endIndex <= s.Length && endIndex >=0) && (s.Length >= total_distance) )
                {
                   ReadOnlySpan<char> sub_str = s.AsSpan(startIndex , endIndex);
                   var subs = new SubSet();
                   subs.current = sub_str.ToString(); 
                    lstsets.Add(subs);  
                   //nums.Add(GetNumberofSubstring(sub_str));
                }
                else 
                {
                  nums.Add(0);
                }
           





    
}

        currentlines.Clear();
        currentlines.TrimExcess();
        // Memory Available Here 
        
        
        var memorystatus = proc.WorkingSet64 /(1024*1024);

Console.WriteLine($"Available Memory is GB {available} and current occupied is MB  {memorystatus}  ");

        Parallel.ForEach(lstsets , p => 
        {
             p.numCount = Result.GetNumberofSubstring(p.current!);
             //Console.WriteLine($"For the index from {p.current} To total number of subset of substring count is {p.numCount}");

             p.current = null;

            
        });
        


        var countnums = from setIn in lstsets 
               select setIn.numCount ;
         nums = countnums.ToList<int>();

        lstsets.Clear();
         lstsets.TrimExcess();

         foreach(var i in nums)
         Console.WriteLine(i);




