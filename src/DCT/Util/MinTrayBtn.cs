/* MinTrayBtn Class
 * Adds a 'Minimize to tray' Button to the Caption bar of the Form
 *
 * Created: 15.04.2005
 * Updated: 11.07.2005
 * Version: 0.8.5
 *
 * Changes: 11.07.2005 - Variable Caption Button size (adjusts itself to the system metrics)
 *                     - Fixed drawing issues on changes of the window width 
 * 
 *          20.05.2005 - added WM_GETTEXT for Drawing Button	(Draws the Button after the Text in the Title bar has changed)
 * 
 * Copyright (C) 2005, Tyron Madlener <mytodo@v-cm.net>
 */


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Resources;
using System.Runtime.InteropServices;

namespace TyronM {
	public delegate void MinTrayBtnClickedEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Summary description for Class.
	/// </summary>
	public class MinTrayBtn : NativeWindow {
		bool pressed = false;
		Size wnd_size = new Size();
		public bool captured;
		Form parent;
		public event MinTrayBtnClickedEventHandler MinTrayBtnClicked;

		#region Constants
		const int WM_SIZE = 5;
		const int WM_SYNCPAINT = 136;
		const int WM_MOVE = 3;
		const int WM_ACTIVATE = 6;
		const int WM_LBUTTONDOWN =513;
		const int WM_LBUTTONUP =514;
		const int WM_LBUTTONDBLCLK =515;
		const int WM_MOUSEMOVE = 512;

		const int WM_PAINT = 15;

		const int WM_GETTEXT = 13;

		const int WM_NCCREATE =129;
		const int WM_NCLBUTTONDOWN = 161;
		const int WM_NCLBUTTONUP = 162;
		const int WM_NCMOUSEMOVE = 160;
		const int WM_NCACTIVATE =134;
		const int WM_NCPAINT = 133;
		const int WM_NCHITTEST = 132;
		const int WM_NCLBUTTONDBLCLK = 163;

		const int VK_LBUTTON = 1;
		
		const int SM_CXSIZE = 30;
		const int SM_CYSIZE = 31;				
		#endregion
		
		#region WinAPI Imports
		[DllImport("user32")]
		public static extern int GetWindowDC(int hwnd);

		[DllImport("user32")]
		public static extern short GetAsyncKeyState(int vKey);

		[DllImport("user32")]
		public static extern int SetCapture(int hwnd);

		[DllImport("user32")]
		public static extern bool ReleaseCapture();

		[DllImport("user32")]
		public static extern int GetSysColor(int nIndex);
		
		[DllImport("user32")]
		public static extern int GetSystemMetrics(int nIndex);
		#endregion

		#region Constructor and Handle-Handler ^^
		public MinTrayBtn(Form parent) {
			parent.HandleCreated += new EventHandler(this.OnHandleCreated);
			parent.HandleDestroyed+= new EventHandler(this.OnHandleDestroyed);
			parent.TextChanged+= new EventHandler(this.OnTextChanged);
			
			this.parent = parent;
		}

		// Listen for the control's window creation and then hook into it.
		internal void OnHandleCreated(object sender, EventArgs e){
			// Window is now created, assign handle to NativeWindow.
			AssignHandle(((Form)sender).Handle);
		}
		internal void OnHandleDestroyed(object sender, EventArgs e) {
			// Window was destroyed, release hook.
			ReleaseHandle();
		}
		
		// Changing the Text invalidates the Window, so we got to Draw the Button again
		private void OnTextChanged(object sender, EventArgs e) {
			DrawButton();
		}
		#endregion

		#region WndProc
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		protected override void WndProc(ref Message m){
			//label3.Text = "Button pressed: " + pressed;
			//label4.Text = "Mouse captured: " + captured;

			// Change the Pressed-State of the Button when the User pressed the
			// left mouse button and moves the cursor over the button
			if(m.Msg==WM_MOUSEMOVE) {
				Point pnt2 = new Point((int)m.LParam);
				Size rel_pos2 = new Size(parent.PointToClient(new Point(parent.Location.X, parent.Location.Y)));
				// Not needed because SetCapture seems to convert the cordinates anyway
				//pnt2 = PointToClient(pnt2);
				pnt2-=rel_pos2;
				//label2.Text = "Cursor @"+pnt2.X+"/"+pnt2.Y;

				if(pressed) {
					Point pnt = new Point((int)m.LParam);
					Size rel_pos = new Size(parent.PointToClient(new Point(parent.Location.X, parent.Location.Y)));
					//pnt = PointToClient(pnt);
					pnt-=rel_pos;

					if(!MouseinBtn(pnt)) {
						pressed = false;
						DrawButton();
					}

				} else {
					if((GetAsyncKeyState(VK_LBUTTON)&(-32768))!=0) {
						Point pnt = new Point((int)m.LParam);
						Size rel_pos = new Size(parent.PointToClient(new Point(parent.Location.X, parent.Location.Y)));
						//pnt = PointToClient(pnt);
						pnt-=rel_pos;

						if(MouseinBtn(pnt)) {
							pressed = true;
							DrawButton();
						}
					}
				}  
			}

			// Ignore Double-Clicks on the Traybutton
			if(m.Msg==WM_NCLBUTTONDBLCLK) {
					Point pnt = new Point((int)m.LParam);
					Size rel_pos = new Size(parent.PointToClient(new Point(parent.Location.X, parent.Location.Y)));
					pnt = parent.PointToClient(pnt);
					pnt-=rel_pos;
					if(MouseinBtn(pnt)) {
						return;
					}
			}

			// Button released and eventually clicked
			if(m.Msg==WM_LBUTTONUP) {
				ReleaseCapture();
				captured = false;

				if(pressed) {
					pressed = false;
					DrawButton();

					Point pnt = new Point((int)m.LParam);
					Size rel_pos = new Size(parent.PointToClient(new Point(parent.Location.X, parent.Location.Y)));
					pnt-=rel_pos;
					if(MouseinBtn(pnt)) {
						//TrayButton_clicked();
						EventArgs e = new EventArgs();
						if (MinTrayBtnClicked != null)
							MinTrayBtnClicked(this, e);
						return;
					}
				}
			}

			// Clicking the Button - Capture the Mouse and await until the Uses relases the Button again
			if(m.Msg==WM_NCLBUTTONDOWN) {
				Point pnt = new Point((int)m.LParam);
				Size rel_pos = new Size(parent.PointToClient(new Point(parent.Location.X, parent.Location.Y)));
				pnt = parent.PointToClient(pnt);
				pnt-=rel_pos;

				if(MouseinBtn(pnt)) {
					pressed = true;
					DrawButton();
					SetCapture((int)parent.Handle);
					captured = true;
					return;
				}
			}

			// Drawing the Button and getting the Real Size of the Window
			if(m.Msg == WM_ACTIVATE || m.Msg==WM_SIZE || m.Msg==WM_SYNCPAINT || m.Msg==WM_NCACTIVATE || m.Msg==WM_NCCREATE || m.Msg==WM_NCPAINT || m.Msg==WM_NCACTIVATE || m.Msg==WM_NCHITTEST || m.Msg==WM_PAINT) {
				if(m.Msg==WM_SIZE) wnd_size = new Size(new Point((int)m.LParam));
				DrawButton();
			}
			
			base.WndProc(ref m);
		}
		#endregion

		#region Button-Specific Functions
		public bool MouseinBtn(Point click) {
			int btn_width = GetSystemMetrics(SM_CXSIZE);
			int btn_height = GetSystemMetrics(SM_CYSIZE);
			Size btn_size = new Size(btn_width, btn_height);
			
			Point pos = new Point(wnd_size.Width-3*btn_width-12-(btn_width-18),6);

			
			return click.X>=pos.X && click.X<=pos.X+btn_size.Width &&
				   click.Y>=pos.Y && click.Y<=pos.Y+btn_size.Height;
		}

		public void DrawButton() {
			Graphics g = Graphics.FromHdc((IntPtr)GetWindowDC((int)parent.Handle)); //m.HWnd));
			DrawButton(g, pressed);
		}

		public void DrawButton(Graphics g, bool pressed) {
			int btn_width = GetSystemMetrics(SM_CXSIZE);
			int btn_height = GetSystemMetrics(SM_CYSIZE);
			
			Point pos = new Point(wnd_size.Width-3*btn_width-12-(btn_width-18),6);
			
			// real button size
			btn_width-=2;
			btn_height-=4;
				
			Color light = SystemColors.ControlLightLight;
			Color icon = SystemColors.ControlText;
			Color background = SystemColors.Control;
			Color shadow1 = SystemColors.ControlDark;
			Color shadow2 = SystemColors.ControlDarkDark;

			Color tmp1, tmp2;

			if(pressed) {
				tmp1 = shadow2;
				tmp2 = light;
			} else {
				tmp1 = light;
				tmp2 = shadow2;
			}

			g.DrawLine(new Pen(tmp1),pos, new Point(pos.X+btn_width-1,pos.Y));
			g.DrawLine(new Pen(tmp1),pos, new Point(pos.X,pos.Y+btn_height-1));

			if(pressed) {
				g.DrawLine(new Pen(shadow1),pos.X+1, pos.Y+1, pos.X+btn_width-2, pos.Y+1);
				g.DrawLine(new Pen(shadow1),pos.X+1, pos.Y+1, pos.X+1, pos.Y+btn_height-2);
			} else {
				g.DrawLine(new Pen(shadow1),pos.X+btn_width-2, pos.Y+1, pos.X+btn_width-2, pos.Y+btn_height-2);
				g.DrawLine(new Pen(shadow1),pos.X+1, pos.Y+btn_height-2, pos.X+btn_width-2, pos.Y+btn_height-2);
			}

			g.DrawLine(new Pen(tmp2),pos.X+btn_width-1, pos.Y+0, pos.X+btn_width-1, pos.Y+btn_height-1);
			g.DrawLine(new Pen(tmp2),pos.X+0, pos.Y+btn_height-1, pos.X+btn_width-1, pos.Y+btn_height-1);
			
			g.FillRectangle(new SolidBrush(background),pos.X+1+Convert.ToInt32(pressed), pos.Y+1+Convert.ToInt32(pressed), btn_width-3,btn_height-3);

			g.FillRectangle(new SolidBrush(icon),pos.X+(float)0.5625*btn_width+Convert.ToInt32(pressed),pos.Y+(float)0.6428*btn_height+Convert.ToInt32(pressed),btn_width*(float)0.1875,btn_height*(float)0.143);
		}
		#endregion

	}
}
