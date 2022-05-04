using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace devexpress_bullets_ticket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Size = new Size(Size.Width, 500);
            richEditControl.Document.AppendText(
                string.Join(
                    Environment.NewLine,
                    new string[]
                {
                    "Level 0",
                    "Level 1 (Press Tab Once)",
                    "Level 2 (Press Tab Twice)",
                    "Level 3 (Press Tab x 3)",
                    "Level 4 (Press Tab x 4)",
                }
            ));
            btnBullets.Click += (sender, e) =>
            {
                richEditControl.SelectAll();
                execSampleCode();
            };
        }

        private void execSampleCode()
        {
            var document = richEditControl.Document;
            try
            {
                document.BeginUpdate();

                // Create a new list pattern object 
                AbstractNumberingList list = document.AbstractNumberingLists.Add();

                //Specify the list's type 
                list.NumberingType = NumberingType.Bullet;

                // Traverse the AbstractNumberingList.Levels collection
                // and customize the required levels of the list
                int i = 0;
                char c = 'i';
                while( i < list.Levels.Count)
                {
                    var level =  list.Levels[i];
                    level.ParagraphProperties.LeftIndent = 100 * (i + 1);

                    // Specify the character to use for the bullet.
                    // Without this step, the list is considered as numbered
                    level.DisplayFormatString = $"{c}";
                    level.CharacterProperties.FontName = "Wingdings 2";
                    i++; c++;
                }

                //Create a new list based on the specific pattern 
                NumberingList bulletedList = document.NumberingLists.Add(0);

                // Add paragraphs to the list 
                document.Paragraphs.AddParagraphsToList(document.Selection, bulletedList, 0);

            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
            finally
            {
                document.EndUpdate();
            }
        }
    }
}
