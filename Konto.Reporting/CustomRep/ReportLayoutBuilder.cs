using GrapeCity.ActiveReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GrapeCity.ActiveReports.PageReportModel; 
using GrapeCity.Enterprise.Data.DataEngine.Expressions; 
using System.Collections.ObjectModel; 
using System.Reflection;
using System.Text.RegularExpressions;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.App.Shared;

namespace Konto.Reporting.CustomRep
{
    public class ReportLayoutBuilder
    {
        //  private static ReportWizardState state;
        private static readonly Dictionary<string, int> NameCounts;
        static ReportLayoutBuilder()
        {
            NameCounts = new Dictionary<string, int>();
        }

        public static PageReport BuildReportLayout1(PageReport report, int ReportId, string datasetName)
        {
            Table table = new Table();
            table.Name = "Table1";
            table.DataSetName = datasetName;

            KontoContext _db = new KontoContext();
            var fieldList = _db.Customreps.Where(k => k.RepId == ReportId).ToList();
            var grouplist = fieldList.Where(k => k.GroupIndex > 0).OrderBy(k => k.GroupIndex).ToList();
            var FieldsWithoutGroup = fieldList.Where(k => !grouplist.Contains(k)).OrderBy(k => k.OrderIndex).ToList();

            TableRow CompNameHeader = new TableRow();
            CompNameHeader.Height = "0.75cm";
            table.Height += "0.75cm";
            TableRow AddressHeader = new TableRow();
            AddressHeader.Height = "0.503cm";
            table.Height += "0.503cm";
            TableRow TitleHeader = new TableRow();
            TitleHeader.Height = "0.503cm";
            table.Height += "0.503cm";
            int i = 0;

            foreach (var item in FieldsWithoutGroup)
            {
                TableColumn tableColumn = new TableColumn();
                if (item.Width > 0)
                {
                    tableColumn.Width = item.Width.ToString() + "cm";
                    table.Width += item.Width.ToString() + "cm";
                }
                else
                {
                    tableColumn.Width = (20 / FieldsWithoutGroup.Count()) + "cm";
                    table.Width += (20 / FieldsWithoutGroup.Count()) + "cm";
                }
                table.TableColumns.Add(tableColumn);

                TableCell tableHeaderCell = new TableCell();
                TextBox tableHeaderTextBox = CreateTextBox();
                tableHeaderTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
                if (i == 0)
                    tableHeaderTextBox.Value = ExpressionInfo.FromString("=First(Fields!CompName.Value, \"Company\")");
                else
                    tableHeaderTextBox.Value = "";
                tableHeaderTextBox.Style.FontFamily = "Tahoma";
                tableHeaderTextBox.Style.FontWeight = "Bold";
                tableHeaderTextBox.Style.FontSize = "12pt";
                tableHeaderTextBox.Style.PaddingBottom = tableHeaderTextBox.Style.PaddingLeft = tableHeaderTextBox.Style.PaddingRight = tableHeaderTextBox.Style.PaddingTop = ExpressionInfo.FromString("2pt");
                tableHeaderTextBox.Style.TextAlign = ExpressionInfo.FromString("Left");

                tableHeaderCell.ColSpan = FieldsWithoutGroup.Count();
                tableHeaderCell.ReportItems.Add(tableHeaderTextBox);
                CompNameHeader.TableCells.Add(tableHeaderCell);

                //Address
                TableCell tableAddressCell = new TableCell();
                TextBox tableAddressTextBox = CreateTextBox();
                tableAddressTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
                if (i == 0)
                    tableAddressTextBox.Value = ExpressionInfo.FromString("=First(Fields!Address1.Value, \"Company\")+First(Fields!Address2.Value, \"Company\")");
                else
                    tableAddressTextBox.Value = "";

                tableAddressTextBox.Style.FontFamily = "Tahoma";
                tableAddressTextBox.Style.FontSize = "8pt";
                tableAddressTextBox.Style.PaddingBottom = tableAddressTextBox.Style.PaddingLeft
                    = tableAddressTextBox.Style.PaddingRight = tableAddressTextBox.Style.PaddingTop
                    = ExpressionInfo.FromString("2pt");
                tableAddressTextBox.Style.TextAlign = ExpressionInfo.FromString("Left");

                tableAddressCell.ColSpan = FieldsWithoutGroup.Count();
                tableAddressCell.ReportItems.Add(tableAddressTextBox);
                AddressHeader.TableCells.Add(tableAddressCell);

                //Title
                TableCell tableTitleCell = new TableCell();
                TextBox tabletitleTextBox = CreateTextBox();
                tabletitleTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
                if (i == 0)
                    tabletitleTextBox.Value = ExpressionInfo.FromString("=Parameters!report_title.Value");
                else
                    tabletitleTextBox.Value = "";

                tabletitleTextBox.Style.FontFamily = "Tahoma";
                tabletitleTextBox.Style.FontSize = "8pt";
                tabletitleTextBox.Style.FontWeight = "Bold";
                tabletitleTextBox.Style.PaddingBottom = tabletitleTextBox.Style.PaddingLeft
                    = tabletitleTextBox.Style.PaddingRight = tabletitleTextBox.Style.PaddingTop
                    = ExpressionInfo.FromString("2pt");
                tabletitleTextBox.Style.TextAlign = ExpressionInfo.FromString("Left");

                tableTitleCell.ColSpan = FieldsWithoutGroup.Count();
                tableTitleCell.ReportItems.Add(tabletitleTextBox);
                TitleHeader.TableCells.Add(tableTitleCell);

                i++;
                //For soring
                if (item.SortIndex > 0)
                {
                    SortBy sortBy = new SortBy();
                    sortBy.SortExpression = ExpressionInfo.FromString("=Field!" + item.FieldName.ToString() + ".Value");
                    table.Details.Sorting.Add(sortBy);
                }
            }

            table.Header.TableRows.Add(CompNameHeader);//compName
            table.Header.TableRows.Add(AddressHeader);//Address
            table.Header.TableRows.Add(TitleHeader);//Report Title

            if (grouplist.Count > 0)
                CreateTableGroups(table, grouplist, FieldsWithoutGroup);
            else
            {
                //create Table Header, Footer and Detail sections
                TableRow tableHeader1 = CreateTableHeaderRow(FieldsWithoutGroup);
                tableHeader1.Height = "0.503cm";
                table.Height += "0.503cm";
                table.Header.TableRows.Add(tableHeader1);
            }

            TableRow tableFooter = CreateTableFooter(FieldsWithoutGroup);
            tableFooter.Height = "0.503cm";
            table.Height += "0.503cm";
            table.Footer.TableRows.Add(tableFooter);

            TableRow tableDetails = CreateTableDetails(FieldsWithoutGroup);
            tableDetails.Height = "0.503cm";
            table.Height += "0.503cm";
            table.Details.TableRows.Add(tableDetails);

            table.Top = "1cm";
            table.Left = "1cm";

            report.Report.Body.ReportItems.Add(table);

            TextBox PageFooterTextBox = CreateTextBox();
            PageFooterTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
            PageFooterTextBox.Value = "=\"Page \" & Globals!PageNumber & \" of \" & Globals!TotalPages";
            PageHeaderFooter pageHeaderFooter = new PageHeaderFooter();
            pageHeaderFooter.PrintOnFirstPage = true;
            pageHeaderFooter.PrintOnLastPage = true;
            pageHeaderFooter.Height = "1cm";
            pageHeaderFooter.Style.TextAlign = "Right";
            pageHeaderFooter.Style.FontWeight = "Bold";
            pageHeaderFooter.ReportItems.Add(PageFooterTextBox);
            report.Report.PageFooter = pageHeaderFooter;

            return report;
        }
        private static TableRow CreateTableHeaderRow(List<CustomRepModel> fieldList)
        {
            TableRow tableHeader = new TableRow();
            foreach (var field in fieldList)
            {
                TableCell tableHeaderCell = new TableCell();
                TextBox tableHeaderTextBox = CreateTextBox();
                tableHeaderTextBox.Name = GetNameForComponent(ComponentNames.TextBox);

                if (field.Heading != null)
                    tableHeaderTextBox.Value = field.Heading.ToString();
                else
                    tableHeaderTextBox.Value = field.FieldName.ToString();

                tableHeaderTextBox.Style.FontFamily = "Tahoma";
                tableHeaderTextBox.Style.FontSize = "8pt";
                tableHeaderTextBox.Style.FontWeight = "Bold";

                tableHeaderTextBox.Style.BorderColor.Top = tableHeaderTextBox.Style.BorderColor.Bottom
                    = tableHeaderTextBox.Style.BorderColor.Left =
                    tableHeaderTextBox.Style.BorderColor.Right = "LightGray";

                tableHeaderTextBox.Style.BorderStyle.Top = tableHeaderTextBox.Style.BorderStyle.Bottom =
                    tableHeaderTextBox.Style.BorderStyle.Left =
                    tableHeaderTextBox.Style.BorderStyle.Right = "Solid";

                tableHeaderTextBox.Style.BorderWidth.Top = tableHeaderTextBox.Style.BorderWidth.Bottom =
                    tableHeaderTextBox.Style.BorderWidth.Left =
                    tableHeaderTextBox.Style.BorderWidth.Right = "1pt";

                tableHeaderTextBox.Style.PaddingBottom = tableHeaderTextBox.Style.PaddingLeft
                    = tableHeaderTextBox.Style.PaddingRight = tableHeaderTextBox.Style.PaddingTop
                    = ExpressionInfo.FromString("2pt");
                tableHeaderTextBox.Style.TextAlign = ExpressionInfo.FromString("Left");

                tableHeaderCell.ReportItems.Add(tableHeaderTextBox);
                tableHeader.TableCells.Add(tableHeaderCell);
            }
            return tableHeader;
        }
        private static TableRow CreateTableFooter(List<CustomRepModel> fieldList)
        {
            TableRow tableFooter = new TableRow();
            foreach (var field in fieldList)
            {
                TableCell tableFooterCell = new TableCell();
                TextBox tableFooterTextBox = CreateTextBox();
                tableFooterTextBox.Name = GetNameForComponent(ComponentNames.TextBox);

                if (field.Summary == true)
                {
                    tableFooterTextBox.Value = GetFieldSummaryExpression(field.FieldName, field.SummaryType);
                    tableFooterTextBox.Style.FontWeight = "Bold";

                    tableFooterTextBox.Style.BorderColor.Top = tableFooterTextBox.Style.BorderColor.Bottom
                        = tableFooterTextBox.Style.BorderColor.Left =
                        tableFooterTextBox.Style.BorderColor.Right = "LightGray";

                    tableFooterTextBox.Style.BorderStyle.Top = tableFooterTextBox.Style.BorderStyle.Bottom =
                        tableFooterTextBox.Style.BorderStyle.Left =
                        tableFooterTextBox.Style.BorderStyle.Right = "Double";

                    tableFooterTextBox.Style.BorderWidth.Top = tableFooterTextBox.Style.BorderWidth.Bottom =
                        tableFooterTextBox.Style.BorderWidth.Left =
                        tableFooterTextBox.Style.BorderWidth.Right = "1pt";

                }

                tableFooterTextBox.Style.FontFamily = "Tahoma";
                tableFooterTextBox.Style.FontSize = "8pt";
                tableFooterTextBox.Style.PaddingBottom = tableFooterTextBox.Style.PaddingLeft
                    = tableFooterTextBox.Style.PaddingRight = tableFooterTextBox.Style.PaddingTop
                    = ExpressionInfo.FromString("2pt");
                tableFooterTextBox.Style.TextAlign = ExpressionInfo.FromString("Left");

                tableFooterCell.ReportItems.Add(tableFooterTextBox);
                tableFooter.TableCells.Add(tableFooterCell);
            }
            return tableFooter;
        }

        private static TableRow CreateTableDetails(List<CustomRepModel> fieldList)
        {
            TableRow tableDetails = new TableRow();
            foreach (var field in fieldList)
            {
                TableCell tableDetailsCell = new TableCell();
                TextBox tableDetailsTextBox = CreateTextBox();

                tableDetailsTextBox.Name = GetNameForComponent(ComponentNames.TextBox);

                tableDetailsTextBox.Value = GetFieldValueExpression(field.FieldName);

                tableDetailsTextBox.Style.BorderColor.Top = tableDetailsTextBox.Style.BorderColor.Bottom
                    = tableDetailsTextBox.Style.BorderColor.Left =
                    tableDetailsTextBox.Style.BorderColor.Right = "LightGray";

                tableDetailsTextBox.Style.BorderStyle.Top = tableDetailsTextBox.Style.BorderStyle.Bottom =
                    tableDetailsTextBox.Style.BorderStyle.Left =
                    tableDetailsTextBox.Style.BorderStyle.Right = "Solid";

                tableDetailsTextBox.Style.BorderWidth.Top = tableDetailsTextBox.Style.BorderWidth.Bottom =
                    tableDetailsTextBox.Style.BorderWidth.Left =
                    tableDetailsTextBox.Style.BorderWidth.Right = "1pt";

                tableDetailsCell.ReportItems.Add(tableDetailsTextBox);
                tableDetails.TableCells.Add(tableDetailsCell);

            }
            return tableDetails;
        }

        private static void CreateTableGroups(Table table, List<CustomRepModel> groupFieldList, List<CustomRepModel> FieldsWithoutGroup)
        {
            int i = 1;
            bool showHeader = false;
            foreach (CustomRepModel groupField in groupFieldList)
            {
                var grpcount = groupFieldList.Count();

                if (i == grpcount)
                    showHeader = true;
                TableGroup group = CreateTableGroup(groupField, FieldsWithoutGroup, showHeader);
                i++;
                if (group != null)
                {
                    table.TableGroups.Add(group);
                    foreach (TableRow row in group.Header.TableRows)
                    {
                        table.Height += row.Height;
                    }
                    foreach (TableRow row in group.Footer.TableRows)
                    {
                        table.Height += row.Height;
                    }
                }
            }
        }
        private static class ComponentNames
        {
            public const string Table = "Table{0}";
            public const string TableGroup = "Table_Group{0}";
            public const string TableDetailGroup = "Table_DetailGroup{0}";
            public const string TextBox = "TextBox{0}";
        }
        private static TableGroup CreateTableGroup(CustomRepModel groupField, List<CustomRepModel> fieldlist, bool showHeader)
        {
            TableGroup tableGroup = new TableGroup();
            TableRow groupHeaderRow = new TableRow();
            TableRow grouptitleHeaderRow = new TableRow();
            TableRow groupFooterRow = new TableRow();
            tableGroup.Grouping.Name = GetNameForComponent(ComponentNames.TableGroup);
            tableGroup.Grouping.GroupExpressions.Add(GetFieldValueExpression(groupField.FieldName));
            groupFooterRow.Height = groupHeaderRow.Height = grouptitleHeaderRow.Height = "0.500cm";
            int i = 0;
            int cnt = 0;

            foreach (CustomRepModel columnField in fieldlist)
            {
                TableCell groupHeaderCell = new TableCell();
                TableCell groupTitleHeaderCell = new TableCell();
                TableCell groupFooterCell = new TableCell();
                //set TextBoxes names avoiding duplicate names
                TextBox groupHeaderTextBox = CreateTextBox();
                groupHeaderTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
                TextBox groupFooterTextBox = CreateTextBox();
                groupFooterTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
                TextBox groupTitleHeaderTextBox = CreateTextBox();
                groupTitleHeaderTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
                if (i == 0)
                {
                    groupHeaderTextBox.Value = GetFieldValueExpression(groupField.FieldName);
                    groupHeaderTextBox.Style.FontWeight = ExpressionInfo.FromString("Bold");

                }

                if (columnField.Heading != null)
                    groupTitleHeaderTextBox.Value = columnField.Heading.ToString();
                else
                    groupTitleHeaderTextBox.Value = columnField.FieldName.ToString();

                groupTitleHeaderTextBox.Style.FontWeight = ExpressionInfo.FromString("Bold");
                groupTitleHeaderTextBox.Style.BorderColor.Top = groupTitleHeaderTextBox.Style.BorderColor.Bottom
                 = groupTitleHeaderTextBox.Style.BorderColor.Left =
                 groupTitleHeaderTextBox.Style.BorderColor.Right = "LightGray";

                groupTitleHeaderTextBox.Style.BorderStyle.Top = groupTitleHeaderTextBox.Style.BorderStyle.Bottom =
                    groupTitleHeaderTextBox.Style.BorderStyle.Left =
                    groupTitleHeaderTextBox.Style.BorderStyle.Right = "Solid";

                groupTitleHeaderTextBox.Style.BorderWidth.Top = groupTitleHeaderTextBox.Style.BorderWidth.Bottom =
                    groupTitleHeaderTextBox.Style.BorderWidth.Left =
                    groupTitleHeaderTextBox.Style.BorderWidth.Right = "1pt";


                //add group subtotals to the table cells if needed
                if (columnField.GroupSummary == true)
                {
                    //We only do subtotals on the fields selected for output, not the groups 

                    groupFooterTextBox.Value = GetFieldSummaryExpression(columnField.FieldName, columnField.SummaryType);
                    groupFooterTextBox.Style.TextAlign = "Right";
                    groupFooterTextBox.Style.FontWeight = "Bold";
                    cnt++;
                }
                groupHeaderCell.ColSpan = fieldlist.Count();
                groupTitleHeaderCell.ReportItems.Add(groupTitleHeaderTextBox);
                groupHeaderCell.ReportItems.Add(groupHeaderTextBox);
                groupFooterCell.ReportItems.Add(groupFooterTextBox);
                groupHeaderRow.TableCells.Add(groupHeaderCell);
                groupFooterRow.TableCells.Add(groupFooterCell);
                if (showHeader)
                {
                    grouptitleHeaderRow.TableCells.Add(groupTitleHeaderCell);
                }
                else
                {
                    grouptitleHeaderRow.Height = "0.1cm";
                }

                i++;
            }

            tableGroup.Header.TableRows.Add(groupHeaderRow);
            tableGroup.Header.TableRows.Add(grouptitleHeaderRow);
            if (cnt > 0)
            {
                tableGroup.Footer.TableRows.Add(groupFooterRow);
            }
            return tableGroup;
        }
        private static string GetFieldSummaryExpression(object field, string SummaryType)
        {
            string format = "={0}({1})";
            string fieldValue = GetFieldValue(field);
            return string.Format(format, SummaryType, fieldValue);
        }
        private static TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.CanGrow = true;
            textBox.Style.FontSize = "8pt";
            return textBox;
        }
        private static string GetFieldValueExpression(object field)
        {
            string format = "={0}";
            return string.Format(format, GetFieldValue(field));
        }
        private static string GetFieldValue(object field)
        {

            const string validName = @"^[a-zA-Z_]([a-zA-Z0-9_]*)$";
            const string itemSyntax = "Fields!{0}.Value";
            const string indexerSyntax = "Fields(\"{0}\").Value";


            string formatString;
            if (Regex.IsMatch(field.ToString(), validName))
            {
                formatString = itemSyntax;
            }
            else
            {
                formatString = indexerSyntax;
            }
            return string.Format(formatString, field.ToString());
        }
        private static string GetNameForComponent(string baseName)
        {
            string name = baseName;
            int count;
            if (!NameCounts.TryGetValue(baseName, out count))
            {
                count = 0;
            }
            count++;
            name = string.Format(name, count);
            NameCounts[baseName] = count;
            return name;
        }
        public static PageReport AddDataSetDataSource(PageReport report, string cmdText, int ReportId, string DataSetName, List<SpParaModel> SpParaList)
        {
            // create DataSource for the report
            DataSource dataSource = new DataSource();
            dataSource.Name = "cnn";
            dataSource.ConnectionProperties.DataProvider = "SQL";
            dataSource.ConnectionProperties.ConnectString = ExpressionInfo.FromString(KontoGlobals.sqlConnectionString.ConnectionString);

            //Create DataSet with specified query and load database fields to the DataSet
            DataSet dataSet = new DataSet();
            Query query = new Query();
            dataSet.Name = DataSetName;
            query.DataSourceName = "cnn";
            query.CommandType = QueryCommandType.StoredProcedure;
            query.CommandText = ExpressionInfo.FromString(cmdText);

            foreach (var item in SpParaList)
            {
                QueryParameter parameter = new QueryParameter();

                parameter.Name = "@" + item.ParaName;
                parameter.Value = "= Parameters!" + item.ParaName + ".Value";
                query.QueryParameters.Add(parameter);
            }
            dataSet.Query = query;
            //String[] fieldsList = new String[] { "MoviedID", "Title", "YearReleased", "MPAA" };
            KontoContext _db = new KontoContext();
            var fieldList = _db.Customreps.Where(k => k.RepId == ReportId);

            foreach (var custrep in fieldList)
            {
                Field field = new Field(custrep.FieldName, custrep.FieldName, null);
                // Field field = new Field(fieldName, fieldName, null);
                dataSet.Fields.Add(field);
            }
            //create report definition with specified DataSet and DataSource
            report.Report.DataSources.Add(dataSource);
            report.Report.DataSets.Add(dataSet);

            DataSet dataSet1 = new DataSet();
            Query query1 = new Query();
            dataSet1.Name = "Company";
            query1.DataSourceName = "cnn";
            query1.CommandType = QueryCommandType.Text;
            query1.CommandText = ExpressionInfo.FromString("select company.* from company where id = @CompanyId");

            QueryParameter parameter1 = new QueryParameter();
            parameter1.Name = "@CompanyId";
            parameter1.Value = "= Parameters!CompanyId.Value";
            query1.QueryParameters.Add(parameter1);

            dataSet1.Query = query1;
            var fieldNames = typeof(CompModel).GetFields()
                            .Select(field => field.Name)
                            .ToList();

            Type myType = typeof(CompModel);

            // Get the fields of the specified class.
            FieldInfo[] myField = myType.GetFields();

            var listOfFieldNames = typeof(CompModel).GetProperties().Select(f => f.Name).ToList();

            foreach (var field in listOfFieldNames)
            {
                if (field.ToString() != "Error")
                {
                    Field field1 = new Field(field.ToString(), field.ToString(), null);
                    dataSet1.Fields.Add(field1);
                }
            }
            report.Report.DataSets.Add(dataSet1);

            return report;
        }
        //This section loads the PageReport object created earlier to a Stream
        public static MemoryStream LoadReportToStream(PageReport report)
        {
            string rpt = report.ToRdlString();
            byte[] data = Encoding.UTF8.GetBytes(rpt);
            MemoryStream stream = new MemoryStream(data);
            return stream;
        }
    }
}