namespace WinService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GRServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.GRService安装包 = new System.ServiceProcess.ServiceInstaller();
            // 
            // GRServiceProcessInstaller
            // 
            this.GRServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.GRServiceProcessInstaller.Password = null;
            this.GRServiceProcessInstaller.Username = null;
            // 
            // GRService安装包
            // 
            this.GRService安装包.Description = "宝坻区地下水资源信息管理系统服务器";
            this.GRService安装包.ServiceName = "宝坻区地下水资源信息管理系统服务器";
            this.GRService安装包.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.GRServiceProcessInstaller,
            this.GRService安装包});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller GRServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller GRService安装包;
    }
}