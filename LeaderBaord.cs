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

public class Leader
{
    public int Rank 
    {
        get;
        set;
    }
    public int Score
    {
        get;
        set;
    }

    public bool IsDuplicate
    {
        get;
        set;
    }
    
}


    /*
     * Complete the 'climbingLeaderboard' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY ranked
     *  2. INTEGER_ARRAY player
     */
   
   public class ClimibingBoardOlympics
   { 
    public static List<int> climbingLeaderboard(List<int> ranked, List<int> player)
    {
        int score_min = ranked.Min();
        int score_max = ranked.Max();
        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Start();
                Console.WriteLine($"It stops at {stopwatch.Elapsed.Microseconds/100000}");            

        List<int> currentRanks = new List<int>();
        List<Leader> lederboard = new List<Leader>();
        var rankedData =  ranked.OrderByDescending(p=>p).GroupBy(p=>p).SelectMany((g, i) =>
                       g.Select(e => new Leader { Score = e, Rank = i + 1 }))
                   .ToList();

        stopwatch.Stop();
        Console.WriteLine($"It stops at {stopwatch.ElapsedMilliseconds/1000000}");            
        
        foreach(int currentscore in player)
        {
            Console.WriteLine($"Current Score is {currentscore}");
            if(currentscore < score_min)
            {
              var rankObject = rankedData.Last(p=> p.Score == score_min);
               // So this will rank after min
                           Console.WriteLine($"Current Score is {currentscore} and Rank is {rankObject.Rank + 1}");

               currentRanks.Add(rankObject.Rank + 1);      
          
            }
            else if (currentscore >=  score_max)
            {
                
                  var rankObject = rankedData.First(p=> p.Score == score_max);
               // So this will rank after min
               Console.WriteLine($" prvious standing was {rankObject.Score} Current Score {currentscore} and Rank is {rankObject.Rank}");
                  currentRanks.Add(rankObject.Rank );  
                  
   
            }
            
            else if(currentscore == score_min)
            {
                
                 var rankObject = rankedData.Last(p=> p.Score == score_min);
               // So this will rank after min
               Console.WriteLine($"Current Score is {currentscore} and Rank is {rankObject.Rank }");
                 currentRanks.Add(rankObject.Rank );    
 
            }
            else if(currentscore > score_min && currentscore < score_max)
            {
                if(rankedData.Any(p=> p.Score == currentscore))
                {
                    // means we have a matching score
                     var rankObject = rankedData.Last(p=> p.Score == currentscore);
               // So this will rank after min
               Console.WriteLine($"Current Score is {currentscore} and Rank is {rankObject.Rank}");
                     currentRanks.Add(rankObject.Rank );   
                    
                }
                else
                {
                    // Find Closest Number very important the loop will time out
                    int closest = ranked.Aggregate((x,y) => Math.Abs(x-currentscore) < Math.Abs(y-currentscore) ? x : y);
                    Console.WriteLine($"The Closest score i have found is {closest}");
                    var currentRankObj = rankedData.Last(p=> p.Score == closest);
                   if(currentRankObj.Score > currentscore )
                   {
                         currentRanks.Add(currentRankObj.Rank + 1  ); 
                   }
                   else if(currentRankObj.Score < currentscore )
                   {
                    
                    // it will replace the rank
                     currentRanks.Add(currentRankObj.Rank ); 

                       
                    }
                  
                    
                }
               
                
                
            }
            
              
            
        }
        return currentRanks;
        
    }

        public static List<int> climbingLeaderboardFast(List<int> ranked, List<int> player)
    {
        int score_min = ranked.Min();
        int score_max = ranked.Max();
        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Start();

        List<int> currentRanks = new List<int>();
        List<Leader> lederboard = new List<Leader>();
        var rankedData =  ranked.OrderByDescending(p=>p).GroupBy(p=>p).SelectMany((g, i) =>
                       g.Select(e => new Leader { Score = e, Rank = i + 1 }))
                   .ToList();
 
         foreach(var p in player )
         {
             if(rankedData.Any(m=>m.Score == p))
             {
                var first = rankedData.First(m=>m.Score == p);
                currentRanks.Add(first.Rank);
             }
             else 
             {
                  int closest = ranked.Aggregate((x,y) => Math.Abs(x-p) < Math.Abs(y-p) ? x : y);
                    Console.WriteLine($"The Closest score i have found is {closest}");
                    var currentRankObj = rankedData.Last(p=> p.Score == closest);
                   if(currentRankObj.Score > p )
                   {
                         currentRanks.Add(currentRankObj.Rank + 1  ); 
                   }
                   else if(currentRankObj.Score < p )
                   {
                    
                    // it will replace the rank
                     currentRanks.Add(currentRankObj.Rank ); 

                       
                    }
             }

         }   
        
        return currentRanks;
        
    }

}


