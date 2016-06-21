using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace LockPDF
{
    [Flags]
    public enum PdfPermissions
    {
        AllowAssembly = PdfWriter.AllowAssembly,
        AllowModifyContents = PdfWriter.AllowModifyContents,
        AllowCopy = PdfWriter.AllowCopy,
        AllowScreenReaders = PdfWriter.AllowScreenReaders,
        AllowPrinting = PdfWriter.AllowPrinting,
        AllowDegradedPrinting = PdfWriter.AllowDegradedPrinting,
        AllowFillIn = PdfWriter.AllowFillIn,
        AllowModifyAnnotations = PdfWriter.AllowModifyAnnotations,
        All = 0,
        All2 = AllowAssembly | AllowModifyContents | AllowCopy | AllowScreenReaders |
            AllowPrinting | AllowDegradedPrinting | AllowFillIn | AllowModifyAnnotations,
    }

    [Flags]
    public enum PdfLockState
    {
        None,
        Protected,
        Locked,
    }

    public class MyPdfReader : PdfReader
    {
        public MyPdfReader(String filename, byte[] pwd) : base(filename, pwd)
        {

        }
        public MyPdfReader(String filename) : base(filename)
        {
        }
        public void Decrypt()
        {
            encrypted = false;
        }
    }
}
