using AutoSalvage.Engine;
using System;
using System.Windows.Forms;

namespace AutoSalvage.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var random = new Random();
            var engine = new GameEngine(random);
            var operatorInterface = engine.CreateNextFloorPlan();
            var floorPlan = operatorInterface.FloorPlan;
            floorPlanView.FloorPlan = floorPlan;
        }
    }
}
