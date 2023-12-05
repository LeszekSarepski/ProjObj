using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Media;
using NAudio.Wave;

class Zapamietaj
{
    static int score = 0;
    static string FullCombo = "";
    static string Input;
    static string Continue;
    static bool Game = true;

    static void Main()
    {
        Console.WriteLine("Witaj\n");
        Console.WriteLine("Bede podawal ci jedn z 3 liter A, S lub D\n");
        Console.WriteLine("Beda one znikac po 5 sekundach a ty bedziesz musial je zapamietac i podac w odpowiedniej kolejnosci\n");
        Thread.Sleep(5000);
        
        while(Game)
        {
            GenerateLetter();
            Console.Clear();
            DisplayGame();
            Console.Clear();
            score = FullCombo.Length;
            Console.WriteLine("Wpisz litery w dobrej kolejnosci");
            Input = Console.ReadLine();
            CheckInput(Input);
        }
    }

    static void GenerateLetter()
    {
        Random random = new Random();
        
        string[] Letter = {"A","S","D"};
        
        Zapamietaj.FullCombo += Letter[random.Next(0, 3)];
    }
    
    static void CheckInput(string Input)
    {
        if (Input.ToUpper() == FullCombo)
        {
                Console.WriteLine("Dobrze, grasz dalej");
        }
        if (Input.ToUpper() != FullCombo)
        {
                
                Console.WriteLine("Przegrales. Twoj wynik to: {0}\nOstatnia kombinacja to: {1} \nGrasz dalej? (y/n)", score - 1, FullCombo);
                Zapamietaj.Continue = Console.ReadLine();
                
                if (Zapamietaj.Continue == "y")
                {
                    Game = true;
                    Zapamietaj.FullCombo = "";
                }
                if (Zapamietaj.Continue == "n")
                {
                    Game = false;
                }
        }
    }
    
    static void PlaySound(string soundFilePath)
    {
        try
        {
            using (var audioFile = new AudioFileReader(soundFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                   
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas odtwarzania dźwięku: {ex.Message}");
        }
    }
    static void DisplayGame()
    {
        foreach (char Letter in Zapamietaj.FullCombo)
        {
            Console.Write($"{Letter} ");
            if (Letter == 'A')
            { 
                PlaySound("a.wav"); 
            }
            if (Letter == 'S')
            { 
                PlaySound("s.wav"); 
            }
            if (Letter == 'D')
            { 
                PlaySound("d.wav"); 
            }
            Thread.Sleep(300);
            
        }
        Console.WriteLine(".");
    }
}