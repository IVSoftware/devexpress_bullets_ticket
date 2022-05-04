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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Size = new Size(Size.Width, 500);
            richEditControl.Document.AppendText(
@"Level 0
Level 1 (Press Tab Once)
Level 2 (Press Tab Twice)
Level 3 (Press Tab x 3)
Level 4 (Press Tab x 4)"
            );
            btnBullets.Click += (sender, e) =>
            {
                execSampleCode();
            };
        }

        private void execSampleCode()
        {
            richEditControl.SelectAll();
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

                    level.DisplayFormatString = $"{c}";
                    level.CharacterProperties.FontName = "Wingdings 2";

                    Debug.Assert(level.BulletLevel,  "Expecting 'true' if DisplayFormatString consists of a single character" );

                    // Spacing between bullet and text.
                    var bulletAlignValue =  100 * (i + 1);
                    var textIndentValue = bulletAlignValue + 75;
                    setBulletListIndents(bulletAlignValue, textIndentValue, level.ParagraphProperties);

                    i++; c++;
                }

                //Create a new list based on the specific pattern 
                NumberingList bulletedList = document.NumberingLists.Add(list.Index);

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

            #region L o c a l M e t h o d 
            static void setBulletListIndents(float bulletAlignValue, float textIndentValue, ParagraphPropertiesBase pp)
            {
                // https://supportcenter.devexpress.com/ticket/details/t810949/richeditcontrol-setup-indent-between-bullet-and-items-in-list
                pp.LeftIndent = textIndentValue;
                float probableFirstLineIndent = textIndentValue - bulletAlignValue;
                if (probableFirstLineIndent > 0)
                {
                    pp.FirstLineIndentType = ParagraphFirstLineIndent.Hanging;
                    pp.FirstLineIndent = probableFirstLineIndent;
                }
                else if (probableFirstLineIndent < 0)
                {
                    pp.FirstLineIndentType = ParagraphFirstLineIndent.Indented;
                    pp.FirstLineIndent = -probableFirstLineIndent;
                }
                else
                {
                    pp.FirstLineIndentType = ParagraphFirstLineIndent.None;
                    pp.FirstLineIndent = 0;
                }
            }
            #endregion L o c a l M e t h o d 
        }        
    }
}
