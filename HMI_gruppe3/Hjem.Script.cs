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
    
    
    public partial class Hjem
    {
		
		void Hjem_Opened(System.Object sender, System.EventArgs e)
		{
			//Text3.Text = Globals.Tags.Regulatortype.Value.ToString();
			//Text7.Text = Globals.Tags.sModus.Value.ToString();
		}
		
		void Button14_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Start.Value = 1;
			Globals.Tags.Stopp.Value = 0;
			Globals.Tags.Done.Value = 0;
			Globals.Tags.Ratebegrensning.Value = 5;
			Globals.Tags.Regulator.Value = 6;
			Globals.Tags.PID_farger.Value = 3;
			Globals.Tags.Organ.Value = 0;
			Globals.Tags.Foroverkobling_farger.Value = 1;
			Globals.Tags.Auto_man_farger.Value = 2;
			Text3.Text = Globals.Tags.Regulatortype.Value.ToString();
			
			}
		
		void Button16_Click(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Stopp.Value = 1;
			Globals.Tags.Start.Value = 0;
		}
    }
}
