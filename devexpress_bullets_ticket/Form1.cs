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
            richEditControl.Document.AppendText(
                string.Join(
                    Environment.NewLine,
                    new string[]
                {
                    "Item 1",
                    "Item 2",
                    "Item 3",
                    "Item 4",
                    "Item 5",
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
                ListLevel level = list.Levels[0];
                level.ParagraphProperties.LeftIndent = 100;

                //Specify the bullets' format 
                //Without this step, the list is considered as numbered 
                level.DisplayFormatString = "\u00B7";
                level.CharacterProperties.FontName = "Symbol";

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
