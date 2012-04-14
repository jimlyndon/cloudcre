using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Cloudcre.Service.Property.ViewModels;

namespace Cloudcre.Service.Report.Summary.Office
{
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.ExtendedProperties;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using DocumentFormat.OpenXml.VariantTypes;
    using Color = DocumentFormat.OpenXml.Spreadsheet.Color;
    using DifferentialFormats = DocumentFormat.OpenXml.Spreadsheet.DifferentialFormats;
    using WorkbookProperties = DocumentFormat.OpenXml.Spreadsheet.WorkbookProperties;

    public class SummaryReport
    {
        private GregorianCalendar _gcalendar;

        public GregorianCalendar GCalendar
        {
            get { return _gcalendar ?? (_gcalendar = new GregorianCalendar()); }
        }

        // Creates a SpreadsheetDocument.
        public void CreatePackage(string filePath, IEnumerable<OfficeViewModel> viewModels)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                CreateParts(package, viewModels);
            }
        }

        public void CreatePackage(MemoryStream mStream, IEnumerable<OfficeViewModel> viewModels)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(mStream, SpreadsheetDocumentType.Workbook))
            {
                CreateParts(package, viewModels);
                package.Close();
            }
        }

        // Adds child parts and generates content of the specified part.
        private void CreateParts(SpreadsheetDocument document, IEnumerable<OfficeViewModel> viewModels)
        {
            var extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            GenerateExtendedFileProperties(extendedFilePropertiesPart1);

            WorkbookPart workbookPart = document.AddWorkbookPart();
            GenerateWorkbook(workbookPart);

            var sharedStringTable = workbookPart.AddNewPart<SharedStringTablePart>("rId6");
            GenerateSharedStringTable(sharedStringTable);

            var propertySummaryWorkSheet = workbookPart.AddNewPart<WorksheetPart>("rId1");
            BuildWorkSheet(propertySummaryWorkSheet, sharedStringTable, viewModels);

            var workbookStyles = workbookPart.AddNewPart<WorkbookStylesPart>("rId5");
            GenerateWorkbookStyles(workbookStyles);

            SetPackageProperties(document);
        }

        // Generates content of extendedFilePropertiesPart1.
        private void GenerateExtendedFileProperties(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            var properties1 = new Properties();
            properties1.AddNamespaceDeclaration("vt",
                                                "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            var totalTime1 = new TotalTime { Text = "0" };
            var application1 = new Application { Text = "Microsoft Excel" };
            var documentSecurity1 = new DocumentSecurity { Text = "0" };
            var scaleCrop1 = new ScaleCrop { Text = "false" };

            var headingPairs1 = new HeadingPairs();

            var vTVector1 = new VTVector { BaseType = VectorBaseValues.Variant, Size = 4U };

            var variant1 = new Variant();
            var vTLPSTR1 = new VTLPSTR { Text = "Worksheets" };

            variant1.Append(vTLPSTR1);

            var variant2 = new Variant();
            var vTInt321 = new VTInt32 { Text = "1" };

            variant2.Append(vTInt321);

            var variant3 = new Variant();
            var vTLPSTR2 = new VTLPSTR { Text = "Named Ranges" };

            variant3.Append(vTLPSTR2);

            var variant4 = new Variant();
            var vTInt322 = new VTInt32 { Text = "1" };

            variant4.Append(vTInt322);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);
            vTVector1.Append(variant3);
            vTVector1.Append(variant4);

            headingPairs1.Append(vTVector1);

            var titlesOfParts1 = new TitlesOfParts();

            var vTVector2 = new VTVector { BaseType = VectorBaseValues.Lpstr, Size = 2U };
            var vTLPSTR3 = new VTLPSTR { Text = "Sales" };
            var vTLPSTR4 = new VTLPSTR { Text = "Sales!Print_Area" };

            vTVector2.Append(vTLPSTR3);
            vTVector2.Append(vTLPSTR4);

            titlesOfParts1.Append(vTVector2);
            var linksUpToDate1 = new LinksUpToDate { Text = "false" };
            var sharedDocument1 = new SharedDocument { Text = "false" };
            var hyperlinksChanged1 = new HyperlinksChanged { Text = "false" };
            var applicationVersion1 = new ApplicationVersion { Text = "14.0300" };

            properties1.Append(totalTime1);
            properties1.Append(application1);
            properties1.Append(documentSecurity1);
            properties1.Append(scaleCrop1);
            properties1.Append(headingPairs1);
            properties1.Append(titlesOfParts1);
            properties1.Append(linksUpToDate1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
        }

        // Generates content of workbookPart1.
        private void GenerateWorkbook(WorkbookPart workbookPart1)
        {
            var workbook1 = new Workbook();
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            var fileVersion1 = new FileVersion { ApplicationName = "xl", LastEdited = "5", LowestEdited = "5", BuildVersion = "9302" };
            var workbookProperties1 = new WorkbookProperties();

            var bookViews1 = new BookViews();
            var workbookView1 = new WorkbookView { XWindow = 0, YWindow = 0, WindowWidth = 16380U, WindowHeight = 8190U };

            bookViews1.Append(workbookView1);

            var sheets1 = new Sheets();
            var sheet1 = new Sheet { Name = "Sales", SheetId = 1U, Id = "rId1" };

            sheets1.Append(sheet1);

            workbook1.Append(fileVersion1);
            workbook1.Append(workbookProperties1);
            workbook1.Append(bookViews1);
            workbook1.Append(sheets1);

            workbookPart1.Workbook = workbook1;
        }

        // Generates content of worksheetPart1.
        private void BuildWorkSheet(WorksheetPart worksheetPart1, SharedStringTablePart sharedStringTablePart, IEnumerable<OfficeViewModel> viewModels)
        {
            var worksheet1 = new Worksheet { MCAttributes = new MarkupCompatibilityAttributes { Ignorable = "x14ac" } };
            worksheet1.AddNamespaceDeclaration("r",
                                               "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            var sheetProperties1 = new SheetProperties();
            var pageSetupProperties1 = new PageSetupProperties { FitToPage = true };

            sheetProperties1.Append(pageSetupProperties1);
            var sheetDimension1 = new SheetDimension { Reference = "A1:BX128" };

            var sheetViews1 = new SheetViews();

            var sheetView1 = new SheetView { TabSelected = true, ZoomScale = 75U, ZoomScaleNormal = 75U, WorkbookViewId = 0U };
            var selection1 = new Selection
            {
                ActiveCell = "B2",
                SequenceOfReferences = new ListValue<StringValue> { InnerText = "B2:Q2" }
            };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            var sheetFormatProperties1 = new SheetFormatProperties { DefaultRowHeight = 12.75D, DyDescent = 0.2D };

            var columns1 = new Columns();
            var column1 = new Column { Min = 1U, Max = 1U, Width = 0.85546875D, CustomWidth = true };
            var column2 = new Column { Min = 2U, Max = 2U, Width = 3.42578125D, CustomWidth = true };
            var column3 = new Column { Min = 3U, Max = 3U, Width = 28.7109375D, CustomWidth = true };
            var column4 = new Column { Min = 4U, Max = 4U, Width = 0.85546875D, CustomWidth = true };
            var column5 = new Column { Min = 5U, Max = 5U, Width = 8.42578125D, CustomWidth = true };
            var column6 = new Column { Min = 6U, Max = 6U, Width = 10.7109375D, CustomWidth = true };
            var column7 = new Column { Min = 7U, Max = 7U, Width = 0.85546875D, CustomWidth = true };
            var column8 = new Column { Min = 8U, Max = 8U, Width = 15.140625D, CustomWidth = true };
            var column9 = new Column { Min = 9U, Max = 9U, Width = 14.7109375D, CustomWidth = true };
            var column10 = new Column { Min = 10U, Max = 10U, Width = 0.85546875D, CustomWidth = true };
            var column11 = new Column { Min = 11U, Max = 11U, Width = 14.85546875D, Style = 1U, CustomWidth = true };
            var column12 = new Column { Min = 12U, Max = 12U, Width = 1.140625D, CustomWidth = true };
            var column13 = new Column { Min = 13U, Max = 13U, Width = 17D, CustomWidth = true };
            var column14 = new Column { Min = 14U, Max = 14U, Width = 24.5703125D, CustomWidth = true };
            var column15 = new Column { Min = 15U, Max = 15U, Width = 10.140625D, Style = 1U, CustomWidth = true };
            var column16 = new Column { Min = 16U, Max = 16U, Width = 0.85546875D, CustomWidth = true };
            var column17 = new Column { Min = 17U, Max = 17U, Width = 74.85546875D, CustomWidth = true };
            var column18 = new Column { Min = 18U, Max = 18U, Width = 0.85546875D, CustomWidth = true };

            columns1.Append(column1, column2, column3, column4, column5, column6, column7, column8, column9, column10,
                            column11, column12, column13, column14, column15, column16, column17, column18);

            var sheetData1 = new SheetData();
            var mergeCells1 = new MergeCells { Count = 19U };
            var properties = viewModels.ToList();

            var comparables = properties.Where(x => x.SaleStatus() != PropertyViewModelHelper.PropertySaleStatus.Listed).ToList();

            // create primary header
            BuildHeaderPrimary(sheetData1, sharedStringTablePart);

            // comparables for current year
            IEnumerable<OfficeViewModel> propertiesYtd =
                comparables.Where(x => x.SaleDate != null && GCalendar.GetYear(x.SaleDate.Value) == GCalendar.GetYear(DateTime.Today));

            mergeCells1.Append(
                BuildProperties(sheetData1, sharedStringTablePart, propertiesYtd).Cast<MergeCell>().Select(
                    x => x.CloneNode(true)));

            // comparables for prior year
            List<OfficeViewModel> propertiesPriorYear =
                comparables.Where(x => x.SaleDate != null && GCalendar.GetYear(x.SaleDate.Value) == GCalendar.GetYear(DateTime.Today.AddYears(-1))).ToList();

            // comparables for previous years
            var previousYearsDates = comparables
                .Where(x => x.SaleDate != null && GCalendar.GetYear(x.SaleDate.Value) < GCalendar.GetYear(DateTime.Today.AddYears(-1)))
                .Select(x => x.SaleDate != null ? GCalendar.GetYear(x.SaleDate.Value) : 0).Distinct().ToList();

            // create subheader for prior year if any prior/previous years exist
            if (propertiesPriorYear.Count() > 0 || previousYearsDates.Count() > 0)
            {
                mergeCells1.Append(BuildSubHeaderPriorYear(sheetData1, sharedStringTablePart).Cast<MergeCell>().Select(
                    x => x.CloneNode(true)));
            }

            // create prior year properties
            if (propertiesPriorYear.Count() > 0)
            {
                mergeCells1.Append(BuildProperties(sheetData1, sharedStringTablePart, propertiesPriorYear).Cast<MergeCell>().Select(
                    x => x.CloneNode(true)));
            }

            // create previous years properties
            foreach (var date in previousYearsDates)
            {
                var d = date;

                BuildSubHeaderForYear(sheetData1, sharedStringTablePart, d);
                
                IEnumerable<OfficeViewModel> propertiesPreviousYears =
                    comparables.Where(
                        x => x.SaleDate != null && GCalendar.GetYear(x.SaleDate.Value) == d);

                mergeCells1.Append(BuildProperties(sheetData1, sharedStringTablePart, propertiesPreviousYears).Cast<MergeCell>().Select(
                    x => x.CloneNode(true)));
            }

            // build Listings
            var listings = properties.Where(x => x.SaleStatus() == PropertyViewModelHelper.PropertySaleStatus.Listed).ToList();
            mergeCells1.Append(BuildListings(sheetData1, sharedStringTablePart, listings).Cast<MergeCell>().Select(
                    x => x.CloneNode(true)));

            // footer/legend
            BuildLegend(sheetData1, sharedStringTablePart);

            var sheetProtection1 = new SheetProtection { SelectLockedCells = true, SelectUnlockedCells = true };

            
            var mergeCell1 = new MergeCell { Reference = "B2:Q2" };
            var mergeCell2 = new MergeCell { Reference = "B3:Q3" };
            var mergeCell3 = new MergeCell { Reference = "E5:F5" };
            var mergeCell4 = new MergeCell { Reference = "H5:I5" };
            var mergeCell5 = new MergeCell { Reference = "M5:O5" };

            mergeCells1.Append(mergeCell1, mergeCell2, mergeCell3, mergeCell4, mergeCell5);

            var pageMargins1 = new PageMargins
            {
                Left = 1.1097222222222223D,
                Right = 0.74791666666666667D,
                Top = 0.98402777777777772D,
                Bottom = 0.98402777777777772D,
                Header = 0.51180555555555551D,
                Footer = 0.51180555555555551D
            };
            var pageSetup1 = new PageSetup
            {
                FirstPageNumber = 0U,
                Orientation = OrientationValues.Landscape,
                HorizontalDpi = 300U,
                VerticalDpi = 300U
            };
            var headerFooter1 = new HeaderFooter { AlignWithMargins = false };

            worksheet1.Append(sheetProperties1);
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(sheetProtection1);
            worksheet1.Append(mergeCells1);
            worksheet1.Append(pageMargins1);
            worksheet1.Append(pageSetup1);
            worksheet1.Append(headerFooter1);

            worksheetPart1.Worksheet = worksheet1;
        }

        private MergeCells BuildListings(SheetData sheetData1, SharedStringTablePart sharedStringTablePart, List<OfficeViewModel> listings)
        {
            if (listings.Count() > 0)
            {
                BuildSubHeaderListings(sheetData1, sharedStringTablePart);
            }

            return BuildProperties(sheetData1, sharedStringTablePart, listings);
        }

        private void BuildHeaderPrimary(SheetData sheetData1, SharedStringTablePart sharedStringTablePart)
        {
            var row1 = new Row
            {
                RowIndex = 1U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                StyleIndex = 2U,
                CustomFormat = true,
                Height = 5.25D,
                CustomHeight = true,
                DyDescent = 0.2D
            };
            var cell1 = new Cell { CellReference = "A1" };
            var cell2 = new Cell { CellReference = "B1" };
            var cell3 = new Cell { CellReference = "C1" };
            var cell4 = new Cell { CellReference = "D1" };
            var cell5 = new Cell { CellReference = "E1" };
            var cell6 = new Cell { CellReference = "F1" };
            var cell7 = new Cell { CellReference = "G1" };
            var cell8 = new Cell { CellReference = "H1" };
            var cell9 = new Cell { CellReference = "I1" };
            var cell10 = new Cell { CellReference = "J1" };
            var cell11 = new Cell { CellReference = "K1", StyleIndex = 1U };
            var cell12 = new Cell { CellReference = "L1" };
            var cell13 = new Cell { CellReference = "M1" };
            var cell14 = new Cell { CellReference = "N1" };
            var cell15 = new Cell { CellReference = "O1", StyleIndex = 1U };
            var cell16 = new Cell { CellReference = "P1" };
            var cell17 = new Cell { CellReference = "Q1" };
            var cell18 = new Cell { CellReference = "R1" };

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);
            row1.Append(cell4);
            row1.Append(cell5);
            row1.Append(cell6);
            row1.Append(cell7);
            row1.Append(cell8);
            row1.Append(cell9);
            row1.Append(cell10);
            row1.Append(cell11);
            row1.Append(cell12);
            row1.Append(cell13);
            row1.Append(cell14);
            row1.Append(cell15);
            row1.Append(cell16);
            row1.Append(cell17);
            row1.Append(cell18);

            var row2 = new Row
            {
                RowIndex = 2U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                StyleIndex = 2U,
                CustomFormat = true,
                Height = 20.25D,
                CustomHeight = true,
                DyDescent = 0.25D
            };

            var cell19 = new Cell { CellReference = "B2", StyleIndex = 84U, DataType = CellValues.SharedString };
            var cellValue1 = new CellValue();
            cellValue1.Text = sharedStringTablePart.SharedString("COMPARABLE OFFICE BUILDING SALES");

            cell19.Append(cellValue1);
            var cell20 = new Cell { CellReference = "C2", StyleIndex = 84U };
            var cell21 = new Cell { CellReference = "D2", StyleIndex = 84U };
            var cell22 = new Cell { CellReference = "E2", StyleIndex = 84U };
            var cell23 = new Cell { CellReference = "F2", StyleIndex = 84U };
            var cell24 = new Cell { CellReference = "G2", StyleIndex = 84U };
            var cell25 = new Cell { CellReference = "H2", StyleIndex = 84U };
            var cell26 = new Cell { CellReference = "I2", StyleIndex = 84U };
            var cell27 = new Cell { CellReference = "J2", StyleIndex = 84U };
            var cell28 = new Cell { CellReference = "K2", StyleIndex = 84U };
            var cell29 = new Cell { CellReference = "L2", StyleIndex = 84U };
            var cell30 = new Cell { CellReference = "M2", StyleIndex = 84U };
            var cell31 = new Cell { CellReference = "N2", StyleIndex = 84U };
            var cell32 = new Cell { CellReference = "O2", StyleIndex = 84U };
            var cell33 = new Cell { CellReference = "P2", StyleIndex = 84U };
            var cell34 = new Cell { CellReference = "Q2", StyleIndex = 84U };

            row2.Append(cell19);
            row2.Append(cell20);
            row2.Append(cell21);
            row2.Append(cell22);
            row2.Append(cell23);
            row2.Append(cell24);
            row2.Append(cell25);
            row2.Append(cell26);
            row2.Append(cell27);
            row2.Append(cell28);
            row2.Append(cell29);
            row2.Append(cell30);
            row2.Append(cell31);
            row2.Append(cell32);
            row2.Append(cell33);
            row2.Append(cell34);

            var row3 = new Row
            {
                RowIndex = 3U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 12.75D,
                CustomHeight = true,
                DyDescent = 0.2D
            };
            var cell35 = new Cell { CellReference = "A3", StyleIndex = 2U };

            var cell36 = new Cell { CellReference = "B3", StyleIndex = 85U, DataType = CellValues.SharedString };
            var cellValue2 = new CellValue
            {
                Text =
                    sharedStringTablePart.SharedString(
                        "123 NE 4th Street, Ft Lauderdale (File:10-0100)")
            };

            cell36.Append(cellValue2);
            var cell37 = new Cell { CellReference = "C3", StyleIndex = 85U };
            var cell38 = new Cell { CellReference = "D3", StyleIndex = 85U };
            var cell39 = new Cell { CellReference = "E3", StyleIndex = 85U };
            var cell40 = new Cell { CellReference = "F3", StyleIndex = 85U };
            var cell41 = new Cell { CellReference = "G3", StyleIndex = 85U };
            var cell42 = new Cell { CellReference = "H3", StyleIndex = 85U };
            var cell43 = new Cell { CellReference = "I3", StyleIndex = 85U };
            var cell44 = new Cell { CellReference = "J3", StyleIndex = 85U };
            var cell45 = new Cell { CellReference = "K3", StyleIndex = 85U };
            var cell46 = new Cell { CellReference = "L3", StyleIndex = 85U };
            var cell47 = new Cell { CellReference = "M3", StyleIndex = 85U };
            var cell48 = new Cell { CellReference = "N3", StyleIndex = 85U };
            var cell49 = new Cell { CellReference = "O3", StyleIndex = 85U };
            var cell50 = new Cell { CellReference = "P3", StyleIndex = 85U };
            var cell51 = new Cell { CellReference = "Q3", StyleIndex = 85U };

            row3.Append(cell35);
            row3.Append(cell36);
            row3.Append(cell37);
            row3.Append(cell38);
            row3.Append(cell39);
            row3.Append(cell40);
            row3.Append(cell41);
            row3.Append(cell42);
            row3.Append(cell43);
            row3.Append(cell44);
            row3.Append(cell45);
            row3.Append(cell46);
            row3.Append(cell47);
            row3.Append(cell48);
            row3.Append(cell49);
            row3.Append(cell50);
            row3.Append(cell51);

            var row4 = new Row
            {
                RowIndex = 4U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 3.75D,
                CustomHeight = true,
                DyDescent = 0.2D
            };
            var cell52 = new Cell { CellReference = "A4", StyleIndex = 3U };
            var cell53 = new Cell { CellReference = "B4", StyleIndex = 3U };
            var cell54 = new Cell { CellReference = "C4", StyleIndex = 3U };
            var cell55 = new Cell { CellReference = "D4", StyleIndex = 3U };
            var cell56 = new Cell { CellReference = "E4", StyleIndex = 3U };
            var cell57 = new Cell { CellReference = "F4", StyleIndex = 3U };
            var cell58 = new Cell { CellReference = "G4", StyleIndex = 3U };
            var cell59 = new Cell { CellReference = "H4", StyleIndex = 3U };
            var cell60 = new Cell { CellReference = "I4", StyleIndex = 3U };
            var cell61 = new Cell { CellReference = "J4", StyleIndex = 3U };
            var cell62 = new Cell { CellReference = "K4", StyleIndex = 4U };
            var cell63 = new Cell { CellReference = "L4", StyleIndex = 3U };
            var cell64 = new Cell { CellReference = "M4", StyleIndex = 3U };
            var cell65 = new Cell { CellReference = "N4", StyleIndex = 3U };
            var cell66 = new Cell { CellReference = "O4", StyleIndex = 4U };
            var cell67 = new Cell { CellReference = "P4", StyleIndex = 3U };
            var cell68 = new Cell { CellReference = "Q4", StyleIndex = 3U };
            var cell69 = new Cell { CellReference = "R4", StyleIndex = 3U };

            row4.Append(cell52);
            row4.Append(cell53);
            row4.Append(cell54);
            row4.Append(cell55);
            row4.Append(cell56);
            row4.Append(cell57);
            row4.Append(cell58);
            row4.Append(cell59);
            row4.Append(cell60);
            row4.Append(cell61);
            row4.Append(cell62);
            row4.Append(cell63);
            row4.Append(cell64);
            row4.Append(cell65);
            row4.Append(cell66);
            row4.Append(cell67);
            row4.Append(cell68);
            row4.Append(cell69);

            var row5 = new Row
            {
                RowIndex = 5U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 19.5D,
                CustomHeight = true,
                DyDescent = 0.2D
            };
            var cell70 = new Cell { CellReference = "A5", StyleIndex = 2U };

            var cell71 = new Cell { CellReference = "B5", StyleIndex = 5U, DataType = CellValues.SharedString };
            var cellValue3 = new CellValue();
            cellValue3.Text = sharedStringTablePart.SharedString("PROPERTY");

            cell71.Append(cellValue3);
            var cell72 = new Cell { CellReference = "C5", StyleIndex = 3U };
            var cell73 = new Cell { CellReference = "D5", StyleIndex = 2U };

            var cell74 = new Cell { CellReference = "E5", StyleIndex = 86U, DataType = CellValues.SharedString };
            var cellValue4 = new CellValue();
            cellValue4.Text = sharedStringTablePart.SharedString("SITE");

            cell74.Append(cellValue4);
            var cell75 = new Cell { CellReference = "F5", StyleIndex = 86U };
            var cell76 = new Cell { CellReference = "G5", StyleIndex = 2U };

            var cell77 = new Cell { CellReference = "H5", StyleIndex = 86U, DataType = CellValues.SharedString };
            var cellValue5 = new CellValue();
            cellValue5.Text = sharedStringTablePart.SharedString("BUILDING");

            cell77.Append(cellValue5);
            var cell78 = new Cell { CellReference = "I5", StyleIndex = 86U };
            var cell79 = new Cell { CellReference = "J5", StyleIndex = 2U };

            var cell80 = new Cell { CellReference = "K5", StyleIndex = 6U, DataType = CellValues.SharedString };
            var cellValue6 = new CellValue();
            cellValue6.Text = sharedStringTablePart.SharedString("ECONOMICS");

            cell80.Append(cellValue6);
            var cell81 = new Cell { CellReference = "L5", StyleIndex = 2U };

            var cell82 = new Cell { CellReference = "M5", StyleIndex = 86U, DataType = CellValues.SharedString };
            var cellValue7 = new CellValue();
            cellValue7.Text = sharedStringTablePart.SharedString("SALE");

            cell82.Append(cellValue7);
            var cell83 = new Cell { CellReference = "N5", StyleIndex = 86U };
            var cell84 = new Cell { CellReference = "O5", StyleIndex = 86U };
            var cell85 = new Cell { CellReference = "P5", StyleIndex = 2U };

            var cell86 = new Cell { CellReference = "Q5", StyleIndex = 5U, DataType = CellValues.SharedString };
            var cellValue8 = new CellValue();
            cellValue8.Text = sharedStringTablePart.SharedString("COMMENTS");

            cell86.Append(cellValue8);

            row5.Append(cell70);
            row5.Append(cell71);
            row5.Append(cell72);
            row5.Append(cell73);
            row5.Append(cell74);
            row5.Append(cell75);
            row5.Append(cell76);
            row5.Append(cell77);
            row5.Append(cell78);
            row5.Append(cell79);
            row5.Append(cell80);
            row5.Append(cell81);
            row5.Append(cell82);
            row5.Append(cell83);
            row5.Append(cell84);
            row5.Append(cell85);
            row5.Append(cell86);

            var row6 = new Row
            {
                RowIndex = 6U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 6D,
                CustomHeight = true,
                DyDescent = 0.2D
            };
            var cell87 = new Cell { CellReference = "A6", StyleIndex = 2U };
            var cell88 = new Cell { CellReference = "B6", StyleIndex = 7U };
            var cell89 = new Cell { CellReference = "C6", StyleIndex = 2U };
            var cell90 = new Cell { CellReference = "D6", StyleIndex = 2U };
            var cell91 = new Cell { CellReference = "E6", StyleIndex = 2U };
            var cell92 = new Cell { CellReference = "F6", StyleIndex = 2U };
            var cell93 = new Cell { CellReference = "G6", StyleIndex = 2U };
            var cell94 = new Cell { CellReference = "H6", StyleIndex = 2U };
            var cell95 = new Cell { CellReference = "I6", StyleIndex = 7U };
            var cell96 = new Cell { CellReference = "J6", StyleIndex = 2U };

            var cell97 = new Cell { CellReference = "K6", StyleIndex = 8U, DataType = CellValues.SharedString };
            var cellValue9 = new CellValue { Text = sharedStringTablePart.SharedString(" ") };

            cell97.Append(cellValue9);
            var cell98 = new Cell { CellReference = "L6", StyleIndex = 2U };
            var cell99 = new Cell { CellReference = "M6", StyleIndex = 2U };
            var cell100 = new Cell { CellReference = "N6", StyleIndex = 2U };
            var cell101 = new Cell { CellReference = "O6", StyleIndex = 8U };
            var cell102 = new Cell { CellReference = "P6", StyleIndex = 2U };
            var cell103 = new Cell { CellReference = "Q6", StyleIndex = 7U };

            row6.Append(cell87);
            row6.Append(cell88);
            row6.Append(cell89);
            row6.Append(cell90);
            row6.Append(cell91);
            row6.Append(cell92);
            row6.Append(cell93);
            row6.Append(cell94);
            row6.Append(cell95);
            row6.Append(cell96);
            row6.Append(cell97);
            row6.Append(cell98);
            row6.Append(cell99);
            row6.Append(cell100);
            row6.Append(cell101);
            row6.Append(cell102);
            row6.Append(cell103);

            var row7 = new Row
            {
                RowIndex = 7U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 14.25D,
                CustomHeight = true,
                DyDescent = 0.2D
            };
            var cell104 = new Cell { CellReference = "A7", StyleIndex = 2U };
            var cell105 = new Cell { CellReference = "B7", StyleIndex = 2U };

            var cell106 = new Cell { CellReference = "C7", StyleIndex = 7U, DataType = CellValues.SharedString };
            var cellValue10 = new CellValue();
            cellValue10.Text = sharedStringTablePart.SharedString("Location");

            cell106.Append(cellValue10);
            var cell107 = new Cell { CellReference = "D7", StyleIndex = 9U };

            var cell108 = new Cell { CellReference = "E7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue11 = new CellValue();
            cellValue11.Text = sharedStringTablePart.SharedString("SF");

            cell108.Append(cellValue11);

            var cell109 = new Cell { CellReference = "F7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue12 = new CellValue();
            cellValue12.Text = sharedStringTablePart.SharedString("FAR");

            cell109.Append(cellValue12);
            var cell110 = new Cell { CellReference = "G7", StyleIndex = 9U };

            var cell111 = new Cell { CellReference = "H7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue13 = new CellValue();
            cellValue13.Text = sharedStringTablePart.SharedString("SF");

            cell111.Append(cellValue13);

            var cell112 = new Cell { CellReference = "I7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue14 = new CellValue();
            cellValue14.Text = sharedStringTablePart.SharedString("Class / Qual");

            cell112.Append(cellValue14);
            var cell113 = new Cell { CellReference = "J7", StyleIndex = 9U };

            var cell114 = new Cell { CellReference = "K7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue15 = new CellValue();
            cellValue15.Text = sharedStringTablePart.SharedString("Occupancy");

            cell114.Append(cellValue15);
            var cell115 = new Cell { CellReference = "L7", StyleIndex = 9U };

            var cell116 = new Cell { CellReference = "M7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue16 = new CellValue();
            cellValue16.Text = sharedStringTablePart.SharedString("Price");

            cell116.Append(cellValue16);

            var cell117 = new Cell { CellReference = "N7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue17 = new CellValue();
            cellValue17.Text = sharedStringTablePart.SharedString("Grantor");

            cell117.Append(cellValue17);

            var cell118 = new Cell { CellReference = "O7", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue18 = new CellValue();
            cellValue18.Text = sharedStringTablePart.SharedString("$/SF Bldg");

            cell118.Append(cellValue18);
            var cell119 = new Cell { CellReference = "P7", StyleIndex = 9U };
            var cell120 = new Cell { CellReference = "Q7", StyleIndex = 8U };

            row7.Append(cell104);
            row7.Append(cell105);
            row7.Append(cell106);
            row7.Append(cell107);
            row7.Append(cell108);
            row7.Append(cell109);
            row7.Append(cell110);
            row7.Append(cell111);
            row7.Append(cell112);
            row7.Append(cell113);
            row7.Append(cell114);
            row7.Append(cell115);
            row7.Append(cell116);
            row7.Append(cell117);
            row7.Append(cell118);
            row7.Append(cell119);
            row7.Append(cell120);

            var row8 = new Row { RowIndex = 8U, Spans = new ListValue<StringValue> { InnerText = "1:76" }, DyDescent = 0.2D };
            var cell121 = new Cell { CellReference = "A8", StyleIndex = 2U };
            var cell122 = new Cell { CellReference = "B8", StyleIndex = 2U };

            var cell123 = new Cell { CellReference = "C8", StyleIndex = 7U, DataType = CellValues.SharedString };
            var cellValue19 = new CellValue { Text = sharedStringTablePart.SharedString("Folio") };

            cell123.Append(cellValue19);
            var cell124 = new Cell { CellReference = "D8", StyleIndex = 9U };

            var cell125 = new Cell { CellReference = "E8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue20 = new CellValue { Text = sharedStringTablePart.SharedString("Acres") };

            cell125.Append(cellValue20);

            var cell126 = new Cell { CellReference = "F8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue21 = new CellValue { Text = sharedStringTablePart.SharedString("Zoning") };

            cell126.Append(cellValue21);
            var cell127 = new Cell { CellReference = "G8", StyleIndex = 9U };

            var cell128 = new Cell { CellReference = "H8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue22 = new CellValue { Text = sharedStringTablePart.SharedString("Built") };

            cell128.Append(cellValue22);

            var cell129 = new Cell { CellReference = "I8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue23 = new CellValue { Text = sharedStringTablePart.SharedString("Stories") };

            cell129.Append(cellValue23);
            var cell130 = new Cell { CellReference = "J8", StyleIndex = 9U };

            var cell131 = new Cell { CellReference = "K8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue24 = new CellValue { Text = sharedStringTablePart.SharedString("NOI/SF") };

            cell131.Append(cellValue24);
            var cell132 = new Cell { CellReference = "L8", StyleIndex = 9U };

            var cell133 = new Cell { CellReference = "M8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue25 = new CellValue { Text = sharedStringTablePart.SharedString("Date") };

            cell133.Append(cellValue25);

            var cell134 = new Cell { CellReference = "N8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue26 = new CellValue { Text = sharedStringTablePart.SharedString("Grantee") };

            cell134.Append(cellValue26);

            var cell135 = new Cell { CellReference = "O8", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue27 = new CellValue { Text = "8" };

            cell135.Append(cellValue27);
            var cell136 = new Cell { CellReference = "P8", StyleIndex = 9U };
            var cell137 = new Cell { CellReference = "Q8", StyleIndex = 8U };

            row8.Append(cell121);
            row8.Append(cell122);
            row8.Append(cell123);
            row8.Append(cell124);
            row8.Append(cell125);
            row8.Append(cell126);
            row8.Append(cell127);
            row8.Append(cell128);
            row8.Append(cell129);
            row8.Append(cell130);
            row8.Append(cell131);
            row8.Append(cell132);
            row8.Append(cell133);
            row8.Append(cell134);
            row8.Append(cell135);
            row8.Append(cell136);
            row8.Append(cell137);

            var row9 = new Row
            {
                RowIndex = 9U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                StyleIndex = 2U,
                CustomFormat = true,
                DyDescent = 0.2D
            };

            var cell138 = new Cell { CellReference = "B9", StyleIndex = 10U, DataType = CellValues.SharedString };
            var cellValue28 = new CellValue { Text = sharedStringTablePart.SharedString(" ") };

            cell138.Append(cellValue28);

            var cell139 = new Cell { CellReference = "C9", StyleIndex = 7U, DataType = CellValues.SharedString };
            var cellValue29 = new CellValue { Text = sharedStringTablePart.SharedString("Verification") };

            cell139.Append(cellValue29);
            var cell140 = new Cell { CellReference = "D9", StyleIndex = 9U };

            var cell141 = new Cell { CellReference = "F9", StyleIndex = 7U, DataType = CellValues.SharedString };
            var cellValue30 = new CellValue { Text = sharedStringTablePart.SharedString("Parking") };

            cell141.Append(cellValue30);
            var cell142 = new Cell { CellReference = "G9", StyleIndex = 9U };

            var cell143 = new Cell { CellReference = "H9", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue31 = new CellValue { Text = sharedStringTablePart.SharedString("Condition") };

            cell143.Append(cellValue31);

            var cell144 = new Cell { CellReference = "I9", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue32 = new CellValue { Text = sharedStringTablePart.SharedString("Use") };

            cell144.Append(cellValue32);
            var cell145 = new Cell { CellReference = "J9", StyleIndex = 9U };

            var cell146 = new Cell { CellReference = "K9", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue33 = new CellValue { Text = sharedStringTablePart.SharedString("OAR") };

            cell146.Append(cellValue33);
            var cell147 = new Cell { CellReference = "L9", StyleIndex = 9U };

            var cell148 = new Cell { CellReference = "M9", StyleIndex = 9U, DataType = CellValues.SharedString };
            var cellValue34 = new CellValue { Text = sharedStringTablePart.SharedString("OR B-P") };

            cell148.Append(cellValue34);
            var cell149 = new Cell { CellReference = "P9", StyleIndex = 9U };
            var cell150 = new Cell { CellReference = "Q9", StyleIndex = 8U };

            row9.Append(cell138);
            row9.Append(cell139);
            row9.Append(cell140);
            row9.Append(cell141);
            row9.Append(cell142);
            row9.Append(cell143);
            row9.Append(cell144);
            row9.Append(cell145);
            row9.Append(cell146);
            row9.Append(cell147);
            row9.Append(cell148);
            row9.Append(cell149);
            row9.Append(cell150);

            var row10 = new Row
            {
                RowIndex = 10U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                StyleIndex = 3U,
                CustomFormat = true,
                Height = 4.5D,
                CustomHeight = true,
                DyDescent = 0.2D
            };
            var cell151 = new Cell { CellReference = "A10", StyleIndex = 2U };
            var cell152 = new Cell { CellReference = "D10", StyleIndex = 6U };
            var cell153 = new Cell { CellReference = "E10", StyleIndex = 4U };
            var cell154 = new Cell { CellReference = "F10", StyleIndex = 4U };
            var cell155 = new Cell { CellReference = "G10", StyleIndex = 6U };
            var cell156 = new Cell { CellReference = "H10", StyleIndex = 4U };
            var cell157 = new Cell { CellReference = "I10", StyleIndex = 6U };
            var cell158 = new Cell { CellReference = "J10", StyleIndex = 6U };
            var cell159 = new Cell { CellReference = "K10", StyleIndex = 6U };
            var cell160 = new Cell { CellReference = "L10", StyleIndex = 6U };
            var cell161 = new Cell { CellReference = "M10", StyleIndex = 6U };
            var cell162 = new Cell { CellReference = "N10", StyleIndex = 6U };
            var cell163 = new Cell { CellReference = "O10", StyleIndex = 6U };
            var cell164 = new Cell { CellReference = "P10", StyleIndex = 6U };
            var cell165 = new Cell { CellReference = "Q10", StyleIndex = 4U };
            var cell166 = new Cell { CellReference = "R10", StyleIndex = 2U };
            var cell167 = new Cell { CellReference = "S10", StyleIndex = 2U };
            var cell168 = new Cell { CellReference = "T10", StyleIndex = 2U };
            var cell169 = new Cell { CellReference = "U10", StyleIndex = 2U };
            var cell170 = new Cell { CellReference = "V10", StyleIndex = 2U };
            var cell171 = new Cell { CellReference = "W10", StyleIndex = 2U };
            var cell172 = new Cell { CellReference = "X10", StyleIndex = 2U };
            var cell173 = new Cell { CellReference = "Y10", StyleIndex = 2U };
            var cell174 = new Cell { CellReference = "Z10", StyleIndex = 2U };
            var cell175 = new Cell { CellReference = "AA10", StyleIndex = 2U };
            var cell176 = new Cell { CellReference = "AB10", StyleIndex = 2U };
            var cell177 = new Cell { CellReference = "AC10", StyleIndex = 2U };
            var cell178 = new Cell { CellReference = "AD10", StyleIndex = 2U };
            var cell179 = new Cell { CellReference = "AE10", StyleIndex = 2U };
            var cell180 = new Cell { CellReference = "AF10", StyleIndex = 2U };
            var cell181 = new Cell { CellReference = "AG10", StyleIndex = 2U };
            var cell182 = new Cell { CellReference = "AH10", StyleIndex = 2U };
            var cell183 = new Cell { CellReference = "AI10", StyleIndex = 2U };
            var cell184 = new Cell { CellReference = "AJ10", StyleIndex = 2U };
            var cell185 = new Cell { CellReference = "AK10", StyleIndex = 2U };
            var cell186 = new Cell { CellReference = "AL10", StyleIndex = 2U };
            var cell187 = new Cell { CellReference = "AM10", StyleIndex = 2U };
            var cell188 = new Cell { CellReference = "AN10", StyleIndex = 2U };
            var cell189 = new Cell { CellReference = "AO10", StyleIndex = 2U };
            var cell190 = new Cell { CellReference = "AP10", StyleIndex = 2U };
            var cell191 = new Cell { CellReference = "AQ10", StyleIndex = 2U };
            var cell192 = new Cell { CellReference = "AR10", StyleIndex = 2U };
            var cell193 = new Cell { CellReference = "AS10", StyleIndex = 2U };
            var cell194 = new Cell { CellReference = "AT10", StyleIndex = 2U };
            var cell195 = new Cell { CellReference = "AU10", StyleIndex = 2U };
            var cell196 = new Cell { CellReference = "AV10", StyleIndex = 2U };
            var cell197 = new Cell { CellReference = "AW10", StyleIndex = 2U };
            var cell198 = new Cell { CellReference = "AX10", StyleIndex = 2U };
            var cell199 = new Cell { CellReference = "AY10", StyleIndex = 2U };
            var cell200 = new Cell { CellReference = "AZ10", StyleIndex = 2U };
            var cell201 = new Cell { CellReference = "BA10", StyleIndex = 2U };
            var cell202 = new Cell { CellReference = "BB10", StyleIndex = 2U };
            var cell203 = new Cell { CellReference = "BC10", StyleIndex = 2U };
            var cell204 = new Cell { CellReference = "BD10", StyleIndex = 2U };
            var cell205 = new Cell { CellReference = "BE10", StyleIndex = 2U };
            var cell206 = new Cell { CellReference = "BF10", StyleIndex = 2U };
            var cell207 = new Cell { CellReference = "BG10", StyleIndex = 2U };
            var cell208 = new Cell { CellReference = "BH10", StyleIndex = 2U };
            var cell209 = new Cell { CellReference = "BI10", StyleIndex = 2U };
            var cell210 = new Cell { CellReference = "BJ10", StyleIndex = 2U };
            var cell211 = new Cell { CellReference = "BK10", StyleIndex = 2U };
            var cell212 = new Cell { CellReference = "BL10", StyleIndex = 2U };
            var cell213 = new Cell { CellReference = "BM10", StyleIndex = 2U };
            var cell214 = new Cell { CellReference = "BN10", StyleIndex = 2U };
            var cell215 = new Cell { CellReference = "BO10", StyleIndex = 2U };
            var cell216 = new Cell { CellReference = "BP10", StyleIndex = 2U };
            var cell217 = new Cell { CellReference = "BQ10", StyleIndex = 2U };
            var cell218 = new Cell { CellReference = "BR10", StyleIndex = 2U };
            var cell219 = new Cell { CellReference = "BS10", StyleIndex = 2U };
            var cell220 = new Cell { CellReference = "BT10", StyleIndex = 2U };
            var cell221 = new Cell { CellReference = "BU10", StyleIndex = 2U };
            var cell222 = new Cell { CellReference = "BV10", StyleIndex = 2U };
            var cell223 = new Cell { CellReference = "BW10", StyleIndex = 2U };
            var cell224 = new Cell { CellReference = "BX10", StyleIndex = 2U };

            row10.Append(cell151);
            row10.Append(cell152);
            row10.Append(cell153);
            row10.Append(cell154);
            row10.Append(cell155);
            row10.Append(cell156);
            row10.Append(cell157);
            row10.Append(cell158);
            row10.Append(cell159);
            row10.Append(cell160);
            row10.Append(cell161);
            row10.Append(cell162);
            row10.Append(cell163);
            row10.Append(cell164);
            row10.Append(cell165);
            row10.Append(cell166);
            row10.Append(cell167);
            row10.Append(cell168);
            row10.Append(cell169);
            row10.Append(cell170);
            row10.Append(cell171);
            row10.Append(cell172);
            row10.Append(cell173);
            row10.Append(cell174);
            row10.Append(cell175);
            row10.Append(cell176);
            row10.Append(cell177);
            row10.Append(cell178);
            row10.Append(cell179);
            row10.Append(cell180);
            row10.Append(cell181);
            row10.Append(cell182);
            row10.Append(cell183);
            row10.Append(cell184);
            row10.Append(cell185);
            row10.Append(cell186);
            row10.Append(cell187);
            row10.Append(cell188);
            row10.Append(cell189);
            row10.Append(cell190);
            row10.Append(cell191);
            row10.Append(cell192);
            row10.Append(cell193);
            row10.Append(cell194);
            row10.Append(cell195);
            row10.Append(cell196);
            row10.Append(cell197);
            row10.Append(cell198);
            row10.Append(cell199);
            row10.Append(cell200);
            row10.Append(cell201);
            row10.Append(cell202);
            row10.Append(cell203);
            row10.Append(cell204);
            row10.Append(cell205);
            row10.Append(cell206);
            row10.Append(cell207);
            row10.Append(cell208);
            row10.Append(cell209);
            row10.Append(cell210);
            row10.Append(cell211);
            row10.Append(cell212);
            row10.Append(cell213);
            row10.Append(cell214);
            row10.Append(cell215);
            row10.Append(cell216);
            row10.Append(cell217);
            row10.Append(cell218);
            row10.Append(cell219);
            row10.Append(cell220);
            row10.Append(cell221);
            row10.Append(cell222);
            row10.Append(cell223);
            row10.Append(cell224);

            var row11 = new Row { RowIndex = 11U, Spans = new ListValue<StringValue> { InnerText = "1:76" }, DyDescent = 0.2D };
            var cell225 = new Cell { CellReference = "A11", StyleIndex = 2U };
            var cell226 = new Cell { CellReference = "B11", StyleIndex = 10U };
            var cell227 = new Cell { CellReference = "C11", StyleIndex = 2U };
            var cell228 = new Cell { CellReference = "D11", StyleIndex = 8U };
            var cell229 = new Cell { CellReference = "E11", StyleIndex = 8U };
            var cell230 = new Cell { CellReference = "F11", StyleIndex = 8U };
            var cell231 = new Cell { CellReference = "G11", StyleIndex = 8U };
            var cell232 = new Cell { CellReference = "H11", StyleIndex = 8U };
            var cell233 = new Cell { CellReference = "I11", StyleIndex = 8U };
            var cell234 = new Cell { CellReference = "J11", StyleIndex = 8U };
            var cell235 = new Cell { CellReference = "K11", StyleIndex = 8U };
            var cell236 = new Cell { CellReference = "L11", StyleIndex = 8U };
            var cell237 = new Cell { CellReference = "M11", StyleIndex = 8U };
            var cell238 = new Cell { CellReference = "N11", StyleIndex = 8U };
            var cell239 = new Cell { CellReference = "O11", StyleIndex = 8U };
            var cell240 = new Cell { CellReference = "P11", StyleIndex = 8U };
            var cell241 = new Cell { CellReference = "Q11", StyleIndex = 8U };

            row11.Append(cell225);
            row11.Append(cell226);
            row11.Append(cell227);
            row11.Append(cell228);
            row11.Append(cell229);
            row11.Append(cell230);
            row11.Append(cell231);
            row11.Append(cell232);
            row11.Append(cell233);
            row11.Append(cell234);
            row11.Append(cell235);
            row11.Append(cell236);
            row11.Append(cell237);
            row11.Append(cell238);
            row11.Append(cell239);
            row11.Append(cell240);
            row11.Append(cell241);

            sheetData1.Append(row1);
            sheetData1.Append(row2);
            sheetData1.Append(row3);
            sheetData1.Append(row4);
            sheetData1.Append(row5);
            sheetData1.Append(row6);
            sheetData1.Append(row7);
            sheetData1.Append(row8);
            sheetData1.Append(row9);
            sheetData1.Append(row10);
            sheetData1.Append(row11);
        }

        private MergeCells BuildProperties(SheetData sheetData1, SharedStringTablePart sstb, IEnumerable<OfficeViewModel> viewModels)
        {
            #region rows

            int rowIndex = sheetData1.Count() + 1;
            var mergeCells1 = new MergeCells();

            foreach (OfficeViewModel viewModel in viewModels)
            {
                var mergeCell1 = new MergeCell { Reference = String.Format("Q{0}:Q{1}", rowIndex, rowIndex + 4) };
                mergeCells1.Append(mergeCell1);

                // row 1
                var row1 = new Row
                {
                    RowIndex = Convert.ToUInt32(rowIndex),
                    Spans = new ListValue<StringValue> { InnerText = "1:76" },
                    Height = 12.75D,
                    CustomHeight = true,
                    DyDescent = 0.2D
                };

                // Location (Property Name)
                var cell3 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 11U, DataType = CellValues.SharedString };
                cell3.Append(new CellValue { Text = sstb.SharedString(viewModel.Name) });

                // Site SF
                var cell5 = new Cell { CellReference = "E" + rowIndex, StyleIndex = 12U };
                cell5.Append(new CellValue { Text = viewModel.SiteTotalSquareFoot.ToString() });

                // FAR
                var cell6 = new Cell { CellReference = "F" + rowIndex, StyleIndex = 13U };
                cell6.Append(new CellFormula { Text = viewModel.FloorToAreaRatio.ToString() });

                // Building SF
                var cell8 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 12U };
                cell8.Append(new CellValue { Text = viewModel.BuildingTotalSquareFoot.ToString() });

                // Class / Qual
                var cell9 = new Cell { CellReference = "I" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                string classquality = SummaryReportHelpers.FormatBuildingClassQuality(viewModel.Class, viewModel.Quality);
                cell9.Append(new CellValue { Text = sstb.SharedString(classquality) });

                // Occupancy
                var cell11 = new Cell { CellReference = "K" + rowIndex, StyleIndex = 14U };
                cell11.Append(new CellValue { Text = viewModel.Occupancy().ToString() });

                // Price
                var cell13 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 15U };
                cell13.Append(new CellValue { Text = viewModel.Price.ToString() });

                // Grantor
                var cell14 = new Cell { CellReference = "N" + rowIndex, StyleIndex = 16U, DataType = CellValues.SharedString };
                cell14.Append(new CellValue { Text = sstb.SharedString(viewModel.Grantor) });

                // $/SF Bldg
                var cell15 = new Cell { CellReference = "O" + rowIndex, StyleIndex = 17U };
                cell15.Append(new CellValue { Text = viewModel.CostPerBuildingSquareFoot.ToString() });

                // Comments
                var cell17 = new Cell { CellReference = "Q" + rowIndex, StyleIndex = 80U, DataType = CellValues.SharedString };
                cell17.Append(new CellValue { Text = sstb.SharedString(viewModel.Comments()) });

                row1.Append(cell3, cell5, cell6, cell8, cell9, cell11, cell13, cell14, cell15, cell17);


                // row 2
                ++rowIndex;
                var row2 = new Row
                {
                    RowIndex = Convert.ToUInt32(rowIndex),
                    Spans = new ListValue<StringValue> { InnerText = "1:76" },
                    DyDescent = 0.2D
                };

                // Location (Street Address Line 1 & 2)
                var cell20 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 19U, DataType = CellValues.SharedString };
                cell20.Append(new CellValue
                                  {
                                      Text =
                                          sstb.SharedString(viewModel.Address.AddressLine1 +
                                                            (string.IsNullOrWhiteSpace(viewModel.Address.AddressLine2)
                                                                 ? string.Empty
                                                                 : ", " + viewModel.Address.AddressLine2))
                                  });

                // Acres
                var cell23 = new Cell { CellReference = "E" + rowIndex, StyleIndex = 13U };
                cell23.Append(new CellValue { Text = viewModel.Acres.ToString() });

                // Zoning
                var cell24 = new Cell { CellReference = "F" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                cell24.Append(new CellValue { Text = sstb.SharedString(viewModel.Zoning) });

                // Built
                var cell26 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                cell26.Append(new CellValue { Text = sstb.SharedString(viewModel.BuiltTimePeriod) });

                // Stories
                var cell27 = new Cell { CellReference = "I" + rowIndex, StyleIndex = 8U };
                cell27.Append(new CellValue { Text = viewModel.Stories.ToString() });

                // NOI/SF
                string noiSf = SummaryReportHelpers.FormatNoiSf(viewModel.NetOperatingIncome);
                var cell29 = new Cell { CellReference = "K" + rowIndex, StyleIndex = 20U };
                cell29.Append(new CellValue { Text = noiSf });

                // Date or Listing
                Cell cell31;
                if (viewModel.SaleStatus() == PropertyViewModelHelper.PropertySaleStatus.Listed)
                {
                    cell31 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 20U, DataType = CellValues.SharedString };
                    cell31.Append(new CellValue { Text = sstb.SharedString("Listing") });
                }
                else
                {
                    cell31 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 21U };
                    if (viewModel.SaleDate != null)
                        cell31.Append(new CellValue { Text = viewModel.SaleDate.Value.ToOADate().ToString() });
                }

                // Grantee
                var cell32 = new Cell { CellReference = "N" + rowIndex, StyleIndex = 16U, DataType = CellValues.SharedString };
                cell32.Append(new CellValue { Text = sstb.SharedString(viewModel.Grantee) });

                row2.Append(cell20, cell23, cell24, cell26, cell27, cell29, cell31, cell32);


                // row 3
                ++rowIndex;
                var row3 = new Row
                {
                    RowIndex = Convert.ToUInt32(rowIndex),
                    Spans = new ListValue<StringValue> { InnerText = "1:76" },
                    DyDescent = 0.2D
                };

                // City
                var cell38 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 19U, DataType = CellValues.SharedString };
                cell38.Append(new CellValue { Text = sstb.SharedString(viewModel.Address.City) });

                // Parking
                var cell41 = new Cell { CellReference = "F" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                cell41.Append(new CellValue { Text = sstb.SharedString(viewModel.Parking) });

                // Condition
                var cell43 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                cell43.Append(new CellValue { Text = sstb.SharedString(viewModel.Condition) });

                // Use
                var cell44 = new Cell { CellReference = "I" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                cell44.Append(new CellValue { Text = sstb.SharedString(viewModel.Use) });

                // OAR
                var cell46 = new Cell { CellReference = "K" + rowIndex, StyleIndex = 23U };
                cell46.Append(new CellValue {Text = viewModel.OverallRate().ToString()});

                // OR B-P
                var cell48 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                cell48.Append(new CellValue { Text = sstb.SharedString(viewModel.OfficialRecordBookAndPage) });
                row3.Append(cell38, cell41, cell43, cell44, cell46, cell48);

                
                // row 4
                ++rowIndex;
                var row4 = new Row
                {
                    RowIndex = Convert.ToUInt32(rowIndex),
                    Spans = new ListValue<StringValue> { InnerText = "1:76" },
                    DyDescent = 0.2D
                };

                // parcel id
                var cell55 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 8U, DataType = CellValues.SharedString };
                cell55.Append(new CellValue { Text = sstb.SharedString(viewModel.ParcelId) });

                row4.Append(cell55);


                // row 5
                ++rowIndex;
                var row5 = new Row
                {
                    RowIndex = Convert.ToUInt32(rowIndex),
                    Spans = new ListValue<StringValue> { InnerText = "1:76" },
                    DyDescent = 0.2D
                };

                // verification
                var cell72 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 24U, DataType = CellValues.SharedString };
                cell72.Append(new CellValue { Text = sstb.SharedString(viewModel.Verification) });

                row5.Append(cell72);


                // row 6
                ++rowIndex;
                var row6 = new Row
                {
                    RowIndex = Convert.ToUInt32(rowIndex),
                    Spans = new ListValue<StringValue> { InnerText = "1:17" },
                    DyDescent = 0.2D
                };

                sheetData1.Append(row1, row2, row3, row4, row5, row6);

                ++rowIndex;
            }

            #endregion

            return mergeCells1;
        }

        private MergeCells BuildSubHeaderPriorYear(SheetData sheetData1, SharedStringTablePart sharedStringTablePart)
        {
            int rowIndex = sheetData1.Count() + 2;

            var row38 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "1:17" },
                Height = 15D,
                DyDescent = 0.2D
            };

            var mergeCells1 = new MergeCells();
            var mergeCell1 = new MergeCell { Reference = String.Format("B{0}:C{0}", rowIndex) };
            mergeCells1.Append(mergeCell1);

            var cell670 = new Cell { CellReference = "A" + rowIndex, StyleIndex = 2U };

            var cell671 = new Cell { CellReference = "B" + rowIndex, StyleIndex = 81U, DataType = CellValues.SharedString };
            var cellValue143 = new CellValue { Text = sharedStringTablePart.SharedString(GCalendar.GetYear(DateTime.Today.AddYears(-1)).ToString(CultureInfo.InvariantCulture) + " & Older") };
            cell671.Append(cellValue143);

            var cell672 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 81U };
            var cell673 = new Cell { CellReference = "D" + rowIndex, StyleIndex = 48U };
            var cell674 = new Cell { CellReference = "E" + rowIndex, StyleIndex = 48U };
            var cell675 = new Cell { CellReference = "F" + rowIndex, StyleIndex = 48U };
            var cell676 = new Cell { CellReference = "G" + rowIndex, StyleIndex = 48U };
            var cell677 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 48U };
            var cell678 = new Cell { CellReference = "I" + rowIndex, StyleIndex = 48U };
            var cell679 = new Cell { CellReference = "J" + rowIndex, StyleIndex = 48U };
            var cell680 = new Cell { CellReference = "K" + rowIndex, StyleIndex = 48U };
            var cell681 = new Cell { CellReference = "L" + rowIndex, StyleIndex = 49U };
            var cell682 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 49U };
            var cell683 = new Cell { CellReference = "N" + rowIndex, StyleIndex = 50U };
            var cell684 = new Cell { CellReference = "O" + rowIndex, StyleIndex = 51U };
            var cell685 = new Cell { CellReference = "P" + rowIndex, StyleIndex = 49U };
            var cell686 = new Cell { CellReference = "Q" + rowIndex, StyleIndex = 52U };

            row38.Append(cell670);
            row38.Append(cell671);
            row38.Append(cell672);
            row38.Append(cell673);
            row38.Append(cell674);
            row38.Append(cell675);
            row38.Append(cell676);
            row38.Append(cell677);
            row38.Append(cell678);
            row38.Append(cell679);
            row38.Append(cell680);
            row38.Append(cell681);
            row38.Append(cell682);
            row38.Append(cell683);
            row38.Append(cell684);
            row38.Append(cell685);
            row38.Append(cell686);

            sheetData1.Append(row38);

            // row 2
            ++rowIndex;
            var row39 = new Row { RowIndex = Convert.ToUInt32(rowIndex), Spans = new ListValue<StringValue> { InnerText = "1:17" }, DyDescent = 0.2D };
            var cell687 = new Cell { CellReference = "A" + rowIndex, StyleIndex = 2U };
            var cell688 = new Cell { CellReference = "B" + rowIndex, StyleIndex = 10U };
            var cell689 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 39U };
            var cell690 = new Cell { CellReference = "D" + rowIndex, StyleIndex = 39U };
            var cell691 = new Cell { CellReference = "E" + rowIndex, StyleIndex = 39U };
            var cell692 = new Cell { CellReference = "F" + rowIndex, StyleIndex = 39U };
            var cell693 = new Cell { CellReference = "G" + rowIndex, StyleIndex = 39U };
            var cell694 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 39U };
            var cell695 = new Cell { CellReference = "I" + rowIndex, StyleIndex = 39U };
            var cell696 = new Cell { CellReference = "J" + rowIndex, StyleIndex = 39U };
            var cell697 = new Cell { CellReference = "K" + rowIndex, StyleIndex = 39U };
            var cell698 = new Cell { CellReference = "L" + rowIndex, StyleIndex = 2U };
            var cell699 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 2U };
            var cell700 = new Cell { CellReference = "N" + rowIndex, StyleIndex = 40U };
            var cell701 = new Cell { CellReference = "O" + rowIndex, StyleIndex = 8U };
            var cell702 = new Cell { CellReference = "P" + rowIndex, StyleIndex = 2U };
            var cell703 = new Cell { CellReference = "Q" + rowIndex, StyleIndex = 18U };

            row39.Append(cell687);
            row39.Append(cell688);
            row39.Append(cell689);
            row39.Append(cell690);
            row39.Append(cell691);
            row39.Append(cell692);
            row39.Append(cell693);
            row39.Append(cell694);
            row39.Append(cell695);
            row39.Append(cell696);
            row39.Append(cell697);
            row39.Append(cell698);
            row39.Append(cell699);
            row39.Append(cell700);
            row39.Append(cell701);
            row39.Append(cell702);
            row39.Append(cell703);

            sheetData1.Append(row39);

            // row 3
            ++rowIndex;
            var row40 = new Row { RowIndex = Convert.ToUInt32(rowIndex), Spans = new ListValue<StringValue> { InnerText = "1:17" }, DyDescent = 0.2D };
            var cellsList = new List<Cell>
            {
                new Cell { CellReference = "A" + rowIndex, StyleIndex = 2U },
                new Cell { CellReference = "B" + rowIndex, StyleIndex = 10U },
                new Cell { CellReference = "C" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "D" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "E" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "F" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "G" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "H" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "I" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "J" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "K" + rowIndex, StyleIndex = 39U },
                new Cell { CellReference = "L" + rowIndex, StyleIndex = 2U },
                new Cell { CellReference = "M" + rowIndex, StyleIndex = 2U },
                new Cell { CellReference = "N" + rowIndex, StyleIndex = 40U },
                new Cell { CellReference = "O" + rowIndex, StyleIndex = 8U },
                new Cell { CellReference = "P" + rowIndex, StyleIndex = 2U },
                new Cell { CellReference = "Q" + rowIndex, StyleIndex = 18U }
            };

            row40.Append(cellsList);

            sheetData1.Append(row40);

            return mergeCells1;
        }

        private void BuildSubHeaderForYear(SheetData sheetData1, SharedStringTablePart sharedStringTablePart, int year)
        {
            int rowIndex = sheetData1.Count() + 2;

            var row58 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "1:17" },
                StyleIndex = 2U,
                CustomFormat = true,
                DyDescent = 0.2D
            };

            var cell1003 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 9U };
            cell1003.Append(new CellValue { Text = year.ToString() });            

            row58.Append(cell1003);


            // row 2
            ++rowIndex;
            var row59 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "1:17" },
                StyleIndex = 2U,
                CustomFormat = true,
                DyDescent = 0.2D
            };

            var cells = new List<Cell>
            {
                new Cell {CellReference = "B" + rowIndex, StyleIndex = 68U},
                new Cell {CellReference = "C" + rowIndex, StyleIndex = 3U},
                new Cell {CellReference = "D" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "E" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "F" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "G" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "H" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "I" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "J" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "K" + rowIndex, StyleIndex = 69U},
                new Cell {CellReference = "L" + rowIndex, StyleIndex = 70U},
                new Cell {CellReference = "M" + rowIndex, StyleIndex = 70U},
                new Cell {CellReference = "N" + rowIndex, StyleIndex = 71U},
                new Cell {CellReference = "O" + rowIndex, StyleIndex = 72U},
                new Cell {CellReference = "P" + rowIndex, StyleIndex = 4U},
                new Cell {CellReference = "Q" + rowIndex, StyleIndex = 73U}
            };

            row59.Append(cells);


            // row 3
            ++rowIndex;
            var row60 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "1:17" },
                StyleIndex = 2U,
                CustomFormat = true,
                DyDescent = 0.2D
            };

            // row 3
            ++rowIndex;
            var row40 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "1:17" },
                DyDescent = 0.2D
            };

            sheetData1.Append(row58, row59, row60, row40);
        }

        private void BuildSubHeaderListings(SheetData sheetData1, SharedStringTablePart sharedStringTablePart)
        {
            int rowIndex = sheetData1.Count();

            var row79 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                StyleIndex = 2U,
                CustomFormat = true,
                DyDescent = 0.2D
            };

            var cell1328 = new Cell { CellReference = "B" + rowIndex, StyleIndex = 10U };
            var cell1329 = new Cell { CellReference = "D" + rowIndex, StyleIndex = 8U };
            var cell1330 = new Cell { CellReference = "E" + rowIndex, StyleIndex = 8U };
            var cell1331 = new Cell { CellReference = "F" + rowIndex, StyleIndex = 8U };
            var cell1332 = new Cell { CellReference = "G" + rowIndex, StyleIndex = 8U };
            var cell1333 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 8U };
            var cell1334 = new Cell { CellReference = "I" + rowIndex, StyleIndex = 8U };
            var cell1335 = new Cell { CellReference = "J" + rowIndex, StyleIndex = 8U };
            var cell1336 = new Cell { CellReference = "K" + rowIndex, StyleIndex = 8U };
            var cell1337 = new Cell { CellReference = "L" + rowIndex, StyleIndex = 8U };
            var cell1338 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 8U };
            var cell1339 = new Cell { CellReference = "N" + rowIndex, StyleIndex = 16U };
            var cell1340 = new Cell { CellReference = "O" + rowIndex, StyleIndex = 22U };
            var cell1341 = new Cell { CellReference = "P" + rowIndex, StyleIndex = 8U };
            var cell1342 = new Cell { CellReference = "Q" + rowIndex, StyleIndex = 63U };

            row79.Append(cell1328);
            row79.Append(cell1329);
            row79.Append(cell1330);
            row79.Append(cell1331);
            row79.Append(cell1332);
            row79.Append(cell1333);
            row79.Append(cell1334);
            row79.Append(cell1335);
            row79.Append(cell1336);
            row79.Append(cell1337);
            row79.Append(cell1338);
            row79.Append(cell1339);
            row79.Append(cell1340);
            row79.Append(cell1341);
            row79.Append(cell1342);


            // row 2
            ++rowIndex;
            var row80 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                Height = 15.75D,
                DyDescent = 0.25D
            };

            var cell1343 = new Cell { CellReference = "B" + rowIndex, StyleIndex = 76U, DataType = CellValues.SharedString };
            var cellValue307 = new CellValue { Text = "166" };

            cell1343.Append(cellValue307);
            var cell1344 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 3U };
            var cell1345 = new Cell { CellReference = "D" + rowIndex, StyleIndex = 3U };
            var cell1346 = new Cell { CellReference = "E" + rowIndex, StyleIndex = 3U };
            var cell1347 = new Cell { CellReference = "F" + rowIndex, StyleIndex = 3U };
            var cell1348 = new Cell { CellReference = "G" + rowIndex, StyleIndex = 3U };
            var cell1349 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 3U };
            var cell1350 = new Cell { CellReference = "I" + rowIndex, StyleIndex = 3U };
            var cell1351 = new Cell { CellReference = "J" + rowIndex, StyleIndex = 3U };
            var cell1352 = new Cell { CellReference = "K" + rowIndex, StyleIndex = 4U };
            var cell1353 = new Cell { CellReference = "L" + rowIndex, StyleIndex = 3U };
            var cell1354 = new Cell { CellReference = "M" + rowIndex, StyleIndex = 3U };
            var cell1355 = new Cell { CellReference = "N" + rowIndex, StyleIndex = 3U };
            var cell1356 = new Cell { CellReference = "O" + rowIndex, StyleIndex = 4U };
            var cell1357 = new Cell { CellReference = "P" + rowIndex, StyleIndex = 3U };
            var cell1358 = new Cell { CellReference = "Q" + rowIndex, StyleIndex = 3U };

            row80.Append(cell1343);
            row80.Append(cell1344);
            row80.Append(cell1345);
            row80.Append(cell1346);
            row80.Append(cell1347);
            row80.Append(cell1348);
            row80.Append(cell1349);
            row80.Append(cell1350);
            row80.Append(cell1351);
            row80.Append(cell1352);
            row80.Append(cell1353);
            row80.Append(cell1354);
            row80.Append(cell1355);
            row80.Append(cell1356);
            row80.Append(cell1357);
            row80.Append(cell1358);

            sheetData1.Append(row79);
            sheetData1.Append(row80);
        }

        private void BuildLegend(SheetData sheetData1, SharedStringTablePart sharedStringTablePart)
        {
            int rowIndex = sheetData1.Count() + 2;

            ++rowIndex;
            var row222 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            row222.Append(new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U });

            // row 1
            var row1 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            var cell1 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U, DataType = CellValues.SharedString };
            cell1.Append(new CellValue { Text = "195" });

            var cell2 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell2.Append(new CellValue { Text = "196" });

            var cell3 = new Cell { CellReference = "Q" + rowIndex, DataType = CellValues.SharedString };
            cell3.Append(new CellValue { Text = "8" });

            row1.Append(cell1, cell2, cell3);

            // row 2
            ++rowIndex;
            var row2 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            var cell4 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U };

            var cell5 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell5.Append(new CellValue { Text = "197" });

            row2.Append(cell4, cell5);

            // row 3
            ++rowIndex;
            var row3 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            var cell6 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U };

            var cell7 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell7.Append(new CellValue { Text = "198" });

            row3.Append(cell6, cell7);

            // row 4
            ++rowIndex;
            var row4 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            row4.Append(new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U });

            // row 5
            ++rowIndex;
            var row5 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            row5.Append(new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U });

            // row 6
            ++rowIndex;
            var row6 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            row6.Append(new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U });

            // row 7
            ++rowIndex;
            var row7 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            row7.Append(new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U });

            // row 8
            ++rowIndex;
            var row8 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            var cell8 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U, DataType = CellValues.SharedString };
            cell8.Append(new CellValue { Text = "199" });

            var cell9 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell9.Append(new CellValue { Text = "200" });

            var cell10 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U, DataType = CellValues.SharedString };
            cell10.Append(new CellValue { Text = "201" });

            var cell11 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell11.Append(new CellValue { Text = "202" });

            var cell12 = new Cell { CellReference = "N" + rowIndex, DataType = CellValues.SharedString };
            cell12.Append(new CellValue { Text = "203" });

            row8.Append(cell8, cell9, cell10, cell11, cell12);

            // row 9
            ++rowIndex;
            var row9 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            var cell13 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U };

            var cell14 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell14.Append(new CellValue { Text = "79" });

            var cell15 = new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U };

            var cell16 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell16.Append(new CellValue { Text = "204" });

            var cell17 = new Cell { CellReference = "N" + rowIndex, DataType = CellValues.SharedString };
            cell17.Append(new CellValue { Text = "205" });

            row9.Append(cell13, cell14, cell15, cell16, cell17);

            // row 10
            ++rowIndex;
            var row10 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            var cell18 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U };

            var cell19 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell19.Append(new CellValue { Text = "128" });

            var cell20 = new Cell { CellReference = "F" + rowIndex, DataType = CellValues.SharedString };
            cell20.Append(new CellValue { Text = "206" });

            var cell21 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell21.Append(new CellValue { Text = "207" });

            var cell22 = new Cell { CellReference = "N" + rowIndex, DataType = CellValues.SharedString };
            cell22.Append(new CellValue { Text = "208" });

            row10.Append(cell18, cell19, cell20, cell21, cell22);

            // row 11
            ++rowIndex;
            var row11 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell23 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U };

            var cell24 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell24.Append(new CellValue { Text = "151" });

            var cell25 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell25.Append(new CellValue { Text = "79" });

            var cell26 = new Cell { CellReference = "N" + rowIndex, DataType = CellValues.SharedString };
            cell26.Append(new CellValue { Text = "130" });

            row11.Append(cell23, cell24, cell25, cell26);

            // row 12
            ++rowIndex;
            var row12 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell27 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U };

            var cell28 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell28.Append(new CellValue { Text = "209" });

            var cell29 = new Cell { CellReference = "I" + rowIndex, DataType = CellValues.SharedString };
            cell29.Append(new CellValue { Text = "128" });

            row12.Append(cell27, cell28, cell29);

            // row 13
            ++rowIndex;
            var row13 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell30 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U };

            row13.Append(cell30);

            // row 14
            ++rowIndex;
            var row14 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell31 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U };

            row14.Append(cell31);

            // row 15
            ++rowIndex;
            var row15 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell32 = new Cell { CellReference = "C" + rowIndex, StyleIndex = 79U, DataType = CellValues.SharedString };
            cell32.Append(new CellValue { Text = "210" });

            var cell33 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell33.Append(new CellValue { Text = "211" });

            row15.Append(cell32, cell33);

            // row 16
            ++rowIndex;
            var row16 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell34 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell34.Append(new CellValue { Text = "79" });

            row16.Append(cell34);

            // row 17
            ++rowIndex;
            var row17 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell35 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell35.Append(new CellValue { Text = "81" });

            row17.Append(cell35);

            // row 18
            ++rowIndex;
            var row18 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell36 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell36.Append(new CellValue { Text = "212" });

            row18.Append(cell36);

            // row 19
            ++rowIndex;
            var row19 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell37 = new Cell { CellReference = "E" + rowIndex, DataType = CellValues.SharedString };
            cell37.Append(new CellValue { Text = "213" });

            row19.Append(cell37);

            //buffer
            ++rowIndex;
            var row333 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            row333.Append(new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U });

            ++rowIndex;
            var row444 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "2:17" },
                DyDescent = 0.2D
            };

            row444.Append(new Cell { CellReference = "H" + rowIndex, StyleIndex = 79U });


            // row 20
            ++rowIndex;
            var row20 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell38 = new Cell { CellReference = "C" + rowIndex, DataType = CellValues.SharedString };
            cell38.Append(new CellValue { Text = "214" });

            row20.Append(cell38);

            // row 21
            ++rowIndex;
            var row21 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell39 = new Cell { CellReference = "C" + rowIndex, DataType = CellValues.SharedString };
            cell39.Append(new CellValue { Text = "215" });

            row21.Append(cell39);

            // row 22
            ++rowIndex;
            var row22 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell40 = new Cell { CellReference = "C" + rowIndex, DataType = CellValues.SharedString };
            cell40.Append(new CellValue { Text = "216" });

            row22.Append(cell40);

            // row 23
            ++rowIndex;
            var row23 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell41 = new Cell { CellReference = "C" + rowIndex, DataType = CellValues.SharedString };
            cell41.Append(new CellValue { Text = "217" });

            row23.Append(cell41);

            // row 24
            ++rowIndex;
            var row24 = new Row
            {
                RowIndex = Convert.ToUInt32(rowIndex),
                Spans = new ListValue<StringValue> { InnerText = "3:14" },
                DyDescent = 0.2D
            };

            var cell42 = new Cell { CellReference = "C" + rowIndex, DataType = CellValues.SharedString };
            cell42.Append(new CellValue { Text = "218" });

            row24.Append(cell42);


            sheetData1.Append(row222);
            sheetData1.Append(row1);
            sheetData1.Append(row2);
            sheetData1.Append(row3);
            sheetData1.Append(row4);
            sheetData1.Append(row5);
            sheetData1.Append(row6);
            sheetData1.Append(row7);
            sheetData1.Append(row8);
            sheetData1.Append(row9);
            sheetData1.Append(row10);
            sheetData1.Append(row11);
            sheetData1.Append(row12);
            sheetData1.Append(row13);
            sheetData1.Append(row14);
            sheetData1.Append(row15);
            sheetData1.Append(row16);
            sheetData1.Append(row17);
            sheetData1.Append(row18);
            sheetData1.Append(row19);
            sheetData1.Append(row333);
            sheetData1.Append(row444);
            sheetData1.Append(row20);
            sheetData1.Append(row21);
            sheetData1.Append(row22);
            sheetData1.Append(row23);
            sheetData1.Append(row24);
        }

        // Generates content of sharedStringTablePart1.
        private void GenerateSharedStringTable(SharedStringTablePart sharedStringTablePart1)
        {
            var sharedStringTable1 = new SharedStringTable { Count = 303U, UniqueCount = 219U };

            var sharedStringItem1 = new SharedStringItem();
            var text1 = new Text();
            text1.Text = "COMPARABLE OFFICE BUILDING SALES";

            sharedStringItem1.Append(text1);

            var sharedStringItem2 = new SharedStringItem();
            var text2 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text2.Text = "123 NE 4th Street, Ft Lauderdale (File:10-0100)";

            sharedStringItem2.Append(text2);

            var sharedStringItem3 = new SharedStringItem();
            var text3 = new Text();
            text3.Text = "PROPERTY";

            sharedStringItem3.Append(text3);

            var sharedStringItem4 = new SharedStringItem();
            var text4 = new Text();
            text4.Text = "SITE";

            sharedStringItem4.Append(text4);

            var sharedStringItem5 = new SharedStringItem();
            var text5 = new Text();
            text5.Text = "BUILDING";

            sharedStringItem5.Append(text5);

            var sharedStringItem6 = new SharedStringItem();
            var text6 = new Text();
            text6.Text = "ECONOMICS";

            sharedStringItem6.Append(text6);

            var sharedStringItem7 = new SharedStringItem();
            var text7 = new Text();
            text7.Text = "SALE";

            sharedStringItem7.Append(text7);

            var sharedStringItem8 = new SharedStringItem();
            var text8 = new Text();
            text8.Text = "COMMENTS";

            sharedStringItem8.Append(text8);

            var sharedStringItem9 = new SharedStringItem();
            var text9 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text9.Text = " ";

            sharedStringItem9.Append(text9);

            var sharedStringItem10 = new SharedStringItem();
            var text10 = new Text();
            text10.Text = "Location";

            sharedStringItem10.Append(text10);

            var sharedStringItem11 = new SharedStringItem();
            var text11 = new Text();
            text11.Text = "SF";

            sharedStringItem11.Append(text11);

            var sharedStringItem12 = new SharedStringItem();
            var text12 = new Text();
            text12.Text = "FAR";

            sharedStringItem12.Append(text12);

            var sharedStringItem13 = new SharedStringItem();
            var text13 = new Text();
            text13.Text = "Class / Qual";

            sharedStringItem13.Append(text13);

            var sharedStringItem14 = new SharedStringItem();
            var text14 = new Text();
            text14.Text = "Occupancy";

            sharedStringItem14.Append(text14);

            var sharedStringItem15 = new SharedStringItem();
            var text15 = new Text();
            text15.Text = "Price";

            sharedStringItem15.Append(text15);

            var sharedStringItem16 = new SharedStringItem();
            var text16 = new Text();
            text16.Text = "Grantor";

            sharedStringItem16.Append(text16);

            var sharedStringItem17 = new SharedStringItem();
            var text17 = new Text();
            text17.Text = "$/SF Bldg";

            sharedStringItem17.Append(text17);

            var sharedStringItem18 = new SharedStringItem();
            var text18 = new Text();
            text18.Text = "Folio";

            sharedStringItem18.Append(text18);

            var sharedStringItem19 = new SharedStringItem();
            var text19 = new Text();
            text19.Text = "Acres";

            sharedStringItem19.Append(text19);

            var sharedStringItem20 = new SharedStringItem();
            var text20 = new Text();
            text20.Text = "Zoning";

            sharedStringItem20.Append(text20);

            var sharedStringItem21 = new SharedStringItem();
            var text21 = new Text();
            text21.Text = "Built";

            sharedStringItem21.Append(text21);

            var sharedStringItem22 = new SharedStringItem();
            var text22 = new Text();
            text22.Text = "Stories";

            sharedStringItem22.Append(text22);

            var sharedStringItem23 = new SharedStringItem();
            var text23 = new Text();
            text23.Text = "NOI/SF";

            sharedStringItem23.Append(text23);

            var sharedStringItem24 = new SharedStringItem();
            var text24 = new Text();
            text24.Text = "Date";

            sharedStringItem24.Append(text24);

            var sharedStringItem25 = new SharedStringItem();
            var text25 = new Text();
            text25.Text = "Grantee";

            sharedStringItem25.Append(text25);

            var sharedStringItem26 = new SharedStringItem();
            var text26 = new Text();
            text26.Text = "Verification";

            sharedStringItem26.Append(text26);

            var sharedStringItem27 = new SharedStringItem();
            var text27 = new Text();
            text27.Text = "Parking";

            sharedStringItem27.Append(text27);

            var sharedStringItem28 = new SharedStringItem();
            var text28 = new Text();
            text28.Text = "Condition";

            sharedStringItem28.Append(text28);

            var sharedStringItem29 = new SharedStringItem();
            var text29 = new Text();
            text29.Text = "Use";

            sharedStringItem29.Append(text29);

            var sharedStringItem30 = new SharedStringItem();
            var text30 = new Text();
            text30.Text = "OAR";

            sharedStringItem30.Append(text30);

            var sharedStringItem31 = new SharedStringItem();
            var text31 = new Text();
            text31.Text = "OR B-P";

            sharedStringItem31.Append(text31);

            // TODO End header here

            var sharedStringItem32 = new SharedStringItem();
            var text32 = new Text();
            text32.Text = "Former Riverside Bank Bldg.";

            sharedStringItem32.Append(text32);

            var sharedStringItem33 = new SharedStringItem();
            var text33 = new Text();
            text33.Text = "B / Avg";

            sharedStringItem33.Append(text33);

            var sharedStringItem34 = new SharedStringItem();
            var text34 = new Text();
            text34.Text = "FDIC";

            sharedStringItem34.Append(text34);

            var sharedStringItem35 = new SharedStringItem();
            var text35 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text35.Text =
                "Listing: approximately 7 months. Escrow: N/A. Financing: none recorded. Prior sales: none in prior 3 years. FDIC took over this property that was previously occupied and owned by Riverside Bank. The building is a three-story Class B office building in average condition. The third floor of the building was being leased to a law firm and the remainder of the building was vacant at the time of this sale. ";

            sharedStringItem35.Append(text35);

            var sharedStringItem36 = new SharedStringItem();
            var text36 = new Text();
            text36.Text = "660 U.S. Highway 1";

            sharedStringItem36.Append(text36);

            var sharedStringItem37 = new SharedStringItem();
            var text37 = new Text();
            text37.Text = "CA";

            sharedStringItem37.Append(text37);

            var sharedStringItem38 = new SharedStringItem();
            var text38 = new Text();
            text38.Text = "N/A";

            sharedStringItem38.Append(text38);

            var sharedStringItem39 = new SharedStringItem();
            var text39 = new Text();
            text39.Text = "660 North Palm Beach, LLC";

            sharedStringItem39.Append(text39);

            var sharedStringItem40 = new SharedStringItem();
            var text40 = new Text();
            text40.Text = "North Palm Beach";

            sharedStringItem40.Append(text40);

            var sharedStringItem41 = new SharedStringItem();
            var text41 = new Text();
            text41.Text = "2.1/1,000";

            sharedStringItem41.Append(text41);

            var sharedStringItem42 = new SharedStringItem();
            var text42 = new Text();
            text42.Text = "Average";

            sharedStringItem42.Append(text42);

            var sharedStringItem43 = new SharedStringItem();
            var text43 = new Text();
            text43.Text = "Office";

            sharedStringItem43.Append(text43);

            var sharedStringItem44 = new SharedStringItem();
            var text44 = new Text();
            text44.Text = "24618-0251";

            sharedStringItem44.Append(text44);

            var sharedStringItem45 = new SharedStringItem();
            var text45 = new Text();
            text45.Text = "68-43-42-09-01-072-0040";

            sharedStringItem45.Append(text45);

            var sharedStringItem46 = new SharedStringItem();
            var text46 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text46.Text = "Atlee Mahorn, Listing Broker, 561-479-1225, JCM, 8/11 (11-0897)";

            sharedStringItem46.Append(text46);


            var sharedStringItem47 = new SharedStringItem();
            var text47 = new Text();
            text47.Text = "Bay Pointe Building";

            sharedStringItem47.Append(text47);

            var sharedStringItem48 = new SharedStringItem();
            var text48 = new Text();
            text48.Text = "Ocean Club Associates, Inc.";

            sharedStringItem48.Append(text48);

            var sharedStringItem49 = new SharedStringItem();
            var text49 = new Text();
            text49.Text =
                "Listing: N/A. Escrow: 30 days. Financing: $660,000 (60% LTV) from Northern Trust Company. Prior sales: none in prior 3 years. The building is a four-story Class B office building in average condition. This property was part of a portfolio short sale. The building was reportedly 50% occupied at the time of the sale.";

            sharedStringItem49.Append(text49);

            var sharedStringItem50 = new SharedStringItem();
            var text50 = new Text();
            text50.Text = "618 U.S. Highway 1";

            sharedStringItem50.Append(text50);

            var sharedStringItem51 = new SharedStringItem();
            var text51 = new Text();
            text51.Text = "1972 (renov 2003)";

            sharedStringItem51.Append(text51);

            var sharedStringItem52 = new SharedStringItem();
            var text52 = new Text();
            text52.Text = "3KAS Partners, LLC";

            sharedStringItem52.Append(text52);

            var sharedStringItem53 = new SharedStringItem();
            var text53 = new Text();
            text53.Text = "3.3/1,000";

            sharedStringItem53.Append(text53);

            var sharedStringItem54 = new SharedStringItem();
            var text54 = new Text();
            text54.Text = "24520-0552";

            sharedStringItem54.Append(text54);

            var sharedStringItem55 = new SharedStringItem();
            var text55 = new Text();
            text55.Text = "68-43-42-16-00-003-0070";

            sharedStringItem55.Append(text55);

            var sharedStringItem56 = new SharedStringItem();
            var text56 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text56.Text = "Douglas Mandel, Listing Broker, 954-245-3400, JCM, 8/11 (11-0897) ";

            sharedStringItem56.Append(text56);

            var sharedStringItem57 = new SharedStringItem();
            var text57 = new Text();
            text57.Text = "3959 Building";

            sharedStringItem57.Append(text57);

            var sharedStringItem58 = new SharedStringItem();
            var text58 = new Text();
            text58.Text = "C / Avg";

            sharedStringItem58.Append(text58);

            var sharedStringItem59 = new SharedStringItem();
            var text59 = new Text();
            text59.Text = "Owner";

            sharedStringItem59.Append(text59);

            var sharedStringItem60 = new SharedStringItem();
            var text60 = new Text();
            text60.Text =
                "Listing: 1 month. Escrow: N/A. Financing: N/A. Prior sales: Sold in November 2010 for $382,200.  Listing broker stated the roof leaked and the interior needed some remodeling.";

            sharedStringItem60.Append(text60);

            var sharedStringItem61 = new SharedStringItem();
            var text61 = new Text();
            text61.Text = "3959 Lake Worth Road";

            sharedStringItem61.Append(text61);

            var sharedStringItem62 = new SharedStringItem();
            var text62 = new Text();
            text62.Text = "CN";

            sharedStringItem62.Append(text62);

            var sharedStringItem63 = new SharedStringItem();
            var text63 = new Text();
            text63.Text = "Palm Springs";

            sharedStringItem63.Append(text63);

            var sharedStringItem64 = new SharedStringItem();
            var text64 = new Text();
            text64.Text = "7/1,000";

            sharedStringItem64.Append(text64);

            var sharedStringItem65 = new SharedStringItem();
            var text65 = new Text();
            text65.Text = "Fair-Avg";

            sharedStringItem65.Append(text65);

            var sharedStringItem66 = new SharedStringItem();
            var text66 = new Text();
            text66.Text = "70-43-44-19-20-066-0150";

            sharedStringItem66.Append(text66);

            var sharedStringItem67 = new SharedStringItem();
            var text67 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text67.Text = "Don Poyner, Listing Broker, 561-307-1900, TEW, 5/11 (11-0530) ";

            sharedStringItem67.Append(text67);

            var sharedStringItem68 = new SharedStringItem();
            var text68 = new Text();
            text68.Text = "Romero Building";

            sharedStringItem68.Append(text68);

            var sharedStringItem69 = new SharedStringItem();
            var text69 = new Text();
            text69.Text = "Rita Romano & Jonathan Bredin";

            sharedStringItem69.Append(text69);

            var sharedStringItem70 = new SharedStringItem();
            var text70 = new Text();
            text70.Text =
                "Listing: N/A. Escrow: N/A. Financing: N/A. Prior sales: Sold in Sep 2010 for $150,100 to Apex, Inc. One-story structure that was originally built as a residence, converted to an office building and then back again. Interior was remodeled. Site has paved parking for 6 cars. Pool on the south side of the site.";

            sharedStringItem70.Append(text70);

            var sharedStringItem71 = new SharedStringItem();
            var text71 = new Text();
            text71.Text = "1531 N Federal Highway";

            sharedStringItem71.Append(text71);

            var sharedStringItem72 = new SharedStringItem();
            var text72 = new Text();
            text72.Text = "POMF20";

            sharedStringItem72.Append(text72);

            var sharedStringItem73 = new SharedStringItem();
            var text73 = new Text();
            text73.Text = "Apex, Inc";

            sharedStringItem73.Append(text73);

            var sharedStringItem74 = new SharedStringItem();
            var text74 = new Text();
            text74.Text = "Lake Worth";

            sharedStringItem74.Append(text74);

            var sharedStringItem75 = new SharedStringItem();
            var text75 = new Text();
            text75.Text = "1.33/1,000";

            sharedStringItem75.Append(text75);

            var sharedStringItem76 = new SharedStringItem();
            var text76 = new Text();
            text76.Text = "24418-0027";

            sharedStringItem76.Append(text76);

            var sharedStringItem77 = new SharedStringItem();
            var text77 = new Text();
            text77.Text = "38-43-44-15-16-015-0090";

            sharedStringItem77.Append(text77);

            var sharedStringItem78 = new SharedStringItem();
            var text78 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text78.Text = "Jonathan Bredin, owner, at the site, TEW, 5/11 (11-0530) ";

            sharedStringItem78.Append(text78);

            var sharedStringItem79 = new SharedStringItem();
            var text79 = new Text();
            text79.Text = "West Palm Beach";

            sharedStringItem79.Append(text79);

            var sharedStringItem80 = new SharedStringItem();
            var text80 = new Text();
            text80.Text = "Good";

            sharedStringItem80.Append(text80);

            var sharedStringItem81 = new SharedStringItem();
            var text81 = new Text();
            text81.Text = "Delray Beach";

            sharedStringItem81.Append(text81);

            var sharedStringItem82 = new SharedStringItem();
            var text82 = new Text();
            text82.Text = "Adequate";

            sharedStringItem82.Append(text82);

            var sharedStringItem83 = new SharedStringItem();
            var text83 = new Text();
            text83.Text = "LB";

            sharedStringItem83.Append(text83);

            var sharedStringItem84 = new SharedStringItem();
            var text84 = new Text();
            text84.Text = "Boca Raton";

            sharedStringItem84.Append(text84);

            var sharedStringItem85 = new SharedStringItem();
            var text85 = new Text();
            text85.Text = "Prof Office";

            sharedStringItem85.Append(text85);

            var sharedStringItem86 = new SharedStringItem();
            var text86 = new Text();
            text86.Text = "B / Avg-Good";

            sharedStringItem86.Append(text86);

            var sharedStringItem87 = new SharedStringItem();
            var text87 = new Text();
            text87.Text = "Avg-Good";

            sharedStringItem87.Append(text87);

            var sharedStringItem88 = new SharedStringItem();
            var text88 = new Text();
            text88.Text = "Med Office";

            sharedStringItem88.Append(text88);

            var sharedStringItem89 = new SharedStringItem();
            var text89 = new Text();
            text89.Text = "Buyer";

            sharedStringItem89.Append(text89);

            var sharedStringItem90 = new SharedStringItem();
            var text90 = new Text();
            text90.Text = "Palm Beach";

            sharedStringItem90.Append(text90);

            var sharedStringItem91 = new SharedStringItem();
            var text91 = new Text();
            text91.Text = "2010 & Older";

            sharedStringItem91.Append(text91);

            var sharedStringItem92 = new SharedStringItem();
            var text92 = new Text();
            text92.Text = "Cypress Trust Bldg";

            sharedStringItem92.Append(text92);

            var sharedStringItem93 = new SharedStringItem();
            var text93 = new Text();
            text93.Text = "Very Good";

            sharedStringItem93.Append(text93);

            var sharedStringItem94 = new SharedStringItem();
            var text94 = new Text();
            text94.Text = "RP/SR Royal Palm Way, LLC";

            sharedStringItem94.Append(text94);

            var sharedStringItem95 = new SharedStringItem();
            var text95 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text95.Text =
                "Listing:  Listed as part of 2-property portfolio at $17,000,000. Contract: N/A. Financing: None recorded. Prior sales: Lender foreclosed in Aug-10. Purchased in conjunction with PNC Wealth Management Bldg and price was allocated per tenancy features. Rents were approximately $39.00/SF NNN at sale. Parking ratio is low, but is adequate since the third floor is a single 800-SF apartment. Attempts to contact broker were not successful, though an extensive marketing package was available. ";

            sharedStringItem95.Append(text95);

            var sharedStringItem96 = new SharedStringItem();
            var text96 = new Text();
            text96.Text = "218 Royal Palm Way";

            sharedStringItem96.Append(text96);

            var sharedStringItem97 = new SharedStringItem();
            var text97 = new Text();
            text97.Text = "C-OPI";

            sharedStringItem97.Append(text97);

            var sharedStringItem98 = new SharedStringItem();
            var text98 = new Text();
            text98.Text = "1946 (ren)";

            sharedStringItem98.Append(text98);

            var sharedStringItem99 = new SharedStringItem();
            var text99 = new Text();
            text99.Text = "218 Royal Palm Way, LLC";

            sharedStringItem99.Append(text99);

            var sharedStringItem100 = new SharedStringItem();
            var text100 = new Text();
            text100.Text = "2.0/1,000";

            sharedStringItem100.Append(text100);

            var sharedStringItem101 = new SharedStringItem();
            var text101 = new Text();
            text101.Text = "Office-Apt";

            sharedStringItem101.Append(text101);

            var sharedStringItem102 = new SharedStringItem();
            var text102 = new Text();
            text102.Text = "24287-0245";

            sharedStringItem102.Append(text102);

            var sharedStringItem103 = new SharedStringItem();
            var text103 = new Text();
            text103.Text = "50-43-43-23-05-025-0370";

            sharedStringItem103.Append(text103);

            var sharedStringItem104 = new SharedStringItem();
            var text104 = new Text();
            text104.Text = "Mark DeLillo, Listing broker, via CoStar, JDW, 7/11 (11-0534) (W)";

            sharedStringItem104.Append(text104);

            var sharedStringItem105 = new SharedStringItem();
            var text105 = new Text();
            text105.Text = "PNC Wealth Mgmt Bldg";

            sharedStringItem105.Append(text105);

            var sharedStringItem106 = new SharedStringItem();
            var text106 = new Text();
            text106.Text = "100% (single)";

            sharedStringItem106.Append(text106);

            var sharedStringItem107 = new SharedStringItem();
            var text107 = new Text();
            text107.Text =
                "Listing: Listed as part of 2-property portfolio at $17,000,000. Contract: N/A. Financing: none recorded. Prior sales: Lender foreclosed in Aug-10. Purchased in conjunction with Cypress Trust Bldg and price was allocated per tenancy features. PNC (credit tenant) is the single occupant and rent was approximately $43.00/ SF NNN at sale. Attempts to contact broker were not successful, though an extensive marketing package was available to us.";

            sharedStringItem107.Append(text107);

            var sharedStringItem108 = new SharedStringItem();
            var text108 = new Text();
            text108.Text = "231 Royal Palm Way";

            sharedStringItem108.Append(text108);

            var sharedStringItem109 = new SharedStringItem();
            var text109 = new Text();
            text109.Text = "1973 (ren)";

            sharedStringItem109.Append(text109);

            var sharedStringItem110 = new SharedStringItem();
            var text110 = new Text();
            text110.Text = "3 (incl. garage)";

            sharedStringItem110.Append(text110);

            var sharedStringItem111 = new SharedStringItem();
            var text111 = new Text();
            text111.Text = "231 Royal Palm Way, LLC";

            sharedStringItem111.Append(text111);

            var sharedStringItem112 = new SharedStringItem();
            var text112 = new Text();
            text112.Text = "3.0/1,000";

            sharedStringItem112.Append(text112);

            var sharedStringItem113 = new SharedStringItem();
            var text113 = new Text();
            text113.Text = "24287-0304";

            sharedStringItem113.Append(text113);

            var sharedStringItem114 = new SharedStringItem();
            var text114 = new Text();
            text114.Text = "50-43-43-23-05-021-0180";

            sharedStringItem114.Append(text114);

            var sharedStringItem115 = new SharedStringItem();
            var text115 = new Text();
            text115.Text = "(garage)";

            sharedStringItem115.Append(text115);

            var sharedStringItem116 = new SharedStringItem();
            var text116 = new Text();
            text116.Text = "Chester Properties";

            sharedStringItem116.Append(text116);

            var sharedStringItem117 = new SharedStringItem();
            var text117 = new Text();
            text117.Text = "Delray 115, LLC";

            sharedStringItem117.Append(text117);

            var sharedStringItem118 = new SharedStringItem();
            var text118 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text118.Text =
                "Listing: Not listed on open market. Contract: 1 month. Financing: Cash. Sales in prior 3 yrs: None. Buyer intended to occupy space after seller vacates within one year of sale date. Property had nice quality finishes and had been recently renovated. Per broker, price in public records does not include 3% commission, but is included herein. ";

            sharedStringItem118.Append(text118);

            var sharedStringItem119 = new SharedStringItem();
            var text119 = new Text();
            text119.Text = "115 SE 4th Ave";

            sharedStringItem119.Append(text119);

            var sharedStringItem120 = new SharedStringItem();
            var text120 = new Text();
            text120.Text = "CBD";

            sharedStringItem120.Append(text120);

            var sharedStringItem121 = new SharedStringItem();
            var text121 = new Text();
            text121.Text = "1939 (reno)";

            sharedStringItem121.Append(text121);

            var sharedStringItem122 = new SharedStringItem();
            var text122 = new Text();
            text122.Text = "Chester Properties, LLC";

            sharedStringItem122.Append(text122);

            var sharedStringItem123 = new SharedStringItem();
            var text123 = new Text();
            text123.Text = "0.8/1,000";

            sharedStringItem123.Append(text123);

            var sharedStringItem124 = new SharedStringItem();
            var text124 = new Text();
            text124.Text = "24296-1729";

            sharedStringItem124.Append(text124);

            var sharedStringItem125 = new SharedStringItem();
            var text125 = new Text();
            text125.Text = "12-43-46-16-01-102-0030";

            sharedStringItem125.Append(text125);

            var sharedStringItem126 = new SharedStringItem();
            var text126 = new Text();
            text126.Text = "(+ street)";

            sharedStringItem126.Append(text126);

            var sharedStringItem127 = new SharedStringItem();
            var text127 = new Text();
            text127.Text = "Chris Lowry, buyer\'s broker, 561-523-3939, JDW, 7/11 (11-0798)";

            sharedStringItem127.Append(text127);

            var sharedStringItem128 = new SharedStringItem();
            var text128 = new Text();
            text128.Text = "B / Good";

            sharedStringItem128.Append(text128);

            var sharedStringItem129 = new SharedStringItem();
            var text129 = new Text();
            text129.Text = "Avg";

            sharedStringItem129.Append(text129);

            var sharedStringItem130 = new SharedStringItem();
            var text130 = new Text();
            text130.Text = "CMUD";

            sharedStringItem130.Append(text130);

            var sharedStringItem131 = new SharedStringItem();
            var text131 = new Text();
            text131.Text = "Bank";

            sharedStringItem131.Append(text131);

            var sharedStringItem132 = new SharedStringItem();
            var text132 = new Text();
            text132.Text = "(shared)";

            sharedStringItem132.Append(text132);

            var sharedStringItem133 = new SharedStringItem();
            var text133 = new Text();
            text133.Text = "Kimley-Horn Bldg";

            sharedStringItem133.Append(text133);

            var sharedStringItem134 = new SharedStringItem();
            var text134 = new Text();
            text134.Text = "70% buyer";

            sharedStringItem134.Append(text134);

            var sharedStringItem135 = new SharedStringItem();
            var text135 = new Text();
            text135.Text = "Grand Bank & Trust of Florida";

            sharedStringItem135.Append(text135);

            var sharedStringItem136 = new SharedStringItem();
            var text136 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text136.Text =
                "Listing: 5 months, listing price at sale was $5,100,000. Escrow: 3 months. Financing: None recorded. Sales in prior 3 yrs: Lender foreclosed on property in April 2009. Buyer purchased for owner use of entire building and expected to require 2 existing tenants to vacate at their lease expirations. Buyer later changed plans and occupies only second floor; 2 tenants remain in 30% of building on first floor. Buyer planned to raze some partially finished interior improvements and considered 70% of building to be in shell condition. Common areas were completed. ";

            sharedStringItem136.Append(text136);

            var sharedStringItem137 = new SharedStringItem();
            var text137 = new Text();
            text137.Text = "1920 Wekiva Way";

            sharedStringItem137.Append(text137);

            var sharedStringItem138 = new SharedStringItem();
            var text138 = new Text();
            text138.Text = "MUPD";

            sharedStringItem138.Append(text138);

            var sharedStringItem139 = new SharedStringItem();
            var text139 = new Text();
            text139.Text = "30% 2 tenants";

            sharedStringItem139.Append(text139);

            var sharedStringItem140 = new SharedStringItem();
            var text140 = new Text();
            text140.Text = "Kimley-Horn and Associates, Inc";

            sharedStringItem140.Append(text140);

            var sharedStringItem141 = new SharedStringItem();
            var text141 = new Text();
            text141.Text = "4/1,000 SF";

            sharedStringItem141.Append(text141);

            var sharedStringItem142 = new SharedStringItem();
            var text142 = new Text();
            text142.Text = "23591-1721";

            sharedStringItem142.Append(text142);

            var sharedStringItem143 = new SharedStringItem();
            var text143 = new Text();
            text143.Text = "74-42-43-28-43-006-0020";

            sharedStringItem143.Append(text143);

            var sharedStringItem144 = new SharedStringItem();
            var text144 = new Text();
            text144.Text =
                "Jonathan Satter, broker for the seller, 561-721-7031, KHD, 12/09, (File A9-1452), (SC11-0208)";

            sharedStringItem144.Append(text144);

            var sharedStringItem145 = new SharedStringItem();
            var text145 = new Text();
            text145.Text = "T & G Bldg";

            sharedStringItem145.Append(text145);

            var sharedStringItem146 = new SharedStringItem();
            var text146 = new Text();
            text146.Text = "Robert Paul Miller";

            sharedStringItem146.Append(text146);

            var sharedStringItem147 = new SharedStringItem();
            var text147 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text147.Text =
                "Listing: n/a, listed for $795,000. Escrow: n/a. Financing: $1,020,000 from IronStone Bank. Prior sales: none in prior 3 years.  Free-standing building subdivided into two spaces. Space consists of 3,484 SF of professional office and 1,635 SF of storage space (former house). Public records show $740,000, but actual price was $750,000 per the broker. ";

            sharedStringItem147.Append(text147);

            var sharedStringItem148 = new SharedStringItem();
            var text148 = new Text();
            text148.Text = "526 SE 5th Avenue";

            sharedStringItem148.Append(text148);

            var sharedStringItem149 = new SharedStringItem();
            var text149 = new Text();
            text149.Text = "GC";

            sharedStringItem149.Append(text149);

            var sharedStringItem150 = new SharedStringItem();
            var text150 = new Text();
            text150.Text = "1947 (renov)";

            sharedStringItem150.Append(text150);

            var sharedStringItem151 = new SharedStringItem();
            var text151 = new Text();
            text151.Text = "T & G 5th Avenue, LLC";

            sharedStringItem151.Append(text151);

            var sharedStringItem152 = new SharedStringItem();
            var text152 = new Text();
            text152.Text = "Fair";

            sharedStringItem152.Append(text152);

            var sharedStringItem153 = new SharedStringItem();
            var text153 = new Text();
            text153.Text = "23601-873";

            sharedStringItem153.Append(text153);

            var sharedStringItem154 = new SharedStringItem();
            var text154 = new Text();
            text154.Text = "12-43-46-21-01-003-0030 & 0020";

            sharedStringItem154.Append(text154);

            var sharedStringItem155 = new SharedStringItem();
            var text155 = new Text();
            text155.Text = "Sue Ann Taurillo, Listing Agent, 561-278-5570, 7/10, KHD (10-0746) (SC10-1255)";

            sharedStringItem155.Append(text155);

            var sharedStringItem156 = new SharedStringItem();
            var text156 = new Text();
            text156.Text = "399 Building";

            sharedStringItem156.Append(text156);

            var sharedStringItem157 = new SharedStringItem();
            var text157 = new Text();
            text157.Text = "399 West Palmetto Park Assoc.";

            sharedStringItem157.Append(text157);

            var sharedStringItem158 = new SharedStringItem();
            var text158 = new Text();
            text158.Text =
                "Listing: 2 months, listing at sale was $2,996,637. Escrow: N/A. Financing: $200,000 PMM (7% LTV). Sales in prior 3 yrs: none. Buyer planned to occupy remaining 10% of space. Site has 11 awning-covered parking spaces plus 34 open parking spaces.";

            sharedStringItem158.Append(text158);

            var sharedStringItem159 = new SharedStringItem();
            var text159 = new Text();
            text159.Text = "399 W Palmetto Park Rd";

            sharedStringItem159.Append(text159);

            var sharedStringItem160 = new SharedStringItem();
            var text160 = new Text();
            text160.Text = "1982 (ren)";

            sharedStringItem160.Append(text160);

            var sharedStringItem161 = new SharedStringItem();
            var text161 = new Text();
            text161.Text = "399 Palmetto, LLC";

            sharedStringItem161.Append(text161);

            var sharedStringItem162 = new SharedStringItem();
            var text162 = new Text();
            text162.Text = "3.8/1,000";

            sharedStringItem162.Append(text162);

            var sharedStringItem163 = new SharedStringItem();
            var text163 = new Text();
            text163.Text = "23431-638";

            sharedStringItem163.Append(text163);

            var sharedStringItem164 = new SharedStringItem();
            var text164 = new Text();
            text164.Text = "06-43-47-19-19-001-0000";

            sharedStringItem164.Append(text164);

            var sharedStringItem165 = new SharedStringItem();
            var text165 = new Text();
            text165.Text = "(covered)";

            sharedStringItem165.Append(text165);

            var sharedStringItem166 = new SharedStringItem();
            var text166 = new Text();
            text166.Text = "Jack Burt, listing broker, 954-465-3692, JCM, 9/09 (A9-1116)";

            sharedStringItem166.Append(text166);

            var sharedStringItem167 = new SharedStringItem();
            var text167 = new Text();
            text167.Text = "Listings";

            sharedStringItem167.Append(text167);

            var sharedStringItem168 = new SharedStringItem();
            var text168 = new Text();
            text168.Text = "Lakeview Corporate Center";

            sharedStringItem168.Append(text168);

            var sharedStringItem169 = new SharedStringItem();
            var text169 = new Text();
            text169.Text = "BLCM, LLC";

            sharedStringItem169.Append(text169);

            var sharedStringItem170 = new SharedStringItem();
            var text170 = new Text();
            text170.Text =
                "Listing: 6 months at listing price. Escrow: Not applicable. Financing: Not applicable. Sales in prior 3 yrs: None. No offers have been made. Most leases are $10.00/SF but landlord wants to rent vacant space and leased one suite and is listing vacant suites at $6.95/SF NNN. Operating expenses are $6.80-$7.00/SF and tenants pay separately for electricity and janitorial service. Common area factor is 16%. Landlord updated hallways and restrooms three years ago. Site includes a lake.";

            sharedStringItem170.Append(text170);

            var sharedStringItem171 = new SharedStringItem();
            var text171 = new Text();
            text171.Text = "6415 Lake Worth Rd";

            sharedStringItem171.Append(text171);

            var sharedStringItem172 = new SharedStringItem();
            var text172 = new Text();
            text172.Text = "CI";

            sharedStringItem172.Append(text172);

            var sharedStringItem173 = new SharedStringItem();
            var text173 = new Text();
            text173.Text = "multiple tenants";

            sharedStringItem173.Append(text173);

            var sharedStringItem174 = new SharedStringItem();
            var text174 = new Text();
            text174.Text = "Listing";

            sharedStringItem174.Append(text174);

            var sharedStringItem175 = new SharedStringItem();
            var text175 = new Text();
            text175.Text = "Greenacres";

            sharedStringItem175.Append(text175);

            var sharedStringItem176 = new SharedStringItem();
            var text176 = new Text();
            text176.Text = "4.4/1,000";

            sharedStringItem176.Append(text176);

            var sharedStringItem177 = new SharedStringItem();
            var text177 = new Text();
            text177.Text = "18-42-44-22-00-000-5026";

            sharedStringItem177.Append(text177);

            var sharedStringItem178 = new SharedStringItem();
            var text178 = new Text();
            text178.Text = "Jerry Lehman, listing broker, 561-995-8887, DSW, 6/11 (11-0575)";

            sharedStringItem178.Append(text178);

            var sharedStringItem179 = new SharedStringItem();
            var text179 = new Text();
            text179.Text = "5700 Professional Park";

            sharedStringItem179.Append(text179);

            var sharedStringItem180 = new SharedStringItem();
            var text180 = new Text();
            text180.Text = "David Associates 5";

            sharedStringItem180.Append(text180);

            var sharedStringItem181 = new SharedStringItem();
            var text181 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text181.Text =
                "Listing: 1 month. Escrow: Not applicable. Financing: Not applicable. Sales in prior 3 yrs: None. EGI as of June 2011 is $20.59/SF and 2010 operating expenses were $8.04/SF for this building and a 1-story front retail building with 12,670 SF not included in this listing. ";

            sharedStringItem181.Append(text181);

            var sharedStringItem182 = new SharedStringItem();
            var text182 = new Text();
            text182.Text = "5700 Lake Worth Rd";

            sharedStringItem182.Append(text182);

            var sharedStringItem183 = new SharedStringItem();
            var text183 = new Text();
            text183.Text = "1986/ren1999";

            sharedStringItem183.Append(text183);

            var sharedStringItem184 = new SharedStringItem();
            var text184 = new Text();
            text184.Text = "32 tenants";

            sharedStringItem184.Append(text184);

            var sharedStringItem185 = new SharedStringItem();
            var text185 = new Text();
            text185.Text = "Prof & med office";

            sharedStringItem185.Append(text185);

            var sharedStringItem186 = new SharedStringItem();
            var text186 = new Text();
            text186.Text = "18-42-44-26-18-001-0000";

            sharedStringItem186.Append(text186);

            var sharedStringItem187 = new SharedStringItem();
            var text187 = new Text();
            text187.Text = "Kevin McCarthy, listing broker, 561-804-9678, DSW, 6/11 (11-0575)";

            sharedStringItem187.Append(text187);

            var sharedStringItem188 = new SharedStringItem();
            var text188 = new Text();
            text188.Text = "Office Bldg";

            sharedStringItem188.Append(text188);

            var sharedStringItem189 = new SharedStringItem();
            var text189 = new Text();
            text189.Text = "Vacant";

            sharedStringItem189.Append(text189);

            var sharedStringItem190 = new SharedStringItem();
            var text190 = new Text();
            text190.Text =
                "Listing: 12 months. Escrow: N/A. Financing: N/A. Sales in prior 3 yrs: none. Consists of two parcels on the corner of Broward Ave and Picadilly St. Somewhat dated interior per broker. Broker indicated market activity has been decent.";

            sharedStringItem190.Append(text190);

            var sharedStringItem191 = new SharedStringItem();
            var text191 = new Text();
            text191.Text = "2045 Broward Ave";

            sharedStringItem191.Append(text191);

            var sharedStringItem192 = new SharedStringItem();
            var text192 = new Text();
            text192.Text = "List";

            sharedStringItem192.Append(text192);

            var sharedStringItem193 = new SharedStringItem();
            var text193 = new Text();
            text193.Text = "3.7/1,000";

            sharedStringItem193.Append(text193);

            var sharedStringItem194 = new SharedStringItem();
            var text194 = new Text();
            text194.Text = "74-43-43-10-19-005-0122, et al";

            sharedStringItem194.Append(text194);

            var sharedStringItem195 = new SharedStringItem();
            var text195 = new Text();
            text195.Text = "Darlene Glayat, listing broker, 561-659-5554, JDW, 11/10 (10-1187)";

            sharedStringItem195.Append(text195);

            var sharedStringItem196 = new SharedStringItem();
            var text196 = new Text { Text = "Class:" };

            sharedStringItem196.Append(text196);

            var sharedStringItem197 = new SharedStringItem();
            var text197 = new Text();
            text197.Text = "A (means interior entries & upgraded building amenities)";

            sharedStringItem197.Append(text197);

            var sharedStringItem198 = new SharedStringItem();
            var text198 = new Text();
            text198.Text = "B (means interior entries)";

            sharedStringItem198.Append(text198);

            var sharedStringItem199 = new SharedStringItem();
            var text199 = new Text();
            text199.Text = "C (means exterior entries)";

            sharedStringItem199.Append(text199);

            var sharedStringItem200 = new SharedStringItem();
            var text200 = new Text();
            text200.Text = "Condition:";

            sharedStringItem200.Append(text200);

            var sharedStringItem201 = new SharedStringItem();
            var text201 = new Text();
            text201.Text = "New";

            sharedStringItem201.Append(text201);

            var sharedStringItem202 = new SharedStringItem();
            var text202 = new Text();
            text202.Text = "Quality:";

            sharedStringItem202.Append(text202);

            var sharedStringItem203 = new SharedStringItem();
            var text203 = new Text();
            text203.Text = "Raw or Van Shell";

            sharedStringItem203.Append(text203);

            var sharedStringItem204 = new SharedStringItem();
            var text204 = new Text();
            text204.Text = "(by seller at time of sale)";

            sharedStringItem204.Append(text204);

            var sharedStringItem205 = new SharedStringItem();
            var text205 = new Text();
            text205.Text = "Excellent";

            sharedStringItem205.Append(text205);

            var sharedStringItem206 = new SharedStringItem();
            var text206 = new Text();
            text206.Text = "Prof office";

            sharedStringItem206.Append(text206);

            var sharedStringItem207 = new SharedStringItem();
            var text207 = new Text();
            text207.Text = "Minor def maint";

            sharedStringItem207.Append(text207);

            var sharedStringItem208 = new SharedStringItem();
            var text208 = new Text();
            text208.Text = "VG";

            sharedStringItem208.Append(text208);

            var sharedStringItem209 = new SharedStringItem();
            var text209 = new Text();
            text209.Text = "Med office";

            sharedStringItem209.Append(text209);

            var sharedStringItem210 = new SharedStringItem();
            var text210 = new Text();
            text210.Text = "Poor (means uninhabitable)";

            sharedStringItem210.Append(text210);

            var sharedStringItem211 = new SharedStringItem();
            var text211 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text211.Text = "Parking: ";

            sharedStringItem211.Append(text211);

            var sharedStringItem212 = new SharedStringItem();
            var text212 = new Text();
            text212.Text =
                "(indicate parking spaces per 1,000 SF of building, if available; otherwise enter a verbal description)";

            sharedStringItem212.Append(text212);

            var sharedStringItem213 = new SharedStringItem();
            var text213 = new Text();
            text213.Text = "Limited";

            sharedStringItem213.Append(text213);

            var sharedStringItem214 = new SharedStringItem();
            var text214 = new Text();
            text214.Text = "None";

            sharedStringItem214.Append(text214);

            var sharedStringItem215 = new SharedStringItem();
            var text215 = new Text();
            text215.Text =
                "When the verification source is included in a file that has a self-contained format, insert SC before the file number within the parentheses.";

            sharedStringItem215.Append(text215);

            var sharedStringItem216 = new SharedStringItem();
            var text216 = new Text();
            text216.Text =
                "When a file noted under the verification source is the subject of that file, insert S after the file number within the parentheses.";

            sharedStringItem216.Append(text216);

            var sharedStringItem217 = new SharedStringItem();
            var text217 = new Text();
            text217.Text = "Add listings below a solid line under sales and contracts.";

            sharedStringItem217.Append(text217);

            var sharedStringItem218 = new SharedStringItem();
            var text218 = new Text();
            text218.Text = "Add subject below a solid line at bottom.";

            sharedStringItem218.Append(text218);

            var sharedStringItem219 = new SharedStringItem();
            var text219 = new Text { Space = SpaceProcessingModeValues.Preserve };
            text219.Text =
                "For Sale Date of listings, show date verified; for Sale Date of contracts, show date contract signed. ";

            sharedStringItem219.Append(text219);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);
            sharedStringTable1.Append(sharedStringItem5);
            sharedStringTable1.Append(sharedStringItem6);
            sharedStringTable1.Append(sharedStringItem7);
            sharedStringTable1.Append(sharedStringItem8);
            sharedStringTable1.Append(sharedStringItem9);
            sharedStringTable1.Append(sharedStringItem10);
            sharedStringTable1.Append(sharedStringItem11);
            sharedStringTable1.Append(sharedStringItem12);
            sharedStringTable1.Append(sharedStringItem13);
            sharedStringTable1.Append(sharedStringItem14);
            sharedStringTable1.Append(sharedStringItem15);
            sharedStringTable1.Append(sharedStringItem16);
            sharedStringTable1.Append(sharedStringItem17);
            sharedStringTable1.Append(sharedStringItem18);
            sharedStringTable1.Append(sharedStringItem19);
            sharedStringTable1.Append(sharedStringItem20);
            sharedStringTable1.Append(sharedStringItem21);
            sharedStringTable1.Append(sharedStringItem22);
            sharedStringTable1.Append(sharedStringItem23);
            sharedStringTable1.Append(sharedStringItem24);
            sharedStringTable1.Append(sharedStringItem25);
            sharedStringTable1.Append(sharedStringItem26);
            sharedStringTable1.Append(sharedStringItem27);
            sharedStringTable1.Append(sharedStringItem28);
            sharedStringTable1.Append(sharedStringItem29);
            sharedStringTable1.Append(sharedStringItem30);
            sharedStringTable1.Append(sharedStringItem31);
            sharedStringTable1.Append(sharedStringItem32);
            sharedStringTable1.Append(sharedStringItem33);
            sharedStringTable1.Append(sharedStringItem34);
            sharedStringTable1.Append(sharedStringItem35);
            sharedStringTable1.Append(sharedStringItem36);
            sharedStringTable1.Append(sharedStringItem37);
            sharedStringTable1.Append(sharedStringItem38);
            sharedStringTable1.Append(sharedStringItem39);
            sharedStringTable1.Append(sharedStringItem40);
            sharedStringTable1.Append(sharedStringItem41);
            sharedStringTable1.Append(sharedStringItem42);
            sharedStringTable1.Append(sharedStringItem43);
            sharedStringTable1.Append(sharedStringItem44);
            sharedStringTable1.Append(sharedStringItem45);
            sharedStringTable1.Append(sharedStringItem46);
            sharedStringTable1.Append(sharedStringItem47);
            sharedStringTable1.Append(sharedStringItem48);
            sharedStringTable1.Append(sharedStringItem49);
            sharedStringTable1.Append(sharedStringItem50);
            sharedStringTable1.Append(sharedStringItem51);
            sharedStringTable1.Append(sharedStringItem52);
            sharedStringTable1.Append(sharedStringItem53);
            sharedStringTable1.Append(sharedStringItem54);
            sharedStringTable1.Append(sharedStringItem55);
            sharedStringTable1.Append(sharedStringItem56);
            sharedStringTable1.Append(sharedStringItem57);
            sharedStringTable1.Append(sharedStringItem58);
            sharedStringTable1.Append(sharedStringItem59);
            sharedStringTable1.Append(sharedStringItem60);
            sharedStringTable1.Append(sharedStringItem61);
            sharedStringTable1.Append(sharedStringItem62);
            sharedStringTable1.Append(sharedStringItem63);
            sharedStringTable1.Append(sharedStringItem64);
            sharedStringTable1.Append(sharedStringItem65);
            sharedStringTable1.Append(sharedStringItem66);
            sharedStringTable1.Append(sharedStringItem67);
            sharedStringTable1.Append(sharedStringItem68);
            sharedStringTable1.Append(sharedStringItem69);
            sharedStringTable1.Append(sharedStringItem70);
            sharedStringTable1.Append(sharedStringItem71);
            sharedStringTable1.Append(sharedStringItem72);
            sharedStringTable1.Append(sharedStringItem73);
            sharedStringTable1.Append(sharedStringItem74);
            sharedStringTable1.Append(sharedStringItem75);
            sharedStringTable1.Append(sharedStringItem76);
            sharedStringTable1.Append(sharedStringItem77);
            sharedStringTable1.Append(sharedStringItem78);
            sharedStringTable1.Append(sharedStringItem79);
            sharedStringTable1.Append(sharedStringItem80);
            sharedStringTable1.Append(sharedStringItem81);
            sharedStringTable1.Append(sharedStringItem82);
            sharedStringTable1.Append(sharedStringItem83);
            sharedStringTable1.Append(sharedStringItem84);
            sharedStringTable1.Append(sharedStringItem85);
            sharedStringTable1.Append(sharedStringItem86);
            sharedStringTable1.Append(sharedStringItem87);
            sharedStringTable1.Append(sharedStringItem88);
            sharedStringTable1.Append(sharedStringItem89);
            sharedStringTable1.Append(sharedStringItem90);
            sharedStringTable1.Append(sharedStringItem91);
            sharedStringTable1.Append(sharedStringItem92);
            sharedStringTable1.Append(sharedStringItem93);
            sharedStringTable1.Append(sharedStringItem94);
            sharedStringTable1.Append(sharedStringItem95);
            sharedStringTable1.Append(sharedStringItem96);
            sharedStringTable1.Append(sharedStringItem97);
            sharedStringTable1.Append(sharedStringItem98);
            sharedStringTable1.Append(sharedStringItem99);
            sharedStringTable1.Append(sharedStringItem100);
            sharedStringTable1.Append(sharedStringItem101);
            sharedStringTable1.Append(sharedStringItem102);
            sharedStringTable1.Append(sharedStringItem103);
            sharedStringTable1.Append(sharedStringItem104);
            sharedStringTable1.Append(sharedStringItem105);
            sharedStringTable1.Append(sharedStringItem106);
            sharedStringTable1.Append(sharedStringItem107);
            sharedStringTable1.Append(sharedStringItem108);
            sharedStringTable1.Append(sharedStringItem109);
            sharedStringTable1.Append(sharedStringItem110);
            sharedStringTable1.Append(sharedStringItem111);
            sharedStringTable1.Append(sharedStringItem112);
            sharedStringTable1.Append(sharedStringItem113);
            sharedStringTable1.Append(sharedStringItem114);
            sharedStringTable1.Append(sharedStringItem115);
            sharedStringTable1.Append(sharedStringItem116);
            sharedStringTable1.Append(sharedStringItem117);
            sharedStringTable1.Append(sharedStringItem118);
            sharedStringTable1.Append(sharedStringItem119);
            sharedStringTable1.Append(sharedStringItem120);
            sharedStringTable1.Append(sharedStringItem121);
            sharedStringTable1.Append(sharedStringItem122);
            sharedStringTable1.Append(sharedStringItem123);
            sharedStringTable1.Append(sharedStringItem124);
            sharedStringTable1.Append(sharedStringItem125);
            sharedStringTable1.Append(sharedStringItem126);
            sharedStringTable1.Append(sharedStringItem127);
            sharedStringTable1.Append(sharedStringItem128);
            sharedStringTable1.Append(sharedStringItem129);
            sharedStringTable1.Append(sharedStringItem130);
            sharedStringTable1.Append(sharedStringItem131);
            sharedStringTable1.Append(sharedStringItem132);
            sharedStringTable1.Append(sharedStringItem133);
            sharedStringTable1.Append(sharedStringItem134);
            sharedStringTable1.Append(sharedStringItem135);
            sharedStringTable1.Append(sharedStringItem136);
            sharedStringTable1.Append(sharedStringItem137);
            sharedStringTable1.Append(sharedStringItem138);
            sharedStringTable1.Append(sharedStringItem139);
            sharedStringTable1.Append(sharedStringItem140);
            sharedStringTable1.Append(sharedStringItem141);
            sharedStringTable1.Append(sharedStringItem142);
            sharedStringTable1.Append(sharedStringItem143);
            sharedStringTable1.Append(sharedStringItem144);
            sharedStringTable1.Append(sharedStringItem145);
            sharedStringTable1.Append(sharedStringItem146);
            sharedStringTable1.Append(sharedStringItem147);
            sharedStringTable1.Append(sharedStringItem148);
            sharedStringTable1.Append(sharedStringItem149);
            sharedStringTable1.Append(sharedStringItem150);
            sharedStringTable1.Append(sharedStringItem151);
            sharedStringTable1.Append(sharedStringItem152);
            sharedStringTable1.Append(sharedStringItem153);
            sharedStringTable1.Append(sharedStringItem154);
            sharedStringTable1.Append(sharedStringItem155);
            sharedStringTable1.Append(sharedStringItem156);
            sharedStringTable1.Append(sharedStringItem157);
            sharedStringTable1.Append(sharedStringItem158);
            sharedStringTable1.Append(sharedStringItem159);
            sharedStringTable1.Append(sharedStringItem160);
            sharedStringTable1.Append(sharedStringItem161);
            sharedStringTable1.Append(sharedStringItem162);
            sharedStringTable1.Append(sharedStringItem163);
            sharedStringTable1.Append(sharedStringItem164);
            sharedStringTable1.Append(sharedStringItem165);
            sharedStringTable1.Append(sharedStringItem166);
            sharedStringTable1.Append(sharedStringItem167);
            sharedStringTable1.Append(sharedStringItem168);
            sharedStringTable1.Append(sharedStringItem169);
            sharedStringTable1.Append(sharedStringItem170);
            sharedStringTable1.Append(sharedStringItem171);
            sharedStringTable1.Append(sharedStringItem172);
            sharedStringTable1.Append(sharedStringItem173);
            sharedStringTable1.Append(sharedStringItem174);
            sharedStringTable1.Append(sharedStringItem175);
            sharedStringTable1.Append(sharedStringItem176);
            sharedStringTable1.Append(sharedStringItem177);
            sharedStringTable1.Append(sharedStringItem178);
            sharedStringTable1.Append(sharedStringItem179);
            sharedStringTable1.Append(sharedStringItem180);
            sharedStringTable1.Append(sharedStringItem181);
            sharedStringTable1.Append(sharedStringItem182);
            sharedStringTable1.Append(sharedStringItem183);
            sharedStringTable1.Append(sharedStringItem184);
            sharedStringTable1.Append(sharedStringItem185);
            sharedStringTable1.Append(sharedStringItem186);
            sharedStringTable1.Append(sharedStringItem187);
            sharedStringTable1.Append(sharedStringItem188);
            sharedStringTable1.Append(sharedStringItem189);
            sharedStringTable1.Append(sharedStringItem190);
            sharedStringTable1.Append(sharedStringItem191);
            sharedStringTable1.Append(sharedStringItem192);
            sharedStringTable1.Append(sharedStringItem193);
            sharedStringTable1.Append(sharedStringItem194);
            sharedStringTable1.Append(sharedStringItem195);
            sharedStringTable1.Append(sharedStringItem196);
            sharedStringTable1.Append(sharedStringItem197);
            sharedStringTable1.Append(sharedStringItem198);
            sharedStringTable1.Append(sharedStringItem199);
            sharedStringTable1.Append(sharedStringItem200);
            sharedStringTable1.Append(sharedStringItem201);
            sharedStringTable1.Append(sharedStringItem202);
            sharedStringTable1.Append(sharedStringItem203);
            sharedStringTable1.Append(sharedStringItem204);
            sharedStringTable1.Append(sharedStringItem205);
            sharedStringTable1.Append(sharedStringItem206);
            sharedStringTable1.Append(sharedStringItem207);
            sharedStringTable1.Append(sharedStringItem208);
            sharedStringTable1.Append(sharedStringItem209);
            sharedStringTable1.Append(sharedStringItem210);
            sharedStringTable1.Append(sharedStringItem211);
            sharedStringTable1.Append(sharedStringItem212);
            sharedStringTable1.Append(sharedStringItem213);
            sharedStringTable1.Append(sharedStringItem214);
            sharedStringTable1.Append(sharedStringItem215);
            sharedStringTable1.Append(sharedStringItem216);
            sharedStringTable1.Append(sharedStringItem217);
            sharedStringTable1.Append(sharedStringItem218);
            sharedStringTable1.Append(sharedStringItem219);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        // Generates content of workbookStylesPart1.
        private void GenerateWorkbookStyles(WorkbookStylesPart workbookStylesPart1)
        {
            var stylesheet1 = new Stylesheet { MCAttributes = new MarkupCompatibilityAttributes { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            var numberingFormats1 = new NumberingFormats { Count = 8U };
            var numberingFormat1 = new NumberingFormat
            {
                NumberFormatId = 164U,
                FormatCode = "_(* #,##0.00_);_(* \\(#,##0.00\\);_(* \\-??_);_(@_)"
            };
            var numberingFormat2 = new NumberingFormat
            {
                NumberFormatId = 165U,
                FormatCode = "_(\\$* #,##0.00_);_(\\$* \\(#,##0.00\\);_(\\$* \\-??_);_(@_)"
            };
            var numberingFormat3 = new NumberingFormat { NumberFormatId = 166U, FormatCode = "\\$#,##0_);[Red]\"($\"#,##0\\)" };
            var numberingFormat4 = new NumberingFormat { NumberFormatId = 167U, FormatCode = "\\$#,##0" };
            var numberingFormat5 = new NumberingFormat { NumberFormatId = 168U, FormatCode = "\\$#,##0.00_);[Red]\"($\"#,##0.00\\)" };
            var numberingFormat6 = new NumberingFormat { NumberFormatId = 169U, FormatCode = "mmm\\-yy;@" };
            var numberingFormat7 = new NumberingFormat { NumberFormatId = 170U, FormatCode = "0.0%" };
            var numberingFormat8 = new NumberingFormat { NumberFormatId = 171U, FormatCode = "#,##0.000" };

            numberingFormats1.Append(numberingFormat1);
            numberingFormats1.Append(numberingFormat2);
            numberingFormats1.Append(numberingFormat3);
            numberingFormats1.Append(numberingFormat4);
            numberingFormats1.Append(numberingFormat5);
            numberingFormats1.Append(numberingFormat6);
            numberingFormats1.Append(numberingFormat7);
            numberingFormats1.Append(numberingFormat8);

            var fonts1 = new Fonts { Count = 31U, KnownFonts = true };

            var font1 = new Font();
            var fontSize1 = new FontSize { Val = 10D };
            var fontName1 = new FontName { Val = "Arial" };
            var fontFamilyNumbering1 = new FontFamilyNumbering { Val = 2 };

            font1.Append(fontSize1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);

            var font2 = new Font();
            var fontSize2 = new FontSize { Val = 11D };
            var color1 = new Color { Indexed = 8U };
            var fontName2 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering2 = new FontFamilyNumbering { Val = 2 };

            font2.Append(fontSize2);
            font2.Append(color1);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);

            var font3 = new Font();
            var fontSize3 = new FontSize { Val = 11D };
            var color2 = new Color { Indexed = 9U };
            var fontName3 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering3 = new FontFamilyNumbering { Val = 2 };

            font3.Append(fontSize3);
            font3.Append(color2);
            font3.Append(fontName3);
            font3.Append(fontFamilyNumbering3);

            var font4 = new Font();
            var fontSize4 = new FontSize { Val = 11D };
            var color3 = new Color { Indexed = 20U };
            var fontName4 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering4 = new FontFamilyNumbering { Val = 2 };

            font4.Append(fontSize4);
            font4.Append(color3);
            font4.Append(fontName4);
            font4.Append(fontFamilyNumbering4);

            var font5 = new Font();
            var bold1 = new Bold();
            var fontSize5 = new FontSize { Val = 11D };
            var color4 = new Color { Indexed = 52U };
            var fontName5 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering5 = new FontFamilyNumbering { Val = 2 };

            font5.Append(bold1);
            font5.Append(fontSize5);
            font5.Append(color4);
            font5.Append(fontName5);
            font5.Append(fontFamilyNumbering5);

            var font6 = new Font();
            var bold2 = new Bold();
            var fontSize6 = new FontSize { Val = 11D };
            var color5 = new Color { Indexed = 9U };
            var fontName6 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering6 = new FontFamilyNumbering { Val = 2 };

            font6.Append(bold2);
            font6.Append(fontSize6);
            font6.Append(color5);
            font6.Append(fontName6);
            font6.Append(fontFamilyNumbering6);

            var font7 = new Font();
            var italic1 = new Italic();
            var fontSize7 = new FontSize { Val = 11D };
            var color6 = new Color { Indexed = 23U };
            var fontName7 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering7 = new FontFamilyNumbering { Val = 2 };

            font7.Append(italic1);
            font7.Append(fontSize7);
            font7.Append(color6);
            font7.Append(fontName7);
            font7.Append(fontFamilyNumbering7);

            var font8 = new Font();
            var fontSize8 = new FontSize { Val = 11D };
            var color7 = new Color { Indexed = 17U };
            var fontName8 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering8 = new FontFamilyNumbering { Val = 2 };

            font8.Append(fontSize8);
            font8.Append(color7);
            font8.Append(fontName8);
            font8.Append(fontFamilyNumbering8);

            var font9 = new Font();
            var bold3 = new Bold();
            var fontSize9 = new FontSize { Val = 15D };
            var color8 = new Color { Indexed = 56U };
            var fontName9 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering9 = new FontFamilyNumbering { Val = 2 };

            font9.Append(bold3);
            font9.Append(fontSize9);
            font9.Append(color8);
            font9.Append(fontName9);
            font9.Append(fontFamilyNumbering9);

            var font10 = new Font();
            var bold4 = new Bold();
            var fontSize10 = new FontSize { Val = 13D };
            var color9 = new Color { Indexed = 56U };
            var fontName10 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering10 = new FontFamilyNumbering { Val = 2 };

            font10.Append(bold4);
            font10.Append(fontSize10);
            font10.Append(color9);
            font10.Append(fontName10);
            font10.Append(fontFamilyNumbering10);

            var font11 = new Font();
            var bold5 = new Bold();
            var fontSize11 = new FontSize { Val = 11D };
            var color10 = new Color { Indexed = 56U };
            var fontName11 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering11 = new FontFamilyNumbering { Val = 2 };

            font11.Append(bold5);
            font11.Append(fontSize11);
            font11.Append(color10);
            font11.Append(fontName11);
            font11.Append(fontFamilyNumbering11);

            var font12 = new Font();
            var fontSize12 = new FontSize { Val = 11D };
            var color11 = new Color { Indexed = 62U };
            var fontName12 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering12 = new FontFamilyNumbering { Val = 2 };

            font12.Append(fontSize12);
            font12.Append(color11);
            font12.Append(fontName12);
            font12.Append(fontFamilyNumbering12);

            var font13 = new Font();
            var fontSize13 = new FontSize { Val = 11D };
            var color12 = new Color { Indexed = 52U };
            var fontName13 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering13 = new FontFamilyNumbering { Val = 2 };

            font13.Append(fontSize13);
            font13.Append(color12);
            font13.Append(fontName13);
            font13.Append(fontFamilyNumbering13);

            var font14 = new Font();
            var fontSize14 = new FontSize { Val = 11D };
            var color13 = new Color { Indexed = 60U };
            var fontName14 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering14 = new FontFamilyNumbering { Val = 2 };

            font14.Append(fontSize14);
            font14.Append(color13);
            font14.Append(fontName14);
            font14.Append(fontFamilyNumbering14);

            var font15 = new Font();
            var bold6 = new Bold();
            var fontSize15 = new FontSize { Val = 11D };
            var color14 = new Color { Indexed = 63U };
            var fontName15 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering15 = new FontFamilyNumbering { Val = 2 };

            font15.Append(bold6);
            font15.Append(fontSize15);
            font15.Append(color14);
            font15.Append(fontName15);
            font15.Append(fontFamilyNumbering15);

            var font16 = new Font();
            var bold7 = new Bold();
            var fontSize16 = new FontSize { Val = 18D };
            var color15 = new Color { Indexed = 56U };
            var fontName16 = new FontName { Val = "Cambria" };
            var fontFamilyNumbering16 = new FontFamilyNumbering { Val = 2 };

            font16.Append(bold7);
            font16.Append(fontSize16);
            font16.Append(color15);
            font16.Append(fontName16);
            font16.Append(fontFamilyNumbering16);

            var font17 = new Font();
            var bold8 = new Bold();
            var fontSize17 = new FontSize { Val = 11D };
            var color16 = new Color { Indexed = 8U };
            var fontName17 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering17 = new FontFamilyNumbering { Val = 2 };

            font17.Append(bold8);
            font17.Append(fontSize17);
            font17.Append(color16);
            font17.Append(fontName17);
            font17.Append(fontFamilyNumbering17);

            var font18 = new Font();
            var fontSize18 = new FontSize { Val = 11D };
            var color17 = new Color { Indexed = 10U };
            var fontName18 = new FontName { Val = "Calibri" };
            var fontFamilyNumbering18 = new FontFamilyNumbering { Val = 2 };

            font18.Append(fontSize18);
            font18.Append(color17);
            font18.Append(fontName18);
            font18.Append(fontFamilyNumbering18);

            var font19 = new Font();
            var bold9 = new Bold();
            var fontSize19 = new FontSize { Val = 12D };
            var fontName19 = new FontName { Val = "Arial" };
            var fontFamilyNumbering19 = new FontFamilyNumbering { Val = 2 };

            font19.Append(bold9);
            font19.Append(fontSize19);
            font19.Append(fontName19);
            font19.Append(fontFamilyNumbering19);

            var font20 = new Font();
            var fontSize20 = new FontSize { Val = 8D };
            var fontName20 = new FontName { Val = "Arial" };
            var fontFamilyNumbering20 = new FontFamilyNumbering { Val = 2 };

            font20.Append(fontSize20);
            font20.Append(fontName20);
            font20.Append(fontFamilyNumbering20);

            var font21 = new Font();
            var bold10 = new Bold();
            var fontSize21 = new FontSize { Val = 10D };
            var fontName21 = new FontName { Val = "Arial" };
            var fontFamilyNumbering21 = new FontFamilyNumbering { Val = 2 };

            font21.Append(bold10);
            font21.Append(fontSize21);
            font21.Append(fontName21);
            font21.Append(fontFamilyNumbering21);

            var font22 = new Font();
            var fontSize22 = new FontSize { Val = 9D };
            var fontName22 = new FontName { Val = "Arial" };
            var fontFamilyNumbering22 = new FontFamilyNumbering { Val = 2 };

            font22.Append(fontSize22);
            font22.Append(fontName22);
            font22.Append(fontFamilyNumbering22);

            var font23 = new Font();
            var italic2 = new Italic();
            var fontSize23 = new FontSize { Val = 8D };
            var fontName23 = new FontName { Val = "Arial" };
            var fontFamilyNumbering23 = new FontFamilyNumbering { Val = 2 };

            font23.Append(italic2);
            font23.Append(fontSize23);
            font23.Append(fontName23);
            font23.Append(fontFamilyNumbering23);

            var font24 = new Font();
            var fontSize24 = new FontSize { Val = 10D };
            var color18 = new Color { Indexed = 8U };
            var fontName24 = new FontName { Val = "Arial" };
            var fontFamilyNumbering24 = new FontFamilyNumbering { Val = 2 };

            font24.Append(fontSize24);
            font24.Append(color18);
            font24.Append(fontName24);
            font24.Append(fontFamilyNumbering24);

            var font25 = new Font();
            var bold11 = new Bold();
            var fontSize25 = new FontSize { Val = 10D };
            var color19 = new Color { Indexed = 8U };
            var fontName25 = new FontName { Val = "Arial" };
            var fontFamilyNumbering25 = new FontFamilyNumbering { Val = 2 };

            font25.Append(bold11);
            font25.Append(fontSize25);
            font25.Append(color19);
            font25.Append(fontName25);
            font25.Append(fontFamilyNumbering25);

            var font26 = new Font();
            var fontSize26 = new FontSize { Val = 8D };
            var color20 = new Color { Indexed = 8U };
            var fontName26 = new FontName { Val = "Arial" };
            var fontFamilyNumbering26 = new FontFamilyNumbering { Val = 2 };

            font26.Append(fontSize26);
            font26.Append(color20);
            font26.Append(fontName26);
            font26.Append(fontFamilyNumbering26);

            var font27 = new Font();
            var bold12 = new Bold();
            var italic3 = new Italic();
            var fontSize27 = new FontSize { Val = 12D };
            var fontName27 = new FontName { Val = "Arial" };
            var fontFamilyNumbering27 = new FontFamilyNumbering { Val = 2 };

            font27.Append(bold12);
            font27.Append(italic3);
            font27.Append(fontSize27);
            font27.Append(fontName27);
            font27.Append(fontFamilyNumbering27);

            var font28 = new Font();
            var fontSize28 = new FontSize { Val = 10D };
            var color21 = new Color { Indexed = 10U };
            var fontName28 = new FontName { Val = "Arial" };
            var fontFamilyNumbering28 = new FontFamilyNumbering { Val = 2 };

            font28.Append(fontSize28);
            font28.Append(color21);
            font28.Append(fontName28);
            font28.Append(fontFamilyNumbering28);

            var font29 = new Font();
            var italic4 = new Italic();
            var fontSize29 = new FontSize { Val = 8D };
            var color22 = new Color { Indexed = 8U };
            var fontName29 = new FontName { Val = "Arial" };
            var fontFamilyNumbering29 = new FontFamilyNumbering { Val = 2 };

            font29.Append(italic4);
            font29.Append(fontSize29);
            font29.Append(color22);
            font29.Append(fontName29);
            font29.Append(fontFamilyNumbering29);

            var font30 = new Font();
            var italic5 = new Italic();
            var fontSize30 = new FontSize { Val = 10D };
            var fontName30 = new FontName { Val = "Arial" };
            var fontFamilyNumbering30 = new FontFamilyNumbering { Val = 2 };

            font30.Append(italic5);
            font30.Append(fontSize30);
            font30.Append(fontName30);
            font30.Append(fontFamilyNumbering30);

            var font31 = new Font();
            var fontSize31 = new FontSize { Val = 10D };
            var fontName31 = new FontName { Val = "Arial" };
            var fontFamilyNumbering31 = new FontFamilyNumbering { Val = 2 };

            font31.Append(fontSize31);
            font31.Append(fontName31);
            font31.Append(fontFamilyNumbering31);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);
            fonts1.Append(font5);
            fonts1.Append(font6);
            fonts1.Append(font7);
            fonts1.Append(font8);
            fonts1.Append(font9);
            fonts1.Append(font10);
            fonts1.Append(font11);
            fonts1.Append(font12);
            fonts1.Append(font13);
            fonts1.Append(font14);
            fonts1.Append(font15);
            fonts1.Append(font16);
            fonts1.Append(font17);
            fonts1.Append(font18);
            fonts1.Append(font19);
            fonts1.Append(font20);
            fonts1.Append(font21);
            fonts1.Append(font22);
            fonts1.Append(font23);
            fonts1.Append(font24);
            fonts1.Append(font25);
            fonts1.Append(font26);
            fonts1.Append(font27);
            fonts1.Append(font28);
            fonts1.Append(font29);
            fonts1.Append(font30);
            fonts1.Append(font31);

            var fills1 = new Fills { Count = 24U };

            var fill1 = new Fill();
            var patternFill1 = new PatternFill { PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            var fill2 = new Fill();
            var patternFill2 = new PatternFill { PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            var fill3 = new Fill();

            var patternFill3 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor1 = new ForegroundColor { Indexed = 31U };
            var backgroundColor1 = new BackgroundColor { Indexed = 41U };

            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);

            fill3.Append(patternFill3);

            var fill4 = new Fill();

            var patternFill4 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor2 = new ForegroundColor { Indexed = 45U };
            var backgroundColor2 = new BackgroundColor { Indexed = 29U };

            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);

            fill4.Append(patternFill4);

            var fill5 = new Fill();

            var patternFill5 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor3 = new ForegroundColor { Indexed = 42U };
            var backgroundColor3 = new BackgroundColor { Indexed = 27U };

            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);

            fill5.Append(patternFill5);

            var fill6 = new Fill();

            var patternFill6 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor4 = new ForegroundColor { Indexed = 46U };
            var backgroundColor4 = new BackgroundColor { Indexed = 24U };

            patternFill6.Append(foregroundColor4);
            patternFill6.Append(backgroundColor4);

            fill6.Append(patternFill6);

            var fill7 = new Fill();

            var patternFill7 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor5 = new ForegroundColor { Indexed = 27U };
            var backgroundColor5 = new BackgroundColor { Indexed = 42U };

            patternFill7.Append(foregroundColor5);
            patternFill7.Append(backgroundColor5);

            fill7.Append(patternFill7);

            var fill8 = new Fill();

            var patternFill8 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor6 = new ForegroundColor { Indexed = 41U };
            var backgroundColor6 = new BackgroundColor { Indexed = 31U };

            patternFill8.Append(foregroundColor6);
            patternFill8.Append(backgroundColor6);

            fill8.Append(patternFill8);

            var fill9 = new Fill();

            var patternFill9 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor7 = new ForegroundColor { Indexed = 44U };
            var backgroundColor7 = new BackgroundColor { Indexed = 31U };

            patternFill9.Append(foregroundColor7);
            patternFill9.Append(backgroundColor7);

            fill9.Append(patternFill9);

            var fill10 = new Fill();

            var patternFill10 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor8 = new ForegroundColor { Indexed = 29U };
            var backgroundColor8 = new BackgroundColor { Indexed = 45U };

            patternFill10.Append(foregroundColor8);
            patternFill10.Append(backgroundColor8);

            fill10.Append(patternFill10);

            var fill11 = new Fill();

            var patternFill11 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor9 = new ForegroundColor { Indexed = 11U };
            var backgroundColor9 = new BackgroundColor { Indexed = 49U };

            patternFill11.Append(foregroundColor9);
            patternFill11.Append(backgroundColor9);

            fill11.Append(patternFill11);

            var fill12 = new Fill();

            var patternFill12 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor10 = new ForegroundColor { Indexed = 51U };
            var backgroundColor10 = new BackgroundColor { Indexed = 13U };

            patternFill12.Append(foregroundColor10);
            patternFill12.Append(backgroundColor10);

            fill12.Append(patternFill12);

            var fill13 = new Fill();

            var patternFill13 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor11 = new ForegroundColor { Indexed = 30U };
            var backgroundColor11 = new BackgroundColor { Indexed = 21U };

            patternFill13.Append(foregroundColor11);
            patternFill13.Append(backgroundColor11);

            fill13.Append(patternFill13);

            var fill14 = new Fill();

            var patternFill14 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor12 = new ForegroundColor { Indexed = 20U };
            var backgroundColor12 = new BackgroundColor { Indexed = 36U };

            patternFill14.Append(foregroundColor12);
            patternFill14.Append(backgroundColor12);

            fill14.Append(patternFill14);

            var fill15 = new Fill();

            var patternFill15 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor13 = new ForegroundColor { Indexed = 49U };
            var backgroundColor13 = new BackgroundColor { Indexed = 40U };

            patternFill15.Append(foregroundColor13);
            patternFill15.Append(backgroundColor13);

            fill15.Append(patternFill15);

            var fill16 = new Fill();

            var patternFill16 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor14 = new ForegroundColor { Indexed = 52U };
            var backgroundColor14 = new BackgroundColor { Indexed = 51U };

            patternFill16.Append(foregroundColor14);
            patternFill16.Append(backgroundColor14);

            fill16.Append(patternFill16);

            var fill17 = new Fill();

            var patternFill17 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor15 = new ForegroundColor { Indexed = 62U };
            var backgroundColor15 = new BackgroundColor { Indexed = 56U };

            patternFill17.Append(foregroundColor15);
            patternFill17.Append(backgroundColor15);

            fill17.Append(patternFill17);

            var fill18 = new Fill();

            var patternFill18 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor16 = new ForegroundColor { Indexed = 10U };
            var backgroundColor16 = new BackgroundColor { Indexed = 60U };

            patternFill18.Append(foregroundColor16);
            patternFill18.Append(backgroundColor16);

            fill18.Append(patternFill18);

            var fill19 = new Fill();

            var patternFill19 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor17 = new ForegroundColor { Indexed = 57U };
            var backgroundColor17 = new BackgroundColor { Indexed = 21U };

            patternFill19.Append(foregroundColor17);
            patternFill19.Append(backgroundColor17);

            fill19.Append(patternFill19);

            var fill20 = new Fill();

            var patternFill20 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor18 = new ForegroundColor { Indexed = 53U };
            var backgroundColor18 = new BackgroundColor { Indexed = 52U };

            patternFill20.Append(foregroundColor18);
            patternFill20.Append(backgroundColor18);

            fill20.Append(patternFill20);

            var fill21 = new Fill();

            var patternFill21 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor19 = new ForegroundColor { Indexed = 22U };
            var backgroundColor19 = new BackgroundColor { Indexed = 31U };

            patternFill21.Append(foregroundColor19);
            patternFill21.Append(backgroundColor19);

            fill21.Append(patternFill21);

            var fill22 = new Fill();

            var patternFill22 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor20 = new ForegroundColor { Indexed = 55U };
            var backgroundColor20 = new BackgroundColor { Indexed = 23U };

            patternFill22.Append(foregroundColor20);
            patternFill22.Append(backgroundColor20);

            fill22.Append(patternFill22);

            var fill23 = new Fill();

            var patternFill23 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor21 = new ForegroundColor { Indexed = 43U };
            var backgroundColor21 = new BackgroundColor { Indexed = 26U };

            patternFill23.Append(foregroundColor21);
            patternFill23.Append(backgroundColor21);

            fill23.Append(patternFill23);

            var fill24 = new Fill();

            var patternFill24 = new PatternFill { PatternType = PatternValues.Solid };
            var foregroundColor22 = new ForegroundColor { Indexed = 26U };
            var backgroundColor22 = new BackgroundColor { Indexed = 9U };

            patternFill24.Append(foregroundColor22);
            patternFill24.Append(backgroundColor22);

            fill24.Append(patternFill24);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);
            fills1.Append(fill6);
            fills1.Append(fill7);
            fills1.Append(fill8);
            fills1.Append(fill9);
            fills1.Append(fill10);
            fills1.Append(fill11);
            fills1.Append(fill12);
            fills1.Append(fill13);
            fills1.Append(fill14);
            fills1.Append(fill15);
            fills1.Append(fill16);
            fills1.Append(fill17);
            fills1.Append(fill18);
            fills1.Append(fill19);
            fills1.Append(fill20);
            fills1.Append(fill21);
            fills1.Append(fill22);
            fills1.Append(fill23);
            fills1.Append(fill24);

            var borders1 = new Borders { Count = 12U };

            var border1 = new Border();
            var leftBorder1 = new LeftBorder();
            var rightBorder1 = new RightBorder();
            var topBorder1 = new TopBorder();
            var bottomBorder1 = new BottomBorder();
            var diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            var border2 = new Border();

            var leftBorder2 = new LeftBorder { Style = BorderStyleValues.Thin };
            var color23 = new Color { Indexed = 23U };

            leftBorder2.Append(color23);

            var rightBorder2 = new RightBorder { Style = BorderStyleValues.Thin };
            var color24 = new Color { Indexed = 23U };

            rightBorder2.Append(color24);

            var topBorder2 = new TopBorder { Style = BorderStyleValues.Thin };
            var color25 = new Color { Indexed = 23U };

            topBorder2.Append(color25);

            var bottomBorder2 = new BottomBorder { Style = BorderStyleValues.Thin };
            var color26 = new Color { Indexed = 23U };

            bottomBorder2.Append(color26);
            var diagonalBorder2 = new DiagonalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(diagonalBorder2);

            var border3 = new Border();

            var leftBorder3 = new LeftBorder { Style = BorderStyleValues.Double };
            var color27 = new Color { Indexed = 63U };

            leftBorder3.Append(color27);

            var rightBorder3 = new RightBorder { Style = BorderStyleValues.Double };
            var color28 = new Color { Indexed = 63U };

            rightBorder3.Append(color28);

            var topBorder3 = new TopBorder { Style = BorderStyleValues.Double };
            var color29 = new Color { Indexed = 63U };

            topBorder3.Append(color29);

            var bottomBorder3 = new BottomBorder { Style = BorderStyleValues.Double };
            var color30 = new Color { Indexed = 63U };

            bottomBorder3.Append(color30);
            var diagonalBorder3 = new DiagonalBorder();

            border3.Append(leftBorder3);
            border3.Append(rightBorder3);
            border3.Append(topBorder3);
            border3.Append(bottomBorder3);
            border3.Append(diagonalBorder3);

            var border4 = new Border();
            var leftBorder4 = new LeftBorder();
            var rightBorder4 = new RightBorder();
            var topBorder4 = new TopBorder();

            var bottomBorder4 = new BottomBorder { Style = BorderStyleValues.Thick };
            var color31 = new Color { Indexed = 62U };

            bottomBorder4.Append(color31);
            var diagonalBorder4 = new DiagonalBorder();

            border4.Append(leftBorder4);
            border4.Append(rightBorder4);
            border4.Append(topBorder4);
            border4.Append(bottomBorder4);
            border4.Append(diagonalBorder4);

            var border5 = new Border();
            var leftBorder5 = new LeftBorder();
            var rightBorder5 = new RightBorder();
            var topBorder5 = new TopBorder();

            var bottomBorder5 = new BottomBorder { Style = BorderStyleValues.Thick };
            var color32 = new Color { Indexed = 22U };

            bottomBorder5.Append(color32);
            var diagonalBorder5 = new DiagonalBorder();

            border5.Append(leftBorder5);
            border5.Append(rightBorder5);
            border5.Append(topBorder5);
            border5.Append(bottomBorder5);
            border5.Append(diagonalBorder5);

            var border6 = new Border();
            var leftBorder6 = new LeftBorder();
            var rightBorder6 = new RightBorder();
            var topBorder6 = new TopBorder();

            var bottomBorder6 = new BottomBorder { Style = BorderStyleValues.Medium };
            var color33 = new Color { Indexed = 30U };

            bottomBorder6.Append(color33);
            var diagonalBorder6 = new DiagonalBorder();

            border6.Append(leftBorder6);
            border6.Append(rightBorder6);
            border6.Append(topBorder6);
            border6.Append(bottomBorder6);
            border6.Append(diagonalBorder6);

            var border7 = new Border();
            var leftBorder7 = new LeftBorder();
            var rightBorder7 = new RightBorder();
            var topBorder7 = new TopBorder();

            var bottomBorder7 = new BottomBorder { Style = BorderStyleValues.Double };
            var color34 = new Color { Indexed = 52U };

            bottomBorder7.Append(color34);
            var diagonalBorder7 = new DiagonalBorder();

            border7.Append(leftBorder7);
            border7.Append(rightBorder7);
            border7.Append(topBorder7);
            border7.Append(bottomBorder7);
            border7.Append(diagonalBorder7);

            var border8 = new Border();

            var leftBorder8 = new LeftBorder { Style = BorderStyleValues.Thin };
            var color35 = new Color { Indexed = 22U };

            leftBorder8.Append(color35);

            var rightBorder8 = new RightBorder { Style = BorderStyleValues.Thin };
            var color36 = new Color { Indexed = 22U };

            rightBorder8.Append(color36);

            var topBorder8 = new TopBorder { Style = BorderStyleValues.Thin };
            var color37 = new Color { Indexed = 22U };

            topBorder8.Append(color37);

            var bottomBorder8 = new BottomBorder { Style = BorderStyleValues.Thin };
            var color38 = new Color { Indexed = 22U };

            bottomBorder8.Append(color38);
            var diagonalBorder8 = new DiagonalBorder();

            border8.Append(leftBorder8);
            border8.Append(rightBorder8);
            border8.Append(topBorder8);
            border8.Append(bottomBorder8);
            border8.Append(diagonalBorder8);

            var border9 = new Border();

            var leftBorder9 = new LeftBorder { Style = BorderStyleValues.Thin };
            var color39 = new Color { Indexed = 63U };

            leftBorder9.Append(color39);

            var rightBorder9 = new RightBorder { Style = BorderStyleValues.Thin };
            var color40 = new Color { Indexed = 63U };

            rightBorder9.Append(color40);

            var topBorder9 = new TopBorder { Style = BorderStyleValues.Thin };
            var color41 = new Color { Indexed = 63U };

            topBorder9.Append(color41);

            var bottomBorder9 = new BottomBorder { Style = BorderStyleValues.Thin };
            var color42 = new Color { Indexed = 63U };

            bottomBorder9.Append(color42);
            var diagonalBorder9 = new DiagonalBorder();

            border9.Append(leftBorder9);
            border9.Append(rightBorder9);
            border9.Append(topBorder9);
            border9.Append(bottomBorder9);
            border9.Append(diagonalBorder9);

            var border10 = new Border();
            var leftBorder10 = new LeftBorder();
            var rightBorder10 = new RightBorder();

            var topBorder10 = new TopBorder { Style = BorderStyleValues.Thin };
            var color43 = new Color { Indexed = 62U };

            topBorder10.Append(color43);

            var bottomBorder10 = new BottomBorder { Style = BorderStyleValues.Double };
            var color44 = new Color { Indexed = 62U };

            bottomBorder10.Append(color44);
            var diagonalBorder10 = new DiagonalBorder();

            border10.Append(leftBorder10);
            border10.Append(rightBorder10);
            border10.Append(topBorder10);
            border10.Append(bottomBorder10);
            border10.Append(diagonalBorder10);

            var border11 = new Border();
            var leftBorder11 = new LeftBorder();
            var rightBorder11 = new RightBorder();
            var topBorder11 = new TopBorder();

            var bottomBorder11 = new BottomBorder { Style = BorderStyleValues.Thin };
            var color45 = new Color { Indexed = 8U };

            bottomBorder11.Append(color45);
            var diagonalBorder11 = new DiagonalBorder();

            border11.Append(leftBorder11);
            border11.Append(rightBorder11);
            border11.Append(topBorder11);
            border11.Append(bottomBorder11);
            border11.Append(diagonalBorder11);

            var border12 = new Border();
            var leftBorder12 = new LeftBorder();
            var rightBorder12 = new RightBorder();
            var topBorder12 = new TopBorder();

            var bottomBorder12 = new BottomBorder { Style = BorderStyleValues.Medium };
            var color46 = new Color { Indexed = 8U };

            bottomBorder12.Append(color46);
            var diagonalBorder12 = new DiagonalBorder();

            border12.Append(leftBorder12);
            border12.Append(rightBorder12);
            border12.Append(topBorder12);
            border12.Append(bottomBorder12);
            border12.Append(diagonalBorder12);

            borders1.Append(border1);
            borders1.Append(border2);
            borders1.Append(border3);
            borders1.Append(border4);
            borders1.Append(border5);
            borders1.Append(border6);
            borders1.Append(border7);
            borders1.Append(border8);
            borders1.Append(border9);
            borders1.Append(border10);
            borders1.Append(border11);
            borders1.Append(border12);

            var cellStyleFormats1 = new CellStyleFormats { Count = 54U };
            var cellFormat1 = new CellFormat { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U };
            var cellFormat2 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 2U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat3 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 3U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat4 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 4U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat5 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 5U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat6 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 6U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat7 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 7U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat8 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 8U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat9 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 9U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat10 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 10U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat11 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 5U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat12 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 8U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat13 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 11U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat14 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 12U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat15 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 9U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat16 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 10U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat17 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 13U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat18 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 14U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat19 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 15U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat20 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 16U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat21 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 17U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat22 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 18U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat23 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 13U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat24 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 14U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat25 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 2U,
                FillId = 19U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat26 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 3U,
                FillId = 3U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat27 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 4U,
                FillId = 20U,
                BorderId = 1U,
                ApplyNumberFormat = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat28 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 5U,
                FillId = 21U,
                BorderId = 2U,
                ApplyNumberFormat = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat29 = new CellFormat
            {
                NumberFormatId = 164U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat30 = new CellFormat
            {
                NumberFormatId = 165U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat31 = new CellFormat
            {
                NumberFormatId = 165U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat32 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 6U,
                FillId = 0U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat33 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 7U,
                FillId = 4U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat34 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 8U,
                FillId = 0U,
                BorderId = 3U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat35 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 9U,
                FillId = 0U,
                BorderId = 4U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat36 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 10U,
                FillId = 0U,
                BorderId = 5U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat37 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 10U,
                FillId = 0U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat38 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 11U,
                FillId = 7U,
                BorderId = 1U,
                ApplyNumberFormat = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat39 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 12U,
                FillId = 0U,
                BorderId = 6U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat40 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 13U,
                FillId = 22U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat41 = new CellFormat { NumberFormatId = 0U, FontId = 30U, FillId = 0U, BorderId = 0U };
            var cellFormat42 = new CellFormat { NumberFormatId = 0U, FontId = 30U, FillId = 0U, BorderId = 0U };
            var cellFormat43 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 30U,
                FillId = 23U,
                BorderId = 7U,
                ApplyNumberFormat = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat44 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 14U,
                FillId = 20U,
                BorderId = 8U,
                ApplyNumberFormat = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat45 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat46 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat47 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat48 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat49 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat50 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat51 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 30U,
                FillId = 0U,
                BorderId = 0U,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat52 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 15U,
                FillId = 0U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat53 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 16U,
                FillId = 0U,
                BorderId = 9U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };
            var cellFormat54 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 17U,
                FillId = 0U,
                BorderId = 0U,
                ApplyNumberFormat = false,
                ApplyFill = false,
                ApplyBorder = false,
                ApplyAlignment = false,
                ApplyProtection = false
            };

            cellStyleFormats1.Append(cellFormat1);
            cellStyleFormats1.Append(cellFormat2);
            cellStyleFormats1.Append(cellFormat3);
            cellStyleFormats1.Append(cellFormat4);
            cellStyleFormats1.Append(cellFormat5);
            cellStyleFormats1.Append(cellFormat6);
            cellStyleFormats1.Append(cellFormat7);
            cellStyleFormats1.Append(cellFormat8);
            cellStyleFormats1.Append(cellFormat9);
            cellStyleFormats1.Append(cellFormat10);
            cellStyleFormats1.Append(cellFormat11);
            cellStyleFormats1.Append(cellFormat12);
            cellStyleFormats1.Append(cellFormat13);
            cellStyleFormats1.Append(cellFormat14);
            cellStyleFormats1.Append(cellFormat15);
            cellStyleFormats1.Append(cellFormat16);
            cellStyleFormats1.Append(cellFormat17);
            cellStyleFormats1.Append(cellFormat18);
            cellStyleFormats1.Append(cellFormat19);
            cellStyleFormats1.Append(cellFormat20);
            cellStyleFormats1.Append(cellFormat21);
            cellStyleFormats1.Append(cellFormat22);
            cellStyleFormats1.Append(cellFormat23);
            cellStyleFormats1.Append(cellFormat24);
            cellStyleFormats1.Append(cellFormat25);
            cellStyleFormats1.Append(cellFormat26);
            cellStyleFormats1.Append(cellFormat27);
            cellStyleFormats1.Append(cellFormat28);
            cellStyleFormats1.Append(cellFormat29);
            cellStyleFormats1.Append(cellFormat30);
            cellStyleFormats1.Append(cellFormat31);
            cellStyleFormats1.Append(cellFormat32);
            cellStyleFormats1.Append(cellFormat33);
            cellStyleFormats1.Append(cellFormat34);
            cellStyleFormats1.Append(cellFormat35);
            cellStyleFormats1.Append(cellFormat36);
            cellStyleFormats1.Append(cellFormat37);
            cellStyleFormats1.Append(cellFormat38);
            cellStyleFormats1.Append(cellFormat39);
            cellStyleFormats1.Append(cellFormat40);
            cellStyleFormats1.Append(cellFormat41);
            cellStyleFormats1.Append(cellFormat42);
            cellStyleFormats1.Append(cellFormat43);
            cellStyleFormats1.Append(cellFormat44);
            cellStyleFormats1.Append(cellFormat45);
            cellStyleFormats1.Append(cellFormat46);
            cellStyleFormats1.Append(cellFormat47);
            cellStyleFormats1.Append(cellFormat48);
            cellStyleFormats1.Append(cellFormat49);
            cellStyleFormats1.Append(cellFormat50);
            cellStyleFormats1.Append(cellFormat51);
            cellStyleFormats1.Append(cellFormat52);
            cellStyleFormats1.Append(cellFormat53);
            cellStyleFormats1.Append(cellFormat54);

            var cellFormats1 = new CellFormats { Count = 87U };
            var cellFormat55 = new CellFormat { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U, FormatId = 0U };

            var cellFormat56 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyAlignment = true
            };
            var alignment1 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat56.Append(alignment1);
            var cellFormat57 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyBorder = true
            };
            var cellFormat58 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyBorder = true
            };

            var cellFormat59 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment2 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat59.Append(alignment2);
            var cellFormat60 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormat61 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment3 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat61.Append(alignment3);
            var cellFormat62 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormat63 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment4 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat63.Append(alignment4);

            var cellFormat64 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment5 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat64.Append(alignment5);

            var cellFormat65 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment6 = new Alignment { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat65.Append(alignment6);
            var cellFormat66 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyFill = true,
                ApplyBorder = true
            };

            var cellFormat67 = new CellFormat
            {
                NumberFormatId = 3U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment7 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat67.Append(alignment7);

            var cellFormat68 = new CellFormat
            {
                NumberFormatId = 2U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment8 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat68.Append(alignment8);

            var cellFormat69 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment9 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat69.Append(alignment9);

            var cellFormat70 = new CellFormat
            {
                NumberFormatId = 166U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment10 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat70.Append(alignment10);

            var cellFormat71 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 19U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment11 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat71.Append(alignment11);

            var cellFormat72 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment12 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat72.Append(alignment12);

            var cellFormat73 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment13 = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Top,
                WrapText = true
            };

            cellFormat73.Append(alignment13);
            var cellFormat74 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormat75 = new CellFormat
            {
                NumberFormatId = 168U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment14 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat75.Append(alignment14);

            var cellFormat76 = new CellFormat
            {
                NumberFormatId = 169U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment15 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat76.Append(alignment15);

            var cellFormat77 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment16 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat77.Append(alignment16);

            var cellFormat78 = new CellFormat
            {
                NumberFormatId = 170U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 44U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyFill = true,
                ApplyBorder = true,
                ApplyAlignment = true,
                ApplyProtection = true
            };
            var alignment17 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat78.Append(alignment17);
            var cellFormat79 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 22U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true
            };

            var cellFormat80 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 22U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment18 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat80.Append(alignment18);

            var cellFormat81 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment19 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat81.Append(alignment19);
            var cellFormat82 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyFill = true,
                ApplyBorder = true
            };

            var cellFormat83 = new CellFormat
            {
                NumberFormatId = 3U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment20 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat83.Append(alignment20);

            var cellFormat84 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment21 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat84.Append(alignment21);

            var cellFormat85 = new CellFormat
            {
                NumberFormatId = 166U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment22 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat85.Append(alignment22);

            var cellFormat86 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 24U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment23 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat86.Append(alignment23);
            var cellFormat87 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true
            };

            var cellFormat88 = new CellFormat
            {
                NumberFormatId = 2U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment24 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat88.Append(alignment24);

            var cellFormat89 = new CellFormat
            {
                NumberFormatId = 168U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment25 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat89.Append(alignment25);

            var cellFormat90 = new CellFormat
            {
                NumberFormatId = 169U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment26 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat90.Append(alignment26);

            var cellFormat91 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment27 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat91.Append(alignment27);

            var cellFormat92 = new CellFormat
            {
                NumberFormatId = 170U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 46U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyFill = true,
                ApplyBorder = true,
                ApplyAlignment = true,
                ApplyProtection = true
            };
            var alignment28 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat92.Append(alignment28);

            var cellFormat93 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 25U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment29 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat93.Append(alignment29);
            var cellFormat94 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 22U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var cellFormat95 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 19U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormat96 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 40U,
                ApplyFont = true,
                ApplyAlignment = true
            };
            var alignment30 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat96.Append(alignment30);

            var cellFormat97 = new CellFormat
            {
                NumberFormatId = 3U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment31 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat97.Append(alignment31);

            var cellFormat98 = new CellFormat
            {
                NumberFormatId = 9U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment32 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat98.Append(alignment32);

            var cellFormat99 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment33 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat99.Append(alignment33);

            var cellFormat100 = new CellFormat
            {
                NumberFormatId = 4U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment34 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat100.Append(alignment34);

            var cellFormat101 = new CellFormat
            {
                NumberFormatId = 171U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment35 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat101.Append(alignment35);

            var cellFormat102 = new CellFormat
            {
                NumberFormatId = 17U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment36 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat102.Append(alignment36);
            var cellFormat103 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 22U,
                FillId = 0U,
                BorderId = 11U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var cellFormat104 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 11U,
                FormatId = 0U,
                ApplyBorder = true
            };
            var cellFormat105 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 19U,
                FillId = 0U,
                BorderId = 11U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormat106 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 11U,
                FormatId = 0U,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment37 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat106.Append(alignment37);

            var cellFormat107 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 11U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment38 = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Top,
                WrapText = true
            };

            cellFormat107.Append(alignment38);

            var cellFormat108 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment39 = new Alignment { Vertical = VerticalAlignmentValues.Top, WrapText = true };

            cellFormat108.Append(alignment39);
            var cellFormat109 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 27U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormat110 = new CellFormat
            {
                NumberFormatId = 2U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment40 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat110.Append(alignment40);

            var cellFormat111 = new CellFormat
            {
                NumberFormatId = 166U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment41 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat111.Append(alignment41);
            var cellFormat112 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 27U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true
            };

            var cellFormat113 = new CellFormat
            {
                NumberFormatId = 169U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment42 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat113.Append(alignment42);

            var cellFormat114 = new CellFormat
            {
                NumberFormatId = 170U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 45U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyFill = true,
                ApplyBorder = true,
                ApplyAlignment = true,
                ApplyProtection = true
            };
            var alignment43 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat114.Append(alignment43);

            var cellFormat115 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 26U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment44 = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Top
            };

            cellFormat115.Append(alignment44);
            var cellFormat116 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };
            var cellFormat117 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 28U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };

            var cellFormat118 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment45 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat118.Append(alignment45);

            var cellFormat119 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 24U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment46 = new Alignment { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat119.Append(alignment46);

            var cellFormat120 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyFill = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment47 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat120.Append(alignment47);

            var cellFormat121 = new CellFormat
            {
                NumberFormatId = 170U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 47U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyFill = true,
                ApplyBorder = true,
                ApplyAlignment = true,
                ApplyProtection = true
            };
            var alignment48 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat121.Append(alignment48);

            var cellFormat122 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 24U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment49 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat122.Append(alignment49);

            var cellFormat123 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 24U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment50 = new Alignment { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat123.Append(alignment50);
            var cellFormat124 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 28U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };

            var cellFormat125 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment51 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat125.Append(alignment51);

            var cellFormat126 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 25U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment52 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat126.Append(alignment52);

            var cellFormat127 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 23U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment53 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat127.Append(alignment53);

            var cellFormat128 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment54 = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Top,
                WrapText = true
            };

            cellFormat128.Append(alignment54);

            var cellFormat129 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 29U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment55 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat129.Append(alignment55);

            var cellFormat130 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 28U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment56 = new Alignment { Horizontal = HorizontalAlignmentValues.Left };

            cellFormat130.Append(alignment56);
            var cellFormat131 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 18U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormat132 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyAlignment = true
            };
            var alignment57 = new Alignment { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat132.Append(alignment57);

            var cellFormat133 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment58 = new Alignment { Vertical = VerticalAlignmentValues.Top, WrapText = true };

            cellFormat133.Append(alignment58);

            var cellFormat134 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyAlignment = true
            };
            var alignment59 = new Alignment { Horizontal = HorizontalAlignmentValues.Right };

            cellFormat134.Append(alignment59);

            var cellFormat135 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment60 = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Top,
                WrapText = true
            };

            cellFormat135.Append(alignment60);

            var cellFormat136 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 26U,
                FillId = 0U,
                BorderId = 11U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment61 = new Alignment { Horizontal = HorizontalAlignmentValues.Center };

            cellFormat136.Append(alignment61);

            var cellFormat137 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment62 = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Left,
                Vertical = VerticalAlignmentValues.Top,
                WrapText = true
            };

            cellFormat137.Append(alignment62);

            var cellFormat138 = new CellFormat
            {
                NumberFormatId = 167U,
                FontId = 21U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyNumberFormat = true,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment63 = new Alignment { Vertical = VerticalAlignmentValues.Top, WrapText = true };

            cellFormat138.Append(alignment63);

            var cellFormat139 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 18U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment64 = new Alignment { Horizontal = HorizontalAlignmentValues.Center, WrapText = true };

            cellFormat139.Append(alignment64);

            var cellFormat140 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 19U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };
            var alignment65 = new Alignment
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };

            cellFormat140.Append(alignment65);
            var cellFormat141 = new CellFormat
            {
                NumberFormatId = 0U,
                FontId = 20U,
                FillId = 0U,
                BorderId = 10U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true,
                ApplyAlignment = true
            };

            cellFormats1.Append(cellFormat55);
            cellFormats1.Append(cellFormat56);
            cellFormats1.Append(cellFormat57);
            cellFormats1.Append(cellFormat58);
            cellFormats1.Append(cellFormat59);
            cellFormats1.Append(cellFormat60);
            cellFormats1.Append(cellFormat61);
            cellFormats1.Append(cellFormat62);
            cellFormats1.Append(cellFormat63);
            cellFormats1.Append(cellFormat64);
            cellFormats1.Append(cellFormat65);
            cellFormats1.Append(cellFormat66);
            cellFormats1.Append(cellFormat67);
            cellFormats1.Append(cellFormat68);
            cellFormats1.Append(cellFormat69);
            cellFormats1.Append(cellFormat70);
            cellFormats1.Append(cellFormat71);
            cellFormats1.Append(cellFormat72);
            cellFormats1.Append(cellFormat73);
            cellFormats1.Append(cellFormat74);
            cellFormats1.Append(cellFormat75);
            cellFormats1.Append(cellFormat76);
            cellFormats1.Append(cellFormat77);
            cellFormats1.Append(cellFormat78);
            cellFormats1.Append(cellFormat79);
            cellFormats1.Append(cellFormat80);
            cellFormats1.Append(cellFormat81);
            cellFormats1.Append(cellFormat82);
            cellFormats1.Append(cellFormat83);
            cellFormats1.Append(cellFormat84);
            cellFormats1.Append(cellFormat85);
            cellFormats1.Append(cellFormat86);
            cellFormats1.Append(cellFormat87);
            cellFormats1.Append(cellFormat88);
            cellFormats1.Append(cellFormat89);
            cellFormats1.Append(cellFormat90);
            cellFormats1.Append(cellFormat91);
            cellFormats1.Append(cellFormat92);
            cellFormats1.Append(cellFormat93);
            cellFormats1.Append(cellFormat94);
            cellFormats1.Append(cellFormat95);
            cellFormats1.Append(cellFormat96);
            cellFormats1.Append(cellFormat97);
            cellFormats1.Append(cellFormat98);
            cellFormats1.Append(cellFormat99);
            cellFormats1.Append(cellFormat100);
            cellFormats1.Append(cellFormat101);
            cellFormats1.Append(cellFormat102);
            cellFormats1.Append(cellFormat103);
            cellFormats1.Append(cellFormat104);
            cellFormats1.Append(cellFormat105);
            cellFormats1.Append(cellFormat106);
            cellFormats1.Append(cellFormat107);
            cellFormats1.Append(cellFormat108);
            cellFormats1.Append(cellFormat109);
            cellFormats1.Append(cellFormat110);
            cellFormats1.Append(cellFormat111);
            cellFormats1.Append(cellFormat112);
            cellFormats1.Append(cellFormat113);
            cellFormats1.Append(cellFormat114);
            cellFormats1.Append(cellFormat115);
            cellFormats1.Append(cellFormat116);
            cellFormats1.Append(cellFormat117);
            cellFormats1.Append(cellFormat118);
            cellFormats1.Append(cellFormat119);
            cellFormats1.Append(cellFormat120);
            cellFormats1.Append(cellFormat121);
            cellFormats1.Append(cellFormat122);
            cellFormats1.Append(cellFormat123);
            cellFormats1.Append(cellFormat124);
            cellFormats1.Append(cellFormat125);
            cellFormats1.Append(cellFormat126);
            cellFormats1.Append(cellFormat127);
            cellFormats1.Append(cellFormat128);
            cellFormats1.Append(cellFormat129);
            cellFormats1.Append(cellFormat130);
            cellFormats1.Append(cellFormat131);
            cellFormats1.Append(cellFormat132);
            cellFormats1.Append(cellFormat133);
            cellFormats1.Append(cellFormat134);
            cellFormats1.Append(cellFormat135);
            cellFormats1.Append(cellFormat136);
            cellFormats1.Append(cellFormat137);
            cellFormats1.Append(cellFormat138);
            cellFormats1.Append(cellFormat139);
            cellFormats1.Append(cellFormat140);
            cellFormats1.Append(cellFormat141);

            var cellStyles1 = new CellStyles { Count = 54U };
            var cellStyle1 = new CellStyle { Name = "20% - Accent1", FormatId = 1U, BuiltinId = 30U, CustomBuiltin = true };
            var cellStyle2 = new CellStyle { Name = "20% - Accent2", FormatId = 2U, BuiltinId = 34U, CustomBuiltin = true };
            var cellStyle3 = new CellStyle { Name = "20% - Accent3", FormatId = 3U, BuiltinId = 38U, CustomBuiltin = true };
            var cellStyle4 = new CellStyle { Name = "20% - Accent4", FormatId = 4U, BuiltinId = 42U, CustomBuiltin = true };
            var cellStyle5 = new CellStyle { Name = "20% - Accent5", FormatId = 5U, BuiltinId = 46U, CustomBuiltin = true };
            var cellStyle6 = new CellStyle { Name = "20% - Accent6", FormatId = 6U, BuiltinId = 50U, CustomBuiltin = true };
            var cellStyle7 = new CellStyle { Name = "40% - Accent1", FormatId = 7U, BuiltinId = 31U, CustomBuiltin = true };
            var cellStyle8 = new CellStyle { Name = "40% - Accent2", FormatId = 8U, BuiltinId = 35U, CustomBuiltin = true };
            var cellStyle9 = new CellStyle { Name = "40% - Accent3", FormatId = 9U, BuiltinId = 39U, CustomBuiltin = true };
            var cellStyle10 = new CellStyle { Name = "40% - Accent4", FormatId = 10U, BuiltinId = 43U, CustomBuiltin = true };
            var cellStyle11 = new CellStyle { Name = "40% - Accent5", FormatId = 11U, BuiltinId = 47U, CustomBuiltin = true };
            var cellStyle12 = new CellStyle { Name = "40% - Accent6", FormatId = 12U, BuiltinId = 51U, CustomBuiltin = true };
            var cellStyle13 = new CellStyle { Name = "60% - Accent1", FormatId = 13U, BuiltinId = 32U, CustomBuiltin = true };
            var cellStyle14 = new CellStyle { Name = "60% - Accent2", FormatId = 14U, BuiltinId = 36U, CustomBuiltin = true };
            var cellStyle15 = new CellStyle { Name = "60% - Accent3", FormatId = 15U, BuiltinId = 40U, CustomBuiltin = true };
            var cellStyle16 = new CellStyle { Name = "60% - Accent4", FormatId = 16U, BuiltinId = 44U, CustomBuiltin = true };
            var cellStyle17 = new CellStyle { Name = "60% - Accent5", FormatId = 17U, BuiltinId = 48U, CustomBuiltin = true };
            var cellStyle18 = new CellStyle { Name = "60% - Accent6", FormatId = 18U, BuiltinId = 52U, CustomBuiltin = true };
            var cellStyle19 = new CellStyle { Name = "Accent1", FormatId = 19U, BuiltinId = 29U, CustomBuiltin = true };
            var cellStyle20 = new CellStyle { Name = "Accent2", FormatId = 20U, BuiltinId = 33U, CustomBuiltin = true };
            var cellStyle21 = new CellStyle { Name = "Accent3", FormatId = 21U, BuiltinId = 37U, CustomBuiltin = true };
            var cellStyle22 = new CellStyle { Name = "Accent4", FormatId = 22U, BuiltinId = 41U, CustomBuiltin = true };
            var cellStyle23 = new CellStyle { Name = "Accent5", FormatId = 23U, BuiltinId = 45U, CustomBuiltin = true };
            var cellStyle24 = new CellStyle { Name = "Accent6", FormatId = 24U, BuiltinId = 49U, CustomBuiltin = true };
            var cellStyle25 = new CellStyle { Name = "Bad", FormatId = 25U, BuiltinId = 27U, CustomBuiltin = true };
            var cellStyle26 = new CellStyle { Name = "Calculation", FormatId = 26U, BuiltinId = 22U, CustomBuiltin = true };
            var cellStyle27 = new CellStyle { Name = "Check Cell", FormatId = 27U, BuiltinId = 23U, CustomBuiltin = true };
            var cellStyle28 = new CellStyle { Name = "Comma 2", FormatId = 28U };
            var cellStyle29 = new CellStyle { Name = "Currency 2", FormatId = 29U };
            var cellStyle30 = new CellStyle { Name = "Currency 2 2", FormatId = 30U };
            var cellStyle31 = new CellStyle { Name = "Explanatory Text", FormatId = 31U, BuiltinId = 53U, CustomBuiltin = true };
            var cellStyle32 = new CellStyle { Name = "Good", FormatId = 32U, BuiltinId = 26U, CustomBuiltin = true };
            var cellStyle33 = new CellStyle { Name = "Heading 1", FormatId = 33U, BuiltinId = 16U, CustomBuiltin = true };
            var cellStyle34 = new CellStyle { Name = "Heading 2", FormatId = 34U, BuiltinId = 17U, CustomBuiltin = true };
            var cellStyle35 = new CellStyle { Name = "Heading 3", FormatId = 35U, BuiltinId = 18U, CustomBuiltin = true };
            var cellStyle36 = new CellStyle { Name = "Heading 4", FormatId = 36U, BuiltinId = 19U, CustomBuiltin = true };
            var cellStyle37 = new CellStyle { Name = "Input", FormatId = 37U, BuiltinId = 20U, CustomBuiltin = true };
            var cellStyle38 = new CellStyle { Name = "Linked Cell", FormatId = 38U, BuiltinId = 24U, CustomBuiltin = true };
            var cellStyle39 = new CellStyle { Name = "Neutral", FormatId = 39U, BuiltinId = 28U, CustomBuiltin = true };
            var cellStyle40 = new CellStyle { Name = "Normal", FormatId = 0U, BuiltinId = 0U };
            var cellStyle41 = new CellStyle { Name = "Normal 2", FormatId = 40U };
            var cellStyle42 = new CellStyle { Name = "Normal 3", FormatId = 41U };
            var cellStyle43 = new CellStyle { Name = "Note", FormatId = 42U, BuiltinId = 10U, CustomBuiltin = true };
            var cellStyle44 = new CellStyle { Name = "Output", FormatId = 43U, BuiltinId = 21U, CustomBuiltin = true };
            var cellStyle45 = new CellStyle { Name = "Percent", FormatId = 44U, BuiltinId = 5U };
            var cellStyle46 = new CellStyle { Name = "Percent 2", FormatId = 45U };
            var cellStyle47 = new CellStyle { Name = "Percent 2 2", FormatId = 46U };
            var cellStyle48 = new CellStyle { Name = "Percent 2 3", FormatId = 47U };
            var cellStyle49 = new CellStyle { Name = "Percent 3", FormatId = 48U };
            var cellStyle50 = new CellStyle { Name = "Percent 4", FormatId = 49U };
            var cellStyle51 = new CellStyle { Name = "Percent 6", FormatId = 50U };
            var cellStyle52 = new CellStyle { Name = "Title", FormatId = 51U, BuiltinId = 15U, CustomBuiltin = true };
            var cellStyle53 = new CellStyle { Name = "Total", FormatId = 52U, BuiltinId = 25U, CustomBuiltin = true };
            var cellStyle54 = new CellStyle { Name = "Warning Text", FormatId = 53U, BuiltinId = 11U, CustomBuiltin = true };

            cellStyles1.Append(cellStyle1);
            cellStyles1.Append(cellStyle2);
            cellStyles1.Append(cellStyle3);
            cellStyles1.Append(cellStyle4);
            cellStyles1.Append(cellStyle5);
            cellStyles1.Append(cellStyle6);
            cellStyles1.Append(cellStyle7);
            cellStyles1.Append(cellStyle8);
            cellStyles1.Append(cellStyle9);
            cellStyles1.Append(cellStyle10);
            cellStyles1.Append(cellStyle11);
            cellStyles1.Append(cellStyle12);
            cellStyles1.Append(cellStyle13);
            cellStyles1.Append(cellStyle14);
            cellStyles1.Append(cellStyle15);
            cellStyles1.Append(cellStyle16);
            cellStyles1.Append(cellStyle17);
            cellStyles1.Append(cellStyle18);
            cellStyles1.Append(cellStyle19);
            cellStyles1.Append(cellStyle20);
            cellStyles1.Append(cellStyle21);
            cellStyles1.Append(cellStyle22);
            cellStyles1.Append(cellStyle23);
            cellStyles1.Append(cellStyle24);
            cellStyles1.Append(cellStyle25);
            cellStyles1.Append(cellStyle26);
            cellStyles1.Append(cellStyle27);
            cellStyles1.Append(cellStyle28);
            cellStyles1.Append(cellStyle29);
            cellStyles1.Append(cellStyle30);
            cellStyles1.Append(cellStyle31);
            cellStyles1.Append(cellStyle32);
            cellStyles1.Append(cellStyle33);
            cellStyles1.Append(cellStyle34);
            cellStyles1.Append(cellStyle35);
            cellStyles1.Append(cellStyle36);
            cellStyles1.Append(cellStyle37);
            cellStyles1.Append(cellStyle38);
            cellStyles1.Append(cellStyle39);
            cellStyles1.Append(cellStyle40);
            cellStyles1.Append(cellStyle41);
            cellStyles1.Append(cellStyle42);
            cellStyles1.Append(cellStyle43);
            cellStyles1.Append(cellStyle44);
            cellStyles1.Append(cellStyle45);
            cellStyles1.Append(cellStyle46);
            cellStyles1.Append(cellStyle47);
            cellStyles1.Append(cellStyle48);
            cellStyles1.Append(cellStyle49);
            cellStyles1.Append(cellStyle50);
            cellStyles1.Append(cellStyle51);
            cellStyles1.Append(cellStyle52);
            cellStyles1.Append(cellStyle53);
            cellStyles1.Append(cellStyle54);
            var differentialFormats1 = new DifferentialFormats { Count = 0U };
            var tableStyles1 = new TableStyles
            {
                Count = 0U,
                DefaultTableStyle = "TableStyleMedium2",
                DefaultPivotStyle = "PivotStyleLight16"
            };

            var colors1 = new Colors();

            var indexedColors1 = new IndexedColors();
            var rgbColor1 = new RgbColor { Rgb = "00000000" };
            var rgbColor2 = new RgbColor { Rgb = "00FFFFFF" };
            var rgbColor3 = new RgbColor { Rgb = "00FF0000" };
            var rgbColor4 = new RgbColor { Rgb = "0000FF00" };
            var rgbColor5 = new RgbColor { Rgb = "000000FF" };
            var rgbColor6 = new RgbColor { Rgb = "00FFFF00" };
            var rgbColor7 = new RgbColor { Rgb = "00FF00FF" };
            var rgbColor8 = new RgbColor { Rgb = "0000FFFF" };
            var rgbColor9 = new RgbColor { Rgb = "00000000" };
            var rgbColor10 = new RgbColor { Rgb = "00FFFFFF" };
            var rgbColor11 = new RgbColor { Rgb = "00FF0000" };
            var rgbColor12 = new RgbColor { Rgb = "0000FF00" };
            var rgbColor13 = new RgbColor { Rgb = "000000FF" };
            var rgbColor14 = new RgbColor { Rgb = "00FFFF00" };
            var rgbColor15 = new RgbColor { Rgb = "00FF00FF" };
            var rgbColor16 = new RgbColor { Rgb = "0000FFFF" };
            var rgbColor17 = new RgbColor { Rgb = "00800000" };
            var rgbColor18 = new RgbColor { Rgb = "00008000" };
            var rgbColor19 = new RgbColor { Rgb = "00000080" };
            var rgbColor20 = new RgbColor { Rgb = "00808000" };
            var rgbColor21 = new RgbColor { Rgb = "00800080" };
            var rgbColor22 = new RgbColor { Rgb = "00008080" };
            var rgbColor23 = new RgbColor { Rgb = "00C0C0C0" };
            var rgbColor24 = new RgbColor { Rgb = "00808080" };
            var rgbColor25 = new RgbColor { Rgb = "009999FF" };
            var rgbColor26 = new RgbColor { Rgb = "00993366" };
            var rgbColor27 = new RgbColor { Rgb = "00FFFFCC" };
            var rgbColor28 = new RgbColor { Rgb = "00CCFFFF" };
            var rgbColor29 = new RgbColor { Rgb = "00660066" };
            var rgbColor30 = new RgbColor { Rgb = "00FF8080" };
            var rgbColor31 = new RgbColor { Rgb = "000066CC" };
            var rgbColor32 = new RgbColor { Rgb = "00CCCCFF" };
            var rgbColor33 = new RgbColor { Rgb = "00000080" };
            var rgbColor34 = new RgbColor { Rgb = "00FF00FF" };
            var rgbColor35 = new RgbColor { Rgb = "00FFFF00" };
            var rgbColor36 = new RgbColor { Rgb = "0000FFFF" };
            var rgbColor37 = new RgbColor { Rgb = "00800080" };
            var rgbColor38 = new RgbColor { Rgb = "00800000" };
            var rgbColor39 = new RgbColor { Rgb = "00008080" };
            var rgbColor40 = new RgbColor { Rgb = "000000FF" };
            var rgbColor41 = new RgbColor { Rgb = "0000CCFF" };
            var rgbColor42 = new RgbColor { Rgb = "00E3E3E3" };
            var rgbColor43 = new RgbColor { Rgb = "00CCFFCC" };
            var rgbColor44 = new RgbColor { Rgb = "00FFFF99" };
            var rgbColor45 = new RgbColor { Rgb = "0099CCFF" };
            var rgbColor46 = new RgbColor { Rgb = "00FF99CC" };
            var rgbColor47 = new RgbColor { Rgb = "00CC99FF" };
            var rgbColor48 = new RgbColor { Rgb = "00FFCC99" };
            var rgbColor49 = new RgbColor { Rgb = "003366FF" };
            var rgbColor50 = new RgbColor { Rgb = "0033CCCC" };
            var rgbColor51 = new RgbColor { Rgb = "0099CC00" };
            var rgbColor52 = new RgbColor { Rgb = "00FFCC00" };
            var rgbColor53 = new RgbColor { Rgb = "00FF9900" };
            var rgbColor54 = new RgbColor { Rgb = "00FF6600" };
            var rgbColor55 = new RgbColor { Rgb = "00666699" };
            var rgbColor56 = new RgbColor { Rgb = "00969696" };
            var rgbColor57 = new RgbColor { Rgb = "00003366" };
            var rgbColor58 = new RgbColor { Rgb = "00339966" };
            var rgbColor59 = new RgbColor { Rgb = "00003300" };
            var rgbColor60 = new RgbColor { Rgb = "00333300" };
            var rgbColor61 = new RgbColor { Rgb = "00993300" };
            var rgbColor62 = new RgbColor { Rgb = "00993366" };
            var rgbColor63 = new RgbColor { Rgb = "00333399" };
            var rgbColor64 = new RgbColor { Rgb = "00333333" };

            indexedColors1.Append(rgbColor1);
            indexedColors1.Append(rgbColor2);
            indexedColors1.Append(rgbColor3);
            indexedColors1.Append(rgbColor4);
            indexedColors1.Append(rgbColor5);
            indexedColors1.Append(rgbColor6);
            indexedColors1.Append(rgbColor7);
            indexedColors1.Append(rgbColor8);
            indexedColors1.Append(rgbColor9);
            indexedColors1.Append(rgbColor10);
            indexedColors1.Append(rgbColor11);
            indexedColors1.Append(rgbColor12);
            indexedColors1.Append(rgbColor13);
            indexedColors1.Append(rgbColor14);
            indexedColors1.Append(rgbColor15);
            indexedColors1.Append(rgbColor16);
            indexedColors1.Append(rgbColor17);
            indexedColors1.Append(rgbColor18);
            indexedColors1.Append(rgbColor19);
            indexedColors1.Append(rgbColor20);
            indexedColors1.Append(rgbColor21);
            indexedColors1.Append(rgbColor22);
            indexedColors1.Append(rgbColor23);
            indexedColors1.Append(rgbColor24);
            indexedColors1.Append(rgbColor25);
            indexedColors1.Append(rgbColor26);
            indexedColors1.Append(rgbColor27);
            indexedColors1.Append(rgbColor28);
            indexedColors1.Append(rgbColor29);
            indexedColors1.Append(rgbColor30);
            indexedColors1.Append(rgbColor31);
            indexedColors1.Append(rgbColor32);
            indexedColors1.Append(rgbColor33);
            indexedColors1.Append(rgbColor34);
            indexedColors1.Append(rgbColor35);
            indexedColors1.Append(rgbColor36);
            indexedColors1.Append(rgbColor37);
            indexedColors1.Append(rgbColor38);
            indexedColors1.Append(rgbColor39);
            indexedColors1.Append(rgbColor40);
            indexedColors1.Append(rgbColor41);
            indexedColors1.Append(rgbColor42);
            indexedColors1.Append(rgbColor43);
            indexedColors1.Append(rgbColor44);
            indexedColors1.Append(rgbColor45);
            indexedColors1.Append(rgbColor46);
            indexedColors1.Append(rgbColor47);
            indexedColors1.Append(rgbColor48);
            indexedColors1.Append(rgbColor49);
            indexedColors1.Append(rgbColor50);
            indexedColors1.Append(rgbColor51);
            indexedColors1.Append(rgbColor52);
            indexedColors1.Append(rgbColor53);
            indexedColors1.Append(rgbColor54);
            indexedColors1.Append(rgbColor55);
            indexedColors1.Append(rgbColor56);
            indexedColors1.Append(rgbColor57);
            indexedColors1.Append(rgbColor58);
            indexedColors1.Append(rgbColor59);
            indexedColors1.Append(rgbColor60);
            indexedColors1.Append(rgbColor61);
            indexedColors1.Append(rgbColor62);
            indexedColors1.Append(rgbColor63);
            indexedColors1.Append(rgbColor64);

            colors1.Append(indexedColors1);

            var stylesheetExtensionList1 = new StylesheetExtensionList();

            var stylesheetExtension1 = new StylesheetExtension { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14",
                                                         "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            var slicerStyles1 = new SlicerStyles { DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);

            stylesheet1.Append(numberingFormats1);
            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(colors1);
            stylesheet1.Append(stylesheetExtensionList1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }

        private void SetPackageProperties(OpenXmlPackage document)
        {
            document.PackageProperties.Creator = "Whitney Comp";
            document.PackageProperties.Revision = "0";
            document.PackageProperties.Created = DateTime.UtcNow;
            document.PackageProperties.Modified = DateTime.UtcNow;
            document.PackageProperties.LastModifiedBy = "Whitney Comp";
            document.PackageProperties.LastPrinted = DateTime.UtcNow;
        }
    }

    internal static class SummaryReportHelpers
    {
        internal static string FormatBuildingClassQuality(string clazz, string quality)
        {
            if (string.IsNullOrWhiteSpace(clazz) && string.IsNullOrWhiteSpace(quality))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(clazz))
                return quality;

            return string.IsNullOrWhiteSpace(quality) ? clazz : String.Format("{0} / {1}", clazz, quality);
        }

        internal static string FormatNoiSf(decimal? netOperatingIncome)
        {
            return netOperatingIncome == null ? "" : netOperatingIncome.ToString();
        }
    }
}
