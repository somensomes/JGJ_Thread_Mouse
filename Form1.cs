using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace JGJ_Thread_Mouse
{
    public partial class Form1 : Form
    {
        //스레드 생성 MouseThread
        Thread MouseThread; 
        public Form1()
        {

            InitializeComponent();
            //위에서 생성한 스레드에 ThreadMouse 함수 사용
            MouseThread = new Thread(ThreadMouse); 
            MouseThread.Start(); 
        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        public void ThreadMouse()
        {
            while (true)
            { 
            int x = MousePosition.X;
            int y = MousePosition.Y;

            // 생성되어진 스레드가 아니고 다른 스레드에서 호출된경우 true반환 -> 스레드생성
            if (this.InvokeRequired) 
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                        labelx.Text = x.ToString();
                        labely.Text = y.ToString();
 
                }
                ));          
            }
                // InvokeRequired false 경우 정상 작업진행
                else
                {
                    labelx.Text = x.ToString();
                    labely.Text = y.ToString();
                }
             }
        }

        // 스레드 중단 버튼 이벤트
        private void button1_Click(object sender, EventArgs e)
        {
            MouseThread.Abort();
        
        }

        // 스레드 재할당 버튼 이벤트
        private void button2_Click(object sender, EventArgs e)
        {
            // 스레드를 중단하게되면 (Abort) 스레드를 재할당해야한다. 
            MouseThread = new Thread(new ThreadStart(ThreadMouse));
            MouseThread.Start();
        }
    }
}
