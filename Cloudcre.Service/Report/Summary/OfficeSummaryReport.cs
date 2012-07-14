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
        protected override IEnumerable<MergeCell> BuildHeaderPrimary(SheetData sheetData, SharedStringTablePart sharedStringTablePart)
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
            cell19.Append(new CellValue {Text = sharedStringTablePart.SharedString("COMPARABLE OFFICE BUILDING SALES")});

            row2.Append(cell19);

            var row3 = new Row
            {
                RowIndex = 3U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 12.75D,
                CustomHeight = true,
                DyDescent = 0.2D
            };

            //var cell36 = new Cell { CellReference = "B3", StyleIndex = 85U, DataType = CellValues.SharedString };
            //var cellValue2 = new CellValue
            //{
            //    Text =
            //        sharedStringTablePart.SharedString(
            //            "123 NE 4th Street, Ft Lauderdale (File:10-0100)")
            //};
            //cell36.Append(cellValue2);

            //row3.Append(cell36);

            var row4 = new Row
            {
                RowIndex = 4U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 3.75D,
                CustomHeight = true,
                DyDescent = 0.2D
            };

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
            cell71.Append(new CellValue { Text = sharedStringTablePart.SharedString("PROPERTY") });

            var cell72 = new Cell { CellReference = "C5", StyleIndex = 3U };
            var cell73 = new Cell { CellReference = "D5", StyleIndex = 2U };

            var cell74 = new Cell { CellReference = "E5", StyleIndex = 86U, DataType = CellValues.SharedString }; ;
            cell74.Append(new CellValue { Text = sharedStringTablePart.SharedString("SITE") });

            var cell75 = new Cell { CellReference = "F5", StyleIndex = 86U };
            var cell76 = new Cell { CellReference = "G5", StyleIndex = 2U };

            var cell77 = new Cell { CellReference = "H5", StyleIndex = 86U, DataType = CellValues.SharedString };
            cell77.Append(new CellValue { Text = sharedStringTablePart.SharedString("BUILDING") });

            var cell78 = new Cell { CellReference = "I5", StyleIndex = 86U };
            var cell79 = new Cell { CellReference = "J5", StyleIndex = 2U };

            var cell80 = new Cell { CellReference = "K5", StyleIndex = 6U, DataType = CellValues.SharedString };
            cell80.Append(new CellValue { Text = sharedStringTablePart.SharedString("ECONOMICS") });

            var cell81 = new Cell { CellReference = "L5", StyleIndex = 2U };

            var cell82 = new Cell { CellReference = "M5", StyleIndex = 86U, DataType = CellValues.SharedString };
            cell82.Append(new CellValue { Text = sharedStringTablePart.SharedString("SALE") });

            var cell83 = new Cell { CellReference = "N5", StyleIndex = 86U };
            var cell84 = new Cell { CellReference = "O5", StyleIndex = 86U };
            var cell85 = new Cell { CellReference = "P5", StyleIndex = 2U };

            var cell86 = new Cell { CellReference = "Q5", StyleIndex = 5U, DataType = CellValues.SharedString };
            cell86.Append(new CellValue { Text = sharedStringTablePart.SharedString("COMMENTS") });

            row5.Append(cell70, cell71, cell72, cell73, cell74, cell75, cell76, cell77, cell78, cell79, cell80, cell81,
                        cell82, cell83, cell84, cell85, cell86);

            var row6 = new Row
            {
                RowIndex = 6U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                Height = 6D,
                CustomHeight = true,
                DyDescent = 0.2D
            };

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
            cell106.Append(new CellValue { Text = sharedStringTablePart.SharedString("Location") });

            var cell107 = new Cell { CellReference = "D7", StyleIndex = 9U };

            var cell108 = new Cell { CellReference = "E7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell108.Append(new CellValue { Text = sharedStringTablePart.SharedString("SF") });

            var cell109 = new Cell { CellReference = "F7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell109.Append(new CellValue { Text = sharedStringTablePart.SharedString("FAR") });

            var cell110 = new Cell { CellReference = "G7", StyleIndex = 9U };

            var cell111 = new Cell { CellReference = "H7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell111.Append(new CellValue { Text = sharedStringTablePart.SharedString("SF") });

            var cell112 = new Cell { CellReference = "I7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell112.Append(new CellValue { Text = sharedStringTablePart.SharedString("Class / Qual") });

            var cell113 = new Cell { CellReference = "J7", StyleIndex = 9U };

            var cell114 = new Cell { CellReference = "K7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell114.Append(new CellValue { Text = sharedStringTablePart.SharedString("Occupancy") });

            var cell115 = new Cell { CellReference = "L7", StyleIndex = 9U };

            var cell116 = new Cell { CellReference = "M7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell116.Append(new CellValue { Text = sharedStringTablePart.SharedString("Price") });

            var cell117 = new Cell { CellReference = "N7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell117.Append(new CellValue { Text = sharedStringTablePart.SharedString("Grantor") });

            var cell118 = new Cell { CellReference = "O7", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell118.Append(new CellValue { Text = sharedStringTablePart.SharedString("$/SF Bldg") });

            var cell119 = new Cell { CellReference = "P7", StyleIndex = 9U };
            var cell120 = new Cell { CellReference = "Q7", StyleIndex = 8U };

            row7.Append(cell104, cell105, cell106, cell107, cell108, cell109, cell110, cell111, cell112, cell113,
                        cell114, cell115, cell116, cell117, cell118, cell119, cell120);



            var row8 = new Row { RowIndex = 8U, Spans = new ListValue<StringValue> { InnerText = "1:76" }, DyDescent = 0.2D };

            var cell121 = new Cell { CellReference = "A8", StyleIndex = 2U };
            var cell122 = new Cell { CellReference = "B8", StyleIndex = 2U };

            var cell123 = new Cell { CellReference = "C8", StyleIndex = 7U, DataType = CellValues.SharedString };
            cell123.Append(new CellValue { Text = sharedStringTablePart.SharedString("Folio") });

            var cell124 = new Cell { CellReference = "D8", StyleIndex = 9U };

            var cell125 = new Cell { CellReference = "E8", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell125.Append(new CellValue { Text = sharedStringTablePart.SharedString("Acres") });

            var cell126 = new Cell { CellReference = "F8", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell126.Append(new CellValue { Text = sharedStringTablePart.SharedString("Zoning") });

            var cell127 = new Cell { CellReference = "G8", StyleIndex = 9U };

            var cell128 = new Cell { CellReference = "H8", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell128.Append(new CellValue { Text = sharedStringTablePart.SharedString("Built") });

            var cell129 = new Cell { CellReference = "I8", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell129.Append(new CellValue { Text = sharedStringTablePart.SharedString("Stories") });

            var cell130 = new Cell { CellReference = "J8", StyleIndex = 9U };

            var cell131 = new Cell { CellReference = "K8", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell131.Append(new CellValue { Text = sharedStringTablePart.SharedString("NOI/SF") });

            var cell132 = new Cell { CellReference = "L8", StyleIndex = 9U };

            var cell133 = new Cell { CellReference = "M8", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell133.Append(new CellValue { Text = sharedStringTablePart.SharedString("Date") });

            var cell134 = new Cell { CellReference = "N8", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell134.Append(new CellValue { Text = sharedStringTablePart.SharedString("Grantee") });

            var cell136 = new Cell { CellReference = "P8", StyleIndex = 9U };
            var cell137 = new Cell { CellReference = "Q8", StyleIndex = 8U };

            row8.Append(cell121, cell122, cell123, cell124, cell125, cell126, cell127, cell128, cell129, cell130,
                        cell131, cell132, cell133, cell134, cell136, cell137);

            
            var row9 = new Row
            {
                RowIndex = 9U,
                Spans = new ListValue<StringValue> { InnerText = "1:76" },
                StyleIndex = 2U,
                CustomFormat = true,
                DyDescent = 0.2D
            };

            var cell139 = new Cell { CellReference = "C9", StyleIndex = 7U, DataType = CellValues.SharedString };
            cell139.Append(new CellValue { Text = sharedStringTablePart.SharedString("Verification") });
            
            var cell141 = new Cell { CellReference = "F9", StyleIndex = 7U, DataType = CellValues.SharedString };
            cell141.Append(new CellValue { Text = sharedStringTablePart.SharedString("Parking") });

            var cell142 = new Cell { CellReference = "G9", StyleIndex = 9U };

            var cell143 = new Cell { CellReference = "H9", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell143.Append(new CellValue { Text = sharedStringTablePart.SharedString("Condition") });

            var cell144 = new Cell { CellReference = "I9", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell144.Append(new CellValue { Text = sharedStringTablePart.SharedString("Use") });

            var cell145 = new Cell { CellReference = "J9", StyleIndex = 9U };

            var cell146 = new Cell { CellReference = "K9", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell146.Append(new CellValue { Text = sharedStringTablePart.SharedString("OAR") });

            var cell147 = new Cell { CellReference = "L9", StyleIndex = 9U };

            var cell148 = new Cell { CellReference = "M9", StyleIndex = 9U, DataType = CellValues.SharedString };
            cell148.Append(new CellValue { Text = sharedStringTablePart.SharedString("OR B-P") });

            var cell149 = new Cell { CellReference = "P9", StyleIndex = 9U };
            var cell150 = new Cell { CellReference = "Q9", StyleIndex = 8U };

            row9.Append(cell139, cell141, cell142, cell143, cell144, cell145, cell146, cell147,
                        cell148, cell149, cell150);

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

            var row11 = new Row { RowIndex = 11U, Spans = new ListValue<StringValue> { InnerText = "1:76" }, DyDescent = 0.2D };

            sheetData.Append(row1, row2, row3, row4, row5, row6, row7, row8, row9, row10, row11);

            var mergeCells = new List<MergeCell>
                                 {
                                     new MergeCell {Reference = "B2:Q2"},
                                     new MergeCell {Reference = "B3:Q3"},
                                     new MergeCell {Reference = "E5:F5"},
                                     new MergeCell {Reference = "H5:I5"},
                                     new MergeCell {Reference = "M5:O5"}
                                 };

            //mergeCells.Append(mergeCell1, mergeCell2, mergeCell3, mergeCell4, mergeCell5);
            return mergeCells;
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

        protected override Columns BuildColumns()
        {
            var columns = new Columns();
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

            columns.Append(column1, column2, column3, column4, column5, column6, column7, column8, column9, column10,
                            column11, column12, column13, column14, column15, column16, column17, column18);

            return columns;
        }
    }
}
