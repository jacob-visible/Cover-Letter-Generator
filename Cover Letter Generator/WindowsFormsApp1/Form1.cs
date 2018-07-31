using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoverLetterGenerator
{
    public partial class CGen : Form
    {
        public CGen()
        {
            InitializeComponent();
        }

        //Path to Directory Variable
        public class Config
        {
            public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\CoverLetterGenerator\";//@"C:\Users\*USERNAME*\My Documents\"
        }

            //Start of generate button press function
            private void GenerateButton_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            string[] qual = textBox1.Lines;

            //Load Config Replace Line Settings
            //Load Config for keywords or qualifications
            if (File.Exists(Config.Path + @"qual.txt"))
            {
                ;
            }
            else
            {
                System.IO.Directory.CreateDirectory(Config.Path);
                System.IO.File.WriteAllText(Config.Path + @"qual.txt", @"each line is a seperate keyword that this program will search for
Add keywords for requirements that will be give qualifications on the output
The correspond text that will output to the textbox will be on the same line on the require.txt doc");
            }

            //Load Config for Output text or requirement
            if (File.Exists(Config.Path + @"require.txt"))
            {
                ;
            }
            else
            {
                System.IO.Directory.CreateDirectory(Config.Path);
                System.IO.File.WriteAllText(Config.Path + @"require.txt", @"each line is a seperate output
Add qualifications that will output when the keyword on the same line in the qual.txt is
The corresponding text will output in the textbox");
            }


            //Load Text to Variables
            string[] RequireText = File.ReadAllLines(Config.Path + @"require.txt");
            string[] QualText = File.ReadAllLines(Config.Path + @"qual.txt");




            //Header Name, Info
            textBox2.Text += @"Your Name
Phone: Number
Email: Address
Date: ";
            textBox2.Text += DateTime.Now.ToString("dd/MM/yyyy")
            + @"

";
            if (textBox4.TextLength > 0)
            {
                textBox2.Text += @"Dear " + textBox4.Text + @",";
            }
            else
            {
                textBox2.Text += @"To whom it may concern,";
            }
            textBox2.Text += @"
            Moving from the west coast to Montreal has been exciting. Your recent posting for ";

            if (textBox3.TextLength > 0)
            {
                if (textBox3.Text.StartsWith( "Assistant" ) || textBox3.Text.StartsWith("Administrative") || textBox3.Text.StartsWith("Admissions"))
                {
                    textBox2.Text += "an ";
                } else
                {
                    textBox2.Text += "a ";
                }
                textBox2.Text += textBox3.Text;
            }
            else
            {
                textBox2.Text += @"a job";
            };

            textBox2.Text += @" caught my attention as I am highly interested in pursuing a career here. Currently, I am able to start immediately. Given my work experience, related knowledge, and excellent capabilities; I would make a good fit for this opening.
";
if (textBox1.TextLength > 0)
            {

                //Requirements section
                textBox2.Text += @"
Your Requirements
";
                int q = 0;
                for (q = 0; q < qual.Length; q++)
                {
                    if (qual[q].StartsWith("    "))
                    {
                        textBox2.Text += "- " + qual[q].Substring(4);
                    }
                    else
                    {
                        if (qual[q].StartsWith("   "))
                        {
                            textBox2.Text += "- " + qual[q].Substring(3);
                        }
                        else
                        {
                            if (qual[q].StartsWith("  ") || qual[q].StartsWith("? ") || qual[q].StartsWith("· ") || qual[q].StartsWith("- ") || qual[q].StartsWith("• "))
                            {
                                textBox2.Text += "- " + qual[q].Substring(2);
                            }
                            else
                            {
                                if (qual[q].StartsWith(" ") || qual[q].StartsWith("·") || qual[q].StartsWith("-") || qual[q].StartsWith("•") || qual[q].StartsWith("?"))
                                {
                                    textBox2.Text += "- " + qual[q].Substring(1);
                                }
                                else
                                {
                                    textBox2.Text += "- " + qual[q];
                                }
                            }
                        }
                    }
                    textBox2.Text += @"
";
                } 
            }
            else
            {
                ;
            }

            //Qualifications section
            textBox2.Text += @"
My Qualifications
";
            int QualCount = 0;
            bool hit = false;
            string RequireTextMain = "";
            string RequireTextRemainder = "";
            //replace requirement lines with new qualification lines
            for (int qualline = 0; qualline < qual.Length; qualline++)
            {
                for (int requireline = 0; requireline < RequireText.Length; requireline++)
                {
                    if (RequireText[requireline].Length <= 0)
                    {
                        ;
                    }
                    else
                    {
                        //Runs || check for multiple options in requirements
                        if (RequireText[requireline].Contains("||"))
                        {
                            //|| loop for requirements checking individually until || in requirements
                            RequireTextRemainder = RequireText[requireline];
                            hit = false;
                            do
                            {
                                RequireTextMain = RequireTextRemainder.Substring(0, RequireText[requireline].IndexOf("||"));
                                RequireTextRemainder = RequireTextRemainder.Substring(RequireTextRemainder.IndexOf("||") + 2);
                                if (qual[qualline].Contains(RequireTextMain))
                                {
                                    textBox2.Text += @"- ";
                                    textBox2.Text += QualText[requireline];
                                    RequireText[requireline] = "";
                                    QualText[requireline] = "";
                                    RequireTextRemainder = "";
                                    hit = true;
                                    QualCount++;
                                    break;
                                }
                                else
                                {
                                    ;
                                }
                            }
                            while (RequireTextRemainder.Contains("||"));
                            if (hit == true)
                            {
                                break;
                            }
                            else
                            {
                                ;
                            }
                        }
                        else
                        {
                            if (qual[qualline].Contains(RequireText[requireline]))
                            {
                                textBox2.Text += @"- ";
                                textBox2.Text += QualText[requireline];
                                RequireText[requireline] = "";
                                QualText[requireline] = "";
                                hit = true;
                                QualCount++;
                                break;
                            }
                            else
                            {
                                ;
                            };
                        }
                    }
                };
                    textBox2.Text += @"
";
            };

            //Final Statement
            textBox2.Text += @"
I appreciate you taking the time to review my qualifications, briefly stated here. My Resume shows these further. Please, let me know if you are interested in meeting me in person. Thank you.

Sincerely, 
Your Name";

            //Auto Copy to Clipboard
            if (textBox2.TextLength > 0)
            {
            //    Clipboard.SetText(textBox2.Text);
            }
            else
            {
                ;
            }

            //Error text label
            if (textBox1.TextLength <= 0)
            {
                errorLabel.ForeColor = Color.Red;
                errorLabel.Text = @"Empty Requirements";
            }
            else
            {
                if (QualCount < qual.Length)
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = @"Missing Qualification(s) for ";
                    errorLabel.Text += qual.Length - QualCount;
                    errorLabel.Text += @" Requirement(s)";
                }
                else
                {
                    errorLabel.ForeColor = Color.Green;
                    errorLabel.Text = @"It Worked";
                }
            }
        }
        //End of generate button press

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0)
            {
                Clipboard.SetText(textBox2.Text);
            }
            else
            {
                ;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            DebugBox.Text = null;
            DebugBox2.Text = null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Load Config for keywords or qualifications
            if (File.Exists(Config.Path + @"qual.txt"))
            {
                ;
            }
            else
            {
                System.IO.Directory.CreateDirectory(Config.Path);
                System.IO.File.WriteAllText(Config.Path + @"qual.txt", @"each line is a seperate keyword that this program will search for
Add keywords for requirements that will be give qualifications on the output
The correspond text that will output to the textbox will be on the same line on the require.txt doc");
            }

            //Load Config for Output text or requirement
            if (File.Exists(Config.Path + @"require.txt"))
            {
                ;
            }
            else
            {
                System.IO.Directory.CreateDirectory(Config.Path);
                System.IO.File.WriteAllText(Config.Path + @"require.txt", @"each line is a seperate output
Add qualifications that will output when the keyword on the same line in the qual.txt is
The corresponding text will output in the textbox");
            }

            Process.Start(Config.Path + @"require.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Load Config for keywords or qualifications
            if (File.Exists(Config.Path + @"qual.txt"))
            {
                ;
            }
            else
            {
                System.IO.Directory.CreateDirectory(Config.Path);
                System.IO.File.WriteAllText(Config.Path + @"qual.txt", @"each line is a seperate keyword that this program will search for
Add keywords for requirements that will be give qualifications on the output
The correspond text that will output to the textbox will be on the same line on the require.txt doc");
            }

            //Load Config for Output text or requirement
            if (File.Exists(Config.Path + @"require.txt"))
            {
                ;
            }
            else
            {
                System.IO.Directory.CreateDirectory(Config.Path);
                System.IO.File.WriteAllText(Config.Path + @"require.txt", @"each line is a seperate output
Add qualifications that will output when the keyword on the same line in the qual.txt is
The corresponding text will output in the textbox");
            }

            Process.Start(Config.Path + @"qual.txt");
        }
    }
}