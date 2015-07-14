using System;
using System.IO;
using System.Threading;
using System.Windows;
using EdmLib;

namespace AirVentsOrderManager.Model
{
    public class PdmFilesFoldersOrder
    {
        public string OrderName { get; set; }

        public string VaultName { get; set; }

        public bool UnitTypeFrameless { get; set; }

        public void CreateOrder()
        {
            int fileId;
            var addStr =  UnitTypeFrameless ? "B" : "";

            CheckInOutPdm(CopyAFile(CreateDestinationDirectory(OrderPath), AsmTemplatePath, OrderName + addStr + ".sldasm", out fileId), true);
            CheckInOutPdm(CopyAFile(CreateDestinationDirectory(OrderPath), DrwTemplatePath, OrderName + addStr + ".slddrw", out fileId), true);
        }

        #region

        static readonly EdmVault5 Vault5 = new EdmVault5();

        #region Path Templates

        static string AsmTemplatePath
        {
            get { return RootFolder + @"\Библиотека проектирования\Templates\Assm.sldasm"; }
        }

        static string DrwTemplatePath
        {
            get { return RootFolder + @"\Библиотека проектирования\Templates\drw.slddrw"; }
        }

        #endregion

        #region Path to Order

        string OrderPath
        {
            get
            {
                if (!LoginVaultAuto()) return null;
                return UnitTypeFrameless ?
                RootFolder + @"\Заказы AirVents Frameless\" + OrderName + "B" :
                RootFolder + @"\Заказы AirVents\" + OrderName;
            }
        }

        #endregion

        static void CheckInOutPdm(string filePath, bool registration)
        {
            var retryCount = 2;
            var success = false;

            while (!success && retryCount > 0)
            {
                try
                {
                    IEdmFolder5 iEdmFolder5;
                    var edmFile5 = Vault5.GetFileFromPath(filePath, out iEdmFolder5);

                    // Разрегистрировать
                    if (registration == false)
                    {
                        // edmFile5.GetFileCopy(0, 0, oFolder.ID, (int)EdmGetFlag.EdmGet_Simple);
                        edmFile5.LockFile(iEdmFolder5.ID, 0);
                    }

                    // Зарегистрировать
                    if (registration)
                    {
                        //edmFile5.UnlockFile(8, "", (int)EdmUnlockFlag.EdmUnlock_Simple, null);
                        edmFile5.UnlockFile(iEdmFolder5.ID, "");
                        Thread.Sleep(50);
                    }
                    success = true;
                }
                catch (Exception exception)
                {
                    retryCount--;
                    Thread.Sleep(200);
                    if (retryCount == 0)
                    {
                        // throw; //or handle error and break/return
                    }
                }
            }
            if (!success)
            {

            }
           // MessageBox.Show(registration.ToString(), filePath);
        }

        bool LoginVaultAuto()
        {
            if (Vault5.IsLoggedIn) return true;
            try
            {
                Vault5.LoginAuto(VaultName, 0);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static string RootFolder
        {
            get { return Vault5.RootFolderPath; }
        }

        IEdmFolder5 CreateDestinationDirectory(string path)
        {
            IEdmFolder5 iEdmFolder5 = null;
            try
            {
                var directoryInfo = new DirectoryInfo(path);
                if (!LoginVaultAuto()) return null;

                var vault2 = (IEdmVault7)Vault5;
                if (directoryInfo.Exists)
                {
                    iEdmFolder5 = vault2.GetFolderFromPath(directoryInfo.FullName);
                }
                else
                {
                    if (directoryInfo.Parent != null)
                    {
                        var parentFolder = vault2.GetFolderFromPath(directoryInfo.Parent.FullName);
                        iEdmFolder5 = parentFolder.AddFolder(0, directoryInfo.Name);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return iEdmFolder5;
        }

        static string CopyAFile(IEdmFolder5 destinationFolder, string sourceFilePath, string newName, out int fileId)
        {
            IEdmFolder5 oFolder;
            var edmFile5 = Vault5.GetFileFromPath(sourceFilePath, out oFolder);
            try
            {
                fileId = destinationFolder.CopyFile(edmFile5.ID, oFolder.ID, 0, newName);
            }
            catch (Exception)
            {
                fileId = 0;
            }
            return destinationFolder.LocalPath + "\\" + newName;
        }

        #endregion
    }
}
