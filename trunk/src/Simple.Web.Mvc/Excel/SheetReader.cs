﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Simple.Web.Mvc.Excel
{
    public class SheetReader<T>
    {
        protected RowReader<T> Reader { get; set; }
        public SheetReader(RowReader<T> reader) 
        {
            Reader = reader;
        }

        public IEnumerable<T> Read(WorksheetPart worksheet)
        {
            var data = worksheet.Worksheet.OfType<SheetData>().FirstOrDefault();
            if (data == null) yield break;

            foreach (var row in data.Descendants<Row>())
                yield return Reader.Read(row);
        }
    }
}
