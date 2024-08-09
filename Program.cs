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
using Microsoft.VisualBasic;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Calculate Many Substrings, World!");



Process proc = Process.GetCurrentProcess();
int div = 1000 * 1000;
var memory = proc.WorkingSet64 /div;
var available = proc.MaxWorkingSet/div ;

Console.WriteLine($"Available Memory in MB {available} and current occupied is  MB {memory}  ");


 List<Line> totallines  = FileContextReader.ReadFile(Environment.CurrentDirectory + "//input/input003.txt");
// data index starts from 2 in our test case
string s = totallines[1].LineContent;

int batch_factor = 10;
int start_index_batch = 2;
int total_counts = totallines.Count - 2;
int remainder = total_counts % batch_factor;
int result = total_counts / batch_factor;



List<int> nums = new List<int>();


           Console.WriteLine($"Current start_batch completed {start_index_batch} to {total_counts}");

  List<Line> currentlines = totallines.GetRange(start_index_batch, total_counts);

int startIndex = 0 ;
int endIndex = 0;
string[] startIndexrange; 
int total_distance = 0;
List<SubSet> lstsets = new List<SubSet>();
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
                   //Console.WriteLine($"Currently adding {subs.current} with startIndex {startIndex} and {endIndex} ");
                    lstsets.Add(subs);  
                   //nums.Add(GetNumberofSubstring(sub_str));
                }
                else 
                {
                  nums.Add(0);
                }
           





    
}

   // Minimize memory by ziiping it GZIP

        currentlines.Clear();
        currentlines.TrimExcess();
        // Memory Available Here 
        
        
        var memorystatus = proc.WorkingSet64 /div;

    Console.WriteLine($"Available Memory is MB {available} and current occupied is MB  {memorystatus}  ");

       
        foreach(var p in lstsets.ToList() )
        {
             p.numCount = Result.countDistinctSubstring(p.current!);
             //Console.WriteLine($"For the index from {p.current} To total number of subset of substring count is {p.numCount}");
             nums.Add(p.numCount);
             p.current = null;
             lstsets.Remove(p);

             
            

            
        }
        

        
        lstsets.Clear();
         lstsets.TrimExcess();
       
        GC.Collect(2000, GCCollectionMode.Forced, false);
        

 
         foreach(var i in nums)
         Console.WriteLine(i);




