using System;
using Microsoft.Office.Interop.Word;
using System.Drawing;
using System.IO;
using PassportOperator;

namespace PassportOperator
{
    public class MATCPassportExporter
    {
        public MATCPassportExporter(string template= "e:\\паспортГУ1.dotx")
        { Template = template; }
        private Application wordApp;
        public Application WordApp { get { if (wordApp == null) {
                    wordApp = new Application(); WordApp.Visible = true;
                    return wordApp; } else { return wordApp; } }} 
        public MATCPassport Passport { get; set; }
        public string Template { get; set; }
        public Document ConvertToWord(MATCPassport _passport)
        {
            Document doc = WordApp.Documents.Add(Template);
            Bookmark b;
            b = doc.Bookmarks["UNIU"];
            b.Range.Text = _passport.UNIU;
            b = doc.Bookmarks["CreateDate"];
            b.Range.Text = _passport.CreateDate;
            b = doc.Bookmarks["Address"];
            b.Range.Text = _passport.Address;
            b = doc.Bookmarks["Okrug"];
            b.Range.Text = _passport.Okrug;
            b = doc.Bookmarks["Raion"];
            b.Range.Text = _passport.Raion;
            b = doc.Bookmarks["GU"];
            b.Range.Text = _passport.GU;
            b = doc.Bookmarks["Closed_loop"];
            b.Range.Text = _passport.Closed_loop;
            b = doc.Bookmarks["Reconstruction"];
            b.Range.Text = _passport.Reconstruction;
            b = doc.Bookmarks["Electricity_connection"];
            b.Range.Text = _passport.Electricity_connection;
            b = doc.Bookmarks["Type_of_surface"];
            b.Range.Text = _passport.Type_of_surface;
            b = doc.Bookmarks["Visibility"];
            b.Range.Text = _passport.Visibility;
            b = doc.Bookmarks["Sidewalk_width"];
            b.Range.Text = _passport.Sidewalk_width;
            b = doc.Bookmarks["Traffic"];
            b.Range.Text = _passport.Traffic;
            b = doc.Bookmarks["State"];
            b.Range.Text = _passport.State;
            Image _foto = Image.FromStream(_passport.Foto);
            Image _plan = Image.FromStream(_passport.Plan);
            string _fotoFileName = Path.GetTempFileName();
            string _planFileName = Path.GetTempFileName();
            ScaleImage(_foto, 490, 270).Save(_fotoFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            ScaleImage(_plan, 490, 270).Save(_planFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            b = doc.Bookmarks["Foto"];
            b.Range.InlineShapes.AddPicture(_fotoFileName, false, true);
            b = doc.Bookmarks["Plan"];
            b.Range.InlineShapes.AddPicture(_planFileName, false, true);
            for (int n = 0; n < _passport.Information_fields.Count; n++)
            {
                MATCPassport_InformationField field = _passport.Information_fields[n];
                Row r = doc.Tables[3].Rows.Add();
                r.Cells[1].Range.Text = (n + 1).ToString();
                r.Cells[2].Range.Text = field.Arrow;
                r.Cells[3].Range.Text = field.Name;
                r.Cells[4].Range.Text = field.Transliteration;
                r.Range.Font.Bold = 9999998;
            }
            return doc;
        }
        public void ExportToDOCX(MATCPassport _passport,DirectoryInfo directory,Func<MATCPassport,string> NamingFunc,bool autoSave=true)
        {
            Document doc = ConvertToWord(_passport);
            if (autoSave)
            {
                doc.SaveAs2(Path.Combine(directory.FullName, NamingFunc(_passport)));
                doc.Close();
            }
        }

        public Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }
}
