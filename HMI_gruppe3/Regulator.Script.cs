//--------------------------------------------------------------
// Press F1 to get help about using script.
// To access an object that is not located in the current class, start the call with Globals.
// When using events and timers be cautious not to generate memoryleaks,
// please see the help for more information.
//---------------------------------------------------------------

namespace Neo.ApplicationFramework.Generated
{
    using System.Windows.Forms;
    using System;
    using System.Drawing;
    using Neo.ApplicationFramework.Tools;
    using Neo.ApplicationFramework.Common.Graphics.Logic;
    using Neo.ApplicationFramework.Controls;
    using Neo.ApplicationFramework.Interfaces;
    
    
    public partial class Regulator
    {
		
		void Button_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Modus.Value = true;
			Globals.Tags.sModus.Value = "Manuell";

		}

		void Button2_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Modus.Value = false;
			Globals.Tags.sModus.Value = "Auto";
		}
		
		float Nom_Kp_P = 1.5f;
		float Nom_Kp_PI = 1.5f;
		float Nom_Ki_PI = 0.02f;
		float Nom_Kp_PD = 1.5f;
		float Nom_Kd_PD = 0.5f;
		float Nom_Kp_PID = 1.5f;
		float Nom_Ki_PID = 0.02f;
		float Nom_Kd_PID = 0.5f;
		
		
		
		void Button6_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Kp.Value = Nom_Kp_P;
			Globals.Tags.Ki.Value = 0;
			Globals.Tags.Kd.Value = 0;
			AnalogNumeric.Value = Globals.Tags.Kp.Value;
			AnalogNumeric2.Value = Globals.Tags.Ki.Value;
			AnalogNumeric3.Value = Globals.Tags.Kd.Value;
			Text2.Text = "Kp tag=" + Globals.Tags.Kp.Value.ToString();
		}
		
		void Button3_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Kp.Value = Nom_Kp_PI;
			Globals.Tags.Ki.Value = Nom_Ki_PI;
			Globals.Tags.Kd.Value = 0;
			AnalogNumeric.Value = Globals.Tags.Kp.Value;
			AnalogNumeric2.Value = Globals.Tags.Ki.Value;
			AnalogNumeric3.Value = Globals.Tags.Kd.Value;
		}
		
		void Button4_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Kp.Value = Nom_Kp_PID;
			Globals.Tags.Ki.Value = Nom_Ki_PID;
			Globals.Tags.Kd.Value = Nom_Kd_PID;
			AnalogNumeric.Value = Globals.Tags.Kp.Value;
			AnalogNumeric2.Value = Globals.Tags.Ki.Value;
			AnalogNumeric3.Value = Globals.Tags.Kd.Value;
		}
		
		void Button17_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Kp.Value = Nom_Kp_PD;
			Globals.Tags.Ki.Value = 0;
			Globals.Tags.Kd.Value = Nom_Kd_PD;
			AnalogNumeric.Value = Globals.Tags.Kp.Value;
			AnalogNumeric2.Value = Globals.Tags.Ki.Value;
			AnalogNumeric3.Value = Globals.Tags.Kd.Value;
		}
		
		void Regulator_Opened(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Kp.Value = Nom_Kp_PID;
			Globals.Tags.Ki.Value = Nom_Ki_PID;
			Globals.Tags.Kd.Value = Nom_Kd_PID;
			AnalogNumeric.Value = Globals.Tags.Kp.Value;
			AnalogNumeric2.Value = Globals.Tags.Ki.Value;
			AnalogNumeric3.Value = Globals.Tags.Kd.Value;
		}
		
		
		
    }
}
