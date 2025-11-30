using System.DirectoryServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name: Abdin Ali
         * Date: December 2, 2025
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: 
         * */

        //class-level random object
        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            radOneRoll.Checked = true;
            //add your name to end of form title
            this.Text += "Abdin Ali";
        } // end form load

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();

        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1, dice2;
            //call ftn RollDice, placing returned number into integers
            dice1 = RollDice();
            dice2 = RollDice();
            //place integers into labels
            lblDice1.Text = dice1.ToString();
            lblDice2.Text = dice2.ToString();
            // call ftn GetName sending total and returning name
            string name = GetName(dice1 + dice2);
            //display name in label
            lblRollName.Text = name;
        }

        /* Name: ClearOneRoll
        *  Sent: nothing
        *  Return: nothing
        *  Clear the labels */
        private void ClearOneRoll()
        {
            lblDice1.Text = string.Empty;
            lblDice2.Text = string.Empty;
            lblRollName.Text = string.Empty;
        }


        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */
        private void ClearStats()
        {
            nudNumber.Value = nudNumber.Minimum;
            chkSeed.Checked = false;
            lblPass.Text = string.Empty;
            lblFail.Text = string.Empty;
            lblAverage.Text = string.Empty;
            lstMarks.Items.Clear();
        }


        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */
        private int RollDice()
        {
            return rand.Next(1, 7);
        }


        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/
        private string GetName(int total)
        {
            string name;
            switch (total)
            {
                case 2:
                    name = "Snake Eyes";
                    break;

                case 3:
                    name = "Litle Joe";
                    break;

                case 5:
                    name = "Fever";
                    break;

                case 7:
                    name = "Most Common";
                    break;

                case 9:
                    name = "Center Field";
                    break;

                case 11:
                    name = "Yo-leven";
                    break;

                case 12:
                    name = "Boxcars";
                    break;

                default:
                    name = "No special name";
                    break;
            }
            return name;
        }

        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            //call ftn DataPresent twice sending string returning boolean
            bool firstDice = DataPresent(lblDice1.Text);
            bool secondDice = DataPresent(lblDice2.Text);
            //if data present in both labels, call SwapData sending both strings
            if (firstDice == true && secondDice == true)
            {
                SwapData(lblDice1.Text, lblDice2.Text, out string newFirstDice, out string newSecondDice);

                //put data back into labels
                lblDice1.Text = newFirstDice;
                lblDice2.Text = newSecondDice;
            }
            //if data not present in either label display error msg
            else
            {
                MessageBox.Show("Roll the dice", "Data Missing");
            }
        }

        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/
        private bool DataPresent(string num)
        {
            bool valid;
            if (num == "")
            {
                valid = false;
            }
            else
            {
                valid = true;
            }

            return valid;
        }


        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/
        private void SwapData(string first, string second, out string newFirst, out string newSecond)
        {
            newFirst = second;
            newSecond = first;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // I added this to clear the listbox because every time you click the Generate button, it would add the new results to the old ones. 
            // I'm not sure if it's supposed to be done like this, but I included it just in case.
            lstMarks.Items.Clear();

            //declare variables and array
            int size = Convert.ToInt32(nudNumber.Value);
            int[] marks = new int[size];

            //check if seed value
            if (chkSeed.Checked)
            {
                rand = new Random(1000);
            }
            else 
            {
                rand = new Random();
            }
            //fill array using random number
            int i = 0;
            while (i < marks.Length)
            {
                marks[i] = rand.Next(40, 101);
                lstMarks.Items.Add(marks[i]);
                i++;
            }
            //call CalcStats sending and returning data
            int passMarks, failMarks;
            double average = CalcStats(marks, out passMarks, out failMarks);
            //display data sent back in labels - average, pass and fail
            lblPass.Text = passMarks.ToString();
            lblFail.Text = failMarks.ToString();
            // Format average always showing 2 decimal places 
            lblAverage.Text = average.ToString("F2");
        } // end Generate click

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            grpOneRoll.Visible = true;
            grpMarkStats.Visible = false;
            ClearOneRoll();
        }

        private void radRollStats_CheckedChanged(object sender, EventArgs e)
        {
            grpOneRoll.Visible = false;
            grpMarkStats.Visible = true;
            ClearStats();
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeed.Checked)
            {
                DialogResult selection = MessageBox.Show("Are you sure you want a seed value?", "Confirm Seed Value", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selection == DialogResult.No)
                {
                    chkSeed.Checked = false;
                }
            }
        }

        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        private double CalcStats(int[] grades, out int pass, out int fail) 
        {
            double sum = 0;
            pass = 0;
            fail = 0;

            foreach (int grade in grades) {
                sum += grade;
                if (grade >= 60)
                {
                    pass++;
                }
                else
                {
                    fail++;
                }
            }
            return sum / grades.Length;
        }
    }
}
