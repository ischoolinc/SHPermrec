﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpdateRecordModule_SH_D.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UpdateRecordModule_SH_D.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap btnItemNameList_Image {
            get {
                object obj = ResourceManager.GetObject("btnItemNameList_Image", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap btnItemNameList_Image1 {
            get {
                object obj = ResourceManager.GetObject("btnItemNameList_Image1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap demographic_reload_64 {
            get {
                object obj = ResourceManager.GetObject("demographic_reload_64", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] EnrollmentListTemplate {
            get {
                object obj = ResourceManager.GetObject("EnrollmentListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon error {
            get {
                object obj = ResourceManager.GetObject("error", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] ExtendingGraduatingStudentListTemplate {
            get {
                object obj = ResourceManager.GetObject("ExtendingGraduatingStudentListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] ExtendingStudentListTemplate {
            get {
                object obj = ResourceManager.GetObject("ExtendingStudentListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] ExtendingStudentUpdateRecordListTemplate {
            get {
                object obj = ResourceManager.GetObject("ExtendingStudentUpdateRecordListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] GraduatingStudentListTemplate {
            get {
                object obj = ResourceManager.GetObject("GraduatingStudentListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap history_save_64 {
            get {
                object obj = ResourceManager.GetObject("history_save_64", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;?xml-stylesheet type=&quot;text/xsl&quot; href=&quot;format.xsl&quot; ?&gt;
        ///&lt;ValidateRule Name=&quot;匯入新生異動&quot;&gt;
        ///  &lt;DuplicateDetection&gt;
        ///    &lt;Detector Name=&quot;PrimaryKey1&quot;&gt;
        ///      &lt;Field Name=&quot;學號&quot; /&gt;
        ///      &lt;Field Name=&quot;狀態&quot; /&gt;
        ///      &lt;Field Name=&quot;異動日期&quot; /&gt;
        ///      &lt;Field Name=&quot;異動代碼&quot; /&gt;
        ///    &lt;/Detector&gt;
        ///  &lt;/DuplicateDetection&gt;
        ///  &lt;FieldList&gt;
        ///    &lt;Field Required=&quot;True&quot; Name=&quot;學號&quot;&gt;
        ///      &lt;Validate AutoCorrect=&quot;False&quot; Description=&quot;「學號」不允許空白。&quot; ErrorType=&quot;Error&quot; Validator=&quot;不可空白&quot; When=&quot;&quot; /&gt;
        ///    &lt;/Field [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ImportEnrollmentListVal {
            get {
                return ResourceManager.GetString("ImportEnrollmentListVal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;?xml-stylesheet type=&quot;text/xsl&quot; href=&quot;format.xsl&quot; ?&gt;
        ///&lt;ValidateRule Name=&quot;匯入畢業異動&quot;&gt;
        ///  &lt;DuplicateDetection&gt;
        ///    &lt;Detector Name=&quot;PrimaryKey1&quot;&gt;
        ///      &lt;Field Name=&quot;學號&quot; /&gt;
        ///      &lt;Field Name=&quot;狀態&quot; /&gt;
        ///      &lt;Field Name=&quot;異動日期&quot; /&gt;
        ///      &lt;Field Name=&quot;異動代碼&quot; /&gt;
        ///    &lt;/Detector&gt;
        ///  &lt;/DuplicateDetection&gt;
        ///  &lt;FieldList&gt;
        ///    &lt;Field Required=&quot;True&quot; Name=&quot;學號&quot;&gt;
        ///      &lt;Validate AutoCorrect=&quot;False&quot; Description=&quot;「學號」不允許空白。&quot; ErrorType=&quot;Error&quot; Validator=&quot;不可空白&quot; When=&quot;&quot; /&gt;
        ///    &lt;/Field [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ImportGraduateListVal {
            get {
                return ResourceManager.GetString("ImportGraduateListVal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;?xml-stylesheet type=&quot;text/xsl&quot; href=&quot;format.xsl&quot; ?&gt;
        ///&lt;ValidateRule Name=&quot;匯入學籍異動&quot;&gt;
        ///  &lt;DuplicateDetection&gt;
        ///    &lt;Detector Name=&quot;PrimaryKey1&quot;&gt;
        ///      &lt;Field Name=&quot;學號&quot; /&gt;
        ///      &lt;Field Name=&quot;狀態&quot; /&gt;
        ///      &lt;Field Name=&quot;異動日期&quot; /&gt;
        ///      &lt;Field Name=&quot;異動代碼&quot; /&gt;
        ///    &lt;/Detector&gt;
        ///  &lt;/DuplicateDetection&gt;
        ///  &lt;FieldList&gt;
        ///    &lt;Field Required=&quot;True&quot; Name=&quot;學號&quot;&gt;
        ///      &lt;Validate AutoCorrect=&quot;False&quot; Description=&quot;「學號」不允許空白。&quot; ErrorType=&quot;Error&quot; Validator=&quot;不可空白&quot; When=&quot;&quot; /&gt;
        ///    &lt;/Field [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ImportStudUpdateRecordListVal {
            get {
                return ResourceManager.GetString("ImportStudUpdateRecordListVal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;?xml-stylesheet type=&quot;text/xsl&quot; href=&quot;format.xsl&quot; ?&gt;
        ///&lt;ValidateRule Name=&quot;匯入轉入異動&quot;&gt;
        ///  &lt;DuplicateDetection&gt;
        ///    &lt;Detector Name=&quot;PrimaryKey1&quot;&gt;
        ///      &lt;Field Name=&quot;學號&quot; /&gt;
        ///      &lt;Field Name=&quot;狀態&quot; /&gt;
        ///      &lt;Field Name=&quot;異動日期&quot; /&gt;
        ///      &lt;Field Name=&quot;異動代碼&quot; /&gt;
        ///    &lt;/Detector&gt;
        ///  &lt;/DuplicateDetection&gt;
        ///  &lt;FieldList&gt;
        ///    &lt;Field Required=&quot;True&quot; Name=&quot;學號&quot;&gt;
        ///      &lt;Validate AutoCorrect=&quot;False&quot; Description=&quot;「學號」不允許空白。&quot; ErrorType=&quot;Error&quot; Validator=&quot;不可空白&quot; When=&quot;&quot; /&gt;
        ///    &lt;/Field [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ImportTransferListVal {
            get {
                return ResourceManager.GetString("ImportTransferListVal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;國中代碼&gt;
        ///  &lt;!--&lt;學校 代碼=&quot;010301&quot; 名稱=&quot;國立華僑高級中等學校附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011301&quot; 名稱=&quot;私立淡江高中附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011302&quot; 名稱=&quot;私立康橋高中附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011306&quot; 名稱=&quot;私立金陵女中附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011307&quot; 名稱=&quot;新北市裕德高級中等學校附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011309&quot; 名稱=&quot;財團法人南山高中附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011310&quot; 名稱=&quot;財團法人恆毅高中附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011311&quot; 名稱=&quot;私立聖心女中附設國中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011312&quot; 名稱=&quot;私 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string JHSchoolList {
            get {
                return ResourceManager.GetString("JHSchoolList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] RetaintoStudentListTemplate {
            get {
                object obj = ResourceManager.GetObject("RetaintoStudentListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;高中代碼&gt;
        ///  &lt;學校 代碼=&quot;010301&quot; 名稱=&quot;國立華僑高級中等學校&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011301&quot; 名稱=&quot;私立淡江高中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011302&quot; 名稱=&quot;私立康橋高中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011306&quot; 名稱=&quot;私立金陵女中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011307&quot; 名稱=&quot;新北市裕德高級中等學校&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011309&quot; 名稱=&quot;財團法人南山高中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011310&quot; 名稱=&quot;財團法人恆毅高中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011311&quot; 名稱=&quot;私立聖心女中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 代碼=&quot;011312&quot; 名稱=&quot;私立崇義高中&quot; 所在地=&quot;新北市&quot; 所在地代碼=&quot;1&quot; /&gt;
        ///  &lt;學校 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SHSchoolList {
            get {
                return ResourceManager.GetString("SHSchoolList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] StudentUpdateRecordListTemplate {
            get {
                object obj = ResourceManager.GetObject("StudentUpdateRecordListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] TemporaryStudentListTemplate {
            get {
                object obj = ResourceManager.GetObject("TemporaryStudentListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] TransferringStudentUpdateRecordListTemplate {
            get {
                object obj = ResourceManager.GetObject("TransferringStudentUpdateRecordListTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;異動代號對照表&gt;
        ///  &lt;異動&gt;
        ///    &lt;代號&gt;001&lt;/代號&gt;
        ///    &lt;原因及事項&gt;持國民中學畢業證書者(含國中補校)&lt;/原因及事項&gt;
        ///    &lt;分類&gt;新生異動&lt;/分類&gt;
        ///  &lt;/異動&gt;
        ///  &lt;異動&gt;
        ///    &lt;代號&gt;002&lt;/代號&gt;
        ///    &lt;原因及事項&gt;持國民中學補習學校資格證明書者&lt;/原因及事項&gt;
        ///    &lt;分類&gt;新生異動&lt;/分類&gt;
        ///  &lt;/異動&gt;
        ///  &lt;異動&gt;
        ///    &lt;代號&gt;003&lt;/代號&gt;
        ///    &lt;原因及事項&gt;持國民中學補習學校結(修)業證明書者&lt;/原因及事項&gt;
        ///    &lt;分類&gt;新生異動&lt;/分類&gt;
        ///  &lt;/異動&gt;
        ///  &lt;異動&gt;
        ///    &lt;代號&gt;004&lt;/代號&gt;
        ///    &lt;原因及事項&gt;持國民中學修業證明書者(修習三年級課程)&lt;/原因及事項&gt;
        ///    &lt;分類&gt;新生異動&lt;/分類&gt;
        ///  &lt;/異動&gt;
        ///  &lt;異動&gt;
        ///    &lt;代號&gt;005&lt;/代號&gt;
        ///    &lt;原因及事項&gt;持國民中學畢業程度學力鑑定考試及格證明書者&lt;/原因及事項&gt;
        ///    &lt;分類&gt;新生異動&lt;/分類&gt;
        ///  &lt;/ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string UpdateCode_SHD {
            get {
                return ResourceManager.GetString("UpdateCode_SHD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon warning {
            get {
                object obj = ResourceManager.GetObject("warning", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
    }
}
