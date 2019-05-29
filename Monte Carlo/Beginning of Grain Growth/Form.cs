﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Beginning_of_Grain_Growth.src;

namespace Beginning_of_Grain_Growth
{
    public partial class Form : System.Windows.Forms.Form
    {


        private Display display;
        private Solver solver;
        private Data data;
        private ProximityAction proximity;
        private MC mc;

        public Form()
        {
            InitializeComponent();

            data = new Data();
            display = new Display(pictureBox, data);
            solver = new Solver(display, data);
            proximity = new ProximityAction(data, solver, display);
            mc = new MC(display, data);

            Set_Limits();
        }

        private void Set_Limits()
        {
            cell_size_box.Minimum = 1;
            cell_size_box.Maximum = 30;
            cell_size_box.Value = 10;

            horizontal_cells_box.Minimum = 1;
            horizontal_cells_box.Value = pictureBox.Height / data.Cell_size;
            horizontal_cells_box.Maximum = 10000;

            vertical_cells_box.Minimum = 1;
            vertical_cells_box.Value = pictureBox.Width / data.Cell_size;
            vertical_cells_box.Maximum = 10000;

            rnd_cells_num_box.Minimum = 1;
            rnd_cells_num_box.Maximum = 100;
            rnd_cells_num_box.Value = 10;

            horizontal_homogenous_cells_box.Minimum = 1;
            horizontal_homogenous_cells_box.Value = 10;

            vertical_homogenous_cells_box.Minimum = 1;
            vertical_homogenous_cells_box.Value = 10;

            radius_box.Minimum = 1;
            radius_box.Value = 5;

            radius_cells_num_box.Minimum = 1;
            radius_cells_num_box.Value = 5;

            speed_box.Minimum = 1;
            speed_box.Maximum = 100;

            neighborhood_radius_box.Minimum = 1;

            kt_box.DecimalPlaces = 1;
            kt_box.Minimum = -100;

            neighborhood_box.Items.Add("Von Neumann");
            neighborhood_box.Items.Add("Moore");
            neighborhood_box.Items.Add("Random Pentagonal");
            neighborhood_box.Items.Add("Left Hexagonal");
            neighborhood_box.Items.Add("Right Hexagonal");
            neighborhood_box.Items.Add("Random Hexagonal");
            neighborhood_box.Items.Add("Radius");
        }


        private void horizontal_cells_box_ValueChanged(object sender, EventArgs e)
        {
            data.Horizontal_cells_num = decimal.ToInt32(horizontal_cells_box.Value);
        }

        private void vertical_cells_box_ValueChanged(object sender, EventArgs e)
        {
            data.Vertical_cells_num = decimal.ToInt32(vertical_cells_box.Value);
        }

        private void Cell_size_box_ValueChanged(object sender, EventArgs e)
        {
            data.Cell_size = decimal.ToInt32(cell_size_box.Value);
        }

        private void periodic_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (periodic_checkbox.Checked)
            {
                data.Is_periodic = true;
            }
            else
            {
                data.Is_periodic = false;
            }
        }

        private void Rnd_cells_num_box_ValueChanged(object sender, EventArgs e)
        {
            data.Rnd_cells_num = decimal.ToInt32(rnd_cells_num_box.Value);
        }

        private void vertical_homogenous_cells_ValueChanged(object sender, EventArgs e)
        {
            data.Vertical_homogenous_cells_num = decimal.ToInt32(vertical_homogenous_cells_box.Value);
        }

        private void horizontal_homogenous_cells_ValueChanged(object sender, EventArgs e)
        {
            data.Horiziontal_homogenous_cells_num = decimal.ToInt32(horizontal_homogenous_cells_box.Value);
        }

        private void radius_cells_num_box_ValueChanged(object sender, EventArgs e)
        {
            data.Radius_cells_num = decimal.ToInt32(radius_cells_num_box.Value);
        }

        private void radius_box_ValueChanged(object sender, EventArgs e)
        {
            data.Radius = decimal.ToInt32(radius_box.Value);
        }

        private void speed_box_ValueChanged(object sender, EventArgs e)
        {
            data.Speed = decimal.ToInt32(speed_box.Value);
        }


        private void custom_button_Click(object sender, EventArgs e)
        {
            data.Kt = decimal.ToInt32(kt_box.Value);
            proximity.NextStep();
        }

        private void random_button_Click(object sender, EventArgs e)
        {
            data.Kt = decimal.ToInt32(kt_box.Value);
            proximity.RandomProximity();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            data.Initialize();
            display.Clear();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            proximity.CustomProximityClick(coordinates);
        }

        private void homogeneous_button_Click(object sender, EventArgs e)
        {
            data.Kt = decimal.ToInt32(kt_box.Value);
            proximity.HomogenousProximity();
        }

        private void radius_button_Click(object sender, EventArgs e)
        {
            data.Kt = decimal.ToInt32(kt_box.Value);
            proximity.RadiusProximity();
        }

        private void neighborhood_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.Neighborhood = neighborhood_box.SelectedItem.ToString();
        }

        private void neighborhood_radius_box_ValueChanged(object sender, EventArgs e)
        {
            data.Neighborhood_radius = decimal.ToInt32(neighborhood_radius_box.Value);
        }

        private void generate_button_Click(object sender, EventArgs e)
        {
            data.Kt = decimal.ToInt32(kt_box.Value);
            proximity.GenerateProximity();
        }

        private void energy_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (energy_checkBox.Checked)
            {
                data.Show_energy = true;

                if (data.Grid_points != null)
                    display.Print_Energry();
            }
            else
            {
                data.Show_energy = false;

                if (data.Grid_energy != null)
                    display.Print_Grid();
            }
        }

        private void MC_next_step_button_click(object sender, EventArgs e)
        {
            mc.Next_step();
        }

        private void kt_box_ValueChanged(object sender, EventArgs e)
        {
            data.Kt = decimal.ToDouble(kt_box.Value);
            if (data.Kt == 0)
                data.Kt = -6;
        }
    }
}
