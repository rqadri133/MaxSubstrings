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


public class SubSet 
{
    public string? current {get;set;}
    public int numCount {get;set;}
    
}
class Result
{

    /*
     * Complete the 'countSubstrings' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. STRING s
     *  2. 2D_INTEGER_ARRAY queries
     */

    private static int NFormulaeRules(string subset)
    {
        int n = subset.Length;
        return n * (n + 1) / 2;
        
    }
    
    public static int GetNumberofSubstring(string subset)
    {
        /// <summary>
        /// Diagnorstics time exexution for this model 
        /// </summary>
        int startIndex =0;
        int currentLength = subset.Length;
      //  List<string> subsets = new List<string>();
        StringBuilder builder = new StringBuilder();

        int total_count = 0;
        while(currentLength > 0)    
        {
            for(int endIndex =1 ; endIndex <= currentLength ; endIndex++ )
            {
               if(endIndex <= subset.Length)
               {
               ReadOnlySpan<char> current = subset.AsSpan(startIndex,endIndex);
               
                  // Dont use has
                   // performance hashset is faster
                   builder.Append(current.ToString());
                   builder.AppendLine();
                   //total_count = total_count + 1;      
                
               
               }          
            }
            startIndex = startIndex + 1;

            currentLength = currentLength - 1; 

        }
       
         List<string> arry = builder.ToString().Split("\n".ToCharArray()).Distinct<string>().ToList();
         int count = arry.Count;
         builder.Clear();
         
         arry.Clear();
         arry.TrimExcess();
         GC.Collect(0, GCCollectionMode.Forced);
        return count ;
        
        
    }
    public static List<int> countSubstrings(string s, List<List<int>> queries)
    {
         int[][] selectedq = queries.Select(a => a.ToArray()).ToArray();

         List<int> nums = new List<int>();
         int startIndex = 0;
         int endIndex = 0;
         int total_distance = 0;
         List<SubSet> lstsets = new List<SubSet>();
         
         
         
        for(int i=0 ; i < selectedq.Length ; i++)
        {
            for(int k=0 ; k < selectedq[i].Length - 1 ; k++)
            {
                startIndex = selectedq[i][k];
                endIndex  = selectedq[i][k+1] - selectedq[i][k] + 1 ;
                total_distance = startIndex + endIndex ;

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
            
        }
        queries.Clear();
        queries.TrimExcess();
        // Memory Available Here 
        
        
        Parallel.ForEach(lstsets , p => 
        {
             p.numCount = GetNumberofSubstring(p.current);
            
        });
        
        var countnums = from setIn in lstsets 
               select setIn.numCount ;
         nums = countnums.ToList<int>();
        return nums;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int q = Convert.ToInt32(firstMultipleInput[1]);

        string s = Console.ReadLine();

        List<List<int>> queries = new List<List<int>>();

        for (int i = 0; i < q; i++)
        {
            queries.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(queriesTemp => Convert.ToInt32(queriesTemp)).ToList());
        }

        List<int> result = Result.countSubstrings(s, queries);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
