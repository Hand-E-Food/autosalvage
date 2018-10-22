using AutoSalvage.Generator;
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
            var uidGenerator = new Hex4UidGenerator(random);
            var generator = new GridFloorPlanGenerator(random, uidGenerator);
            var floorPlan = generator.CreateFloorPlan();
            floorPlanView.FloorPlan = floorPlan;
        }
    }
}
