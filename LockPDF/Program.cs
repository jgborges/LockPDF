using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace LockPDF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] parameters)
        {
            PdfReader.unethicalreading = true;

            if (GetContext() == false && WasContextDisabled() == false)
                SetContext();

            string filename = parameters.Length > 0 ? parameters[0] : null;

            bool showDialog = false;
            bool unlock = false;

            string param2 = parameters.Length > 1 ? parameters[1] : null;
            if (param2 != null)
            {
                if (param2.ToLower().Equals("show-dialog") || param2.ToLower().Equals("-d"))
                    showDialog = true;
                else if (param2.ToLower().Equals("unlock") || param2.ToLower().Equals("-u"))
                    unlock = true;
            }

#if DEBUG
            //MessageBox.Show(filename);
            //MessageBox.Show(param2);

            //filename = new FileInfo("teste.pdf").FullName;
            //unlock = true;
#endif
            bool fileExists = !string.IsNullOrEmpty(filename) && filename.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) && File.Exists(filename);

            if (showDialog || fileExists == false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(filename));
            }
            else if (unlock)
            {
                try
                {
                    string pwd;
                    if (GetPDFPassword(filename, out pwd) == false)
                        return;

                    if (AskReplace(filename, true) == false)
                        return;

                    string error;
                    bool r = UnlockPDF(filename, pwd, out error);

                    if (r == false || error != null)
                    {
                        string errorMsg = "Error: " + error ?? "could not unlock file";
                        System.Console.WriteLine(errorMsg);

                        if (IsConsolePresent == false)
                            MessageBox.Show(null, errorMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        System.Console.WriteLine("Succesfully unlocked");
                    }
                }
                catch { }
            }
            else
            {
                string pwd;
                if (GetPDFPassword(filename, out pwd) == false)
                    return;

                PdfPermissions permissions;
                bool replace;
                string ownerPwd, userPwd;
                LoadDefault(out permissions, out replace, out ownerPwd, out userPwd);

                if (ownerPwd == null)
                    ownerPwd = RandomPassword;

                if (AskReplace(filename, replace, false) == false)
                    return;

                string error;
                bool r = LockPDF(filename, pwd, permissions, RandomPassword, userPwd, replace, out error);

                if (r == false || error != null)
                {
                    string errorMsg = "Error: " + error ?? "could not lock file";
                    System.Console.WriteLine(errorMsg);

                    if (IsConsolePresent == false)
                        MessageBox.Show(null, errorMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    System.Console.WriteLine("Succesfully locked");
                }
            }
        }

        public static bool AskReplace(string file, bool isUnlock)
        {
            return AskReplace(null, file, false, isUnlock);
        }
        public static bool AskReplace(IWin32Window owner, string file, bool isUnlock)
        {
            return AskReplace(owner, file, false, isUnlock);
        }
        public static bool AskReplace(string file, bool replace, bool isUnlock)
        {
            return AskReplace(null, file, false, isUnlock);
        }
        public static bool AskReplace(IWin32Window owner, string file, bool replace, bool isUnlock)
        {
            var result = GetFilenames(file, replace, isUnlock);

            if (File.Exists(result.Item1) && MessageBox.Show(null, "Are you sure you want to overwrite?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
            { 
                return false;
            }

            return true;
        }

        private const string MenuName = "Folder\\shell\\NewMenuOption";
        private const string Command = "Folder\\shell\\NewMenuOption\\command";

        public static string RandomPassword = Guid.NewGuid().ToString();

        private const string MenuTitle1 = "Lock this PDF (default settings)";
        private const string MenuTitle2 = "Lock this PDF...";
        private const string MenuTitle3 = "Unlock this PDF";

        private static bool? _console_present;
        public static bool IsConsolePresent
        {
            get
            {
                if (_console_present == null)
                {
                    _console_present = true;
                    try { int window_height = Console.WindowHeight; }
                    catch { _console_present = false; }
                }
                return _console_present.Value;
            }
        }

        private static bool GetPDFPassword(string filename, out string pwd)
        {
            pwd = null;

            while (TryUnlock(filename, pwd) == false)
            {
                if (pwd != null && MessageBox.Show(null, "Bad password, please try again.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    return false;

                pwd = InputBox.Show(null, "PDF Password", "Please type the owner or user password:", pwd);
                if (pwd == null)
                    return false;
            }

            return true;
        }

        private static bool WasContextDisabled()
        {
            try
            {
                RegistryKey reg = Registry.Users.OpenSubKey("Software\\LockPDF");

                return reg != null && (bool)reg.GetValue("ContextDisabled", false);
            }
            catch
            {
                return false;
            }
        }

        public static bool GetContext()
        {
            try
            {
                RegistryKey reg1 = Registry.ClassesRoot.OpenSubKey(".pdf");
                if (reg1 == null)
                    return false;

                string ProgID = reg1.GetValue(null).ToString();

                string progID_key = "Software\\Classes\\" + ProgID + "\\shell";
                RegistryKey reg2 = Registry.CurrentUser.OpenSubKey(progID_key);
                if (reg2 == null)
                    return false;

                string[] verbs = reg2.GetSubKeyNames();

                foreach (string verb in verbs)
                {
                    if (verb.Equals(MenuTitle1) || verb.Equals(MenuTitle2) || verb.Equals(MenuTitle3))
                    {
                        return true;
                    }

                    string verb_key = "Software\\Classes\\" + ProgID + "\\shell\\" + verb + "\\command";

                    RegistryKey reg3 = Registry.CurrentUser.OpenSubKey(verb_key);
                    if (reg3 == null)
                        continue;

                    string cmd = reg3.GetValue(null).ToString();
                    if (string.IsNullOrEmpty(cmd) || File.Exists(cmd) == false || cmd.Equals(Application.ExecutablePath))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
                return false;
            }
        }

        public static bool SetContext()
        {
            try
            {
                RegistryKey reg = Registry.ClassesRoot.OpenSubKey(".pdf");
                if (reg == null)
                {
                    return false;
                }

                string progID = reg.GetValue(null).ToString();

                string progID_key1 = "Software\\Classes\\" + progID + "\\shell\\" + MenuTitle1 + "\\command";
                string progID_key2 = "Software\\Classes\\" + progID + "\\shell\\" + MenuTitle2 + "\\command";
                string progID_key3 = "Software\\Classes\\" + progID + "\\shell\\" + MenuTitle3 + "\\command";

                RegistryKey reg1 = Registry.CurrentUser.OpenSubKey(progID_key1);
                if (reg1 != null)
                    Registry.CurrentUser.DeleteSubKeyTree(progID_key1);
                
                reg1 = Registry.CurrentUser.CreateSubKey(progID_key1);
                reg1.SetValue(null, Application.ExecutablePath + " \"%1\"");

                RegistryKey reg2 = Registry.CurrentUser.OpenSubKey(progID_key2);
                if (reg2 != null)
                    Registry.CurrentUser.DeleteSubKeyTree(progID_key2);

                reg2 = Registry.CurrentUser.CreateSubKey(progID_key2);
                reg2.SetValue(null, Application.ExecutablePath + " \"%1\" show-dialog");

                RegistryKey reg3 = Registry.CurrentUser.OpenSubKey(progID_key3);
                if (reg3 != null)
                    Registry.CurrentUser.DeleteSubKeyTree(progID_key3);

                reg3 = Registry.CurrentUser.CreateSubKey(progID_key3);
                reg3.SetValue(null, Application.ExecutablePath + " \"%1\" unlock");

                return true;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
                return false;
            }
        }

        public static bool DisableContext()
        {
            try
            {
                RegistryKey reg1 = Registry.ClassesRoot.OpenSubKey(".pdf");
                if (reg1 == null)
                    return true;

                string ProgID = reg1.GetValue(null).ToString();

                string progID_key = "Software\\Classes\\" + ProgID + "\\shell";
                RegistryKey reg2 = Registry.CurrentUser.OpenSubKey(progID_key);
                if (reg2 == null)
                    return true;

                string[] verbs = reg2.GetSubKeyNames();

                foreach (string verb in verbs)
                {
                    if (verb.Equals(MenuTitle1) || verb.Equals(MenuTitle2))
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(progID_key + "\\" + verb);
                        continue;
                    }

                    string verb_key = "Software\\Classes\\" + ProgID + "\\shell\\" + verb + "\\command";

                    RegistryKey reg3 = Registry.CurrentUser.OpenSubKey(verb_key);
                    if (reg3 == null)
                        continue;

                    string cmd = reg3.GetValue(null).ToString();
                    if (string.IsNullOrEmpty(cmd) || File.Exists(cmd) == false || cmd.Equals(Application.ExecutablePath))
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(progID_key + "\\" + verb);
                        continue;
                    }
                }


                // this will prevent the creation of ContextMenu every time the app initializes
                RegistryKey reg4 = Registry.Users.OpenSubKey("Software\\LockPDF");
                if (reg4 == null)
                    reg4 = Registry.CurrentUser.CreateSubKey("Software\\LockPDF");

                reg4.GetValue("ContextDisabled", true);

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
                return false;
            }
        }

        public static bool TryUnlock(string file, string pwd)
        {
            bool fullPermission;
            return TryUnlock(file, pwd, out fullPermission);
        }

        public static bool TryUnlock(string file, string pwd, out bool fullPermission)
        {
            fullPermission = false;

            if (string.IsNullOrEmpty(file) || !file.EndsWith(".PDF", StringComparison.OrdinalIgnoreCase) || File.Exists(file) == false)
                return false;

            byte[] pwd_buf = pwd != null ? System.Text.Encoding.UTF8.GetBytes(pwd) : null;

            try
            {
                using (PdfReader reader = new PdfReader(file, pwd_buf))
                {
                    fullPermission = reader.IsOpenedWithFullPermissions;
                    return true;
                }
            }
            catch (iTextSharp.text.exceptions.BadPasswordException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ReadPDF(string file, string owner_pwd, out bool wrong_pwd, out bool fullPermission, out PdfPermissions permissions)
        {
            // we are using unethical reading, so if there is only a owner password, we can ignore all restrictions

            permissions = PdfPermissions.All;
            fullPermission = false;
            wrong_pwd = false;

            if (string.IsNullOrEmpty(file) || !file.EndsWith(".PDF", StringComparison.OrdinalIgnoreCase) || File.Exists(file) == false)
                return false;

            byte[] own_pwdbuf = owner_pwd != null ? System.Text.Encoding.UTF8.GetBytes(owner_pwd) : null;

            try
            {
                using (PdfReader reader = new PdfReader(file, own_pwdbuf))
                {
                    if (PdfEncryptor.IsAssemblyAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowAssembly;
                    if (PdfEncryptor.IsCopyAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowCopy;
                    if (PdfEncryptor.IsDegradedPrintingAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowDegradedPrinting;
                    if (PdfEncryptor.IsFillInAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowFillIn;
                    if (PdfEncryptor.IsModifyAnnotationsAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowModifyAnnotations;
                    if (PdfEncryptor.IsModifyContentsAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowModifyContents;
                    if (PdfEncryptor.IsPrintingAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowPrinting;
                    if (PdfEncryptor.IsScreenReadersAllowed((int)reader.Permissions))
                        permissions |= PdfPermissions.AllowScreenReaders;

                    fullPermission = reader.IsOpenedWithFullPermissions;
                    return true;
                }
            }
            catch (iTextSharp.text.exceptions.BadPasswordException)
            {
                wrong_pwd = true;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool LockPDF(string file, string owner_pwd, PdfPermissions permissions, string usr_pwd_new, bool replace, out string error)
        {
            return LockPDF(file, owner_pwd, permissions, RandomPassword, usr_pwd_new, replace, out error);
        }

        public static bool LockPDF(string file, string pwd, PdfPermissions permissions, string owner_pwd, string usr_pwd, bool replace, out string error)
        {
            error = null;

            try
            {
                var result = GetFilenames(file, replace, false);
                string output = result.Item1;
                string tout = result.Item2;
                
                byte[] pwdbuf = pwd != null ? System.Text.Encoding.UTF8.GetBytes(pwd) : null;

                byte[] usr_pwdbuf = usr_pwd != null ? System.Text.Encoding.UTF8.GetBytes(usr_pwd) : null;
                byte[] own_pwdbuf = owner_pwd != null ? System.Text.Encoding.UTF8.GetBytes(owner_pwd) : null;

                File.Delete(tout);

                using (PdfReader reader = new PdfReader(file, pwdbuf))
                using (FileStream fs = new FileStream(tout, FileMode.Create, FileAccess.ReadWrite))
                {
                    PdfEncryptor.Encrypt(reader, fs,
                        usr_pwdbuf, // user password
                        own_pwdbuf, // owner password
                        (int)permissions, // permissions
                        true
                    );
                }

                bool ethicalState = PdfReader.unethicalreading;

                try
                {
                    PdfReader.unethicalreading = false;

                    using (PdfReader reader = new PdfReader(tout, usr_pwdbuf))
                    {
                        PdfPermissions p = PdfPermissions.All;

                        if (PdfEncryptor.IsAssemblyAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowAssembly;
                        if (PdfEncryptor.IsCopyAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowCopy;
                        if (PdfEncryptor.IsDegradedPrintingAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowDegradedPrinting;
                        if (PdfEncryptor.IsFillInAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowFillIn;
                        if (PdfEncryptor.IsModifyAnnotationsAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowModifyAnnotations;
                        if (PdfEncryptor.IsModifyContentsAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowModifyContents;
                        if (PdfEncryptor.IsPrintingAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowPrinting;
                        if (PdfEncryptor.IsScreenReadersAllowed((int)reader.Permissions))
                            p |= PdfPermissions.AllowScreenReaders;

                        if ((p & permissions) < permissions)
                            error += "Could not change permissions.";

                        if (permissions != PdfPermissions.All && reader.IsOpenedWithFullPermissions == true)
                            error += "Could not lock file";
                    }
                }
                catch (iTextSharp.text.exceptions.BadPasswordException)
                {
                    if (usr_pwdbuf != null)
                    {
                        error += "Could not protect file";
                    }
                }
                finally
                {
                    PdfReader.unethicalreading = ethicalState;
                }

                File.Delete(output);
                File.Move(tout, output);
                
                return File.Exists(output) && error == null;
            }
            catch (Exception e)
            {
            }

            return false;
        }

        public static Tuple<string, string> GetFilenames(string file, bool replace, bool isUnlock)
        {
            string output = replace ? file : file.Substring(0, file.Length - 4) + "_" + (isUnlock ? "un" : "") + "locked.pdf";
            FileInfo fi = new FileInfo(output);
            string temp = fi.DirectoryName + "\\~" + fi.Name;

            return new Tuple<string, string>(output, temp);
        }

        public static bool UnlockPDF(string file, string pwd, out string error)
        {
            error = null;

            bool ethicalState = PdfReader.unethicalreading;

            try
            {
                PdfReader.unethicalreading = true;

                var result = GetFilenames(file, false, true);
                string output = result.Item1;
                string tout = result.Item2;

                byte[] pwdbuf = pwd != null ? System.Text.Encoding.UTF8.GetBytes(pwd) : null;

                File.Delete(tout);

                using (MyPdfReader reader = new MyPdfReader(file, pwdbuf))
                {
                    reader.Decrypt();

                    using (FileStream fs = new FileStream(tout, FileMode.Create, FileAccess.ReadWrite))
                    using (PdfStamper s = new PdfStamper(reader, fs))
                    {
                        //PdfEncryptor.Encrypt(reader, fs,
                        //    null, // user password
                        //    System.Text.Encoding.UTF8.GetBytes(""), // owner password
                        //    (int)PdfPermissions.All2, // permissions
                        //    false
                        //);
                    }
                }
                
                try
                {
                    PdfReader.unethicalreading = false;

                    using (PdfReader reader = new PdfReader(tout))
                    {
                        if (reader.IsOpenedWithFullPermissions == false)
                            error += "Could not unlock file";
                    }
                }
                catch (iTextSharp.text.exceptions.BadPasswordException)
                {
                    if (pwd != null)
                    {
                        error += "Could not open file (bad password)";
                    }
                }

                File.Delete(output);
                File.Move(tout, output);

                return File.Exists(output) && error == null;
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            finally
            {
                PdfReader.unethicalreading = ethicalState;
            }

            return false;

        }

        public static void LoadDefault(out PdfPermissions permissions, out bool replace, out string ownerPwd, out string userPwd)
        {
            RegistryKey reg = Registry.Users.OpenSubKey("Software\\LockPDF\\Default");
            if (reg == null)
                reg = Registry.CurrentUser.CreateSubKey("Software\\LockPDF\\Default");

            permissions = (PdfPermissions)reg.GetValue("Permissions",(int)(PdfPermissions.AllowDegradedPrinting | PdfPermissions.AllowScreenReaders));
            replace = Convert.ToBoolean(reg.GetValue("ReplaceFile", false));

            ownerPwd = null;

            if (reg.GetValueNames().Contains("OwnerPassword"))
            {
                ownerPwd = reg.GetValue("OwnerPassword", null) as string;
                if (ownerPwd == string.Empty)
                    ownerPwd = null;
            }

            userPwd = null;
            if (reg.GetValueNames().Contains("UserPassword"))
            {
                userPwd = reg.GetValue("UserPassword", null) as string;
                if (userPwd == string.Empty)
                    userPwd = null;
            }

            reg.Close();
        }

        public static void SaveDefault(PdfPermissions permissions, bool replace, string ownerPwd, string userPwd)
        {
            RegistryKey reg = Registry.Users.OpenSubKey("Software\\LockPDF\\Default");
            if (reg == null)
                reg = Registry.CurrentUser.CreateSubKey("Software\\LockPDF\\Default");

            reg.SetValue("Permissions", (int)permissions);
            reg.SetValue("ReplaceFile", replace ? 1 : 0);

            if (ownerPwd != null)
                reg.SetValue("OwnerPassword", ownerPwd);
            else if (reg.GetValueNames().Contains("OwnerPassword"))
                reg.DeleteValue("OwnerPassword");

            if (userPwd != null)
                reg.SetValue("UserPassword", userPwd);
            else if (reg.GetValueNames().Contains("UserPassword"))
                reg.DeleteValue("UserPassword");

            reg.Close();
        }
    }


}
