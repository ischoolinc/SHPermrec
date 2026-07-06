## Goal

Update the reinstatement notice program to exclude students who have transfer-out update records.

Target file:

- `休學期滿復學通知單.cs`

After completing the change, document the modification in:

- `復學通知單調整0701.md`

## Background

The current program generates the reinstatement notice list based on leave-of-school update records.

In `GetLeaveSchoolList(int LeaveTime)`, the program uses `QuitCode` to exclude students who should not appear in the reinstatement notice list.

Current logic:

- Students are first selected by leave update codes.
- The program checks each student's update records.
- If the student has a code in `QuitCode`, `LastStatu` is set to `"放棄學籍"`.
- Only students with `LastStatu == "休學"` are listed.

## Required Change

Add the following transfer-out update codes into `QuitCode`:

```csharp
"301", "302",
"310", "311", "312", "313", "314", "316"
````

Meaning:

| Code | Description     |
| ---- | --------------- |
| 301  | 科班轉出            |
| 302  | 跳級（轉出）          |
| 310  | 轉出(參與非學校型態實驗教育) |
| 311  | 轉出(舉家遷移)        |
| 312  | 轉出(家長、學生調職)     |
| 313  | 轉出(改變環境)        |
| 314  | 轉出(其他)          |
| 316  | 轉出(休學)(不計人數)    |

## Implementation

Find this code in `GetLeaveSchoolList(int LeaveTime)`:

```csharp
List<string> QuitCode = new List<string>() 
{ 
    "361", "367", "369", "371", "374", "375", 
    "376", "377", "378", "379", "380", "381" 
};
```

Change it to:

```csharp
List<string> QuitCode = new List<string>() 
{ 
    "301", "302",
    "310", "311", "312", "313", "314", "316",
    "361", "367", "369", "371", "374", "375", 
    "376", "377", "378", "379", "380", "381" 
};
```

## Important Notes

* Do not change other program logic.
* Do not change the existing leave code list.
* Do not change the existing reinstatement code list.
* Do not change the report generation flow.
* Do not change the Word mail merge fields.
* Do not change template loading logic.
* Only add the transfer-out codes to `QuitCode`.

## Expected Result

After the change:

* If a student has leave-of-school records and also has one of the transfer-out codes, the student should not be listed in the reinstatement notice.
* Existing behavior for first-time leave and second-time leave should remain unchanged.

## Completion Record

After finishing the code change, create or update:

```md
復學通知單調整0701.md
```

Record:

* Modified file name
* Modified method name
* Added transfer-out exclusion codes
* Testing or verification notes

```
```
