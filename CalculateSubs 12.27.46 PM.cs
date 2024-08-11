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
using System.Diagnostics;
using System;
using System.Data.SqlTypes;

public class SubSet 
{
    public string? current {get;set;}
    public int numCount {get;set;}
    public int StartIndex {get;set;}
    public int EndIndex {get;set;}
    
}

public class ASSCIIBytesBase 
{

    public byte[]? bytes {get;set;}
    public int Index {get;set;}


} 
class NodeString {
    public bool isWord;
    public NodeString[] childnode = new NodeString[26];
    public NodeString()
    {
      this.isWord=false;
      for (int i = 0; i < 26; i++) {
        childnode[i] = null;
      }
    }
  }
 
  
class Result
{

    public static int countDistinctSubstring(string str)
  {
    NodeString head = new NodeString();
 
    // will hold the count of unique substrings
    int count = 0;
    // included count of substr " "
 
    for (int i = 0; i < str.Length; i++) {
      NodeString temp = head;
 
      for (int j = i; j < str.Length; j++)
      {
         
        // when char not present add it to the trie
        if (temp.childnode[str[j] - 'a'] == null) {
          temp.childnode[str[j] - 'a'] = new NodeString();
          temp.isWord = true;
          count++;
        }
        Console.WriteLine($"Current structure index is {str[j] - 'a'} and node is {temp.childnode[str[j] - 'a']}"); 

        // move on to the next char
        temp = temp.childnode[str[j] - 'a'];
      }
    }
 
    return count;
  }

    /*
     * Complete the 'countSubstrings' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. STRING s
     *  2. 2D_INTEGER_ARRAY queries
     */
     private static bool VerifyString(string currentString , HashSet<ASSCIIBytesBase> aSSCIIBytesBases)
     { 

            string template = String.Empty;
            bool isFound = false;
             foreach(ASSCIIBytesBase aSSCIIBytes in aSSCIIBytesBases)
             {

                template = Encoding.ASCII.GetString(aSSCIIBytes.bytes!);
                if(template == currentString)
                {
                    isFound = true;
                    template = String.Empty;
                    return isFound;

                }
             }
             template = String.Empty;
             return isFound;
     }

     public static int GetNumberToBytesHexa(string subset)
    {
        /// <summary>
        /// Diagnorstics time exexution for this model 
        /// </summary>
        int startIndex =0;
        int count = 0;
        int currentLength = subset.Length;
      //  List<string> subsets = new List<string>();
        HashSet<byte[]> builder = new HashSet<byte[]>();
         ReadOnlySpan<char> current ;

   
        int total_count = 0;
       try 
       {
        while(currentLength > 0)    
        {
            for(int endIndex =1 ; endIndex <= currentLength ; endIndex++ )
            {
               if(endIndex <= subset.Length)
               {
                current = subset.AsSpan(startIndex,endIndex);
               
                  // Dont use has
                   // performance hashset is faster
                  //var asciibyteObj = new ASSCIIBytesBase();
                   var currentbytes = Encoding.ASCII.GetBytes(current.ToString());
                  if(!builder.Contains(currentbytes))
                  {
                     builder.Add(currentbytes);
                  }
               
               }          
            }
            startIndex = startIndex + 1;

            currentLength = currentLength - 1; 
            // Add Total memory comparison ...

        }
       //   totalstring = builder.ToString().TrimEnd('\n');
        // List<string> arry = totalstring.Split("\n".ToCharArray()).Distinct<string>().ToList();     
        //  totalstring = null;
         count = builder.Distinct<byte[]>().ToList<byte[]>().Count;
         builder.Clear();
         builder.TrimExcess();
         
       //  arry.Clear();
        // arry.TrimExcess();
       }
       catch(Exception st)
       {
           Console.WriteLine($"Exception is {st.Message} {startIndex}");
           
       }
       
       
         //GC.Collect(0, GCCollectionMode.Forced);
        return count ;
        
        
    }
     
public static int GetNumberofSubstring(string subset)
    {
        /// <summary>
        /// Diagnorstics time exexution for this model 
        /// </summary>
        int startIndex =0;
        int count = 0;
        int currentLength = subset.Length;
         string totalstring = String.Empty;
      //  List<string> subsets = new List<string>();
        StringBuilder builder = new StringBuilder(32);
         ReadOnlySpan<char> current ;

   
        int total_count = 0;
       try 
       {
        while(currentLength > 0)    
        {
            for(int endIndex =1 ; endIndex <= currentLength ; endIndex++ )
            {
               if(endIndex <= subset.Length)
               {
                current = subset.AsSpan(startIndex,endIndex);
               
                  // Dont use has
                   // performance hashset is faster
                   builder.Append(current.ToString());
                   builder.AppendLine();
                   total_count = total_count + 1;      
                
               
               }          
            }
            startIndex = startIndex + 1;

            currentLength = currentLength - 1; 
            // Add Total memory comparison ...

        }
          totalstring = builder.ToString().TrimEnd('\n');
         List<string> arry = totalstring.Split("\n".ToCharArray()).Distinct<string>().ToList();     
          totalstring = String.Empty;
        count = arry.Count;
         builder.Clear();
         
         arry.Clear();
         arry.TrimExcess();
       }
       catch(Exception st)
       {
           Console.WriteLine($"Exception is {st.Message} {startIndex}");
           
       }
       
       
         //GC.Collect(0, GCCollectionMode.Forced);
        return count ;
        
        
    }
    public static List<int> countSubstrings(string s, List<List<int>> queries)
    {
        Process proc = Process.GetCurrentProcess();
var div = Math.Pow(1000,2);

var memory = proc.WorkingSet64 /div;

var available = proc.MaxWorkingSet / div ;

Console.WriteLine($"Available Memory in MB {available} and current occupied is  MB {memory}  ");

List<int> nums = new List<int>();
         int startIndex = 0;
         int endIndex = 0;
         int total_distance = 0;
         List<SubSet> lstsets = new List<SubSet>();
         
        queries = queries.GetRange(8,1);
         
        for(int i=0 ; i < queries.Count ; i++)
        {
            for(int k=0 ; k < queries[i].Count - 1 ; k++)
            {
                startIndex = queries[i][k];
                endIndex  = queries[i][k+1] - queries[i] [k] + 1 ;
                total_distance = startIndex + endIndex ;
if( (endIndex <= s.Length && endIndex >=0) && (s.Length >= total_distance) )
    
               {
                   ReadOnlySpan<char> sub_str = s.AsSpan(startIndex , endIndex);
                   var subs = new SubSet();
                   subs.current = sub_str.ToString(); 
                   Console.WriteLine($"Currently adding {subs.current} with startIndex {startIndex} and {endIndex} ");
                    lstsets.Add(subs);  
                   //nums.Add(GetNumberofSubstring(sub_str));
                }
                else 
                {
                  nums.Add(0);
                }
                
                
            }
            
        }
        
        
        var memorystatus = proc.WorkingSet64 /div;

Console.WriteLine($"Available Memory is M {available} and current occupied is MB  {memorystatus}  ");  


queries.Clear();

queries.TrimExcess();

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
