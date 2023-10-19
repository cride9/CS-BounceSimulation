using System.Windows.Forms;

namespace Simulation {
    public partial class Simulation : Form {
        public Simulation() {
            InitializeComponent();
        }

        static System.Windows.Forms.Timer timer = new();
        static System.Windows.Forms.Timer Mousetimer = new();

        static List<Square> squares = new List<Square>();
        static Point mousePosition = new();

        private void Initialize(object sender, EventArgs e) {

            this.FormBorderStyle = FormBorderStyle.None;

            timer.Interval = 1;
            timer.Tick += MainTimer;
            timer.Start();

            Mousetimer.Interval = 10;
            Mousetimer.Tick += MouseSpeedTicks;
            Mousetimer.Start();

            squares.Add(new(Controls, (Size.Width, Size.Height), squares.Count));
        }

        private void GenerateSquare(object sender, EventArgs e) {

            squares.Add(new(Controls, (Size.Width, Size.Height), squares.Count));
        }

        private void MainTimer(object sender, EventArgs e) {



            foreach (Square it in squares) {

                if (!it.bHolding) {
                    it.Falling();
                }
                else {
                    it.MoveToMouse(new(MousePosition.X - Location.X, MousePosition.Y - Location.Y));
                }
            }
            Refresh();
        }

        private void MouseSpeedTicks(object sender, EventArgs e) {

            Point cursorSpeed = new Point(MousePosition.X - mousePosition.X, MousePosition.Y - mousePosition.Y);
            mousePosition = new Point(MousePosition.X, MousePosition.Y);

            foreach (Square it in squares) {

                if (it.bHolding)
                    it.pStartVelocity = cursorSpeed;
            }
        }

        private void RemoveObject(object sender, EventArgs e) {

            if (squares.Count <= 0)
                return;

            Controls.Remove((squares[squares.Count - 1]).panel);
            squares.RemoveAt(squares.Count - 1);
            Refresh();
        }
    }

    class Square {

        static Random rnd = new();

        public int ID;
        public (int X, int Y) tWindowLimit = new();

        public Panel panel;

        public int iFallingVelocity;
        public int iSidewayVelocity;
        public Point pStartVelocity = new();

        public bool bFalling;
        public bool bHolding;
        public bool bBouncingY;

        public Square(Control.ControlCollection control, (int X, int Y) limit, int iD) {

            ID = iD;
            tWindowLimit = limit;

            panel = new();

            bFalling = true;
            bHolding = false;

            iFallingVelocity = 1;

            panel.Size = new(50, 50);
            panel.Location = new(rnd.Next(0, tWindowLimit.X), rnd.Next(0, tWindowLimit.Y));
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.MouseDown += HoldingSquare;
            panel.MouseUp += ReleasedSquare;

            control.Add(panel);
        }

        public void Falling() {

            if (pStartVelocity.Y != 0) {

                iFallingVelocity = pStartVelocity.Y;
                pStartVelocity.Y = 0;
            }
            if (pStartVelocity.X != 0) {

                iSidewayVelocity = pStartVelocity.X;
                pStartVelocity.X = 0;
            }

            iFallingVelocity = Math.Clamp(iFallingVelocity, -50, 50);
            iSidewayVelocity = Math.Clamp(iSidewayVelocity, -50, 50);

            if (panel.Location.Y == tWindowLimit.Y - panel.Height || panel.Location.Y == 0) {

                bBouncingY = !bBouncingY;

                iFallingVelocity += iFallingVelocity <= 0 ? iFallingVelocity == 0 ? 0 : 1 : -1;

                if (iFallingVelocity != 0)
                    panel.BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            }
            if (panel.Location.X == 0 || panel.Location.X == tWindowLimit.X - panel.Width) {

                iSidewayVelocity *= -1;
                iSidewayVelocity += iSidewayVelocity <= 0 ? iSidewayVelocity == 0 ? 0 : 2 : -2;

                if (iSidewayVelocity != 0)
                    panel.BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            }

            if (bBouncingY) {

                if (iFallingVelocity == 0) {
                    bBouncingY = false;
                }

                iFallingVelocity -= 1;
                panel.Location = Clamp(new(panel.Location.X + iSidewayVelocity, panel.Location.Y - iFallingVelocity));
            }
            else {

                iFallingVelocity += 1;
                panel.Location = Clamp(new(panel.Location.X + iSidewayVelocity, panel.Location.Y + iFallingVelocity));
            }
        }

        public void MoveToMouse(Point mousePosition) {

            iFallingVelocity = 0;
            panel.Location = Clamp(new(mousePosition.X - (panel.Width / 2), mousePosition.Y - (panel.Height / 2)));
        }

        public Point Clamp(Point point) {

            Point output = new();

            output.X = Math.Clamp(point.X, 0, tWindowLimit.X - panel.Width);
            output.Y = Math.Clamp(point.Y, 0, tWindowLimit.Y - panel.Height);

            return output;
        }

        private void HoldingSquare(object sender, EventArgs e) =>
            bHolding = true;

        private void ReleasedSquare(object sender, EventArgs e) =>
            bHolding = false;

    }
}