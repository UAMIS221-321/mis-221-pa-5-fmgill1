using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
namespace mis_221_pa_5_fmgill1
{
    public class MenuSelector
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;
        
        public MenuSelector(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        private void DisplayOptions()
        {
            WriteLine(Prompt);
            for(int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;

                if(i == SelectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Magenta;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Magenta;
                }
                WriteLine($"{prefix} << {currentOption} >>");
            }
            ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if(keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if(SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;

                    }
                }
                else if(keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if(SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }
}