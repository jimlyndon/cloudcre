using System;
using System.Collections.Generic;
using System.Linq;
using Cloudcre.Service.Property.ViewModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Cloudcre.Service.Report.Summary
{
    public class OfficeSummaryReport : SummaryReport<OfficeViewModel>
    {
        protected override void BuildHeaderPrimary(SheetData sheetData1, SharedStringTablePart sharedStringTablePart)
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

            //var cell36 = new Cell { CellReference = "B3", StyleIndex = 85U, DataType = CellValues.SharedString };
            //var cellValue2 = new CellValue
            //{
            //    Text =
            //        sharedStringTablePart.SharedString(
            //            "123 NE 4th Street, Ft Lauderdale (File:10-0100)")
            //};
            //cell36.Append(cellValue2);

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
            //row3.Append(cell36);
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

        protected override MergeCells BuildProperties(SheetData sheetData1, SharedStringTablePart sstb, IEnumerable<OfficeViewModel> viewModels)
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
                cell46.Append(new CellValue { Text = viewModel.OverallRate().ToString() });

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

    }
}
