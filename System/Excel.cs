using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace Common
{
    /// <summary>
    /// excel读写操作
    /// cb 2011-1025
    /// </summary>
    public static class Excel
    {

        /// <summary>
        /// 将excel内容读到DataTable中
        /// </summary>
        /// <param name="dt">存放数据DataTable</param>
        /// <param name="fileName">excel文件</param>
        /// <param name="iSheet">第几sheet页（0开头）</param>
        /// <param name="isFirstRowColumn">excel第一行为标题</param>
        /// <returns>数据行数</returns>
        public static int ReadExcel(ref DataTable dt,string fileName, int iSheet, bool isFirstRowColumn)
        {
            IWorkbook workbook = null;
            FileStream fs = null;
            ISheet sheet = null;
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.ToLower().IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.ToLower().IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                //if (sheetName != null)
                //{
                //    sheet = workbook.GetSheet(sheetName);
                //    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                //    {
                //        sheet = workbook.GetSheetAt(0);
                //    }
                //}

                 sheet = workbook.GetSheetAt(iSheet);

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    dt.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = dt.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception("读取excel失败:" + ex.Message);
            }
        }
        /// <summary>
        /// 将excel内容读到DataTable中,读第一Sheet页
        /// </summary>
        /// <param name="dt">存放数据DataTable</param>
        /// <param name="sPathFile">excel文件</param>
        /// <param name="bTitle">excel第一行为标题</param>
        /// <returns>数据行数</returns>
        public static int ReadExcel(ref DataTable dt, string sPathFile, bool bTitle)
        {
            return ReadExcel(ref dt, sPathFile, 0, bTitle);
        }

        /// <summary>
        /// 将excel内容读到DataTable中,读第一Sheet页，第一行作为标题
        /// </summary>
        /// <param name="dt">存放数据DataTable</param>
        /// <param name="sPathFile">excel文件</param>
        /// <returns>数据行数</returns>
        public static int ReadExcel(ref DataTable dt, string sPathFile)
        {
            return ReadExcel(ref dt, sPathFile, 0, true);
        }

        /// <summary>
        /// 将excel内容读到DataTable中
        /// </summary>
        /// <param name="sPathFile">excel文件</param>
        /// <param name="iSheet">第几sheet页（0开头）</param>
        /// <param name="bTitle">excel第一行为标题</param>
        /// <returns></returns>
        public static DataTable ReadExcel(string sPathFile, int iSheet,bool bTitle)
        {
            DataTable dt = new DataTable();
            ReadExcel(ref dt, sPathFile, iSheet, bTitle);
            return dt;
        }


        /// <summary>
        /// 将excel内容读到DataTable中,第一行作字段名
        /// </summary>
        /// <param name="sPathFile">excel文件</param>
        /// <param name="iSheet">第几sheet页（0开头）</param>
        /// <returns></returns>
        public static DataTable ReadExcel(string sPathFile, int iSheet)
        {
            return ReadExcel(sPathFile, iSheet, true);
        }

        /// <summary>
        /// 将excel第一sheet页内容读到DataTable中,第一行作字段名
        /// </summary>
        /// <param name="sPathFile">excel文件</param>
        /// <returns></returns>
        public static DataTable ReadExcel(string sPathFile)
        {
            return ReadExcel(sPathFile, 0, true);
        }

        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource,string strFileName)
        {
            using (MemoryStream ms = Export(dtSource))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        private static MemoryStream Export(DataTable dtSource)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            //HSSFWorkbook workbook = new HSSFWorkbook();
            //HSSFSheet sheet = workbook.CreateSheet();
            ISheet sheet = workbook.CreateSheet();

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 0)
                {

                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            //解决导出excel表中的某个单元格数据过大，显示单元格最大列宽255错误
                            int colWidth = (arrColWidth[column.Ordinal] + 1) * 256;
                            if (colWidth < 255 * 256)
                            {
                                sheet.SetColumnWidth(column.Ordinal, colWidth < 3000 ? 3000 : colWidth);
                            }
                            else
                            {
                                sheet.SetColumnWidth(column.Ordinal, 6000);
                            }

                            //sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        //headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 1;
                }
                #endregion


                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                //ms.Position = 0;
                workbook.Close();
                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        /*
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        private static MemoryStream Export(DataTable dtSource)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            //HSSFSheet sheet = workbook.CreateSheet();
            ISheet sheet = workbook.CreateSheet();
            #region 右击文件 属性信息
            {
                NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "TongChi";
                workbook.DocumentSummaryInformation = dsi;

                NPOI.HPSF.SummaryInformation si = NPOI.HPSF.PropertySetFactory.CreateSummaryInformation();
                si.Author = "TCSOFT"; //填加xls文件作者信息
                si.ApplicationName = "TCSOFT"; //填加xls文件创建程序信息
                si.LastAuthor = "TCSOFT"; //填加xls文件最后保存者信息
                si.Comments = "同驰软件生成"; //填加xls文件作者信息
                si.Title = "TCSOFT"; //填加xls文件标题信息
                si.Subject = "TCSOFT";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    //#region 表头及样式
                    //{
                    //    Row headerRow = sheet.CreateRow(0);
                    //    headerRow.HeightInPoints = 25;
                    //    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                    //    CellStyle headStyle = workbook.CreateCellStyle();
                    //    headStyle.Alignment = HorizontalAlignment.CENTER;
                    //    Font font = workbook.CreateFont();
                    //    font.FontHeightInPoints = 20;
                    //    font.Boldweight = 700;
                    //    headStyle.SetFont(font);
                    //    headerRow.GetCell(0).CellStyle = headStyle;
                    //    sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                    //    headerRow.Dispose();
                    //}
                    //#endregion


                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            //解决导出excel表中的某个单元格数据过大，显示单元格最大列宽255错误
                            int colWidth = (arrColWidth[column.Ordinal] + 1) * 256;
                            if (colWidth < 255 * 256)
                            {
                                sheet.SetColumnWidth(column.Ordinal, colWidth < 3000 ? 3000 : colWidth);
                            }
                            else
                            {
                                sheet.SetColumnWidth(column.Ordinal, 6000);
                            }

                            //sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        //headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 1;
                }
                #endregion


                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook.Close();
                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        */


        ///// <summary>
        ///// 用于Web导出
        ///// </summary>
        ///// <param name="dtSource">源DataTable</param>
        ///// <param name="strHeaderText">表头文本</param>
        ///// <param name="strFileName">文件名</param>
        //public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        //{
        //    HttpContext curContext = HttpContext.Current;

        //    // 设置编码和附件格式
        //    curContext.Response.ContentType = "application/vnd.ms-excel";
        //    curContext.Response.ContentEncoding = Encoding.UTF8;
        //    curContext.Response.Charset = "";
        //    curContext.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

        //    curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
        //    curContext.Response.End();
        //} 

    }
}
