using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    public static List<string> GetWordsAsList(this string s)
    {
        List<string> words = new List<string>();
        words.AddRange(s.Split(' '));
        return words;
    }

    public static string[] GetWordsAsArray(this string s)
    {
        return s.Split(' ');
    }

    public static List<List<string>> GetWordsInChunks(this string s, int lettersPerChunk, bool checkForForceStop = false)
    {
        List<List<string>> chunks = new List<List<string>>();

        List<string> newChunk = new List<string>();

        string[] words = GetWordsAsArray(s);
        int lengthOfChunk = 0;

        foreach (string word in words)
        {
           
            if (lengthOfChunk + word.Length > lettersPerChunk)
            {
                lengthOfChunk = 0;
                chunks.Add(newChunk);
                foreach (var item in newChunk)
                {
                    ServiceLocator.GetDebugProvider().Log(item);
                }
                newChunk = new List<string>();
                newChunk.Add(word);
            }
            else
            {
                if (checkForForceStop && newChunk.Count > 0)
                {
                    if (newChunk[newChunk.Count - 1].EndsWith('§'.ToString()))
                    {
                        ServiceLocator.GetDebugProvider().Log("force stop found");
                        lengthOfChunk = 0;
                        chunks.Add(newChunk);
                        newChunk = new List<string>();
                        lengthOfChunk += word.Length;
                        newChunk.Add(word);
                    }
                    else
                    {
                        lengthOfChunk += word.Length;
                        newChunk.Add(word);
                    }
                }
                else
                {
                    lengthOfChunk += word.Length;
                    newChunk.Add(word);

                }
            }
        }

        if(newChunk.Count > 0)
        {
            chunks.Add(newChunk);
        }


        return chunks;
    }
}
