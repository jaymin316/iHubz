using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;

namespace iHubz.Infrastructure.CrossCutting.Excel.AsposeCells
{
    public class AsposeExcelWorkbook : IExcelWorkbook
    {
        #region Private properies

        private readonly Workbook _workbook;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wb"></param>
        public AsposeExcelWorkbook(Workbook wb)
        {
            if (wb == null)
                throw new NullReferenceException("wb");

            _workbook = wb;
        }

        #endregion

        #region IExcelWorkbook Members

        /// <summary>
        /// Add new sheet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IExcelWorksheet AddSheet(string name)
        {
            var ws = _workbook.Worksheets.Add(name);
            return new AsposeExcelWorksheet(ws);
        }

        /// <summary>
        /// Copy excel websheet from the source to the target workbook
        /// </summary>
        /// <param name="sourceSheetName"></param>
        /// <param name="sourceSheet"></param>
        /// <param name="targetSheetName"></param>
        /// <returns></returns>
        public void CopySheet(string sourceSheetName, IExcelWorkbook sourceSheet, string targetSheetName)
        {
            var targetWorksheet = GetSheet(targetSheetName) ?? AddSheet(targetSheetName);
            var sourceWorksheet = sourceSheet.GetSheet(sourceSheetName); // Get source websheet

            targetWorksheet.CopyFrom(sourceWorksheet);
        }

        /// <summary>
        /// Create named range
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reference"></param>
        public void CreateNamedRange(string name, string reference)
        {
            var i = _workbook.Worksheets.Names.Add(name);
            _workbook.Worksheets.Names[i].RefersTo = reference;
        }

        /// <summary>
        /// Get worksheet
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IExcelWorksheet GetSheet(string name)
        {
            var ws = _workbook.Worksheets[name];
            return ws != null ? new AsposeExcelWorksheet(ws) : null;
        }

        /// <summary>
        /// Remove sheet by name
        /// </summary>
        /// <param name="name"></param>
        public void RemoveSheet(string name)
        {
            _workbook.Worksheets.RemoveAt(name);
        }

        /// <summary>
        /// Remove sheet by index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveSheet(int index)
        {
            _workbook.Worksheets.RemoveAt(index);
        }

        /// <summary>
        /// Save workbook data to memory stream
        /// </summary>
        /// <returns></returns>
        public MemoryStream SaveToStream()
        {
            var stream = new MemoryStream();
            _workbook.Save(stream, GetSaveFormat());
            return stream;
        }

        /// <summary>
        /// Get all sheets
        /// </summary>
        public List<IExcelWorksheet> Sheets
        {
            get
            {
                return _workbook.Worksheets.Cast<Worksheet>()
                    .Select(i => new AsposeExcelWorksheet(i))
                    .Cast<IExcelWorksheet>()
                    .ToList();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get save format
        /// </summary>
        /// <returns></returns>
        private SaveFormat GetSaveFormat()
        {
            var fileFormat = _workbook.FileFormat;
            var saveFormat = SaveFormat.Excel97To2003;
            switch (fileFormat)
            {
                case FileFormatType.Xlsx:
                    saveFormat = SaveFormat.Xlsx;
                    break;

                case FileFormatType.Xlsm:
                    saveFormat = SaveFormat.Xlsm;
                    break;

                case FileFormatType.Xlsb:
                    saveFormat = SaveFormat.Xlsb;
                    break;
            }
            return saveFormat;
        }

        #endregion
    }
}
