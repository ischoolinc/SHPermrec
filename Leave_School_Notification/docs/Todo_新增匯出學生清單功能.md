## Goal

Add an Excel export feature for the student list in the reinstatement notice form.

Target file:

- `休學期滿復學通知單.cs`

After completing the change, document the modification in:

- `復學通知單調整0702.md`

## Requirements

Add export logic to:

```csharp
btnExportStudent_Click
````

The export source must be the same student list used by the print function:

```csharp
lstStudent
```

The Excel columns must match the `lstStudent` columns:

* 狀態
* 班級
* 學號
* 姓名
* 休學日期
* 休學原因
* 系統編號

## Implementation Notes

Use the existing utility method:

```csharp
Utility.ExportXls(string ReportName, Workbook wbXls)
```

Do not create a custom save-file flow inside `btnExportStudent_Click`.

Expected flow:

1. Check whether `lstStudent.Items.Count` is greater than 0.
2. If there is no data, show a message and stop.
3. Create an `Aspose.Cells.Workbook`.
4. Use `lstStudent.Columns` to create the Excel header row.
5. Use `lstStudent.Items` and `SubItems` to create the Excel data rows.
6. Auto-fit columns.
7. Call `Utility.ExportXls(...)` to export the file.

## Important Notes

* Do not change existing print logic.
* Do not change student list generation logic.
* Do not change leave/reinstatement/quit-code judgment logic.
* Do not change Designer unless the export button event is missing.
* Keep the Excel data source exactly the same as the visible `lstStudent` list.
* Export all columns currently in `lstStudent`, including `系統編號`.

## Suggested Code Structure

```csharp
private void btnExportStudent_Click(object sender, EventArgs e)
{
    if (lstStudent.Items.Count <= 0)
    {
        MessageBox.Show("清單內沒有學生，無法匯出");
        return;
    }

    try
    {
        Workbook wb = new Workbook();
        Worksheet ws = wb.Worksheets[0];
        ws.Name = "學生清單";

        for (int col = 0; col < lstStudent.Columns.Count; col++)
        {
            ws.Cells[0, col].PutValue(lstStudent.Columns[col].Text);
        }

        for (int row = 0; row < lstStudent.Items.Count; row++)
        {
            ListViewItem item = lstStudent.Items[row];

            for (int col = 0; col < lstStudent.Columns.Count; col++)
            {
                string value = "";

                if (col < item.SubItems.Count)
                    value = item.SubItems[col].Text;

                ws.Cells[row + 1, col].PutValue(value);
            }
        }

        ws.AutoFitColumns();
        Utility.ExportXls(cboReportKind.Text + "學生清單", wb);
    }
    catch (Exception ex)
    {
        FISCA.Presentation.Controls.MsgBox.Show("匯出發生錯誤:\n" + ex.Message);
    }
}
```

## Completion Record

After finishing the change, update:

```md
復學通知單調整0702.md
```

Record:

* Modified file name
* Modified method name
* Export source
* Export columns
* Utility method used
* Verification notes

```
```
