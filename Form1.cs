using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Voice_Recognition
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                synthesizer.SelectVoiceByHints(VoiceGender.Female);
                Choices commands = new Choices();
                commands.Add(new string[] { "say hello", "print my name" });
                GrammarBuilder gBuilder = new GrammarBuilder();
                gBuilder.Append(commands);
                Grammar grammar = new Grammar(gBuilder);

                recEngine.LoadGrammarAsync(grammar);
                recEngine.SetInputToDefaultAudioDevice();
                recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "say hello":
                    richTextBox1.Text += "\n say hello";
                    synthesizer.SpeakAsync("hello admin");
                    break;
                case "print my name":
                    richTextBox1.Text += "\n print my name";
                    synthesizer.SpeakAsync("Alex");
                    break;
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            try
            {
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
                btnEnable.Enabled = false;
                btnDisable.Enabled = true;
                if (label2.Text.Equals("Voice Control Off"))
                {
                    label2.Text = "Voice Control On";
                    label2.ForeColor = Color.ForestGreen;
                }
                else
                {
                    label2.Text = "Voice Control Off";
                    label2.ForeColor = Color.Black;
                }              
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }

        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            try
            {
                recEngine.RecognizeAsyncStop();
                btnDisable.Enabled = false;
                btnEnable.Enabled = true;

                if (label2.Text.Equals("Voice Control On"))
                {
                    label2.Text = "Voice Control Off";
                    label2.ForeColor = Color.Black;
                }
                else
                {
                    label2.Text = "Voice Control On";
                    label2.ForeColor = Color.ForestGreen;
                }

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

       
    }
}
