using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelHelper
{
    public class ExcelEdit
    {
        public string mFilename;
        public Application app;
        public Workbooks wbs;
        public Workbook wb;
        public Worksheets wss;
        public Worksheet ws;
        public ExcelEdit()
        {

        }

        /// <summary>
        /// 创建一个Microsoft.Office.Interop.Excel对象
        /// </summary>
        public void Create()
        {
            app = new Application();
            wbs = app.Workbooks;
            wb = wbs.Add(true);
        }

        /// <summary>
        /// 打开一个Microsoft.Office.Interop.Excel文件
        /// </summary>
        /// <param name="FileName"></param>
        public void Open(string FileName)
        {
            app = new Application();
            wbs = app.Workbooks;
            wb = wbs.Add(FileName);
            mFilename = FileName;
        }

        /// <summary>
        /// 获取一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public Worksheet GetSheet(string SheetName)
        {
            Worksheet s = (Worksheet)wb.Worksheets[SheetName];
            return s;
        }

        /// <summary>
        /// 添加一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public Worksheet AddSheet(string SheetName)
        {
            Worksheet s = (Worksheet)wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;
            return s;
        }

        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        public void DelSheet(string SheetName)
        {
            ((Worksheet)wb.Worksheets[SheetName]).Delete();
        }

        /// <summary>
        /// 重命名一个工作表
        /// </summary>
        /// <param name="OldSheetName"></param>
        /// <param name="NewSheetName"></param>
        /// <returns></returns>
        public Worksheet ReNameSheet(string OldSheetName, string NewSheetName)
        {
            Worksheet s = (Worksheet)wb.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }

        /// <summary>
        /// 重命名一个工作表
        /// </summary>
        /// <param name="Sheet"></param>
        /// <param name="NewSheetName"></param>
        /// <returns></returns>
        public Worksheet ReNameSheet(Worksheet Sheet, string NewSheetName)
        {

            Sheet.Name = NewSheetName;

            return Sheet;
        }

        /// <summary>
        /// ws：要设值的工作表     X行Y列     value   值
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellValue(Worksheet ws, int x, int y, object value)
        {
            ws.Cells[x, y] = value;
        }

        /// <summary>
        /// ws：要设值的工作表的名称 X行Y列 value 值
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellValue(string ws, int x, int y, object value)
        {

            GetSheet(ws).Cells[x, y] = value;
        }

        /// <summary>
        /// 设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="Startx"></param>
        /// <param name="Starty"></param>
        /// <param name="Endx"></param>
        /// <param name="Endy"></param>
        /// <param name="size"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="HorizontalAlignment"></param>
        public void SetCellProperty(Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Constants color, Constants HorizontalAlignment)
        {
            name = "宋体";
            size = 12;
            color = Constants.xlAutomatic;
            HorizontalAlignment = Constants.xlRight;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }

        /// <summary>
        /// 设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        /// </summary>
        /// <param name="wsn"></param>
        /// <param name="Startx"></param>
        /// <param name="Starty"></param>
        /// <param name="Endx"></param>
        /// <param name="Endy"></param>
        /// <param name="size"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="HorizontalAlignment"></param>
        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Constants color, Constants HorizontalAlignment)
        {
            //name = "宋体";
            //size = 12;
            //color = Constants.xlAutomatic;
            //HorizontalAlignment = Constants.xlRight;

            Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;

            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }


        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(Worksheet ws, int x1, int y1, int x2, int y2)
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);

        }


        /// <summary>
        /// 将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置 为在使用模板时控制格式时使用
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();

                }

            }

        }

        /// <summary>
        /// 将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void InsertTable(System.Data.DataTable dt, Worksheet ws, int startX, int startY)
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    ws.Cells[startX + i, j + startY] = dt.Rows[i][j];

                }

            }

        }


        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置一
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void AddTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    GetSheet(ws).Cells[i + startX, j + startY] = dt.Rows[i][j];

                }

            }

        }


        /// <summary>
        /// 将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void AddTable(System.Data.DataTable dt, Worksheet ws, int startX, int startY)
        {


            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {

                    ws.Cells[i + startX, j + startY] = dt.Rows[i][j];

                }
            }

        }

        /// <summary>
        /// 插入图片操作一
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="ws"></param>
        public void InsertPictures(string Filename, string ws)
        {
            //GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
            //后面的数字表示位置
        }


        /// <summary>
        /// 插入图表操作
        /// </summary>
        /// <param name="ChartType"></param>
        /// <param name="ws"></param>
        /// <param name="DataSourcesX1"></param>
        /// <param name="DataSourcesY1"></param>
        /// <param name="DataSourcesX2"></param>
        /// <param name="DataSourcesY2"></param>
        /// <param name="ChartDataType"></param>
        public void InsertActiveChart(XlChartType ChartType, string ws, int DataSourcesX1, int DataSourcesY1, int DataSourcesX2, int DataSourcesY2, XlRowCol ChartDataType)
        {
            ChartDataType = XlRowCol.xlColumns;
            wb.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            {
                wb.ActiveChart.ChartType = ChartType;
                wb.ActiveChart.SetSourceData(GetSheet(ws).get_Range(GetSheet(ws).Cells[DataSourcesX1, DataSourcesY1], GetSheet(ws).Cells[DataSourcesX2, DataSourcesY2]), ChartDataType);
                wb.ActiveChart.Location(XlChartLocation.xlLocationAsObject, ws);
            }
        }


        /// <summary>
        /// 保存文档
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();
                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// 文档另存为
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileFormat"></param>
        /// <returns></returns>
        public bool SaveAs(object fileName, XlFileFormat fileFormat)
        {
            try
            {
                wb.SaveAs(Filename: fileName, FileFormat: fileFormat, AccessMode: XlSaveAsAccessMode.xlExclusive);
                return true;
            }

            catch (Exception ex)
            {
                return false;

            }
        }


        /// <summary>
        /// 关闭一个Microsoft.Office.Interop.Excel对象，销毁对象
        /// </summary>
        public void Close()
        {
            wb.Close(false, Type.Missing, Type.Missing);
            wbs.Close();
            app.Quit();
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();
        }
    }
}
