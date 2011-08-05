using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

namespace TCPUtil
{   // NameSpace : TCPUtil

	/// <summary>
	///     An asynchronous socket server and client.
	/// </summary>
	public class TCPUtil : System.Windows.Forms.Form
    {   // CLASS : TCPUtil
        
		/// <summary>
		///     The main callback for Worker Sockets
		/// </summary>
		public AsyncCallback WorkerCallBack;

        /// <summary>
        ///     The main collection of sockets that actually do things
        /// </summary>
        private List<Socket> WorkerSockets = new List<Socket>();

        
		private Queue OutgoingData;
		
		public TCPUtil()
		{   // CONSTRUCTOR

			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			OutgoingData = new Queue();

        }   // CONSTRUCTOR

        #region Windows Forms Designer generated code
        [STAThread]
		public static void Main(string[] args)
		{   // Main

			Application.Run(new TCPUtil());

		}   // Main

        private ContextMenuStrip mnuTX;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem mnuTXSelectAll;
        private ToolStripMenuItem mnuTXCopy;
        private ToolStripMenuItem mnuTxPaste;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuTxSendAllSelected;
        private TextBox txtTXMessage;
        private SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ListBox lstConnections;
        private System.Windows.Forms.RichTextBox richTextRxMessage;
        private System.Windows.Forms.Button btnRXClear;
        private System.Windows.Forms.Button btnSaveToDisk;
        private System.Windows.Forms.SaveFileDialog dlgSave;

		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCPUtil));
            this.btnSend = new System.Windows.Forms.Button();
            this.mnuTX = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTXSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTXCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTxSendAllSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextRxMessage = new System.Windows.Forms.RichTextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnBind = new System.Windows.Forms.Button();
            this.btnRXClear = new System.Windows.Forms.Button();
            this.btnSaveToDisk = new System.Windows.Forms.Button();
            this.lstConnections = new System.Windows.Forms.ListBox();
            this.txtTXMessage = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mnuTX.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(694, 444);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(87, 24);
            this.btnSend.TabIndex = 14;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // mnuTX
            // 
            this.mnuTX.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTXSelectAll,
            this.mnuTXCopy,
            this.mnuTxPaste,
            this.toolStripSeparator1,
            this.mnuTxSendAllSelected});
            this.mnuTX.Name = "mnuTX";
            this.mnuTX.Size = new System.Drawing.Size(165, 98);
            // 
            // mnuTXSelectAll
            // 
            this.mnuTXSelectAll.Name = "mnuTXSelectAll";
            this.mnuTXSelectAll.Size = new System.Drawing.Size(164, 22);
            this.mnuTXSelectAll.Text = "Select &All";
            this.mnuTXSelectAll.Click += new System.EventHandler(this.mnuTXSelectAll_Click);
            // 
            // mnuTXCopy
            // 
            this.mnuTXCopy.Name = "mnuTXCopy";
            this.mnuTXCopy.Size = new System.Drawing.Size(164, 22);
            this.mnuTXCopy.Text = "&Copy";
            this.mnuTXCopy.Click += new System.EventHandler(this.mnuTXCopy_Click);
            // 
            // mnuTxPaste
            // 
            this.mnuTxPaste.Name = "mnuTxPaste";
            this.mnuTxPaste.Size = new System.Drawing.Size(164, 22);
            this.mnuTxPaste.Text = "&Paste";
            this.mnuTxPaste.Click += new System.EventHandler(this.mnuTxPaste_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // mnuTxSendAllSelected
            // 
            this.mnuTxSendAllSelected.Name = "mnuTxSendAllSelected";
            this.mnuTxSendAllSelected.Size = new System.Drawing.Size(164, 22);
            this.mnuTxSendAllSelected.Text = "Send All Selected";
            this.mnuTxSendAllSelected.Click += new System.EventHandler(this.mnuTxSendAllSelected_Click);
            // 
            // richTextRxMessage
            // 
            this.richTextRxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextRxMessage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.richTextRxMessage.ForeColor = System.Drawing.SystemColors.Menu;
            this.richTextRxMessage.Location = new System.Drawing.Point(3, 0);
            this.richTextRxMessage.MinimumSize = new System.Drawing.Size(200, 200);
            this.richTextRxMessage.Name = "richTextRxMessage";
            this.richTextRxMessage.ReadOnly = true;
            this.richTextRxMessage.Size = new System.Drawing.Size(374, 411);
            this.richTextRxMessage.TabIndex = 1;
            this.richTextRxMessage.Text = "";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(248, 2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(40, 20);
            this.txtPort.TabIndex = 6;
            this.txtPort.Text = "8000";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(294, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(64, 24);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(40, 2);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(168, 20);
            this.txtIP.TabIndex = 3;
            this.txtIP.Text = "127.0.0.1";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.btnDisconnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.ForeColor = System.Drawing.Color.Black;
            this.btnDisconnect.Location = new System.Drawing.Point(442, 2);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(88, 24);
            this.btnDisconnect.TabIndex = 15;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // lblHost
            // 
            this.lblHost.Location = new System.Drawing.Point(8, 3);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(32, 16);
            this.lblHost.TabIndex = 4;
            this.lblHost.Text = "Host:";
            this.lblHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(216, 3);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(32, 16);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port:";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnBind
            // 
            this.btnBind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBind.Location = new System.Drawing.Point(364, 2);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(72, 24);
            this.btnBind.TabIndex = 16;
            this.btnBind.Text = "Bind";
            this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
            // 
            // btnRXClear
            // 
            this.btnRXClear.Location = new System.Drawing.Point(707, 2);
            this.btnRXClear.Name = "btnRXClear";
            this.btnRXClear.Size = new System.Drawing.Size(72, 22);
            this.btnRXClear.TabIndex = 18;
            this.btnRXClear.Text = "Clear";
            this.btnRXClear.Click += new System.EventHandler(this.btnRXClear_Click);
            // 
            // btnSaveToDisk
            // 
            this.btnSaveToDisk.Location = new System.Drawing.Point(621, 2);
            this.btnSaveToDisk.Name = "btnSaveToDisk";
            this.btnSaveToDisk.Size = new System.Drawing.Size(80, 22);
            this.btnSaveToDisk.TabIndex = 20;
            this.btnSaveToDisk.Text = "SaveToDisk";
            this.btnSaveToDisk.Click += new System.EventHandler(this.btnSaveToDisk_Click);
            // 
            // lstConnections
            // 
            this.lstConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConnections.FormattingEnabled = true;
            this.lstConnections.Location = new System.Drawing.Point(2, 442);
            this.lstConnections.Name = "lstConnections";
            this.lstConnections.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstConnections.Size = new System.Drawing.Size(684, 69);
            this.lstConnections.TabIndex = 21;
            // 
            // txtTXMessage
            // 
            this.txtTXMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTXMessage.ContextMenuStrip = this.mnuTX;
            this.txtTXMessage.Location = new System.Drawing.Point(3, 0);
            this.txtTXMessage.MinimumSize = new System.Drawing.Size(200, 200);
            this.txtTXMessage.Multiline = true;
            this.txtTXMessage.Name = "txtTXMessage";
            this.txtTXMessage.Size = new System.Drawing.Size(389, 411);
            this.txtTXMessage.TabIndex = 23;
            this.txtTXMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTXMessage_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtTXMessage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextRxMessage);
            this.splitContainer1.Size = new System.Drawing.Size(779, 411);
            this.splitContainer1.SplitterDistance = 395;
            this.splitContainer1.TabIndex = 24;
            // 
            // TCPUtil
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(784, 512);
            this.Controls.Add(this.lstConnections);
            this.Controls.Add(this.btnSaveToDisk);
            this.Controls.Add(this.btnRXClear);
            this.Controls.Add(this.btnBind);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "TCPUtil";
            this.Text = "TCP Util";
            this.mnuTX.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        #region GUI Methods

        /// <summary>
        ///     Send everything in the buffer out to all connected sockets.
        /// </summary>
        void SendBuffer()
        {   // METHOD : SendBuffer

            byte[] buffer = GetBytes();
            foreach ( Socket thisSocket in this.WorkerSockets )
            {   // Step through all sockets

                if ( thisSocket != null )
                {   // SocketCreated

                    if ( thisSocket.Connected )
                    {   // Socket connected

                        try
                        {   // TRY

                            thisSocket.Send( buffer, 0, buffer.Length, SocketFlags.None );

                        }   // TRY
                        catch
                        {   // CATCH

                            thisSocket.Close();
                            UpdateControls();

                        }   // CATCH

                    }   // Socket connected

                }   // Socket created

            }   // StepThrough all sockets

        }   // METHOD : SendBuffer

        /// <summary>
        ///     Get the byte[] to send down the socket.  Fixes any special chars
        /// </summary>
        /// <returns>
        ///     byte[] ready to be sent
        /// </returns>
        private byte[] FixString( string strMsg )
        {   // METHOD : FixString

                strMsg = strMsg.Replace( "\r", "" );    // Carriage Returns
                strMsg = strMsg.Replace( "\n", "" );    // Line Feeds
                                                        // We don't really care about these characters.
                                                        // if someone wanted a Carriage Returns, they 
                                                        // should put a <0D> or a <0A>

                for ( int i = 0 ; i < 256 ; i++ )
                {   // StepThrough Chars 1-256

                    string StringVal = i.ToString( "X" );
                    string BinVal = Convert.ToString( (char)i );
                    strMsg = strMsg.Replace( "<" + StringVal.ToLower() + ">", BinVal );
                    strMsg = strMsg.Replace( "<" + StringVal.ToUpper() + ">", BinVal );
                    if ( StringVal.Length == 1 )
                    {   // Single Digit Hex Value

                        // We are doing this because we want to detect both <B> and <0B> as they are both valid.
                        strMsg = strMsg.Replace( "<" + "0" + StringVal.ToLower() + ">", BinVal );

                    }   // Single Digit Hex Value

                }   // Step Through Chars 1-256

                ///This section defines some special character shortcuts, these are the ANSII names
                ///for the unprintable characters.  These are probably not right, but its what I know
                ///the chars as and is easiest for me.
                
                strMsg = strMsg.Replace( "<NUL>", "\x0" );
                strMsg = strMsg.Replace( "<NULL>", "\x0" );
                strMsg = strMsg.Replace( "<SOH>", "\x1" );
                strMsg = strMsg.Replace( "<STX>", "\x2" );
                strMsg = strMsg.Replace( "<ETX>", "\x3" );
                strMsg = strMsg.Replace( "<EOT>", "\x4" );
                strMsg = strMsg.Replace( "<ENQ>", "\x5" );
                strMsg = strMsg.Replace( "<ACK>", "\x6" );
                strMsg = strMsg.Replace( "<BEL>", "\x7");
                strMsg = strMsg.Replace( "<BS>", "\x8" );
                strMsg = strMsg.Replace( "<TAB>", "\x9" );
                strMsg = strMsg.Replace( "<LF>", "\xA" );
                strMsg = strMsg.Replace( "<VT>", "\xB" );
                strMsg = strMsg.Replace( "<FF>", "\xC" );
                strMsg = strMsg.Replace( "<CR>", "\xD" );
                strMsg = strMsg.Replace( "<SO>", "\xE" );
                strMsg = strMsg.Replace( "<SI>", "\xF" );
                strMsg = strMsg.Replace( "<DLE>", "\x10" );
                strMsg = strMsg.Replace( "<DC1>", "\x11" );
                strMsg = strMsg.Replace( "<DC2>", "\x12" );
                strMsg = strMsg.Replace( "<DC3>", "\x13" );
                strMsg = strMsg.Replace( "<DC4>", "\x14" );
                strMsg = strMsg.Replace( "<NAK>", "\x15" );
                strMsg = strMsg.Replace( "<SYN>", "\x16" );
                strMsg = strMsg.Replace( "<ETB>", "\x17" );
                strMsg = strMsg.Replace( "<CAN>", "\x18" );
                strMsg = strMsg.Replace( "<EM>", "\x19" );
                strMsg = strMsg.Replace( "<SUB>", "\x1A" );
                strMsg = strMsg.Replace( "<ESC>", "\x1B" );
                strMsg = strMsg.Replace( "<FS>", "\x1C" );
                strMsg = strMsg.Replace( "<GS>", "\x1D" );
                strMsg = strMsg.Replace( "<RS>", "\x1E" );
                strMsg = strMsg.Replace( "<US>", "\x1F" );


                ///Note, we can't use ASCII encoding, because ASCII is a 7 bit encoding and therefor only
                ///has 128 chars, the chars 128-255 are not in ASCII.  Those chars are in the ANSI character
                ///set and then into the ISO-8859-1 character set which is backward compatible with ANSI.
                System.Text.Encoding enc = System.Text.Encoding.GetEncoding( "ISO-8859-1" );
                return enc.GetBytes( strMsg );

        }   // METHOD : FixString

        /// <summary>
        ///     Blank Delegate needed to call UpdateGUI
        /// </summary>
        public delegate void GUIUpdater();

        /// <summary>
        ///     Executes on its own thread to update the GUI with the connections and button states
        /// </summary>
        private void UpdateGUI()
        {   // METHOD : UpdateGUI

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnBind.Enabled = true;
            lock ( this.WorkerSockets )
            {   // LOCK : WorkerSockets

                this.lstConnections.Items.Clear();
                bool somethingConnected = false;
                foreach ( Socket thisSocket in this.WorkerSockets )
                {   // Step Through All Sockets

                    string Descriptor = "";
                    if ( thisSocket.IsBound )
                    {   // Listening

                        Descriptor += "Local:" + thisSocket.LocalEndPoint;
                        somethingConnected = true;

                    }   // Listening
                    if ( thisSocket.Connected )
                    {   // Connected

                        Descriptor += " - Remote:" + thisSocket.RemoteEndPoint.ToString();
                        somethingConnected = true;

                    }   // Connected
                    if ( Descriptor != "" )
                    {   // Connected And/Or Bound

                        this.lstConnections.Items.Add( Descriptor );

                    }   // Connected And/Or Bound

                }   // Step Through All Sockets

                if ( somethingConnected )
                {   // At Least One Socket Was Connected

                    btnConnect.Enabled = false;
                    btnBind.Enabled = false;
                    btnDisconnect.Enabled = true;

                }   // At Least One Socket Was Connected

            }   // LOCK : WorkerSockets

        }   // METHOD : UpdateGUI

        /// <summary>
        ///     Update all the GUI Controls (spawns a new thread to do it)
        /// </summary>
        public void UpdateControls()
        {   // METHOD : UpdateControls

            try
            {   // TRY

                CleanupDeadSockets();
                this.Invoke( new GUIUpdater( UpdateGUI ) );

            }   // TRY
            catch { }

        }   // METHOD : UpdateControls

        /// <summary>
        ///     Method that actually writes data to the RX text box (executes on its own thread)
        /// </summary>
        /// <param name="Message">
        ///     Message to report was received
        /// </param>
        public void WriteToGUI( string Message )
        {   // METHOD : WriteToGUI

            this.richTextRxMessage.AppendText( Message );

        }   // METHOD : WriteToGUI

        /// <summary>
        ///     Delegate needed to link WriteRX to WriteToGUI
        /// </summary>
        /// <param name="Message">
        ///     string to pass to either WriteRX or WriteToGUI method
        /// </param>
        public delegate void StringWriter( string Message );

        /// <summary>
        ///     Writes some content into the RX textbox (spawns a new thread to do this.
        /// </summary>
        /// <param name="Message">
        ///     string to report as received on the socket
        /// </param>
        public void WriteRX( string Message )
        {   // METHOD : WriteRX

            try
            {
                this.richTextRxMessage.Invoke( new StringWriter( WriteToGUI ), new object[] { Message } );
            }
            catch { }

        }   // METHOD : WriteRX

        /// <summary>
        ///     Figure out when the User is trying to use a keyboard shortcut (select all or 
        ///     Ctrl+A in this case)
        /// </summary>
        private void txtTXMessage_KeyDown( object sender, KeyEventArgs e )
        {   // METHOD : txtTXMessage_KeyDown

            if ( e.Control )
            {   // User Pressed [Ctrl]

                if ( e.KeyCode == Keys.A )
                {   // User Pressed [Ctrl] + [A]

                    this.txtTXMessage.SelectAll();

                }   // User Pressed [Ctrl] + [A]

            }   // User Pressed [Ctrl]

        }   // METHOD : txtTXMessage_KeyDown

        #endregion
        #region Buttons
        /// <summary>
        ///     The Close Button was clicked
        /// </summary>
        private void BtnClose_Click( object sender, System.EventArgs e )
        {   // METHOD : BtnClose_Click

            ShutDown();
            Application.Exit();

        }   // METHOD : BtnClose_Click

        private void BtnConnect_Click( object sender, System.EventArgs e )
        {   // BtnConnect_Click

            // See if we have text on the IP and Port text fields
            if ( txtIP.Text == "" || txtPort.Text == "" )
            {   // Blank IP Or Port

                MessageBox.Show( "IP Address and Port Number are required to connect to the Server\n" );
                return;

            }   // Blank IP Or Port
            try
            {   // TRY

                UInt16 port;
                if ( UInt16.TryParse( txtPort.Text, out port ) )
                {   // Valid Port

                    IPAddress ip;
                    if ( IPAddress.TryParse( this.txtIP.Text, out ip ) )
                    {   // Valid IP Address

                        Socket thisSock = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
                        IPEndPoint ipEnd = new IPEndPoint( ip, port );
                        // Connect to the remote host
                        thisSock.Connect( ipEnd );
                        this.WorkerSockets.Add( thisSock );
                        if ( thisSock.Connected )
                        {   // Connect Successful

                            UpdateControls();
                            WaitForData(thisSock);

                        }   // Connect Successful

                    }   // Valid IP Address

                }   // Valid Port

            }   // TRY
            catch (SocketException se)
            {   // CATCH : Socket Exception

                MessageBox.Show( "\nConnection failed, is the server running?\n" + se.Message );
                UpdateControls();
                ShutDown();

            }   // CATCH : Socket Exception

        }   // METHOD : BtnConnect_Click

        /// <summary>
        ///     The Send Button was clicked
        /// </summary>
        private void BtnSend_Click(object sender, System.EventArgs e)
        {   // METHOD : BtnSend_Click

            try
            {   // TRY

                Send( FixString( this.txtTXMessage.Text ) );
                SendBuffer();

            }   // TRY
            catch
            {
            }
            finally
            {   // FINALLY

                UpdateControls();

            }   // FINALLY

        }   // METHOD : BtnSend_Click

        /// <summary>
        ///     The Disconnect button was clicked
        /// </summary>
        void BtnDisconnect_Click(object sender, System.EventArgs e)
        {   // METHOD : BtnDisconnect_Click

            ShutDown();

        }   // METHOD : BtnDisconnect_Click

        /// <summary>
        ///     The Bind button was clicked
        /// </summary>
        private void btnBind_Click( object sender, System.EventArgs e )
        {   // METHOD : btnBind_Click

            try
            {   // TRY

                // Check the port value
                UInt16 port;
                if ( UInt16.TryParse( txtPort.Text, out port ) )
                {   // Valid Port

                    Socket MainSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
                    IPEndPoint ipLocal = new IPEndPoint( IPAddress.Any, port );
                    
                    MainSocket.Bind( ipLocal );
                    MainSocket.Listen( 4 );
                    MainSocket.BeginAccept( new AsyncCallback( OnClientConnect ), MainSocket ); // This will go on listening until someone tries 
                                                                                                // to connect, then the OnClientConnect callback 
                                                                                                // will fire and we can deal with it there.  We 
                                                                                                // are passing in this socket object as the state 
                                                                                                // object.  This will allow us to keep track of 
                                                                                                // this particular socket so we only try to accept 
                                                                                                // connections on the one socket we are bound to.
                    this.WorkerSockets.Add( MainSocket );
                    UpdateControls();

                }   // Valid Port
                else
                {   // Invalid Port

                    MessageBox.Show( "Please enter a Port Number" );
                    return;

                }   // Invalid Port

            }   // TRY
            catch
            {   // CATCH

                UpdateControls();
                ShutDown();

            }   // CATCH

        }   // METHOD : btnBind_Click

        /// <summary>
        ///     Save button was clicked
        /// </summary>
        private void btnSaveToDisk_Click( object sender, System.EventArgs e )
        {   // METHOD : btnSaveToDisk_Click

            dlgSave = new SaveFileDialog();
            dlgSave.ShowDialog();
            dlgSave.FileOk += new System.ComponentModel.CancelEventHandler( dlgSave_FileOk );

        }   // METHOD : btnSaveToDisk_Click

        /// <summary>
        ///     The OK button was clicked on the "Save File As" dialog
        /// </summary>
        void dlgSave_FileOk( object sender, System.ComponentModel.CancelEventArgs e )
        {   // METHOD : dlgSave_FileOk

            System.IO.Stream strmOutput = dlgSave.OpenFile();
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding( "ISO-8859-1" );
            byte[] RawOutput = System.Text.Encoding.ASCII.GetBytes( this.richTextRxMessage.Text );
            strmOutput.Write( RawOutput, 0, RawOutput.Length );
            strmOutput.Close();

        }   // METHOD : dlgSave_FileOk

        /// <summary>
        ///     Clears all content from the TX Box
        /// </summary>
        private void btnTXClear_Click( object sender, System.EventArgs e )
        {   // METHOD : btnTXClear_Click

            this.txtTXMessage.Clear();

        }   // METHOD : btnTXClear_Click

        /// <summary>
        ///     Clears all content from the RX Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRXClear_Click( object sender, System.EventArgs e )
        {   // METHOD : btnRXClear_Click

            this.richTextRxMessage.Clear();

        }   // METHOD : btnRXClear_Click

        #endregion
        #region Socket Handling Methods

        /// <summary>
        ///     Close all sockets
        /// </summary>
        public void ShutDown()
        {   // METHOD :  ShutDown

            lock ( this.WorkerSockets )
            {   // LOCK : WorkerSockets

                foreach ( Socket thisSocket in this.WorkerSockets )
                {   // Step Through All Sockets

                    thisSocket.Close();

                }   // Step Through All Sockets

            }   // LOCK : WorkerSockets
            UpdateControls();

        }   // METHOD : ShutDown

        /// <summary>
        ///     Get rid of any sockets that are no longer connected
        /// </summary>
        public void CleanupDeadSockets()
        {   // METHOD : CleanupDeadSockets

            int i = 0;
            lock ( this.WorkerSockets )
            {   // LOCK : WorkerSockets

                while ( i < this.WorkerSockets.Count )
                {   // Step Through Sockets

                    if ( this.WorkerSockets[i] == null )
                    {   // Null Socket

                        this.WorkerSockets.RemoveAt( i );

                    }   // Null Socket
                    else
                    {   // Valid Socket

                        try
                        {   // TRY

                            if ( this.WorkerSockets[i].IsBound || this.WorkerSockets[i].Connected )
                            {   // Connected Socket

                                int k = this.WorkerSockets[i].Available;    // This really does nothing, but it will cause
                                                                            // an exception to get thrown if the socket is
                                                                            // not valid.  The exception is handled and the
                                                                            // socket is treated just as if the socket were
                                                                            // not connected.
                                i++;

                            }   // Connected Socket
                            else
                            {   // Disconnected Socket

                                this.WorkerSockets[i].Close();
                                this.WorkerSockets.RemoveAt( i );

                            }   // Disconnected Socket
                        }   // TRY
                        catch
                        {   // CATCH

                            this.WorkerSockets[i].Close();
                            this.WorkerSockets.RemoveAt( i );

                        }   // CATCH

                    }   // Valid Socket

                }   // Step Through Sockets

            }   // LOCK : WorkerSockets

        }   // METHOD : CleanupDeadSockets

        /// <summary>
        ///     Databuffer + socket container object.  Used as a state object for the socket
        /// </summary>
        public class SocketPacket
        {   // SocketPacket

            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[256];

        }   // SocketPacket

        #endregion
        #region SocketIO

        /// <summary>
        ///     Add One byte to the outbound buffer to be sent down the sockets
        /// </summary>
        /// <param name="Character">
        ///     Send a single Character
        /// </param>
		protected void Send( byte Character )
        {   // METHOD : Send
			
            this.OutgoingData.Enqueue( Character );

        }   // METHOD : Send

        /// <summary>
        ///     Start waiting for data from the client 
        /// </summary>
        /// <param name="thisSocket">
        ///     Socket to wait on
        /// </param>
        public void WaitForData( System.Net.Sockets.Socket thisSocket )
        {   // METHOD : WaitForData

            try
            {   // TRY

                if (WorkerCallBack == null)
                {   // Callback not created yet

                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected client
                    WorkerCallBack = new AsyncCallback(OnDataReceived);

                }   // Callback not created yet
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.thisSocket = thisSocket;
                // Start receiving any data written by the connected client
                // asynchronously
                thisSocket.BeginReceive(theSocPkt.dataBuffer, 0,
                    theSocPkt.dataBuffer.Length,
                    SocketFlags.None,
                    WorkerCallBack,
                    theSocPkt);

            }   // TRY
            catch (SocketException)
            {   // CATCH : SocketException

                ///We kill this one socket, that is probably already in the .Connected=false
                ///state, but just for fun we .Close it here.  Then we call UpdateControls
                ///which calls CleanUpDeadSockets which will get rid of this.  No error message
                ///will be displayed, but this connection will drop out of the connection list
                ///at the bottom of the window.
                thisSocket.Close();
                this.UpdateControls();

            }   // CATCH : SocketException

        }   // WaitForData

        /// <summary>
        ///     Gets a byte[] of all the data queued to be sent down the socket
        /// </summary>
        /// <returns>
        ///     byte[] containing data to be sent
        /// </returns>
        protected byte[] GetBytes()
        {   // METHOD : GetBytes

            byte[] output = new byte[this.OutgoingData.Count];
            int i = 0;
            while ( this.OutgoingData.Count > 0 )
            {   // Step Through All Bytes

                output[i] = (byte)this.OutgoingData.Dequeue();
                i++;

            }   // Step Through All Bytes
            return output;

        }   // METHOD : GetBytes

        /// <summary>
        ///     Add a byte[] to be sent down the sockets
        /// </summary>
        /// <param name="Sequence">
        ///     byte[] to send
        /// </param>
		protected void Send( byte[] Sequence )
        {   // METHOD : Send

			foreach( byte thisByte in Sequence )
			{   // Step Through All Bytes In Array

				Send( thisByte );

			}   // Step Through All Bytes In Array

        }   // METHOD : Send

        /// <summary>
        ///     Async callback that happens every time Data Is received.  In this
        ///     method someplace, the socket.EndReceive is called.  That returns
        ///     the number of bytes we got.  If this fires and that number is 0, 
        ///     then we have been disconnected
        /// </summary>
        /// <param name="asyn">
        ///     IAsyncResult
        ///     AsyncState object is a SocketPacket object that contains a buffer and the socket
        /// </param>
        public void OnDataReceived( IAsyncResult asyn )
        {   // METHOD : OnDataReceived

            try
            {   // TRY

                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
                if ( theSockId != null )
                {   // Packet Received

                    int iRx = theSockId.thisSocket.EndReceive( asyn );
                    char[] chars = new char[iRx + 1];

                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding( "ISO-8859-1" );
                    enc.GetChars( theSockId.dataBuffer, 0, iRx, chars, 0 );
                    string Endpoint = theSockId.thisSocket.RemoteEndPoint.ToString();
                    foreach ( char thisChar in chars )
                    {	// Step Through Chars

                        int charcode = (int)thisChar;
                        if ( charcode < 32 )
                        {	// Control Character

                            if ( charcode != 0 )
                            {	// Non-Null character

                                string Message = "<" + charcode.ToString("X") + ">";
                                WriteRX( Message );

                                if ( charcode == 13 )
                                {	// Carriage Return

                                    WriteRX( Environment.NewLine );

                                }	// Carriage Return

                            }	// Non-Null character

                        }	// Control Character
                        else
                        {	// Printable Character

                            WriteRX( thisChar.ToString() );

                        }	// Printable Character

                    }	// Step Through Chars
                    WaitForData( theSockId.thisSocket );

                }   // Packet Received

            }   // TRY
            catch
            {   // CATCH

                UpdateControls();

            }   // CATCH

        }   // METHOD : OnDataReceived

        /// <summary>
        ///     Async callback that gets triggered when we have successfully connecteed
        ///     out to a server
        /// </summary>
        /// <param name="asyn">
        ///     IAsyncresult object.  
        ///     AsyncState object is the socket.
        /// </param>
        public void OnClientConnect( IAsyncResult asyn )
        {   // METHOD : OnClientConnect

            Socket MainSocket = (Socket)asyn.AsyncState;
            try
            {   // TRY

                // Here we complete/end the BeginAccept() asynchronous call
                // by calling EndAccept() - which returns the reference to
                // a new Socket object
                Socket thisWorker = MainSocket.EndAccept( asyn );
                lock ( this.WorkerSockets )
                {   // LOCK : WorkerSockets

                    WorkerSockets.Add( thisWorker );

                }   // LOCK : WorkerSockets
                WaitForData( thisWorker );
                MainSocket.BeginAccept( new AsyncCallback( OnClientConnect ), MainSocket );

            }   // TRY
            catch 
            {   // CATCH

                ShutDown();

            }   // CATCH
            finally
            {   // FINALLY

                UpdateControls();

            }   // FINALLY

        }   // METHOD : OnClientConnect

        #endregion
        #region Menu

        /// <summary>
        ///     Select everything in the TX Message box
        /// </summary>
        private void mnuTXSelectAll_Click( object sender, EventArgs e )
        {   // METHOD : mnuTXSelectAll_Click

            this.txtTXMessage.SelectAll();

        }   // METHOD : mnuTXSelectAll_Click
        
        /// <summary>
        ///     Grab whatever is selected in the TX (to be sent)
        /// </summary>
        private void mnuTXCopy_Click(object sender, EventArgs e)
        {   // METHOD : mnuTXCopy_Click

            Clipboard.SetText(this.txtTXMessage.SelectedText);

        }   // METHOD : mnuTXCopy_Click

        /// <summary>
        ///     Put whatever is in the windows clipboard into the Tx Textbox
        /// </summary>
        private void mnuTxPaste_Click(object sender, EventArgs e)
        {   // METHOD : mnuTxPaste_Click

            this.txtTXMessage.Paste();

        }   // METHOD : mnuTxPaste_Click

        /// <summary>
        ///     Send only what is selected in the send box. 
        ///     If nothing is selected, send nothing.
        /// </summary>
        private void mnuTxSendAllSelected_Click(object sender, EventArgs e)
        {   // METHOD : mnuTxSendAllSelected_Click

            string Selection = this.txtTXMessage.SelectedText;
            if (!string.IsNullOrEmpty(Selection))
            {   // Something Selected

                Send(FixString(Selection));
                SendBuffer();

            }   // Something Selected

        }   // METHOD : mnuTxSendAllSelected_Click

        #endregion

    }   // CLASS : TCPUtil

}   // NameSpace : TCPUtil
