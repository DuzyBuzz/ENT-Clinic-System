using ENT_Clinic_System.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.UI
{
    public partial class WelcomeForm : Form
    {
        private Timer animationTimer;
        private int animationStep = 0;
        private const int totalSteps = 30; // total animation frames
        private const int displayDuration = 0; // 1.5 seconds display

        public WelcomeForm(string role, string fullName)
        {
            InitializeComponent();
            SetupUI(role, fullName);
            StartAnimation();
        }

        private void SetupUI(string role, string fullName)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Width = 400;
            this.Height = 300;

            Label welcomeLabel = new Label();
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.AutoSize = false;
            welcomeLabel.Dock = DockStyle.Fill;
            welcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            welcomeLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            welcomeLabel.ForeColor = Color.Black;

            string greeting;
            if (role == "Doctor")
                greeting = $"Welcome \n Dr. {UserCredentials.Fullname}";
            else if (role == "Receptionist")
                greeting = $"Welcome {fullName}";
            else if (role == "Admin")
                greeting = $"Welcome Admin {fullName}";
            else
                greeting = $"Welcome {fullName}";

            welcomeLabel.Text = greeting;

            this.Controls.Add(welcomeLabel);
            this.Opacity = 0; // start transparent
        }

        private void StartAnimation()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 1; // ~15ms per step (~0.45s fade-in)
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private async void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animationStep < totalSteps)
            {
                // Fade in
                this.Opacity = animationStep / (double)totalSteps;
                animationStep++;
            }
            else
            {
                animationTimer.Stop();
                // Hold for displayDuration
                await System.Threading.Tasks.Task.Delay(displayDuration);

                // Fade out
                for (int i = totalSteps; i >= 0; i--)
                {
                    this.Opacity = i / (double)totalSteps;
                    await System.Threading.Tasks.Task.Delay(15);
                }

                this.Close();
            }
        }
    }
}
