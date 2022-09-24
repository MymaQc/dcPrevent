using dcPrevent.GMA;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace dcPrevent {
    
    public partial class App : Form {
        
        private readonly bool firstClick;
        private readonly Stopwatch stopwatch;
        private IKeyboardMouseEvents keyboardMouseEvents;

        public App() {
            firstClick = true;
            stopwatch = new Stopwatch();
            InitializeComponent();
            SubscribeGlobal();
            CheckForIllegalCrossThreadCalls = false;
            FormClosing += App_FormClosing;
        }

        private void App_Load(object sender, EventArgs e) {
            ActiveControl = lstLog;
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e) => Unsubscribe();

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e) {
            ShowInTaskbar = true;
            niTray.Visible = false;
            WindowState = FormWindowState.Normal;
            Show();
        }

        private void btnHideToTray_Click(object sender, EventArgs e)  {
            if (WindowState != FormWindowState.Normal)  {
                return;
            }
            MessageBox.Show(@"Double-click the tray icon in the hidden icons to redisplay dcPrevent.", @"Notice", 0, (MessageBoxIcon)64, 0);
            ShowInTaskbar = false;
            niTray.Visible = true;
            Hide();
        }

        private void SubscribeGlobal() {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events) {
            keyboardMouseEvents = events;
            stopwatch.Start();
            keyboardMouseEvents.MouseDownExt += HookManager_Supress;
        }

        private void Unsubscribe() {
            if (keyboardMouseEvents is null) { 
                return;
            }
            keyboardMouseEvents.MouseDownExt -= HookManager_Supress;
            keyboardMouseEvents.Dispose();
            keyboardMouseEvents = null;
        }

        private void HookManager_Supress(object sender, MouseEventExtArgs e) {
            if (e.Button is MouseButtons.Middle)  {
                return;
            }
            if (stopwatch.ElapsedMilliseconds < 50L & stopwatch.IsRunning & cbFilterDC.Checked) {
                e.Handled = true;
                AddLog($"Suppressed a DC\t\t {e.Button}\n");
                stopwatch.Reset();
                stopwatch.Start();
            } else {
                AddLog($"MouseDown \t\t {e.Button}\n");
                stopwatch.Reset();
                stopwatch.Start();
            }
        }

        private void AddLog(string text) {
            if (IsDisposed) { 
                return;
            }

            lstLog.Items.Add(text);
            lstLog.TopIndex = checked(lstLog.Items.Count - 1);
        }

    }

}