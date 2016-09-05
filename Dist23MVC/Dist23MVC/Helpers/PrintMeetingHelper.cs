using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dist23MVC.Models;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text.pdf;

namespace Dist23MVC.Helpers

{
    public static class PrintMeetingHelper
    {
        static clsDataGetter dg;
        const int MYSIZE = 10;

        public static void MakeMeetingPDF()
        {
            dg = new clsDataGetter("Data Source = 184.168.194.75; Initial Catalog = aaphones2; Persist Security Info = True; User ID = billwilson12; Password = keepitsimple62");
            SqlDataReader dr = dg.GetDataReader("EXEC sp_GetMeetingsPrint");
            MakePDFForm(@"D:\My Documents\Projects\dist23mvc\Dist23MVC\Dist23MVC\upload\district_23_template.pdf", @"D:\My Documents\Projects\dist23mvc\Dist23MVC\Dist23MVC\upload\district_23101.pdf", dr);
            dg.KillReader(dr);
            dr = dg.GetDataReader("EXEC sp_GetMeetingsPrint");
            FillPDFForm(@"D:\My Documents\Projects\dist23mvc\Dist23MVC\Dist23MVC\upload\district_23101.pdf", @"D:\My Documents\Projects\dist23mvc\Dist23MVC\Dist23MVC\upload\district_23curr.pdf", dr);
            dg.KillReader(dr);
        }

        private static void MakePDFForm(string spath, string destpath, SqlDataReader dr)
        {
            using (var fs = new FileStream(destpath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var reader = new iTextSharp.text.pdf.PdfReader(new RandomAccessFileOrArray(spath), null);
                addFields(reader, fs, dr);
            }
        }
        private static void FillPDFForm(string spath, string destpath, SqlDataReader dr)
        {
            using (var fs = new FileStream(destpath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var reader = new iTextSharp.text.pdf.PdfReader(new RandomAccessFileOrArray(spath), null);
                fillFormFields(reader, fs, dr);
            }
        }

        private static void addFields(iTextSharp.text.pdf.PdfReader reader, FileStream fs, SqlDataReader dr)
        {
            using (var pdfStamper = new PdfStamper(reader, fs))
            {
                int i = 0;
                int fieldIndex = 0;
                int page = 1;
                int top;
                int bottom;
                while (dr.Read())
                {
                    if (i > 22 && page == 1)
                    {
                        page = 2;
                        i = 1;
                    }
                    i++;
                    fieldIndex++;
                    int lineSp = (i - 1) * 20;
                    if (page == 1)
                    {
                        top = 450 - lineSp;
                        bottom = 470 - lineSp;

                    }
                    else
                    {
                        top = 600 - lineSp;
                        bottom = 620 - lineSp;
                    }
                    iTextSharp.text.BaseColor currBackgrd;
                    if (i % 2 == 0)
                        currBackgrd = iTextSharp.text.BaseColor.WHITE;
                    else
                        currBackgrd = iTextSharp.text.BaseColor.LIGHT_GRAY;


                    TextField field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(20, top, 90, bottom), "Day" + fieldIndex.ToString());
                    field.FontSize = MYSIZE;
                    field.Rotation = 90;
                    field.BackgroundColor = currBackgrd;
                    pdfStamper.AddAnnotation(field.GetTextField(), page);

                    field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(92, top, 145, bottom), "Time" + fieldIndex.ToString());
                    field.FontSize = MYSIZE;
                    field.Rotation = 90;
                    field.BackgroundColor = currBackgrd;
                    pdfStamper.AddAnnotation(field.GetTextField(), page);

                    field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(147, top, 197, bottom), "Type" + fieldIndex.ToString());
                    field.FontSize = MYSIZE;
                    field.Rotation = 90;
                    field.BackgroundColor = currBackgrd;
                    pdfStamper.AddAnnotation(field.GetTextField(), page);

                    field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(199, top, 309, bottom), "Topic" + fieldIndex.ToString());
                    field.FontSize = MYSIZE;
                    field.Rotation = 90;
                    field.BackgroundColor = currBackgrd;
                    pdfStamper.AddAnnotation(field.GetTextField(), page);

                    field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(311, top, 461, bottom), "aaGroup" + fieldIndex.ToString());
                    field.FontSize = MYSIZE;
                    field.Rotation = 90;
                    field.BackgroundColor = currBackgrd;
                    pdfStamper.AddAnnotation(field.GetTextField(), page);

                    field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(463, top, 690, bottom), "Location" + fieldIndex.ToString());
                    field.FontSize = MYSIZE;
                    field.Rotation = 90;
                    field.BackgroundColor = currBackgrd;
                    pdfStamper.AddAnnotation(field.GetTextField(), page);

                    field = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(692, top, 782, bottom), "City" + fieldIndex.ToString());
                    field.FontSize = MYSIZE;
                    field.Rotation = 90;
                    field.BackgroundColor = currBackgrd;
                    pdfStamper.AddAnnotation(field.GetTextField(), page);

                }
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
                reader.Close();
            }
            fs.Close();
        }
        private static void fillFormFields(iTextSharp.text.pdf.PdfReader reader, FileStream fs, SqlDataReader dr)
        {
            using (var pdfStamper = new PdfStamper(reader, fs))
            {
                int i = 0;
                while (dr.Read())
                {
                    i++;
                    pdfStamper.AcroFields.SetField("Day" + i.ToString(), FormatField(dr["Day"].ToString(), "Day"));
                    pdfStamper.AcroFields.SetField("Time" + i.ToString(), FormatField(dr["Time"].ToString(), "Time"));
                    pdfStamper.AcroFields.SetField("Type" + i.ToString(), FormatField(dr["Type"].ToString(), "Type"));
                    pdfStamper.AcroFields.SetField("Topic" + i.ToString(), FormatField(dr["Topic"].ToString(), "Topic"));
                    pdfStamper.AcroFields.SetField("Group" + i.ToString(), FormatField(dr["aaGroup"].ToString(), "aaGroup"));
                    pdfStamper.AcroFields.SetField("Location" + i.ToString(), FormatField(dr["Location"].ToString(), "Location"));
                    pdfStamper.AcroFields.SetField("City" + i.ToString(), FormatField(dr["City"].ToString(), "City"));
                }
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
                reader.Close();
            }

        }

        private static PdfStamper AddField(string source, string dest)
        {
            PdfReader reader = new PdfReader(source);

            FileStream output = new FileStream(dest, FileMode.Create, FileAccess.Write);

            PdfStamper stamp = new PdfStamper(reader, output);

            return stamp;
        }
        private static string FormatField(string fieldData, string fieldName)
        {
            return fieldData;
            //bool mustSplit = false;
            //int maxLength = dg.GetScalarInteger("SELECT MAX(LEN(" + fieldName + ")) FROM meetings");
            //if (maxLength > 15 && fieldData.Contains(" "))
            //    mustSplit = true;

            //string newStr = "";
            //if (mustSplit)
            //{
            //    string[] strs = fieldData.Split(' ');
            //    int wordCount = strs.Length;
            //    decimal splitDec = wordCount / 2;
            //    int splitSpot = ((int)Math.Floor(splitDec) - 1);
            //    strs[splitSpot] += "\n\r";
            //    for (int x = 0;x < wordCount; x++)
            //    {
            //        newStr += strs[x];
            //    }

            //}
            //else
            //    newStr = fieldData.PadRight(maxLength);
            //return newStr;
        }
    }
}
